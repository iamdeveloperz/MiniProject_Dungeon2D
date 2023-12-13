
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    #region Resources Mebmer

    private Dictionary<string, GameObject> _resources = new Dictionary<string, GameObject>();

    private bool _isLoaded = false;

    public bool IsLoaded { get { return _isLoaded; } }

    #endregion

    public void LoadAllPrefabs(string foldePath = null)
    {
        GameObject[] prefabs = Resources.LoadAll<GameObject>(foldePath ?? "Prefabs");

        foreach (GameObject prefab in prefabs)
        {
            if (!_resources.ContainsKey(prefab.name))
            {
                _resources.Add(prefab.name, prefab);
            }
            else
            {
                Debug.LogWarning($"Prefab with name {prefab.name} already exists in the dictionary.");
            }
        }

        _isLoaded = true;
    }

    public GameObject GetPrefab(string prefabName)
    {
        if (_resources.TryGetValue(prefabName, out GameObject prefab))
        {
            return prefab;
        }
        else
        {
            Debug.LogWarning($"Prefab with name {prefabName} not found in the dictionary.");
            return null;
        }
    }

    public GameObject Instantiate(string prefabName, Transform parent = null)
    {
        GameObject prefab = GetPrefab(prefabName);

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
