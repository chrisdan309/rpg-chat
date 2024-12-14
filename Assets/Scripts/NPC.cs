using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    public ApiConfig apiConfig;
    private ApiService apiService;

    private void Start()
    {
        apiService = FindFirstObjectByType<ApiService>();
        if (apiService == null)
        {
            Debug.LogError("ApiService no encontrado en la escena.");
        }

        if (apiConfig == null)
        {
            Debug.LogError("ApiConfig no asignado en el NPC.");
        }
    }

    public void Interact()
    {
        if (apiService != null && apiConfig != null)
        {
            DialogManager.Instance.ShowDialogue("Cargando mensaje...");
            switch (DialogManager.Instance.ApiType)
            {
                case 0: StartCoroutine(apiService.FetchMessage(apiConfig, OnFetchSuccess, OnFetchError));
                    break;
                case 1: StartCoroutine(apiService.FetchMessageOpenAiModel(apiConfig, OnFetchSuccess, OnFetchError));
                    break;
                case 2: StartCoroutine(apiService.FetchMessage(apiConfig, OnFetchSuccess, OnFetchError));
                    break;
            }
            
            StartCoroutine(apiService.FetchMessage(apiConfig, OnFetchSuccess, OnFetchError));
        }
        else
        {
            DialogManager.Instance.ShowDialogue("Error de configuración o servicio");
        }
    }

    private void OnFetchSuccess(string jsonResponse)
    {
        try
        {
            ApiResponse response = JsonUtility.FromJson<ApiResponse>(jsonResponse);
            if (response != null && !string.IsNullOrEmpty(response.message))
            {
                DialogManager.Instance.ShowDialogue(response.message);
            }
            else
            {
                DialogManager.Instance.ShowDialogue("Mensaje no disponible");
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error al interpretar la respuesta del servidor: {ex.Message}");
            DialogManager.Instance.ShowDialogue("Error al interpretar la respuesta del servidor");
        }
    }

    private void OnFetchError(string errorMessage)
    {
        Debug.LogError(errorMessage);
        DialogManager.Instance.ShowDialogue(errorMessage);
    }

    [System.Serializable]
    public class ApiResponse
    {
        public string message;
    }
}

