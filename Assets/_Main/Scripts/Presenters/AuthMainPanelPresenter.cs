using System;
using UnityEngine;
using UnityEngine.UI;

public class AuthMainPanelPresenter : MonoBehaviour
{
    public event EventHandler OnRegisterButtonPressed;
    public event EventHandler OnLoginButtonPressed;

    [SerializeField] private GameObject authMainPanelGameObject;
    [SerializeField] private Button registerButton;
    [SerializeField] private Button loginButton;

    private void Start()
    {
        registerButton.onClick.AddListener(() =>
        {
            OnRegisterButtonPressed?.Invoke(this, EventArgs.Empty);
        });
        loginButton.onClick.AddListener(() =>
        {
            OnLoginButtonPressed?.Invoke(this, EventArgs.Empty);
        });
    }

    public void Show()
    {
        authMainPanelGameObject.SetActive(true);
    }

    public void Hide()
    {
        authMainPanelGameObject.SetActive(false);
    }
}
