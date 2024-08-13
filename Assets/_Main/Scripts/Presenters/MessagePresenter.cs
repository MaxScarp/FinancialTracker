using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessagePresenter : MonoBehaviour
{
    [SerializeField] private GameObject messageBoxGameObject;

    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Button okButton;

    private void Start()
    {
        okButton.onClick.AddListener(() =>
        {
            Hide();
        });
    }

    public void Show(string message, Color messageColor)
    {
        messageBoxGameObject.SetActive(true);

        messageText.text = message;
        messageText.color = messageColor;
    }

    private void Hide()
    {
        messageText.text = string.Empty;
        messageText.color = Color.white;

        messageBoxGameObject.SetActive(false);
    }
}
