using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartItem : MonoBehaviour
{
    public Inventory inventory;
    public Item[] items;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SetStartItem", 0.5f);
    }

    public void SetStartItem()
    {
        for (int i = 0; i < 6; i++)
            inventory.AddItem(items[i]);
    }
}
