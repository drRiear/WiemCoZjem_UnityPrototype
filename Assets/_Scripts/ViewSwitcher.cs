using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ViewSwitcher : MonoBehaviour
{
    [SerializeField] List<View> viewsPrefabs;
    [SerializeField] List<View> views;
    [SerializeField] Transform viewsContainer;

    View activeView;

    void Awake()
    {
        foreach(var view in viewsPrefabs)
        {
            var newView = Instantiate(view);
            newView.transform.SetParent(viewsContainer, false);
            newView.Setup();
        }
    }

    public void ShowView<T>() where T : View
    {
        var view = views.FirstOrDefault(x => x.GetType() == typeof(T));
        if (view != null)
        {
            activeView.Hide();
            view.Show();
        }else
        {
            Debug.LogWarning("View not found");
        }
        
    }
}