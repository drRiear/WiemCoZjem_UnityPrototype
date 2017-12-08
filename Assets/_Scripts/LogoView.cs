using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoView : View
{
    void Awake()
    {

    }

    protected override void OnSetup()
    {
        Debug.Log("onSetup view: " + name);
    }

    public override void Show()
    {
        base.Show();
        
    }
    public override void Hide()
    {
        base.Hide();
    }

}
