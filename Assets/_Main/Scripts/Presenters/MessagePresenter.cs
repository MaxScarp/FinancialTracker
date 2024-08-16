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

    public void Show(string message, Color messageColor, bool isOkButtonDisabled = false)
    {
        messageBoxGameObject.SetActive(true);

        messageText.text = message;
        messageText.color = messageColor;

        okButton.enabled = !isOkButtonDisabled;
    }

    public void Hide()
    {
        messageText.text = string.Empty;
        messageText.color = Color.white;

        messageBoxGameObject.SetActive(false);
    }
}
