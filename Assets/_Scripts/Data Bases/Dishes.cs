using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Dishes : MonoBehaviour
{
    #region Singleton Implement.

    private static Dishes dishes;
    public static Dishes Instance
    {
        get
        {
            if (!dishes)
            {
                dishes = FindObjectOfType(typeof(Dishes)) as Dishes;

                if (!dishes)
                    Debug.LogError("There needs to be one active Dishes script on a GameObject in your scene.");
            }
            return dishes;
        }
    }
    #endregion

    [SerializeField] private string dishesURL;

    public RootObject dishesRoot;
    public Item lastSearchedDish;

    private void OnEnable()
    {
        dishesURL = "http://localhost:3000/dishes/5a2aeba20c97b2337c43d163";

        StartCoroutine(getDishes());
    }

    private IEnumerator getDishes()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(dishesURL))
        {
            www.chunkedTransfer = false;
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
                Debug.LogError(www.error);
            else
                if (www.isDone)
                    dishesRoot = JsonUtility.FromJson<RootObject>(www.downloadHandler.text);
        }
    }
    #region JSON classes
    [System.Serializable]
    public class RootObject
    {
        public string _id;
        public int __v;
        public List<Item> items;
    }
    [System.Serializable]
    public class Item
    {
        public string name;
        public string description;
        public bool vege;
        public string photoLink;
        public string _id;
        public List<string> ingredients;
        public List<string> alergens;
    }
    #endregion
}

