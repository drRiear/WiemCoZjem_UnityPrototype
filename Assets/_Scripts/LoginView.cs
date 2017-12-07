using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class LoginView : View {
    [SerializeField] Button loginButton;

    void Awake()
    {
        loginButton.onClick.AddListener(LoginButtonHandler);
    }

    void LoginButtonHandler()
    {
        Debug.Log("Login button pressed");
    }

    protected override void OnSetup()
    {
        Debug.Log("onSetup view: " + name);
    }
}
