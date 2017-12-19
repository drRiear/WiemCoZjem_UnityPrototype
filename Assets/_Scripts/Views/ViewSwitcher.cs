using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class ViewSwitcher : MonoBehaviour
{
    #region Singleton Implement.
    private static ViewSwitcher viewSwitcher;
    public static ViewSwitcher Instance
    {
        get
        {
            if (!viewSwitcher)
            {
                viewSwitcher = FindObjectOfType(typeof(ViewSwitcher)) as ViewSwitcher;

                if (!viewSwitcher)
                    Debug.LogError("There needs to be one active ViewSwitcher script on a GameObject in your scene.");
            }
            return viewSwitcher;
        }
    }
    #endregion

    #region Serializable Variables
    [Header("Top Bar")]
    [SerializeField] private Text logoText;
    [SerializeField] private Button backButton;
    [Header("Views")]
    [SerializeField] private Transform viewsContainer;
    [SerializeField] private List<GameObject> viewsPrefabs;

    [SerializeField] private List<View> views;
    [SerializeField] private int startViewIndex;
    #endregion
    
    private View activeView;

    void Awake()
    {
        foreach(var viewPrefab in viewsPrefabs)
        {
            var newView = Instantiate(viewPrefab, viewsContainer);
            var viewComponent = newView.GetComponent<View>();
            views.Add(viewComponent);
        }

        activeView = views[startViewIndex];
        activeView.Show();

        backButton.onClick.AddListener(delegate { Back(); });
    }

    public void ShowView<T>() where T : View
    {
        var view = views.FirstOrDefault(x => x.GetType() == typeof(T));
        if (view != null)
        {
            activeView.Hide();
            view.Show();
            activeView = view;
            SetTopBarLogo();
        }
        else
        {
            Debug.LogWarning("View not found");
        }
    }
    

    private void SetTopBarLogo()
    {
        logoText.resizeTextForBestFit = true;
        if (activeView.GetType() == typeof(RezultView) || activeView.GetType() == typeof(DishDescriptionView))
            logoText.text = Dishes.Instance.lastSearchedDish.name;
        else if (activeView.GetType() == typeof(MapView))
            logoText.text = Places.Instance.lastSearchedPlace.name;
        else
            logoText.text = "WIEM CO ZJEM";
    }
    private void Back()
    {
        if (activeView.GetType() == typeof(LoginView) || activeView.GetType() == typeof(SearchView))
            ShowView<MainView>();
        else if (activeView.GetType() == typeof(RezultView))
            ShowView<SearchView>();
        else if (activeView.GetType() == typeof(MapView) || activeView.GetType() == typeof(DishDescriptionView))
            ShowView<RezultView>();
    }
}