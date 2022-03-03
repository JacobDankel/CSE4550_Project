using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        key,
    }

    public ItemType item;
    public int itemCount;
}
