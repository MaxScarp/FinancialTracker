using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPresenter : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuGameObject;

    [SerializeField] private OutcomePresenter outcomePresenter;

    [SerializeField] private Button addOutcomeButton;

    private void Start()
    {
        addOutcomeButton.onClick.AddListener(() =>
        {
            outcomePresenter.Show();

            Hide();
        });

        outcomePresenter.OnBackButtonSelected += OutcomePresenter_OnBackButtonSelected;
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
