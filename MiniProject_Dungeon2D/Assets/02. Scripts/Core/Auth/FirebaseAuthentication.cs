
using System;
using Firebase.Auth;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public abstract class FirebaseAuthentication
{
    // Firebase Email,Password
    protected IEnumerator HandleFirebaseAuth(Task<AuthResult> authTask, bool isRegister)
    {
        yield return new WaitUntil(predicate: () => authTask.IsCompleted);

        if(authTask.IsCanceled || authTask.IsFaulted) 
        {
            Exception exception = authTask.Exception.GetBaseException();

            Managers.Auth.InfoMessage = AuthErrorMessage.GetErrorMessage(exception);
            if (isRegister)
                Managers.Auth.IsRegister = false;
            else
                Managers.Auth.IsLoggedIn = false;

            Debug.LogWarning(message: $"Authentication failed: {Managers.Auth.InfoMessage}");
        }
        else
        {
            if (!isRegister)
            {
                Managers.Auth.SetUser(authTask.Result.User);
                Managers.Auth.IsLoggedIn = true;
                Managers.Auth.InfoMessage = "����� �α��� ����";

                Debug.Log(message: $"User signed in successfully: {Managers.Auth.User.Email}");
            }
            else
            {
                Managers.Auth.InfoMessage = "����� ���� ���� ����";
                Managers.Auth.IsRegister = true;

                Debug.Log(message: $"User register in successfully: {authTask.Result.User.Email}");
            }
        }
    }

    // Google Play Games
    protected IEnumerator HandleFirebaseAuth(Task<FirebaseUser> authTask)
    {
        yield return new WaitUntil(predicate: () => authTask.IsCompleted);

        if (authTask.IsCanceled || authTask.IsFaulted)
        {
            Exception exception = authTask.Exception.GetBaseException();

            Managers.Auth.InfoMessage = AuthErrorMessage.GetErrorMessage(exception);

            Debug.LogWarning(message: $"Authentication failed: {Managers.Auth.InfoMessage}");
        }
        else
        {
            Managers.Auth.SetUser(authTask.Result);

            Managers.Auth.InfoMessage = "���� ���� ������ ���� �߽��ϴ�";

            Managers.DB.UpdateUserName(authTask.Result.DisplayName, authTask.Result);

            Debug.Log(message: $"User signed in successfully: {Managers.Auth.User.Email}");
        }
    }
}