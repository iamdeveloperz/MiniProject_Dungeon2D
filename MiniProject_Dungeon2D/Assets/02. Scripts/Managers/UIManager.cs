
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager
{
    #region Member Variables

    // LayerOrder
    private int _order = 10;

    // Popups
    private Stack<UI_Popup> _popups = new Stack<UI_Popup>();

    // Scenes Overlay
    private UI_Scene _scene;

    #endregion



    #region Set Root UI
    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null)
                root = new GameObject { name = "@UI_Root" };

            return root;
        }
    }
    #endregion



    #region Scene
    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject gameObject = Managers.Resource.Instantiate(name);

        T sceneUI = Utility.GetOrAddComponent<T>(gameObject);

        gameObject.transform.SetParent(Root.transform);

        return sceneUI;
    }
    #endregion



    #region Popups
    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject gameObject = Managers.Resource.Instantiate(name);

        T popupUI = Utility.GetOrAddComponent<T>(gameObject);

        gameObject.transform.SetParent(Root.transform);

        return popupUI;
    }

    public void ClosePopupUI(UI_Popup popup)
    {
        if (_popups.Count == 0) return;

        if(_popups.Peek() != popup)
        {
            Debug.LogWarning("Close Popup failed");
            return;
        }

        ClosePopupUI();
    }

    public void ClosePopupUI()
    {
        if (_popups.Count == 0) return;

        UI_Popup popup = _popups.Pop();
        Managers.Destroy(popup.gameObject);
        popup = null;
        _order -= 1;
    }

    public void CloseAllPopupUI()
    {
        while (_popups.Count > 0)
            ClosePopupUI();
    }
    #endregion



    #region Setup Canvas
    public void SetCanvas(GameObject go, bool sorting = true)
    {
        Canvas canvas = Utility.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        CanvasScaler scaler = Utility.GetOrAddComponent<CanvasScaler>(go);
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;

        Vector2 resolution = new Vector2(Literals.ScreenX, Literals.ScreenY);
        scaler.referenceResolution = resolution;
        scaler.matchWidthOrHeight = Literals.MatchWidth;

        if(sorting)
        {
            canvas.sortingOrder = _order;
            _order += 1;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }
    #endregion
}