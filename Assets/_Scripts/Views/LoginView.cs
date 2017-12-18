using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginView : View
{
    #region Private Vars
    private string _login;
    private string _password;
    private bool errorVisible;
    #endregion

    #region Private Serializable Vars
    [SerializeField] private GameObject wrongData;
    [Header("Inputs")]
    [SerializeField] private InputField loginInput;
    [SerializeField] private InputField passInput;
    [Header("Buttons")]
    [SerializeField] private Button loginButton;
    #endregion


    #region Unity Events
    private void OnEnable()
    {
        loginInput.Select();
        loginButton.onClick.AddListener(CheckLoginData);
        loginInput.onEndEdit.AddListener(GetLogin);
        passInput.onEndEdit.AddListener(GetPassword);
    }
    #endregion

    #region Private Methods  
    private void GetLogin(string login)
    {
        _login = login;
        passInput.Select();
    }
    private void GetPassword(string password)
    {
        _password = password;
        loginButton.Select();
    }
    private void CheckLoginData()
    {
        foreach (var info in UserInfo.Instance.loginDB)
        {
            if (_login == info.login)
            {
                if (_password == info.password)
                {
                    UserInfo.Instance.currentUser = info;
                    ViewSwitcher.Instance.ShowView<MainView>();
                }
                else
                {
                    if (!errorVisible)
                        StartCoroutine(ShowLoginError("Wrong login or password."));
                    break;
                }
            }
        }
    }
    private IEnumerator ShowLoginError(string error)
    {
        errorVisible = true;
        wrongData.GetComponentInChildren<Text>().text = error;
        wrongData.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        wrongData.SetActive(false);
        errorVisible = false;
    }
    #endregion
}