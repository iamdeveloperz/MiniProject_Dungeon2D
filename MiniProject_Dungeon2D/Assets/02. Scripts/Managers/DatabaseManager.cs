
using UnityEngine;
using Firebase.Database;
using System.Collections;
using Firebase.Auth;

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



    #region Public Methods
    public void UpdateUserName(string userName, FirebaseUser newUser = null)
    {
        CoroutineManager.StartManagedCoroutine(UpdateUserNameDatabase(userName, newUser));
    }

    public void UpdatePlayerData(PlayerData playerData)
    {
        CoroutineManager.StartManagedCoroutine(UpdatePlayerDataFromDB(playerData));
    }

    public void UpdateAccountData(AccountData accountData)
    {
        CoroutineManager.StartManagedCoroutine(UpdateAccountDataFromDB(accountData));
    }
    #endregion



    #region Coroutines
    private IEnumerator UpdateUserNameDatabase(string userName, FirebaseUser newUser)
    {
        if (Managers.Auth.User == null && newUser == null) yield break;

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

    private IEnumerator UpdatePlayerDataFromDB(PlayerData playerData)
    {
        if(Managers.Auth.User == null)
        {
            Debug.LogError("User is not sign in");
            yield break;
        }

        UniqueUserUpdate();

        var DBTask = _uniqueUser.Child(Literals.DB_PLAYERDATA).SetValueAsync(JsonUtility.ToJson(playerData));

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to DB Task with {DBTask.Exception}");
        }
        else
        {
            Debug.Log($"Update Complete : {playerData}");
        }
    }

    private IEnumerator UpdateAccountDataFromDB(AccountData accountData)
    {
        if (Managers.Auth.User == null)
        {
            Debug.LogError("User is not sign in");
            yield break;
        }

        UniqueUserUpdate();

        var DBTask = _uniqueUser.Child(Literals.DB_PLAYERDATA).SetValueAsync(JsonUtility.ToJson(accountData));

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to DB Task with {DBTask.Exception}");
        }
        else
        {
            Debug.Log($"Update Complete : {accountData}");
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
