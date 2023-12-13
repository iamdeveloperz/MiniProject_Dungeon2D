
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LoginSystem : MonoBehaviour
{
    [Header("Register")]
    public InputField registerEmail;
    public InputField registerUserName;
    public InputField registerPassword;
    public InputField registerPasswordConfirm;

    [Header("Login")]
    public InputField loginEmail;
    public InputField loginPassword;

    public Button loginDone;
    public Button registerDone;
    public Button gpgsDone;
    public Button logout;

    public Text infoText;

    public static LoginSystem Instance { get; private set; } 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        loginDone.onClick.AddListener(LoginButton);
        registerDone.onClick.AddListener(RegisterButton);
        gpgsDone.onClick.AddListener(GpgsLogin);
        logout.onClick.AddListener(Logout);
    }

    private void LoginButton()
    {
        string email = loginEmail.text;
        string pw = loginPassword.text;

        Managers.Auth.EmailLogin(email, pw);
    }

    private void RegisterButton()
    {
        string email = registerEmail.text;
        string pw = registerPassword.text;
        string pwConfirm = registerPasswordConfirm.text;
        string username = registerUserName.text;

        Managers.Auth.EmailRegister(email, username, pw, pwConfirm);
    }

    private void GpgsLogin()
    {
        Managers.Auth.GpgsLogin();
    }

    private void Logout()
    {
        Managers.Auth.Logout();
    }
}
