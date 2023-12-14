
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
                Managers.Auth.InfoMessage = "사용자 로그인 성공";

                Debug.Log(message: $"User signed in successfully: {Managers.Auth.User.Email}");
            }
            else
            {
                Managers.Auth.InfoMessage = "사용자 계정 생성 성공";
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

            Managers.Auth.InfoMessage = "구글 계정 연동에 성공 했습니다";

            Managers.DB.UpdateUserName(authTask.Result.DisplayName, authTask.Result);

            Debug.Log(message: $"User signed in successfully: {Managers.Auth.User.Email}");
        }
    }
}