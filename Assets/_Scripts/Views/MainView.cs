using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MainView : View {

    #region Serializable Variables
    [SerializeField] private Button searchButton;
    [SerializeField] private Button loginButton;
    [SerializeField] private Button signupButton;
    [SerializeField] private Text helloText;
    #endregion
    
    private bool showLoginButtons = false;

    #region Unity Events
    private void Awake()
    {
        searchButton.onClick.AddListener(SearchButtonHandler);
        loginButton.onClick.AddListener(LoginButtonHandler);
    }
    private void OnEnable()
    {
        helloText.text = "Hello " + UserInfo.Instance.currentUser.fullName;
        
        if (UserInfo.Instance.currentUser.login.Length == 0)
            showLoginButtons = true;
        else
            showLoginButtons = false;


        loginButton.gameObject.SetActive(showLoginButtons);
        signupButton.gameObject.SetActive(showLoginButtons);
    }
    #endregion

    #region Private Methods
    private void SearchButtonHandler()
    {
        ViewSwitcher.Instance.ShowView<SearchView>();
    }

    private void LoginButtonHandler()
    {
        ViewSwitcher.Instance.ShowView<LoginView>();
    }
    #endregion
}
