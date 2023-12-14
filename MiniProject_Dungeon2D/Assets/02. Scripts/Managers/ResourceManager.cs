
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    #region Resources Mebmer

    // 1. string : 폴더에 대한 경로
    // 2. key : 키 값
    private Dictionary<string, Dictionary<string, GameObject>> _resourcesByFolder =
        new Dictionary<string, Dictionary<string, GameObject>>();

    private bool _isLoaded = false;

    public bool IsLoaded { get { return _isLoaded; } }

    #endregion



    #region Load Resources
    public void LoadAlls()
    {
        LoadAllPrefabs(Literals.PATH_INIT);
        LoadAllPrefabs(Literals.PATH_UI);

        _isLoaded = true;
    }

    public void LoadAllPrefabs(string folderPath = null)
    {
        GameObject[] prefabs = Resources.LoadAll<GameObject>(folderPath ?? "Prefabs");
        string folderKey = folderPath ?? "Prefabs";

        if (!_resourcesByFolder.ContainsKey(folderKey))
        {
            _resourcesByFolder[folderKey] = new Dictionary<string, GameObject>();
        }

        foreach (GameObject prefab in prefabs)
        {
            if (!_resourcesByFolder[folderKey].ContainsKey(prefab.name))
            {
                _resourcesByFolder[folderKey].Add(prefab.name, prefab);
            }
            else
            {
                Debug.LogWarning($"Prefab with name {prefab.name} already exists in the dictionary for folder {folderKey}.");
            }
        }
    }
    #endregion



    #region Get Resources
    public GameObject GetPrefab(string prefabName, string folderPath = "Prefabs")
    {
        if (_resourcesByFolder.TryGetValue(folderPath, out var folderDict) && 
            folderDict.TryGetValue(prefabName, out GameObject prefab))
        {
            return prefab;
        }
        else
        {
            Debug.LogWarning($"Prefab with name {prefabName} not found in the folder {folderPath}.");
            return null;
        }
    }

    public GameObject[] GetPrefabs(string folderPath)
    {
        if (!string.IsNullOrEmpty(folderPath) && _resourcesByFolder.TryGetValue(folderPath, out var folderDict))
        {
            return new List<GameObject>(folderDict.Values).ToArray();
        }
        else
        {
            Debug.LogWarning($"Folder path is invalid or not loaded: {folderPath}");
            return new GameObject[0];
        }
    }
    #endregion

    public GameObject Instantiate(string prefabName, string folderPath = "Prefabs", Transform parent = null)
    {
        GameObject prefab = GetPrefab(prefabName, folderPath);

        if (prefab == null)
        {
            Debug.LogWarning($"Failed to load prefab: {prefabName}");
            return null;
        }

        return Object.Instantiate(prefab, parent);
    }

    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        Object.Destroy(go);
    }
}
