
using System;
using Firebase.Auth;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public abstract class FirebaseAuthentication
{
    protected string _infoMessage;

    // Firebase Email,Password
    protected IEnumerator HandleFirebaseAuth(Task<AuthResult> authTask, bool isRegister)
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
            if (!isRegister)
            {
                Managers.Auth.SetUser(authTask.Result.User);

                _infoMessage = "사용자 로그인 성공";

                Debug.Log(message: $"User signed in successfully: {Managers.Auth.User.Email}");
            }
            else
            {
                _infoMessage = "사용자 계정 생성 성공";

                Debug.Log(message: $"User register in successfully: {Managers.Auth}");
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

            _infoMessage = AuthErrorMessage.GetErrorMessage(exception);

            Debug.LogWarning(message: $"Authentication failed: {_infoMessage}");
        }
        else
        {
            Managers.Auth.SetUser(authTask.Result);

            _infoMessage = "구글 계정 연동에 성공 했습니다";

            Debug.Log(message: $"User signed in successfully: {Managers.Auth.User.Email}");
        }
    }
}