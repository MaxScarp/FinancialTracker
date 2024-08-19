using System.Threading.Tasks;
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
            LoginAndDelete();
        });
        backButton.onClick.AddListener(() =>
        {
            Hide();
        });
    }

    private async Task<bool> DeleteUser()
    {
        string errorMessage = await Authenticator.DeleteUser();
        if (!string.IsNullOrEmpty(errorMessage))
        {
            messagePresenter.Show($"{errorMessage}", Color.red);
            return false;
        }

        authMenuPresenter.Show();
        Hide();

        messagePresenter.Show($"User deleted succesfully", Color.green);

        return true;
    }

    private async void LoginAndDelete()
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

        if (await DeleteUser())
        {
            Authenticator.LogoutUser();
        }
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

        passwordInputField.text = string.Empty;

        messageBoxConfirmGameObject.SetActive(false);
    }
}
