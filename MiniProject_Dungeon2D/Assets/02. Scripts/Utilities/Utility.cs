
using Newtonsoft.Json;
using UnityEngine;

public static class Utility
{
    #region Generate UserName and Tag
    public static string GenerateUserNameAndTag(string userName)
    {
        int tagNumber = GetRandomNumber(1000, 9999);

        string userNameWithTag = $"{userName}#{tagNumber}";
        return userNameWithTag;
    }
    #endregion



    #region To Component
    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        if (recursive == false)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null)
                        return component;
                }
            }
        }
        else
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }

        return null;
    }
    #endregion



    #region To GameObject
    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);
        if (transform == null)
            return null;

        return transform.gameObject;
    }
    #endregion




    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();

        if (component == null)
            component = go.AddComponent<T>();

        return component;
    }



    public static int GetRandomNumber(int min, int max)
    {
        System.Random random = new System.Random();
        max += 1;

        return random.Next(min, max);
    }



    public static T JsonConvertDeserialize<T>(T dataObject)
    {
        string json = JsonConvert.SerializeObject(dataObject);

        T deseralizeData = JsonConvert.DeserializeObject<T>(json);

        return deseralizeData;
    }

    public static string JsonConvertSerialize<T>(T dataObject)
    {
        string json = JsonConvert.SerializeObject(dataObject);

        return json;
    }
}
