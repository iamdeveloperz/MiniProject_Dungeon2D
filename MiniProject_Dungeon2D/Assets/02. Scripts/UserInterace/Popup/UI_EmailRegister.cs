
using System;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;

public class UI_EmailRegister : UI_Popup
{
    #region Member Variables
    private string _userEmail;
    private string _userName;
    private string _userPassword;
    private string _userPasswordConfirm;
    #endregion



    #region Enums

    enum Buttons
    {
        BTN_Close,
        BTN_Register
    }

    enum InputFields
    {
        IF_Email,
        IF_UserName,
        IF_Password,
        IF_PasswordConfirm
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

        GetButton((int)Buttons.BTN_Close).gameObject.BindEvent(ClosePopup);
        GetButton((int)Buttons.BTN_Register).gameObject.BindEvent(EmailRegister);
    }
    #endregion



    #region Events
    public void ClosePopup(PointerEventData eventData)
    {
        Managers.UI.ClosePopupUI(this);
    }

    public void EmailRegister(PointerEventData eventData)
    {
        _userEmail = GetInputField((int)InputFields.IF_Email).text;
        _userName = GetInputField((int)InputFields.IF_UserName).text;
        _userPassword = GetInputField((int)InputFields.IF_Password).text;
        _userPasswordConfirm = GetInputField((int)InputFields.IF_PasswordConfirm).text;

        CoroutineManager.StartManagedCoroutine(RegisterCoroutine());
    }
    #endregion



    #region Coroutine
    private IEnumerator RegisterCoroutine()
    {
        Managers.Auth.EmailRegister(_userEmail, _userName, _userPassword, _userPasswordConfirm);

        float timeoutDuration = 3.0f;
        float elapsedTime = 0f;

        while (Managers.Auth.User == null && elapsedTime < timeoutDuration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (Managers.Auth.User != null)
        {
            Managers.UI.ClosePopupUI(this);
        }
    }
    #endregion
}
