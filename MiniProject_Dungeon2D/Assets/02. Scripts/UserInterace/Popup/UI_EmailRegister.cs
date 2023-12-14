
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

        foreach(InputFields inputField in Enum.GetValues(typeof(InputFields)))
        {
            GetInputField((int)inputField).onValidateInput += ValidateInput;
        }
    }
    #endregion



    #region InputField Methods
    private char ValidateInput(string text, int charIndex, char addedChar)
    {
        // 영어 알파벳과 숫자, 일부 특수 문자만 허용
        if (addedChar >= 'a' && addedChar <= 'z' || addedChar >= 'A' && addedChar <= 'Z' ||
            addedChar >= '0' && addedChar <= '9' ||
            addedChar == '.' || addedChar == ',' || addedChar == '?' || addedChar == '!' || addedChar == '-' || addedChar == '@')
        {
            return addedChar;
        }
        else
        {
            // 허용되지 않는 문자는 무시
            return '\0';
        }
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

        float timeoutDuration = 10.0f;
        float elapsedTime = 0f;

        while (!Managers.Auth.IsRegister && elapsedTime < timeoutDuration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (Managers.Auth.IsRegister)
        {
            Managers.UI.ClosePopupUI(this);
        }
    }
    #endregion
}
