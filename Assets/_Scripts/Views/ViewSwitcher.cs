using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    [SerializeField] Transform viewsContainer;
    [SerializeField] List<GameObject> viewsPrefabs;

    [SerializeField] private List<View> views;
    private View activeView;

    void Awake()
    {
        foreach(var viewPrefab in viewsPrefabs)
        {
            var newView = Instantiate(viewPrefab);
            newView.transform.SetParent(viewsContainer, false);
            var viewComponent = newView.GetComponent<View>();
            viewComponent.Setup();
            views.Add(viewComponent);
        }
        
        activeView = views[0];
        activeView.Show();
    }

    public void ShowView<T>() where T : View
    {
        var view = views.FirstOrDefault(x => x.GetType() == typeof(T));
        if (view != null)
        {
            activeView.Hide();
            view.Show();
            activeView = view;
        }else
        {
            Debug.LogWarning("View not found");
        }
    }
}