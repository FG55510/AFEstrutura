using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldItem : MonoBehaviour
{
    List<Item> handItem = new List<Item>();
    

    
    private void OnTriggerEnter(Collider other)
    {
        I_Collectable collectableItem;

        if (other.TryGetComponent<I_Collectable>(out collectableItem))
        {
            
            collectableItem.Get();

            
            handItem.Add(other.GetComponent<Item>());
            
        }
    }
    void GetItem(Item item)
    {
        handItem.Add(item);
    }
}
