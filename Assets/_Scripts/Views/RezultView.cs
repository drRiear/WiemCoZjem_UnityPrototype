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
    #endregion

    #region Serializable Variables
    [SerializeField] private RawImage mainImage;
    [SerializeField] private Button toMapButton;
    [Header("Child")]
    [SerializeField]
    private GameObject childPrefab;
    [SerializeField] private Transform childContainer;
    #endregion

    #region Unity Events
    private void OnEnable()
    {
        specifics = Specifics.Instance.specificsRoot;
        places = Places.Instance.placesRoot;
        dish = Dishes.Instance.lastSearchedDish;
        StartCoroutine(SetDishPhoto());

        Search();

        toMapButton.onClick.AddListener(delegate { ShowMap(); });
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
        var childData = displayedChild.GetComponent<RezultViewChild>();
        var currentPlace = FindCurrentPlace(specificsItem.placeID);

        childData.PriceText.text = specificsItem.price + "zł.";
        childData.NameText.text = currentPlace.name;
        childData.AdressText.text = currentPlace.adress;

        displayedChild.GetComponentInChildren<Button>().onClick
            .AddListener(delegate { SetSelected(childData, currentPlace); });
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
        selectedChild = childData;
        Places.Instance.lastSearchedPlace = place;
    }

    private void ShowMap()
    {
        if (selectedChild != null)
            ViewSwitcher.Instance.ShowView<MapView>();
    }

    #endregion
}
