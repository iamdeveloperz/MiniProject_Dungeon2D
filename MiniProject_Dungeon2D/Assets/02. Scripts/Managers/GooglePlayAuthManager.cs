using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using Firebase.Auth;
using Google;
using System.Data.SqlTypes;

public class GooglePlayAuthManager : MonoBehaviour
{
    public GameObject square1;
    public GameObject square2;
    public GameObject square3;

    private void Start()
    {
        square1.SetActive(true);

        PlayGamesPlatform.Instance.Authenticate((status) =>
        {
            if (status == SignInStatus.Success)
            {
                square2.SetActive(true);
                PlayGamesPlatform.Instance.RequestServerSideAccess(true, code =>
                {
                    Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;

                    Credential credential = PlayGamesAuthProvider.GetCredential(code);

                    //auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
                    //{
                    //    if(task.is)
                    //})

                    StartCoroutine(AuthGet());

                    IEnumerator AuthGet()
                    {
                        System.Threading.Tasks.Task<FirebaseUser> task = auth.SignInWithCredentialAsync(credential);

                        while (!task.IsCompleted) yield return null;

                        if (task.IsCanceled)
                            Debug.Log("Cancled");
                        else if (task.IsFaulted)
                            Debug.Log("isFaulted");
                        else
                        {
                            FirebaseUser user = task.Result;
                            Debug.Log("Fully Authenticated user");
                            square3.SetActive(true);
                        }
                    }
                });
            }
            else
            {
                Debug.Log("PGP Auth failed");
            }
        });
    }
}