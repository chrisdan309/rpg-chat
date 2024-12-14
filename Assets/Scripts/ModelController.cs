using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ModelController : MonoBehaviour
{
    public TextMeshProUGUI[] options;
    public Color defaultColor = Color.black;
    public Color selectedColor = Color.red;

    private int currentIndex = 0;

    void Start()
    {
        UpdateMenu();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            currentIndex = (currentIndex > 0) ? currentIndex - 1 : options.Length - 1;
            UpdateMenu();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            currentIndex = (currentIndex < options.Length - 1) ? currentIndex + 1 : 0;
            UpdateMenu();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            SelectOption();
        }
    }

    void UpdateMenu()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].color = (i == currentIndex) ? selectedColor : defaultColor;
        }
    }

    void SelectOption()
    {
        Debug.Log($"Selected Option: {options[currentIndex].text}");
        DialogManager.Instance.ApiType = currentIndex;
    }
}
