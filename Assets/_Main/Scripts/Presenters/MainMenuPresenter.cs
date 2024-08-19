using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPresenter : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuGameObject;

    [SerializeField] private AddOutcomePresenter addOutcomePresenter;
    [SerializeField] private DeleteOutcomePresenter deleteOutcomePresenter;
    [SerializeField] private AuthMenuPresenter authMenuPresenter;
    [SerializeField] private MessageYesNoPresenter messageYesNoPresenter;
    [SerializeField] private MessageConfirmPresenter messageConfirmPresenter;

    [SerializeField] private Button monthlyEntryButton;
    [SerializeField] private Button addOutcomeButton;
    [SerializeField] private Button deleteOutcomeButton;
    [SerializeField] private Button deleteUserButton;
    [SerializeField] private Button logoutButton;

    private void Start()
    {
        //monthlyEntryButton.onClick.AddListener(() =>
        //{
        //    addOutcomePresenter.Show();

        //    Hide();
        //});
        addOutcomeButton.onClick.AddListener(() =>
        {
            addOutcomePresenter.Show();

            Hide();
        });
        deleteOutcomeButton.onClick.AddListener(() =>
        {
            deleteOutcomePresenter.Show();

            Hide();
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
    }

    private void MessageYesNoPresenter_OnYesButtonSelected(object sender, EventArgs e)
    {
        messageConfirmPresenter.Show("Please, confirm your password to delete your account");
    }

    private void DeleteOutcomePresenter_OnBackButtonSelected(object sender, EventArgs e)
    {
        deleteOutcomePresenter.Hide();

        Show();
    }

    private void OutcomePresenter_OnBackButtonSelected(object sender, EventArgs e)
    {
        addOutcomePresenter.Hide();

        Show();
    }

    private void Show()
    {
        mainMenuGameObject.SetActive(true);

        addOutcomePresenter.OnBackButtonSelected += OutcomePresenter_OnBackButtonSelected;
        deleteOutcomePresenter.OnBackButtonSelected += DeleteOutcomePresenter_OnBackButtonSelected;
        messageYesNoPresenter.OnYesButtonSelected += MessageYesNoPresenter_OnYesButtonSelected;
    }

    private void Hide()
    {
        addOutcomePresenter.OnBackButtonSelected -= OutcomePresenter_OnBackButtonSelected;
        deleteOutcomePresenter.OnBackButtonSelected -= DeleteOutcomePresenter_OnBackButtonSelected;
        messageYesNoPresenter.OnYesButtonSelected -= MessageYesNoPresenter_OnYesButtonSelected;

        mainMenuGameObject.SetActive(false);
    }
}
