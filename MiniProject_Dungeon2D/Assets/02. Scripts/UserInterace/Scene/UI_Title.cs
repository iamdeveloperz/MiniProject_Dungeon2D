
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UI_Title : UI_Scene
{
    #region Enums

    enum Buttons
    {
        BTN_GPGS_Login,
        BTN_EMAIL_Login,
        BTN_EMAIL_Regist,
        BTN_Play
    }

    enum GameObjects
    {
        ButtonGroup,
        LoginCompleteGroup
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
        BindObject(typeof(GameObjects));

        GetObject((int)GameObjects.LoginCompleteGroup).SetActive(false);

        GetButton((int)Buttons.BTN_GPGS_Login).gameObject.BindEvent(GooglePlayLogin);
        GetButton((int)Buttons.BTN_EMAIL_Login).gameObject.BindEvent(EmailLoginActivate);
        GetButton((int)Buttons.BTN_EMAIL_Regist).gameObject.BindEvent(EmailRegisterActivate);
        GetButton((int)Buttons.BTN_Play).gameObject.BindEvent(PlayMainStart);
    }
    #endregion



    #region Events
    public void GooglePlayLogin(PointerEventData eventData)
    {
        Managers.Auth.GpgsLogin();

        ActivateLoginComplete();
    }

    public void EmailLoginActivate(PointerEventData eventData)
    {
        var emailLoginPopup = Managers.UI.ShowPopupUI<UI_EmailLogin>(Literals.TITLE_Popup_Email_Login);

        emailLoginPopup.OnLoginComplete += ActivateLoginComplete;
    }

    public void EmailRegisterActivate(PointerEventData eventData)
    {
        Managers.UI.ShowPopupUI<UI_EmailRegister>(Literals.TITLE_Popup_Email_Register);
    }

    public void PlayMainStart(PointerEventData eventData)
    {
        SceneManager.LoadScene(Enums.Scene.Lobby.ToString());
    }
    #endregion


    private void ActivateLoginComplete()
    {
        if (Managers.Auth.User != null)
        {
            Managers.DB.LoadUser();
            GetObject((int)GameObjects.ButtonGroup).SetActive(false);
            GetObject((int)GameObjects.LoginCompleteGroup).SetActive(true);
        }
    }
}
