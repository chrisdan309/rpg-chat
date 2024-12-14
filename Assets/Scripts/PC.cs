using UnityEngine;

public class PC : MonoBehaviour, IInteractable
{ 
    public void Interact()
    {
        print("Interact");
        DialogManager.Instance.ShowOptions();
    }
}
