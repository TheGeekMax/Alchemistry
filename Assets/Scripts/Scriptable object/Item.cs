using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject{
    public string itemName;
    public Sprite itemSprite;
    public int cap = 5;

    //fonction virtuelle pour Itemtype
    public virtual string GetItemType(){
        return "Item";
    }
}
