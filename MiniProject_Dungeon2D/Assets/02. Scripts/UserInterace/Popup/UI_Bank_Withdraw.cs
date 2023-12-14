
using UnityEngine.EventSystems;

public class UI_Bank_Withdraw : UI_Popup
{
    #region Enums

    enum Buttons
    {
        BTN_Withdraw_10000,
        BTN_Withdraw_30000,
        BTN_Withdraw_50000,
        BTN_Withdraw_Input,
        BTN_Close
    }

    enum InputFields
    {
        IF_Gold
    }

    #endregion



    #region Initialize
    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        BindButton(typeof(Buttons));
        BindInputField(typeof(InputFields));

        GetButton((int)Buttons.BTN_Withdraw_10000).gameObject.BindEvent(Withdraw_10000);
        GetButton((int)Buttons.BTN_Withdraw_30000).gameObject.BindEvent(Withdraw_30000);
        GetButton((int)Buttons.BTN_Withdraw_50000).gameObject.BindEvent(Withdraw_50000);
        GetButton((int)Buttons.BTN_Withdraw_Input).gameObject.BindEvent(Withdraw_Input);
        GetButton((int)Buttons.BTN_Close).gameObject.BindEvent(ClosePopup);
    }
    #endregion



    #region Events

    private void ClosePopup(PointerEventData data)
    {
        Managers.UI.ClosePopupUI(this);
    }

    private void Withdraw_10000(PointerEventData data)
    {

    }

    private void Withdraw_30000(PointerEventData data)
    {

    }

    private void Withdraw_50000(PointerEventData data)
    {

    }

    private void Withdraw_Input(PointerEventData data)
    {

    }

    #endregion
}