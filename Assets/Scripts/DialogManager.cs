using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance;

    public GameObject dialogueCanvas;
    public GameObject optionCanvas;
    public GameObject modelCanvas;
    public TextMeshProUGUI dialogueText;
    public float typingSpeed = 0.075f;
    private Coroutine typingCoroutine;
    public int ApiType;
    private bool isDialogueActive;
    private bool isOptionActive;
    private bool isModelActive;
    public string emotion = "joy";
    public bool IsDialogueActive => isDialogueActive;
    public bool IsOptionActive => isOptionActive;
    public bool IsModelActive => isModelActive;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ApiType = 0;
        dialogueCanvas.SetActive(false);
        optionCanvas.SetActive(false);
        modelCanvas.SetActive(false);
        isDialogueActive = false;
        isOptionActive = false;
        isModelActive = false;
    }
    
    public void ShowOptions()
    {
        isOptionActive = true;
        optionCanvas.SetActive(true);
    } 
    
    public void HideOptions()
    {
        isOptionActive = false; 
        optionCanvas.SetActive(false);
    }
    
    public void ShowModels()
    {
        isModelActive = true;
        modelCanvas.SetActive(true);
    } 
    
    public void HideModels()
    {
        isModelActive = false; 
        modelCanvas.SetActive(false);
    }
    
    public void ShowDialogue(string dialogue)
    {
        isDialogueActive = true; // Indica que el diálogo está activo
        dialogueCanvas.SetActive(true);

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(TypeText(dialogue));
    }

    public void HideDialogue()
    {
        isDialogueActive = false; // Indica que el diálogo ya no está activo
        dialogueCanvas.SetActive(false);

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }
    }

    private IEnumerator TypeText(string message)
    {
        dialogueText.text = "";

        foreach (char letter in message.ToArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        typingCoroutine = null;
    }
}