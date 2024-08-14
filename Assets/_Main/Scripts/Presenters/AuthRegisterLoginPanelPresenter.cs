using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AuthRegisterLoginPanelPresenter : MonoBehaviour
{
    private const string REGISTER_TEXT = "Register";
    private const string REGISTRATION_TEXT = "Registration";
    private const string LOGIN_TEXT = "Login";
    private const string LOG_TEXT = "Log";

    public event EventHandler OnRegisterLoginButtonPressed;
    public event EventHandler OnBackButtonPressed;

    [SerializeField] private GameObject authRegisterLoginPanelGameOBject;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TMP_InputField emailInputField;
    [SerializeField] private TMP_InputField passwordInputField;
    [SerializeField] private Button registerLoginButton;
    [SerializeField] private Button backButton;

    private void Start()
    {
        backButton.onClick.AddListener(() =>
        {
            OnBackButtonPressed?.Invoke(this, EventArgs.Empty);
        });
    }

    private void Show()
    {
        authRegisterLoginPanelGameOBject.SetActive(true);
    }

    public void ShowRegister()
    {
        Show();

        registerLoginButton.GetComponentInChildren<TextMeshProUGUI>().text = REGISTER_TEXT;
        title.text = REGISTRATION_TEXT;
    }

    public void ShowLogin()
    {
        Show();

        registerLoginButton.GetComponentInChildren<TextMeshProUGUI>().text = LOGIN_TEXT;
        title.text = LOG_TEXT;
    }

    public void Hide()
    {
        registerLoginButton.onClick.RemoveAllListeners();

        authRegisterLoginPanelGameOBject.SetActive(false);
    }
}
