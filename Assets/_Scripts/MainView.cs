using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MainView : View {
    [SerializeField] Button loginButton;
    [SerializeField] Button searchButton;
    
    void Awake()
    {
        loginButton.onClick.AddListener(LoginButtonHandler);
        searchButton.onClick.AddListener(SearchButtonHandler);
    }

    void LoginButtonHandler()
    {
        Debug.Log("Login button pressed");
        ViewSwitcher.Instance.ShowView<MainView>();
    }

    void SearchButtonHandler()
    {
        ViewSwitcher.Instance.ShowView<SearchView>();
    }

    protected override void OnSetup()
    {
        Debug.Log("onSetup view: " + name);
    }
    public override void Show()
    {
        base.Show();

    }
    public override void Hide()
    {
        base.Hide();
    }
}
