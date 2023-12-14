
using System.Collections;
using UnityEngine;

public static class InitOnLoad 
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void InitApplication()
    {
        Managers.Resource.LoadAlls();

        var prefabs = Managers.Resource.GetPrefabs(Literals.PATH_INIT);

        if (prefabs.Length > 0)
        {
            foreach (var prefab in prefabs)
            {
                GameObject go = Object.Instantiate(prefab);
                go.name = prefab.name;
                Object.DontDestroyOnLoad(go);
            }
        }
    }
}