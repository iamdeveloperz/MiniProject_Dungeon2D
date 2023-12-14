
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
            Managers.Auth.InfoMessage = "비어 있는 입력이 존재합니다.";
            yield break;
        }
        else if(!string.Equals(pw, pwConfirm))
        {
            Managers.Auth.InfoMessage = "비밀번호가 같지 않습니다.";
            yield break;
        }
        else if(userName.Length < 2 || userName.Length > 10)
        {
            Managers.Auth.InfoMessage = "닉네임 길이를 준수해주세요.";
            yield break;
        }

        var registerTask = Managers.Auth.FBAuth.CreateUserWithEmailAndPasswordAsync(email, pw);

        yield return HandleFirebaseAuth(registerTask, true);

        if(!registerTask.IsCanceled && !registerTask.IsFaulted)
        {
            CoroutineManager.StartManagedCoroutine(RegisterUserNickname(userName, registerTask.Result.User));
        }
    }



    #region UserName Methods
    private IEnumerator RegisterUserNickname(string userName, FirebaseUser newUser = null)
    {
        var user = newUser;

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
                Managers.Auth.InfoMessage = "사용자 닉네임 설정에 실패했습니다.";
            }
            else
            {
                Managers.DB.UpdateUserName(user.DisplayName, user);

                NewPlayerSetupAndDB(user);
            }
        }
    }
    #endregion



    private void NewPlayerSetupAndDB(FirebaseUser newUser)
    {
        var user = newUser;

        if (user != null)
        {
            PlayerData newPlayerData = new PlayerData();
            AccountData newAccountData = new AccountData();

            Managers.DB.UpdatePlayerData(newPlayerData);
            Managers.DB.UpdateAccountData(newAccountData);
        }
    }
}