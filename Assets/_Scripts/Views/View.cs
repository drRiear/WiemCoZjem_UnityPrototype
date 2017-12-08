using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class View : MonoBehaviour
{
    public void Setup()
    {
        OnSetup();
    }

    protected abstract void OnSetup();

    public virtual void Show()
    {
        print("Show " + name);
        gameObject.SetActive(true);
    }
    public virtual void Hide()
    {
        print("Hide " + name);
        gameObject.SetActive(false);
    }


}
