using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour {

    private string _login;
    private string _password;

    [SerializeField] private InputField loginInput;
    [SerializeField] private InputField passInput;
    [SerializeField] private GameObject wrongData;

    public void GetLogin()
    {
        _login = loginInput.text;
    }   
    public void GetPassword()
    {
        _password = passInput.text;
    }


    public void CheckLoginData()
    {
        if (_login == UserInfo.Instance.login && _password == UserInfo.Instance.password)
            ViewSwitcher.Instance.ShowView<MainView>();
        else
            wrongData.SetActive(true);

    }
}
