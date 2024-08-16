using System;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AuthRegisterLoginPanelPresenter : MonoBehaviour
{
    private const string REGISTER_TEXT = "Register";
    private const string REGISTRATION_TEXT = "Registration";
    private const string LOGIN_TEXT = "Login";
    private const string LOG_TEXT = "Log";

    private const string EMAIL_PATTERN = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
    private const string PASSWORD_PATTERN = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\W]).{8,}$";

    public event EventHandler<UserCredentialsEventArgs> OnRegister;
    public event EventHandler<UserCredentialsEventArgs> OnLogin;
    public event EventHandler OnBackButtonPressed;

    public class UserCredentialsEventArgs : EventArgs
    {
        public string Email;
        public string Password;
    }

    [SerializeField] private GameObject authRegisterLoginPanelGameOBject;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TMP_InputField emailInputField;
    [SerializeField] private TMP_InputField passwordInputField;
    [SerializeField] private TMP_InputField reEnterpasswordInputField;
    [SerializeField] private Button registerLoginButton;
    [SerializeField] private Button backButton;

    [SerializeField] private MessagePresenter messagePresenter;

    private Regex emailRegex;
    private Regex passwordRegex;

    private void Awake()
    {
        emailRegex = new Regex(EMAIL_PATTERN);
        passwordRegex = new Regex(PASSWORD_PATTERN);
    }

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

        registerLoginButton.onClick.AddListener(() =>
        {
            if (!IsValidInput())
            {
                return;
            }

            OnRegister?.Invoke(this, new UserCredentialsEventArgs { Email = emailInputField.text, Password = passwordInputField.text });
        });

        reEnterpasswordInputField.gameObject.SetActive(true);
    }

    public void ShowLogin()
    {
        Show();

        registerLoginButton.GetComponentInChildren<TextMeshProUGUI>().text = LOGIN_TEXT;
        title.text = LOG_TEXT;

        registerLoginButton.onClick.AddListener(() =>
        {
            if (!IsValidInput())
            {
                return;
            }

            OnLogin?.Invoke(this, new UserCredentialsEventArgs { Email = emailInputField.text, Password = passwordInputField.text });
        });

        reEnterpasswordInputField.gameObject.SetActive(false);
    }

    public void Hide()
    {
        registerLoginButton.onClick.RemoveAllListeners();

        emailInputField.text = string.Empty;
        passwordInputField.text = string.Empty;
        reEnterpasswordInputField.text = string.Empty;

        authRegisterLoginPanelGameOBject.SetActive(false);
    }

    private bool IsValidInput()
    {
        if (!emailRegex.IsMatch(emailInputField.text))
        {
            ShowEmailError();
            return false;
        }
        if (!passwordRegex.IsMatch(passwordInputField.text))
        {
            ShowPasswordRuleError();
            return false;
        }
        if (reEnterpasswordInputField.gameObject.activeSelf && reEnterpasswordInputField.text != passwordInputField.text)
        {
            ShowPasswordNoMatchError();
            return false;
        }

        return true;
    }

    private void ShowPasswordNoMatchError()
    {
        messagePresenter.Show("Passwords are not the same!", Color.red);
    }

    private void ShowEmailError()
    {
        messagePresenter.Show("Email is not valid!", Color.red);
    }

    private void ShowPasswordRuleError()
    {
        messagePresenter.Show("Password is not valid!\n- At least one number.\n- At least one lowercase letter.\n- At least one uppercase letter.\n- At least one special character.\n- Minimum length of 8 characters.", Color.red);
    }
}
