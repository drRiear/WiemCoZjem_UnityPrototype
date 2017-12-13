using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MainView : View {
    [SerializeField] Button searchButton;
    [SerializeField] Text helloText;
    
    void Awake()
    {
        searchButton.onClick.AddListener(SearchButtonHandler);
    }
    
    void SearchButtonHandler()
    {
        ViewSwitcher.Instance.ShowView<SearchView>();
    }

    protected override void OnSetup()
    {
    }
    public override void Show()
    {
        base.Show();
        helloText.text += UserInfo.Instance.currentUser.fullName;

    }
    public override void Hide()
    {
        base.Hide();
    }

    public void ToLogin()
    {
        ViewSwitcher.Instance.ShowView<LoginView>();
    }
}
