using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RezultViewChild : MonoBehaviour
{
    #region Public Variables
    [Header("Name/Adress")]
    public Text NameText;
    public Text AdressText;
    [Header("Price/Destination")]
    public Text PriceText;
    public Text DestinationText;
    [Header("Background")]
    public Image Background;
    #endregion

    #region Unity Events
    private void Update()
    {
        isSelcted();
    }
    #endregion

    #region Private Methods
    private void isSelcted()
    {
        if (GetComponentInParent<RezultView>().selectedChild == this)
            Background.color = Color.cyan;
        else
            Background.color = Color.white;
    }
    #endregion
}
