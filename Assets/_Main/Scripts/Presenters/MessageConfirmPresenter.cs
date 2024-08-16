using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageConfirmPresenter : MonoBehaviour
{
    [SerializeField] private GameObject messageBoxConfirmGameObject;

    [SerializeField] private MessagePresenter messagePresenter;
    [SerializeField] private AuthMenuPresenter authMenuPresenter;

    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private TMP_InputField passwordInputField;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button backButton;

    private void Start()
    {
        confirmButton.onClick.AddListener(() =>
        {
            Login();
        });
        backButton.onClick.AddListener(() =>
        {
            Hide();
        });
    }

    private async void Login()
    {
        Authenticator.UserResult reLoginUser = await Authenticator.ReLoginUser(Authenticator.User.Email, passwordInputField.text);
        if (reLoginUser.Result == null)
        {
            if (string.IsNullOrEmpty(reLoginUser.ErrorMessage))
            {
                Debug.LogError($"Error: Text of messageBox passed is null or empty!");
                return;
            }

            messagePresenter.Show($"{reLoginUser.ErrorMessage}", Color.red);
            return;
        }

        string errorMessage = await Authenticator.DeleteUser();
        if (!string.IsNullOrEmpty(errorMessage))
        {
            messagePresenter.Show($"{errorMessage}", Color.red);
            return;
        }

        Authenticator.LogoutUser();

        authMenuPresenter.Show();
        Hide();

        messagePresenter.Show($"User deleted succesfully", Color.green);
    }

    public void Show(string message)
    {
        messageBoxConfirmGameObject.SetActive(true);

        messageText.text = message;
    }

    private void Hide()
    {
        messageText.text = string.Empty;
        messageText.color = Color.white;

        messageBoxConfirmGameObject.SetActive(false);
    }
}
