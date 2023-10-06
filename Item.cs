using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable] public class Item
{
    public enum ItemType
    {
        IFMachine,
        Starter,
        Treadmill,
        Grabber,
    }

    public ItemType itemType;
    public int amount;


}

