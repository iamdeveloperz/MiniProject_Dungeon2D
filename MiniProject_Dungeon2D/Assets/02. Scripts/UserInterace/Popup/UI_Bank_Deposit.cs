
using UnityEngine.EventSystems;

public class UI_Bank_Deposit : UI_Popup
{
    #region Enums

    enum Buttons
    {
        BTN_Deposit_10000,
        BTN_Deposit_30000,
        BTN_Deposit_50000,
        BTN_Deposit_Input,
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

        GetButton((int)Buttons.BTN_Deposit_10000).gameObject.BindEvent(Deposit_10000);
        GetButton((int)Buttons.BTN_Deposit_30000).gameObject.BindEvent(Deposit_30000);
        GetButton((int)Buttons.BTN_Deposit_50000).gameObject.BindEvent(Deposit_50000);
        GetButton((int)Buttons.BTN_Deposit_Input).gameObject.BindEvent(Deposit_Input);
        GetButton((int)Buttons.BTN_Close).gameObject.BindEvent(ClosePopup);
    }
    #endregion



    #region Events

    private void ClosePopup(PointerEventData data)
    {
        Managers.UI.ClosePopupUI(this);
    }

    private void Deposit_10000(PointerEventData data)
    {
        Managers.Game.playerData.Gold -= 10000;
        Managers.Game.accountData.Cash += 10000;

        Managers.DB.UpdatePlayer(Literals.DB_PLAYERDATA, Managers.Game.playerData);
        Managers.DB.UpdatePlayer(Literals.DB_ACCOUNT, Managers.Game.accountData);
    }

    private void Deposit_30000(PointerEventData data)
    {
        Managers.Game.playerData.Gold -= 30000;
        Managers.Game.accountData.Cash += 30000;

        Managers.DB.UpdatePlayer(Literals.DB_PLAYERDATA, Managers.Game.playerData);
        Managers.DB.UpdatePlayer(Literals.DB_ACCOUNT, Managers.Game.accountData);
    }

    private void Deposit_50000(PointerEventData data)
    {
        Managers.Game.playerData.Gold -= 50000;
        Managers.Game.accountData.Cash += 50000;

        Managers.DB.UpdatePlayer(Literals.DB_PLAYERDATA, Managers.Game.playerData);
        Managers.DB.UpdatePlayer(Literals.DB_ACCOUNT, Managers.Game.accountData);
    }

    private void Deposit_Input(PointerEventData data)
    {
        int inputGold = int.Parse(GetInputField((int)InputFields.IF_Gold).text);

        Managers.Game.playerData.Gold -= inputGold;
        Managers.Game.accountData.Cash += inputGold;

        Managers.DB.UpdatePlayer(Literals.DB_PLAYERDATA, Managers.Game.playerData);
        Managers.DB.UpdatePlayer(Literals.DB_ACCOUNT, Managers.Game.playerData);
    }

    #endregion
}