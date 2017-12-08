using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MainView : View {
    [SerializeField] Button searchButton;
    
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
