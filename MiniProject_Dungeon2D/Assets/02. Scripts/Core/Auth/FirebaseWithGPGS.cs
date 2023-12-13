
using Firebase.Auth;
using GooglePlayGames.BasicApi;
using System.Collections;
using UnityEngine;

public class FirebaseWithGPGS : FirebaseAuthentication
{
    public void GPGSLogin()
    {
        Managers.Auth.GpgsAuth.Authenticate((status) =>
        {
            if (status == SignInStatus.Success)
            {
                Managers.Auth.GpgsAuth.RequestServerSideAccess(true, code =>
                {
                    CoroutineManager.StartManagedCoroutine(GPGSLoginAsync(code));
                });
            }
            else
            {
                _infoMessage = "���� ���� ������ ���� �߽��ϴ�.";

                LoginSystem.Instance.infoText.text = _infoMessage;

                Debug.LogError("GPGS Auth failed.");
            }
        });
    }

    private IEnumerator GPGSLoginAsync(string code)
    {
        Credential credential = PlayGamesAuthProvider.GetCredential(code);

        var gpgsTask = Managers.Auth.FBAuth.SignInWithCredentialAsync(credential);

        yield return HandleFirebaseAuth(gpgsTask);
    }
}