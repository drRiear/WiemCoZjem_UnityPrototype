using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class View : MonoBehaviour
{
    public void Setup()
    {
        //doit parent logic
        OnSetup();
    }

    protected abstract void OnSetup();

    public void Show() { }
    public void Hide() { }
}
