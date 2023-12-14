
using UnityEngine;
using Firebase.Database;
using System.Collections;
using Firebase.Auth;
using Newtonsoft.Json;

public class DatabaseManager
{
    #region Member Variables

    private DatabaseReference _DBReference;
    private DatabaseReference _uniqueUser = null;

    #endregion



    #region Properties
    public bool IsInit { get; private set; } = false;
    #endregion



    #region Intialize
    public void Initialize()
    {
        _DBReference = FirebaseDatabase.DefaultInstance.RootReference;

        IsInit = true;

        // Auto Login User Check
        UniqueUserUpdate();
    }
    #endregion



    #region Public Save Methods
    public void UpdateUserName(string userName, FirebaseUser newUser = null)
    {
        CoroutineManager.StartManagedCoroutine(UpdateUserNameDatabase(userName, newUser));
    }

    public void UpdatePlayer<T>(string path, T dataObject, FirebaseUser newUser = null)
    {
        CoroutineManager.StartManagedCoroutine(UpdatePlayerDatabase(path, dataObject, newUser));
    }
    #endregion



    #region Coroutines Save(Write)
    private IEnumerator UpdateUserNameDatabase(string userName, FirebaseUser newUser)
    {
        if (Managers.Auth.User == null && newUser == null)
        {
            Debug.LogError("User is not sign in");
            yield break;
        }

        UniqueUserUpdate(newUser);

        var userNameWithTag = Utility.GenerateUserNameAndTag(userName);

        var DBTask = _uniqueUser.Child(Literals.DB_USERNAME).SetValueAsync(userNameWithTag);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if(DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to DB Task with {DBTask.Exception}");
        }
        else
        {
            Debug.Log($"Update Complete : {userNameWithTag}");
        }
    }

    private IEnumerator UpdatePlayerDatabase<T>(string path, T dataObject, FirebaseUser newUser)
    {
        if (Managers.Auth.User == null && newUser == null)
        {
            Debug.LogError("User is not sign in");
            yield break;
        }

        UniqueUserUpdate(newUser);

        /* TASK */
        var serializeObject = Utility.JsonConvertSerialize(dataObject);
        var DBTask = _uniqueUser.Child(path).SetRawJsonValueAsync(serializeObject);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to DB Task with {DBTask.Exception}");
        }
        else
        {
            Debug.Log($"Update Complete Data");
        }
    }
    #endregion



    #region Public Load Methods
    public void LoadUser()
    {
        CoroutineManager.StartManagedCoroutine(LoadUserData());
    }
    #endregion



    #region Coroutines Load(Read)
    private IEnumerator LoadUserData()
    {
        if (Managers.Auth.User == null)
        {
            Debug.LogError("User Invalid");
            yield break;
        }

        UniqueUserUpdate();

        /* TASK */
        var DBTask = _uniqueUser.GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to DB Task with {DBTask.Exception}");
        }
        else if(DBTask.Result.Value == null)
        {
            Debug.LogError($"Can't find User Data");
        }
        else
        {
            DataSnapshot snapshot = DBTask.Result;

            if (snapshot.HasChild("username"))
            {
                string username = snapshot.Child("username").Value.ToString();

                Managers.Game.userName = username;
                Debug.Log($"Username: {username}");
            }

            if (snapshot.HasChild("playerdata"))
            {
                string playerDataJson = snapshot.Child("playerdata").GetRawJsonValue();
                // playerDataJson을 원하는 클래스로 역직렬화
                PlayerData playerData = JsonConvert.DeserializeObject<PlayerData>(playerDataJson);
                if (playerData != null)
                {
                    Managers.Game.playerData = playerData;
                    Debug.Log($"Player Data: {playerData}");
                }
            }

            if (snapshot.HasChild("account"))
            {
                string accountJson = snapshot.Child("account").GetRawJsonValue();
                // accountJson을 원하는 클래스로 역직렬화
                AccountData account = JsonConvert.DeserializeObject<AccountData>(accountJson);
                if (account != null)
                {
                    Managers.Game.accountData = account;
                    Debug.Log($"Account Data: {account}");
                }
            }
        }
    }
    #endregion



    #region Utils
    private void UniqueUserUpdate(FirebaseUser newUser = null)
    {
        if (_uniqueUser != null) return;

        if(newUser == null)
        {
            if (Managers.Auth.User == null) return;
            else
                _uniqueUser = _DBReference.Child(Literals.DB_ROOT).Child(Managers.Auth.User.UserId);
        }
        else
        {
            _uniqueUser = _DBReference.Child(Literals.DB_ROOT).Child(newUser.UserId);
        }
    }
    #endregion
}
