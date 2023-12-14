

using System;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;

public class UI_EmailLogin : UI_Popup
{
    #region Member Variables
    private string _userEmail;
    private string _userPassword;

    public event Action OnLoginComplete;
    #endregion



    #region Enums

    enum Buttons
    { 
        BTN_Close,
        BTN_Login
    }

    enum InputFields
    {
        IF_Email,
        IF_Password
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
        GetButton((int)Buttons.BTN_Login).gameObject.BindEvent(EmailLogin);
    }
    #endregion



    #region Events
    public void ClosePopup(PointerEventData eventData)
    {
        Managers.UI.ClosePopupUI(this);

        if (OnLoginComplete != null)
            OnLoginComplete = null;
    }

    public void EmailLogin(PointerEventData eventData)
    {
        _userEmail = GetInputField((int)InputFields.IF_Email).text;
        _userPassword = GetInputField((int)InputFields.IF_Password).text;

        CoroutineManager.StartManagedCoroutine(LoginCoroutine());
    }
    #endregion



    #region Coroutine
    private IEnumerator LoginCoroutine()
    {
        Managers.Auth.EmailLogin(_userEmail, _userPassword);

        float timeoutDuration = 3.0f;
        float elapsedTime = 0f;

        while (Managers.Auth.User == null && elapsedTime < timeoutDuration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (Managers.Auth.User != null)
        {
            OnLoginComplete?.Invoke();
            Managers.UI.ClosePopupUI(this);
        }
    }
    #endregion

}