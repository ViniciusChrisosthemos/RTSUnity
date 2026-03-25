using UnityEngine;

public class InputArg
{
    private object m_value;

    public InputArg(object value)
    {
        m_value = value;
    }

    public T ReadValue<T>() => (T)m_value;
}
