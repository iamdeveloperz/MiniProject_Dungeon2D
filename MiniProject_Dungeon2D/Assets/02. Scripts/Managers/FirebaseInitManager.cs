
using UnityEngine;
using Firebase;

public class FirebaseInitManager
{
    #region Member Variables

    // Firebase Variables
    private DependencyStatus _dependencyStatus;

    public bool IsInit { get; private set; }

    #endregion


    public void InitializeFirebase()
    {
        
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            IsInit = true;

            _dependencyStatus = task.Result;

            if (_dependencyStatus == DependencyStatus.Available)
            {
                Managers.Auth.Initialize();
                Managers.DB.Initialize();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase Dependecies : " + _dependencyStatus);
            }
        });
    }
}