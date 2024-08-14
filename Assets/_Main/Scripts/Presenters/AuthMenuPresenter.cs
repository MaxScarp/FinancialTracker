using System;
using UnityEngine;

public class AuthMenuPresenter : MonoBehaviour
{
    [SerializeField] private GameObject authMenuGameObject;
    [SerializeField] private AuthMainPanelPresenter authMainPanelPresenter;
    [SerializeField] private AuthRegisterLoginPanelPresenter authRegisterLoginPanelPresenter;

    private void Start()
    {
        authMainPanelPresenter.OnRegisterButtonPressed += AuthMainPanelPresenter_OnRegisterButtonPressed;
        authMainPanelPresenter.OnLoginButtonPressed += AuthMainPanelPresenter_OnLoginButtonPressed;
        authRegisterLoginPanelPresenter.OnRegisterLoginButtonPressed += AuthRegisterLoginPanelPresenter_OnRegisterLoginButtonPressed;
        authRegisterLoginPanelPresenter.OnBackButtonPressed += AuthRegisterLoginPanelPresenter_OnBackButtonPressed;
    }

    private void AuthRegisterLoginPanelPresenter_OnBackButtonPressed(object sender, EventArgs e)
    {
        authRegisterLoginPanelPresenter.Hide();

        authMainPanelPresenter.Show();
    }

    private void AuthRegisterLoginPanelPresenter_OnRegisterLoginButtonPressed(object sender, EventArgs e)
    {
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
