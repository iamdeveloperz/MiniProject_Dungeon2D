
using UnityEngine;

public class Managers : SingletonBehaviour<Managers>
{
    #region Managers Variables

    private AuthManager _authManager;

    // No intialized
    private readonly UIManager _uiManager = new();
    private readonly ResourceManager _resourceManager = new();

    #endregion



    #region Properties : No Initialize

    public static UIManager UI => Instance?._uiManager;
    public static ResourceManager Resource => Instance?._resourceManager;


    #endregion



    #region Properties : Initialized
    public static AuthManager Auth
    {
        get
        {
            if (Instance._authManager == null)
            {
                Instance._authManager = new AuthManager();
                Instance._authManager.Initialize();
            }

            return Instance._authManager;
        }
    }
    #endregion
}
