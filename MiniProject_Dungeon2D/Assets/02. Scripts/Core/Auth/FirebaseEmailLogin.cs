
using System.Collections;

public class FirebaseEmailLogin : FirebaseAuthentication
{
    public void Login(string email, string password)
    {
        CoroutineManager.StartManagedCoroutine(LoginAsync(email, password));
    }

    private IEnumerator LoginAsync(string email, string password)
    {
        var loginTask = Managers.Auth.FBAuth.SignInWithEmailAndPasswordAsync(email, password);

        yield return HandleFirebaseAuth(loginTask, false);
    }
}