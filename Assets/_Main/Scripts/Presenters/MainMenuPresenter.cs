using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPresenter : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuGameObject;

    [SerializeField] private OutcomePresenter outcomePresenter;
    [SerializeField] private AuthMenuPresenter authMenuPresenter;
    [SerializeField] private MessageYesNoPresenter messageYesNoPresenter;
    [SerializeField] private MessageConfirmPresenter messageConfirmPresenter;

    [SerializeField] private Button addOutcomeButton;
    [SerializeField] private Button modifyEmailButton;
    [SerializeField] private Button modifyPasswordButton;
    [SerializeField] private Button deleteUserButton;
    [SerializeField] private Button logoutButton;

    private void Start()
    {
        addOutcomeButton.onClick.AddListener(() =>
        {
            outcomePresenter.Show();

            Hide();
        });
        modifyEmailButton.onClick.AddListener(() =>
        {

        });
        modifyPasswordButton.onClick.AddListener(() =>
        {

        });
        deleteUserButton.onClick.AddListener(() =>
        {
            messageYesNoPresenter.Show("Are you sure you want to delete your account?");
        });
        logoutButton.onClick.AddListener(() =>
        {
            Authenticator.LogoutUser();
            authMenuPresenter.Show();
        });

        outcomePresenter.OnBackButtonSelected += OutcomePresenter_OnBackButtonSelected;
        messageYesNoPresenter.OnYesButtonSelected += MessageYesNoPresenter_OnYesButtonSelected;
    }

    private void MessageYesNoPresenter_OnYesButtonSelected(object sender, EventArgs e)
    {
        messageConfirmPresenter.Show("Please, confirm your password to delete your account");
    }

    private void OutcomePresenter_OnBackButtonSelected(object sender, EventArgs e)
    {
        outcomePresenter.Hide();

        Show();
    }

    private void Show()
    {
        mainMenuGameObject.SetActive(true);
    }

    private void Hide()
    {
        mainMenuGameObject.SetActive(false);
    }
}
