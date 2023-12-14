
using UnityEngine;

public class Managers : SingletonBehaviour<Managers>
{
    #region Managers Variables

    private AuthManager _authManager = new();
    private readonly UIManager _uiManager = new();
    private readonly ResourceManager _resourceManager = new();

    #endregion



    #region Properties : No Initialize

    public static AuthManager Auth => Instance?._authManager;
    public static UIManager UI => Instance?._uiManager;
    public static ResourceManager Resource => Instance?._resourceManager;

    #endregion



}
