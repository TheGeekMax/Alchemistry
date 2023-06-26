using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Inventory/Recipe", order = 1)]
public class Recipe : ScriptableObject{
    public string item1;
    public string item2;
    public string result;
}
