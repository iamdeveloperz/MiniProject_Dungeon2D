
using UnityEngine;
using Firebase;
using Firebase.Auth;
using GooglePlayGames;

public class AuthManager
{
    #region Member Variables


    private FirebaseUser _user;

    // Auths
    private FirebaseAuth _fbAuth;
    private PlayGamesPlatform _gpgsAuth;

    // Auth System
    private FirebaseEmailLogin _emailLogin;
    private FirebaseEmailRegister _emailRegister;
    private FirebaseWithGPGS _gpgsLogin;

    #endregion



    #region Properties

    public bool IsInit { get; private set; } = false;
    public bool IsLoggedIn { get; set; } = false;
    public bool IsRegister { get; set; } = false;
    public string InfoMessage { get; set; }


    public FirebaseAuth FBAuth => _fbAuth;
    public PlayGamesPlatform GpgsAuth => _gpgsAuth;

    public FirebaseUser User => _user;

    #endregion



    #region Setter
    public void SetUser(FirebaseUser user)
    {
        if (user == null) return;

        _user = user;
    }
    #endregion



    #region Initialize

    public void Initialize()
    {
        // FB
        InitializeFirebaseAuth();
        // GPGS
        InitializeGooglePlayGameS();
        // Dependencies Scripts
        InitDependenciesLoginSystem();

        IsInit = true;
    }

    private void InitializeGooglePlayGameS()
    {
        _gpgsAuth = PlayGamesPlatform.Instance;
    }

    private void InitializeFirebaseAuth()
    {
        Debug.Log("Set up the Firebase Auth");

        _fbAuth = FirebaseAuth.DefaultInstance;
    }

    private void InitDependenciesLoginSystem()
    {
        _emailLogin = new FirebaseEmailLogin();
        _emailRegister = new FirebaseEmailRegister();
        _gpgsLogin = new FirebaseWithGPGS();
    }

    #endregion



    #region Main Methods

    public void EmailLogin(string email, string password)
    {
        _emailLogin?.Login(email, password);
    }

    public void EmailRegister(string email, string userName, string password, string passwordConfirm)
    {
        _emailRegister?.Register(email, userName, password, passwordConfirm);
    }

    public void GpgsLogin()
    {
        _gpgsLogin?.GPGSLogin();
    }

    #endregion



    #region Util Methods
    public void Logout()
    {
        if(_fbAuth != null && _user != null)
        {
            _fbAuth.SignOut();
            _user = null;
        }
    }
    #endregion

}