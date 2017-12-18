using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DishDescriptionView : View
{
    #region Serializable Variables
    [SerializeField] private Text descriptionText;
    [SerializeField] private RawImage dishImage;
    #endregion

    #region Unity Events
    private void OnEnable()
    {
        StartCoroutine(SetDishPhoto());

        descriptionText.text = Dishes.Instance.lastSearchedDish.description;
    }
    #endregion


    #region Private Methods
    private IEnumerator SetDishPhoto()
    {
        string photoLink = Dishes.Instance.lastSearchedDish.photoLink;
        WWW www = new WWW(photoLink);
        yield return www;
        dishImage.texture = www.texture;
    }
    #endregion
}
