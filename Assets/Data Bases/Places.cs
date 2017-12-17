using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class Places : MonoBehaviour
{

    #region Singleton Implement.
    private static Places places;
    public static Places Instance
    {
        get
        {
            if (!places)
            {
                places = FindObjectOfType(typeof(Places)) as Places;

                if (!places)
                    Debug.LogError("There needs to be one active Places script on a GameObject in your scene.");
            }
            return places;
        }
    }
    #endregion

    [SerializeField] private string placesURL;

    public RootObject placesRoot;
    public Item lastSearchedPlace;

    private void OnEnable()
    {
        placesURL = "http://localhost:3000/places/5a2f192b2951be209c98b915";

        StartCoroutine(getPlaces());
    }

    private IEnumerator getPlaces()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(placesURL))
        {
            www.chunkedTransfer = false;
            yield return www.SendWebRequest ();
            if (www.isNetworkError || www.isHttpError)
                Debug.LogError(www.error);
            else
                if (www.isDone)
                    placesRoot = JsonUtility.FromJson<RootObject>(www.downloadHandler.text);
        }
    }
    [System.Serializable]
    public class Hours
    {
        public string sunday;
        public string saturday;
        public string friday;
        public string thursday;
        public string wednesday;
        public string thuesday;
        public string monday;
    }
    [System.Serializable]
    public class Item
    {
        public string name;
        public string description;
        public string adress;
        public float [] coordinates;
        public string _id;
        public List<int> stars;
        public Hours hours;
    }
    [System.Serializable]
    public class RootObject
    {
        public string _id;
        public int __v;
        public List<Item> items;
    }
}
