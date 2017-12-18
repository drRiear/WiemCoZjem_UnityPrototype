using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MapView : View
{
    #region Serializable variables
    [SerializeField] private uint zoom = 16;
    [SerializeField] private Vector2 userCoordinates = new Vector2(50.06397f, 19.93799f);
    #endregion

    /*coords 
     * (50.004045f, 19.885776f) 
     * (50.06397f, 19.93799f) 
     * */

    #region Private Variables
    private IEnumerator cor;
    private RawImage image;
    private string mapURL;
    private Vector2 placeCoordinates;
    [SerializeField] private Vector2 center;
    #endregion

    #region Unity Events
    private void OnEnable()
    {
        image = GetComponentInChildren<RawImage>();
        
        placeCoordinates.x = Places.Instance.lastSearchedPlace.coordinates[0];
        placeCoordinates.y = Places.Instance.lastSearchedPlace.coordinates[1];

        center.x = (userCoordinates.x + placeCoordinates.x) / 2;
        center.y = (userCoordinates.y + placeCoordinates.y) / 2;

        cor = Map();
        StartCoroutine(cor);
    }


    private void Update()
    {
        cor = Map();
        if(Input.GetKeyDown(KeyCode.Y))
            StartCoroutine(cor);
    }
    #endregion

    #region Private Methods
    private IEnumerator Map()
    {
        yield return new WaitForSeconds(0.5f);
        mapURL = "https://maps.googleapis.com/maps/api/staticmap"
                + "?center=" + center.x + "," + center.y + "&zoom=" + zoom
                + "&markers=color:red|"+ userCoordinates.x + "," + userCoordinates.y 
                + "&markers=color:green|" + placeCoordinates.x + "," + placeCoordinates.y
                + "&size=" + Screen.width + "x" + Screen.height + "&key=AIzaSyBQMufrhEGywO7cCmyLGAivu5EAqthZ2UU";

        WWW www = new WWW(mapURL);
        yield return www;
        image.texture = www.texture;
        StopCoroutine(cor);
    }
    #endregion
}
