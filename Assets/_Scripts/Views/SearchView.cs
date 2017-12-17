using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchView : View
{
    #region Serializable Vars
    [Header("Child")]
    [SerializeField] private GameObject noRezultPrefab;
    [SerializeField] private GameObject childPrefab;
    [SerializeField] private Transform childContainer;
    #endregion

    #region Private vars
    private Dishes.RootObject dishes;
    private InputField searchInput;
    private List<GameObject> displayedChilds = new List<GameObject>();
    #endregion

    #region Unity Events
    private void OnEnable()
    {
        dishes = Dishes.Instance.dishesRoot;

        searchInput = GetComponentInChildren<InputField>();
        searchInput.onEndEdit.AddListener(delegate { Search(); });
    }
    private void OnDisable()
    {
        DestroyPreviousChilds();
    }
    #endregion

    #region Private Methods
    private void Search()
    {
        bool isFound = false;
        DestroyPreviousChilds();
        string searchString = searchInput.text;

        foreach (var item in dishes.items)
            if (item.name.Contains(searchString))
            {
                ShowDishes(item);
                isFound = true;
            }

        if(!isFound)
            ShowNoReultView();
    }

    private void DestroyPreviousChilds()
    {
        if(displayedChilds.Count != 0)
            foreach (var child in displayedChilds)
                Destroy(child);
    }

    private IEnumerator cor;
    private void ShowDishes(Dishes.Item item)
    {
        var displayedChild = Instantiate(childPrefab, childContainer);

        displayedChild.GetComponentInChildren<Text>().text = item.name;
        cor = SetDishPhoto(item.photoLink, displayedChild);
        StartCoroutine(cor);

        displayedChilds.Add(displayedChild);
        displayedChild.GetComponentInChildren<Button>().onClick
            .AddListener(delegate { ToRezultView(item); });
    }
    IEnumerator SetDishPhoto(string photoLink, GameObject child)
    {
        WWW www = new WWW(photoLink);
        yield return www;
        child.GetComponentInChildren<RawImage>().texture = www.texture;
    }

    private void ShowNoReultView()
    {
        var displayedChild = Instantiate(noRezultPrefab, childContainer);
        displayedChilds.Add(displayedChild);
    }

    private void ToRezultView(Dishes.Item item)
    {
        Dishes.Instance.lastSearchedDish = item;
        ViewSwitcher.Instance.ShowView<RezultView>();
    }
    #endregion
  
}
