using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineManager : SingletonBehaviour<CoroutineManager>
{
    #region Coroutines

    private Dictionary<IEnumerator, Coroutine> _coroutines = 
        new Dictionary<IEnumerator, Coroutine>();

    #endregion



    #region Managed Coroutine
    public static Coroutine StartManagedCoroutine(IEnumerator coroutine, Action onComplete = null)
    {
        Coroutine runningCoroutine = Instance.StartCoroutine(Instance.CoroutineWrapper(coroutine, onComplete));

        Instance._coroutines.Add(coroutine, runningCoroutine);

        return runningCoroutine;
    }

    public static void StopManagedCoroutine(IEnumerator coroutine)
    {
        if (Instance._coroutines.TryGetValue(coroutine, out Coroutine runningCoroutine))
        {
            Instance.StopCoroutine(runningCoroutine);

            Instance._coroutines.Remove(coroutine);
        }
    }
    #endregion



    #region Get Running Coroutine
    public static List<IEnumerator> GetRunningCoroutines()
    {
        return new List<IEnumerator>(Instance._coroutines.Keys);
    }
    #endregion



    #region Utils
    private IEnumerator CoroutineWrapper(IEnumerator coroutine, Action onComplete)
    {
        yield return StartCoroutine(coroutine);

        onComplete?.Invoke();

        _coroutines.Remove(coroutine);
    }
    #endregion
}