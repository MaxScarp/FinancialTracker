using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageYesNoPresenter : MonoBehaviour
{
    public event EventHandler OnYesButtonSelected;

    [SerializeField] private GameObject messageBoxYesNoGameObject;

    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;

    private void Start()
    {
        yesButton.onClick.AddListener(() =>
        {
            Hide();

            OnYesButtonSelected?.Invoke(this, EventArgs.Empty);
        });
        noButton.onClick.AddListener(() =>
        {
            Hide();
        });
    }

    public void Show(string message)
    {
        messageBoxYesNoGameObject.SetActive(true);

        messageText.text = message;
    }

    private void Hide()
    {
        messageText.text = string.Empty;
        messageText.color = Color.white;

        messageBoxYesNoGameObject.SetActive(false);
    }
}
