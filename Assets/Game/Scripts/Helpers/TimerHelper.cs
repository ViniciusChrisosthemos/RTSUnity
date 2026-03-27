using System;
using System.Collections;
using UnityEngine;

public static class TimerHelper
{
    public static IEnumerator TimerCoroutine(float interval, Action callback)
    {
        var accumTime = 0f;

        while (true)
        {
            accumTime += Time.deltaTime;

            if (accumTime >= interval)
            {
                callback?.Invoke();

                accumTime = 0f;
            }

            yield return null;
        }
    }
}
