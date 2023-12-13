
using System;
using Firebase.Auth;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public abstract class FirebaseAuthentication
{
    protected string _infoMessage;

    // Firebase Email,Password
    protected IEnumerator HandleFirebaseAuth(Task<AuthResult> authTask)
    {
        yield return new WaitUntil(predicate: () => authTask.IsCompleted);

        if(authTask.IsCanceled || authTask.IsFaulted) 
        {
            Exception exception = authTask.Exception.GetBaseException();

            _infoMessage = AuthErrorMessage.GetErrorMessage(exception);

            Debug.LogWarning(message: $"Authentication failed: {_infoMessage}");
        }
        else
        {
            Managers.Auth.SetUser(authTask.Result.User);

            _infoMessage = "����� ������ �Ϸ�Ǿ����ϴ�. " + Managers.Auth.User.UserId;

            Debug.Log(message: $"User signed in successfully: {Managers.Auth.User.Email}");
        }

        LoginSystem.Instance.infoText.text = _infoMessage;
    }

    // Google Play Games
    protected IEnumerator HandleFirebaseAuth(Task<FirebaseUser> authTask)
    {
        yield return new WaitUntil(predicate: () => authTask.IsCompleted);

        if (authTask.IsCanceled || authTask.IsFaulted)
        {
            Exception exception = authTask.Exception.GetBaseException();

            _infoMessage = AuthErrorMessage.GetErrorMessage(exception);

            Debug.LogWarning(message: $"Authentication failed: {_infoMessage}");
        }
        else
        {
            Managers.Auth.SetUser(authTask.Result);

            _infoMessage = "���� ���� ������ ���� �߽��ϴ�. " + Managers.Auth.User.UserId;

            Debug.Log(message: $"User signed in successfully: {Managers.Auth.User.Email}");
        }

        LoginSystem.Instance.infoText.text = _infoMessage;
    }
}