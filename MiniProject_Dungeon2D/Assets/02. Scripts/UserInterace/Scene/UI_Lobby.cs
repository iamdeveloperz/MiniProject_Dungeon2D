
using JetBrains.Annotations;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Lobby : UI_Scene
{
    #region Enums
    
    enum Buttons
    {
        BTN_Bank,
        BTN_Character,
        BTN_Inventory,
        BTN_Store
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

        GetButton((int)Buttons.BTN_Bank).gameObject.BindEvent(ShowBankPopup);
    }

    private void ShowBankPopup(PointerEventData data)
    {
        Managers.UI.ShowPopupUI<UI_Bank>(Literals.LOBBY_Popup_Bank);
    }
}