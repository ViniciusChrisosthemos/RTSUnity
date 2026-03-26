using System;
using UnityEngine;

public class Timer
{
    private bool m_isRunning;
    private float m_interval;
    private Action m_callback;

    public Timer(float interval, Action callback)
    {
        m_interval = interval;
        m_callback = callback;

        m_isRunning = false;
    }

    public void Start()
    {
        m_isRunning = true;

        Run();
    }

    private async void Run()
    {
        var accumTime = 0f;

        m_isRunning = true;

        while (m_isRunning)
        {
            accumTime += Time.deltaTime;

            if (accumTime >= m_interval)
            {
                m_callback?.Invoke();

                accumTime = 0f;
            }
        }
    }

    public void Stop()
    {
        m_isRunning = false;
    }
}
