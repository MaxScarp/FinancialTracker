using System;
using System.Threading.Tasks;
using UnityEngine;

public class AuthMenuPresenter : MonoBehaviour
{
    [SerializeField] private GameObject authMenuGameObject;
    [SerializeField] private AuthMainPanelPresenter authMainPanelPresenter;
    [SerializeField] private AuthRegisterLoginPanelPresenter authRegisterLoginPanelPresenter;

    [SerializeField] private MessagePresenter messagePresenter;

    public void Show()
    {
        authMenuGameObject.SetActive(true);

        authMainPanelPresenter.Show();

        authMainPanelPresenter.OnRegisterButtonPressed += AuthMainPanelPresenter_OnRegisterButtonPressed;
        authMainPanelPresenter.OnLoginButtonPressed += AuthMainPanelPresenter_OnLoginButtonPressed;
        authRegisterLoginPanelPresenter.OnBackButtonPressed += AuthRegisterLoginPanelPresenter_OnBackButtonPressed;
        authRegisterLoginPanelPresenter.OnRegister += AuthRegisterLoginPanelPresenter_OnRegister;
        authRegisterLoginPanelPresenter.OnLogin += AuthRegisterLoginPanelPresenter_OnLogin;

        authRegisterLoginPanelPresenter.Hide();
    }

    private void Hide()
    {
        authMainPanelPresenter.OnRegisterButtonPressed -= AuthMainPanelPresenter_OnRegisterButtonPressed;
        authMainPanelPresenter.OnLoginButtonPressed -= AuthMainPanelPresenter_OnLoginButtonPressed;
        authRegisterLoginPanelPresenter.OnBackButtonPressed -= AuthRegisterLoginPanelPresenter_OnBackButtonPressed;
        authRegisterLoginPanelPresenter.OnRegister -= AuthRegisterLoginPanelPresenter_OnRegister;
        authRegisterLoginPanelPresenter.OnLogin -= AuthRegisterLoginPanelPresenter_OnLogin;

        authMenuGameObject.SetActive(false);
    }

    private async Task<bool> LoginUser(AuthRegisterLoginPanelPresenter.UserCredentialsEventArgs userCredentials)
    {
        Authenticator.UserResult loginUserResult = await Authenticator.LoginUser(userCredentials.Email, userCredentials.Password);
        if (loginUserResult.Result == null)
        {
            if (string.IsNullOrEmpty(loginUserResult.ErrorMessage))
            {
                Debug.LogError($"Error: Text of messageBox passed is null or empty!");
                return false;
            }

            messagePresenter.Show($"{loginUserResult.ErrorMessage}", Color.red);
            authMainPanelPresenter.Show();
            return false;
        }

        return true;
    }

    private async void AuthRegisterLoginPanelPresenter_OnLogin(object sender, AuthRegisterLoginPanelPresenter.UserCredentialsEventArgs userCredentials)
    {
        authRegisterLoginPanelPresenter.Hide();

        messagePresenter.Show("WAITING FOR LOGIN...", Color.white, true);

        if (await LoginUser(userCredentials))
        {
            messagePresenter.Hide();

            Hide();
        }
    }

    private async Task<bool> RegisterUser(AuthRegisterLoginPanelPresenter.UserCredentialsEventArgs userCredentials)
    {
        Authenticator.UserResult newUserResult = await Authenticator.RegisterNewUser(userCredentials.Email, userCredentials.Password);
        if (newUserResult.Result == null)
        {
            if (string.IsNullOrEmpty(newUserResult.ErrorMessage))
            {
                Debug.LogError($"Error: Text of messageBox passed is null or empty!");
                return false;
            }

            messagePresenter.Show($"{newUserResult.ErrorMessage}", Color.red);
            authMainPanelPresenter.Show();
            return false;
        }

        return true;
    }

    private async void AuthRegisterLoginPanelPresenter_OnRegister(object sender, AuthRegisterLoginPanelPresenter.UserCredentialsEventArgs userCredentials)
    {
        authRegisterLoginPanelPresenter.Hide();

        messagePresenter.Show("WAITING FOR REGISTRATION...", Color.white, true);

        if (await RegisterUser(userCredentials))
        {
            string errorMessage = await Authenticator.SendVerificationEmail();
            if (!string.IsNullOrEmpty(errorMessage))
            {
                messagePresenter.Show($"{errorMessage}", Color.red);
                authMainPanelPresenter.Show();

                return;
            }

            messagePresenter.Show("User registered succesfully, please see your email for confirmation", Color.green);
            authMainPanelPresenter.Show();
        }
    }

    private void AuthRegisterLoginPanelPresenter_OnBackButtonPressed(object sender, EventArgs e)
    {
        authRegisterLoginPanelPresenter.Hide();

        authMainPanelPresenter.Show();
    }

    private void AuthMainPanelPresenter_OnLoginButtonPressed(object sender, EventArgs e)
    {
        authMainPanelPresenter.Hide();

        authRegisterLoginPanelPresenter.ShowLogin();
    }

    private void AuthMainPanelPresenter_OnRegisterButtonPressed(object sender, EventArgs e)
    {
        authMainPanelPresenter.Hide();

        authRegisterLoginPanelPresenter.ShowRegister();
    }
}
