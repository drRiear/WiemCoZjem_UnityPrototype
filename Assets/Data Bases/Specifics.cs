using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class Specifics : MonoBehaviour
{
    #region Singleton Implement.
    private static Specifics specifics;
    public static Specifics Instance
    {
        get
        {
            if (!specifics)
            {
                specifics = FindObjectOfType(typeof(Specifics)) as Specifics;

                if (!specifics)
                    Debug.LogError("There needs to be one active Specifics script on a GameObject in your scene.");
            }
            return specifics;
        }
    }
    #endregion

    [SerializeField] private string specificsURL;

    public RootObject specificsRoot;

    private void OnEnable()
    {
        specificsURL = "http://localhost:3000/specifics/5a2f1c37f9db1506ac0fefc9";

        StartCoroutine(getPlaces());
    }

    private IEnumerator getPlaces()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(specificsURL))
        {
            www.chunkedTransfer = false;
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
                Debug.LogError(www.error);
            else
                if (www.isDone)
                specificsRoot = JsonUtility.FromJson<RootObject>(www.downloadHandler.text);
        }
    }
    [System.Serializable]
    public class Item
    {
        public string _id;
        public string price;
        public string dishID;
        public string placeID;
        public List<int> stars;
    }
    [System.Serializable]
    public class RootObject
    {
        public string _id;
        public int __v;
        public List<Item> items;
    }
}
