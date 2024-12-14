using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ApiService : MonoBehaviour
{
    [System.Serializable]
    public class EmotionRequest
    {
        public string texto;
        public string emocion;

        public EmotionRequest(string texto, string emocion)
        {
            this.texto = texto;
            this.emocion = emocion;
        }
    }

    [System.Serializable]
    public class ApiResponse
    {
        public string original_message;
        public string emotion;
        public string message;
    }
    public IEnumerator FetchMessage(ApiConfig apiConfig, System.Action<string> onSuccess, System.Action<string> onError)
    {
        
        if (apiConfig == null || string.IsNullOrEmpty(apiConfig.apiURL))
        {
            onError?.Invoke("Configuración de API inválida.");
            yield break;
        }

        using (UnityWebRequest webRequest = UnityWebRequest.Get(apiConfig.apiURL))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = webRequest.downloadHandler.text;
                onSuccess?.Invoke(jsonResponse);
            }
            else
            {
                onError?.Invoke("Error conectando al servidor");
            }
        }
    }
    
    
    
    
    public IEnumerator FetchMessageOpenAiModel(ApiConfig apiConfig, System.Action<string> onSuccess, System.Action<string> onError)
    {
        
        if (apiConfig == null || string.IsNullOrEmpty(apiConfig.apiURL1))
        {
            onError?.Invoke("Configuración de API inválida.");
            yield break;
        }
        EmotionRequest emotionRequest = new EmotionRequest("", DialogManager.Instance.emotion);
        string jsonRequest = JsonUtility.ToJson(emotionRequest);
        using (UnityWebRequest webRequest = new UnityWebRequest(apiConfig.apiURL1, UnityWebRequest.kHttpVerbPOST))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonRequest);
            webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = webRequest.downloadHandler.text;
                onSuccess?.Invoke(jsonResponse);
            }
            else
            {
                onError?.Invoke("Error conectando al servidor");
            }
        }
    }
}
