using UnityEngine;

public class ModelPC : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        print("Interact");
        DialogManager.Instance.ShowModels();
    }
}
