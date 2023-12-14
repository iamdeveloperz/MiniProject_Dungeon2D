
using UnityEngine;

public class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static bool _isDisabled = false;
    private static object _locked = new object();
    private static T _instance;

    #region Properties (GETTER)
    public static T Instance
    {
        get
        {
            if (_isDisabled)
            {
                Debug.LogWarning("[Singleton] Instance '" + typeof(T) + "' already destroyed. Returning Null Instance");
                return null;
            }

            lock (_locked)
            {
                if (_instance == null)
                {
                    _instance = (T)FindFirstObjectByType(typeof(T));

                    if (_instance == null)
                    {
                        GameObject singleton = new GameObject();

                        _instance = singleton.AddComponent<T>();

                        singleton.name = "(singleton) " + typeof(T).ToString();

                        DontDestroyOnLoad(singleton);

                        Debug.Log("[Singleton] An instance of '" + typeof(T) + "' is needed in the scene, so Create with DontDestroyOnLoad.");
                    }
                    else
                    {
                        DontDestroyOnLoad(_instance);
                        Debug.Log("[Singleton] Using isntance already created. " + _instance.gameObject.name);
                    }
                }

                return _instance;
            }
        }
    }
    #endregion

    protected void OnDestroy()
    {
        if (_instance == this)
        {
            _isDisabled = true;
        }
    }
}