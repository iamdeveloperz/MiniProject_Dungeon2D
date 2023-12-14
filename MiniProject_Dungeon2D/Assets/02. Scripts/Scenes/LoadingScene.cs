
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
            // �ε� ���� ���¿� ���� �����̴� ���� ������Ʈ�մϴ�.
            if (op.progress < 0.9f)
            {
                _progressBar.value = op.progress;
            }
            else
            {
                // �ε��� ���� �Ϸ�Ǿ����� �����̴��� ���������� �ø��ϴ�.
                _progressBar.value = Mathf.MoveTowards(_progressBar.value, 1f, Time.deltaTime);
                if (_progressBar.value >= 1f)
                {
                    InitFirebase();
                    op.allowSceneActivation = true; // �ε� �Ϸ� �� �� Ȱ��ȭ
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
