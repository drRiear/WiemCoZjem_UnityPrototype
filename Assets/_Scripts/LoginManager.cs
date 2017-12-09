using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour {
    
    #region Private Vars
    private string _login;
    private string _password;
    #endregion

    #region Private Serializable Vars
    [Header("Inputs")]
    [SerializeField] private InputField loginInput;
    [SerializeField] private InputField passInput;
    [Header("Buttons")]
    [SerializeField] private Button loginButton;

    [SerializeField] private GameObject wrongData;
    #endregion

    #region Unity Events
    private void OnEnable()
    {
        loginInput.Select();
    }
    #endregion

    #region Public Methods  
    public void GetLogin()
    {
        _login = loginInput.text;
        passInput.Select();
    }   
    public void GetPassword()
    {
        _password = passInput.text;
        loginButton.Select();
    }
    public void CheckLoginData()
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
                    passInput.Select();
                    ShowLoginError("Wrong password");
                    break;
                }
            }
            else
            {
                loginInput.Select();
                ShowLoginError("Wrong login");
                continue;
            }
        }
    }

    private void ShowLoginError(string error)
    {
        wrongData.GetComponentInChildren<Text>().text = error;
        wrongData.SetActive(true);
    }
    #endregion
}
