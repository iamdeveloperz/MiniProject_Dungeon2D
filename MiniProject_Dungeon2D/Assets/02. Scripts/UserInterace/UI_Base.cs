using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UI_Base : MonoBehaviour
{
    public abstract void Init();



    #region Bind Member Variables

    protected Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    #endregion



    #region Bind Object Properties (Getter)
    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;

        if (_objects.TryGetValue(typeof(T), out objects) == false)
            return null;

        return objects[idx] as T;
    }
    protected GameObject GetObject(int idx) { return Get<GameObject>(idx); }
    protected TMP_Text GetText(int idx) { return Get<TMP_Text>(idx); }
    protected TMP_InputField GetInputField(int idx) { return Get<TMP_InputField>(idx); }
    protected Button GetButton(int idx) { return Get<Button>(idx); }
    protected Image GetImage(int idx) { return Get<Image>(idx); }
    #endregion



    #region Bind Object
    protected void BindObject(Type type) => Bind<GameObject>(type);
    protected void BindText(Type type) => Bind<TMP_Text>(type);
    protected void BindInputField(Type type) => Bind<TMP_InputField>(type);
    protected void BindButton(Type type) => Bind<Button>(type);
    protected void BindImage(Type type) => Bind<Image>(type);
    #endregion



    #region Bind

    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] objectNames = Enum.GetNames(type);
        UnityEngine.Object[] objects = new UnityEngine.Object[objectNames.Length];

        _objects.Add(typeof(T), objects);

        for(int idx = 0; idx < objects.Length; ++idx)
        {
            if (typeof(T) == typeof(GameObject))
                objects[idx] = Utility.FindChild(gameObject, objectNames[idx], true);
            else
                objects[idx] = Utility.FindChild<T>(gameObject, objectNames[idx], true);

            if (objects[idx] == null)
                Debug.LogError(message: $"Failed to binding ({objectNames[idx]})");
        }
    }

    #endregion



    #region Bind Events
    public static void BindEvent(GameObject go, Action<PointerEventData> action, DefineUIEvent.UIEvent type = DefineUIEvent.UIEvent.Click)
    {
        UI_EventHandler evtHandler = Utility.GetOrAddComponent<UI_EventHandler>(go);

        switch(type)
        {
            case DefineUIEvent.UIEvent.Click:
                evtHandler.OnClickHandler -= action;
                evtHandler.OnClickHandler += action;
                break;
            case DefineUIEvent.UIEvent.Drag:
                evtHandler.OnDragHandler -= action;
                evtHandler.OnDragHandler += action;
                break;
        }
    }
    #endregion
}