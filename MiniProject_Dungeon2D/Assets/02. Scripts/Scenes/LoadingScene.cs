
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingScene : BaseScene
{
    private static string _nextScene;
    private Slider _progressBar;

    protected override bool Initialize()
    {
        if (!base.Initialize()) return false;

        _progressBar = FindObjectOfType<Slider>();

        if (string.IsNullOrEmpty(_nextScene))
            _nextScene = Enums.Scene.Title.ToString();

        StartCoroutine(LoadScene());

        return true;
    }



    #region Load Scene
    public static void LoadScene(string sceneName)
    {
        _nextScene = sceneName;

        SceneManager.LoadScene(Enums.Scene.Loading.ToString());
    }
    #endregion



    private IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation op = SceneManager.LoadSceneAsync(_nextScene);
        op.allowSceneActivation = false;

        while (!op.isDone)
        {
            // 로딩 진행 상태에 따라 슬라이더 값을 업데이트합니다.
            if (op.progress < 0.9f)
            {
                _progressBar.value = op.progress;
            }
            else
            {
                // 로딩이 거의 완료되었으면 슬라이더를 마무리까지 올립니다.
                _progressBar.value = Mathf.MoveTowards(_progressBar.value, 1f, Time.deltaTime);
                if (_progressBar.value >= 1f)
                {
                    InitFirebase();
                    op.allowSceneActivation = true; // 로딩 완료 후 씬 활성화
                }
            }
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void InitFirebase()
    {
        if (Managers.Auth != null)
            Managers.Auth.Initialize();

        if (Managers.DB != null)
            Managers.DB.Initialize();
    }
}
