using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoogleMaps : MonoBehaviour {

    public uint zoom = 10;
    public Vector2 center;
    
    private IEnumerator cor;
    private RawImage image;
    private string mapURL;

    private void Start()
    {
        image = GetComponent<RawImage>();
        cor = Map();

        StartCoroutine(cor);
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ++zoom;

            cor = Map();
            StartCoroutine(cor);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            --zoom;

            cor = Map();
            StartCoroutine(cor);
        }
    }

    IEnumerator Map()
    {
        mapURL = "https://maps.googleapis.com/maps/api/staticmap"
                + "?center=" + center.x + "," + center.y + "&zoom=" + zoom
                + "&size=" + Screen.width + "x" + Screen.height + "&key=AIzaSyBQMufrhEGywO7cCmyLGAivu5EAqthZ2UU";
        
        WWW www = new WWW(mapURL);
        yield return www;
        image.texture = www.texture;
        StopCoroutine(cor);
    }
}
