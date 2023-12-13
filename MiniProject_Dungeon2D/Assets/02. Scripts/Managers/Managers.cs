
using UnityEngine;

public class Managers : SingletonBehaviour<Managers>
{
    #region Managers Variables

    private readonly AuthManager _authManager = new();

    #endregion



    #region Properties

    public static AuthManager Auth => Instance?._authManager;

    #endregion

    private void Awake()
    {
        _authManager.Initialize();
    }
}
