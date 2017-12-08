using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ViewSwitcher : MonoBehaviour
{
    #region Singleton Implement.
    private static ViewSwitcher characterManager;
    public static ViewSwitcher Instance
    {
        get
        {
            if (!characterManager)
            {
                characterManager = FindObjectOfType(typeof(ViewSwitcher)) as ViewSwitcher;

                if (!characterManager)
                    Debug.LogError("There needs to be one active CharacterManager script on a GameObject in your scene.");
            }
            return characterManager;
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
            views.Add(newView.GetComponent<View>());
        }
        

        activeView = views[1];
        activeView.Show();
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