
using UnityEngine.EventSystems;

public class UI_Bank : UI_Popup
{
    #region Enums

    enum Buttons
    {
        BTN_Close,
        BTN_Deposit,
        BTN_Withdraw
    }

    enum Texts
    {
        GoldText,
        CashText,
        UserNameText
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
        BindText(typeof(Texts));

        GetButton((int)Buttons.BTN_Close).gameObject.BindEvent(ClosePopup);
        GetButton((int)Buttons.BTN_Deposit).gameObject.BindEvent(ProcessDeposit);
        GetButton((int)Buttons.BTN_Withdraw).gameObject.BindEvent(ProcessWithdraw);
    }
    #endregion



    #region Events
    private void ProcessWithdraw(PointerEventData data)
    {
        Managers.UI.ShowPopupUI<UI_Bank_Withdraw>(Literals.LOBBY_Popup_Bank_Withdraw);
    }

    private void ProcessDeposit(PointerEventData data)
    {
        Managers.UI.ShowPopupUI<UI_Bank_Deposit>(Literals.LOBBY_Popup_Bank_Deposit);
    }

    private void ClosePopup(PointerEventData data)
    {
        Managers.UI.ClosePopupUI(this);
    }
    #endregion
}