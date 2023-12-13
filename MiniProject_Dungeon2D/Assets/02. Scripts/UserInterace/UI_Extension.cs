
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public static class UI_Extension
{
    public static T GetOrAddComponent<T>(this GameObject go) where T : UnityEngine.Component
    {
        return Utility.GetOrAddComponent<T>(go);
    }

    public static void BindEvent(this GameObject go, Action<PointerEventData> action, DefineUIEvent.UIEvent type = DefineUIEvent.UIEvent.Click)
    {
        UI_Base.BindEvent(go, action, type);
    }
}
