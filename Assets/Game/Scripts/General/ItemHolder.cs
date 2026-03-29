using UnityEngine;

public class ItemHolder<T>
{
    public T Item { get; private set; }
    public int Amount { get; private set; }

    public ItemHolder(T item, int amount)
    {
        Item = item;
        Amount = amount;
    }
}
