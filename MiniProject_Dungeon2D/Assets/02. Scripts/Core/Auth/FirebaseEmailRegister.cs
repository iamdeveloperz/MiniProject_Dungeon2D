
using System.Collections;
using UnityEngine;
using Firebase.Auth;
using Firebase;

public class FirebaseEmailRegister : FirebaseAuthentication
{
    public void Register(string email, string userName, string pw, string pwConfirm)
    {
        CoroutineManager.StartManagedCoroutine(RegisterAsync(email, userName, pw, pwConfirm));
    }

    private IEnumerator RegisterAsync(string email, string userName, string pw, string pwConfirm)
    {
        if(string.IsNullOrEmpty(email) || string.IsNullOrEmpty(userName) ||
            string.IsNullOrEmpty(pw) || string.IsNullOrEmpty(pwConfirm))
        {
            _infoMessage = "비어 있는 입력이 존재합니다.";
            LoginSystem.Instance.infoText.text = _infoMessage;
            yield break;
        }
        else if(!string.Equals(pw, pwConfirm))
        {
            _infoMessage = "비밀번호가 같지 않습니다.";
            LoginSystem.Instance.infoText.text = _infoMessage;
            yield break;
        }

        var registerTask = Managers.Auth.FBAuth.CreateUserWithEmailAndPasswordAsync(email, pw);

        yield return HandleFirebaseAuth(registerTask);

        if(!registerTask.IsCanceled && !registerTask.IsFaulted)
        {
            CoroutineManager.StartManagedCoroutine(RegisterUserNickname(userName));
        }
    }



    #region UserName Coroutine
    private IEnumerator RegisterUserNickname(string userName)
    {
        var user = Managers.Auth.User;

        if (user != null)
        {
            UserProfile profile = new UserProfile { DisplayName = userName };

            var profileTask = user.UpdateUserProfileAsync(profile);

            yield return new WaitUntil(predicate: () => profileTask.IsCompleted);

            if (profileTask.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {profileTask.Exception}");

                FirebaseException firebaseException = profileTask.Exception.GetBaseException() as FirebaseException;

                AuthError authError = (AuthError)firebaseException.ErrorCode;

                // Text Process
                _infoMessage = "사용자 닉네임 설정에 실패했습니다.";
                LoginSystem.Instance.infoText.text = _infoMessage;
            }
            else
            {
                // User name setting complete
                // return to screen or scene
                LoginSystem.Instance.infoText.text = "닉네임 설정까지 완료";
            }
        }
    }
    #endregion
}