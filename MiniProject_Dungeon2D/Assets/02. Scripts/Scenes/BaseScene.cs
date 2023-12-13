using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScene : MonoBehaviour
{
    #region Member Variables

    public UI_Scene SceneUI { get; protected set; }

    private bool _initialized = false;

    #endregion



    #region Init
    private void Awake()
    {
        if (!Managers.Resource.IsLoaded)
        {
            Managers.Resource.LoadAllPrefabs();
            //Managers.Resource.LoadAllPrefabs(Literals.PATH_UI);

            Initialize();
        }
        else
        {
            Initialize();
        }
    }

    protected virtual bool Initialize()
    {
        if (_initialized) return false;

        _initialized = true;
        return true;
    }
    #endregion
}
