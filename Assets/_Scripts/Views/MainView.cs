using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MainView : View {

    #region Serializable Variables
    [SerializeField] Button searchButton;
    [SerializeField] Button loginButton;
    [SerializeField] Text helloText;
    #endregion

    #region Unity Events
    private void Awake()
    {
        searchButton.onClick.AddListener(SearchButtonHandler);
        loginButton.onClick.AddListener(LoginButtonHandler);
    }
    private void OnEnable()
    {
        helloText.text = "Hello " + UserInfo.Instance.currentUser.fullName;
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
