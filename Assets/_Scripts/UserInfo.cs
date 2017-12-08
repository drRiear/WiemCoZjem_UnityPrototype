using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfo : MonoBehaviour {

    #region Singleton Implement.
    private static UserInfo userInfo;
    public static UserInfo Instance
    {
        get
        {
            if (!userInfo)
            {
                userInfo = FindObjectOfType(typeof(UserInfo)) as UserInfo;

                if (!userInfo)
                    Debug.LogError("There needs to be one active UserInfo script on a GameObject in your scene.");
            }
            return userInfo;
        }
    }
    #endregion

    [SerializeField]public string login;
    [SerializeField]public string password;

    [SerializeField]public string fullName;

    [SerializeField]public bool loggined;

}
