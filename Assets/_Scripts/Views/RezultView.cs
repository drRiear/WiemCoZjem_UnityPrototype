using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class RezultView : View
{

    #region Private Variables
    private Dishes.Item dish;
    private Specifics.RootObject specifics;
    private Places.RootObject places;

    [HideInInspector] public RezultViewChild selectedChild;
    private SortingState sortingState = 0;
    private List<GameObject> displayedChilds = new List<GameObject>();
    #endregion

    #region Serializable Variables
    [SerializeField] private RawImage mainImage;
    [Header("Buttons")]
    [SerializeField] private Button toMapButton;
    [SerializeField] private Button toDescriptionButton;
    [Header("Sorting")]
    [SerializeField] private Text sortingText;
    [SerializeField] private Button sortingButton;
    [Header("Child")]
    [SerializeField]
    private GameObject childPrefab;
    [SerializeField] private Transform childContainer;
    #endregion

    #region Enumerators
    enum SortingState { name, adress, price, distance };
    #endregion

    #region Unity Events
    private void OnEnable()
    {
        specifics = Specifics.Instance.specificsRoot;
        places = Places.Instance.placesRoot;
        dish = Dishes.Instance.lastSearchedDish;
        StartCoroutine(SetDishPhoto());

        Search();

        sortingButton.onClick.AddListener(Sort);

        toMapButton.onClick.AddListener(ShowMap);
        toDescriptionButton.onClick.AddListener(delegate { ViewSwitcher.Instance.ShowView<DishDescriptionView>(); });
    }
    #endregion

    #region Private Methods
    private IEnumerator SetDishPhoto()
    {
        WWW www = new WWW(dish.photoLink);
        yield return www;
        mainImage.texture = www.texture;
    }
    private void Search()
    {
        foreach (var specificsItem in specifics.items)
            if (specificsItem.dishID == dish._id)
                ShowInfo(specificsItem);
    }
    private void ShowInfo(Specifics.Item specificsItem)
    {
        var displayedChild = Instantiate(childPrefab, childContainer);
        var childComponents = displayedChild.GetComponent<RezultViewChild>();
        var currentPlace = FindCurrentPlace(specificsItem.placeID);

        childComponents.PriceText.text = specificsItem.price + "zł.";
        childComponents.NameText.text = currentPlace.name;
        childComponents.AdressText.text = currentPlace.adress;

        displayedChild.name = currentPlace.name;

        displayedChilds.Add(displayedChild);
        displayedChild.GetComponentInChildren<Button>().onClick
            .AddListener(delegate { SetSelected(childComponents, currentPlace); });
    }
    private Places.Item FindCurrentPlace(string placeID)
    {
        foreach (var placesItem in places.items)
            if (placeID == placesItem._id)
                return placesItem;
        return null;
    }

    private void SetSelected(RezultViewChild childData, Places.Item place)
    {
        if (selectedChild == childData)
            selectedChild = null;
        else
        {
            selectedChild = childData;
            Places.Instance.lastSearchedPlace = place;
        }
    }
    private void DestroyPreviousChilds()
    {
        if (displayedChilds.Count != 0)
            foreach (var child in displayedChilds)
                Destroy(child);
        displayedChilds = new List<GameObject>();
    }
    private void ShowMap()
    {
        if (selectedChild != null)
            ViewSwitcher.Instance.ShowView<MapView>();
    }

    private void Sort()
    {
        switch (sortingState)
        {
            case SortingState.name:
                sortingText.text = "Name";
                ++sortingState;
                break;
            case SortingState.adress:
                sortingText.text = "Adress";
                ++sortingState;
                break;
            case SortingState.price:
                sortingText.text = "Price";
                ++sortingState;
                break;
            case SortingState.distance:
                sortingText.text = "Distance";
                sortingState = 0;
                break;
        }
    }
    #endregion
}
    
