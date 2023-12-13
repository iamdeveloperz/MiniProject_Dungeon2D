using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Title : UI_Scene
{
    #region Enums

    enum Buttons
    {
        BTN_GPGS_Login,
        BTN_EMAIL_Login,
        BTN_EMAIL_Regist
    }

    enum GameObjects
    {
        ButtonGroup,
        LoginCompleteGroup
    }
    #endregion

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        BindButton(typeof(Buttons));
        BindObject(typeof(GameObjects));

        
    }

    
}
