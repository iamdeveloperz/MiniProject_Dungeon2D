
public class Managers : SingletonBehaviour<Managers>
{
    #region Managers Variables

    /* Firebase */
    private readonly FirebaseInitManager _fbInitManager = new();
    private readonly AuthManager _authManager = new();
    private readonly DatabaseManager _dbManager = new();

    /* Games */
    private readonly UIManager _uiManager = new();
    private readonly ResourceManager _resourceManager = new();
    private readonly GameManager _gameManager = new();

    #endregion



    #region Properties : Firebase

    public static FirebaseInitManager FBInit => Instance?._fbInitManager;
    public static AuthManager Auth => Instance?._authManager;
    public static DatabaseManager DB => Instance?._dbManager;

    #endregion



    #region Properties : Games

    public static UIManager UI => Instance?._uiManager;
    public static ResourceManager Resource => Instance?._resourceManager;

    public static GameManager Game => Instance?._gameManager;

    #endregion
}
