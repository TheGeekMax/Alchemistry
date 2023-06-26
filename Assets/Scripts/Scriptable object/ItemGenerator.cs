using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Generator", menuName = "Inventory/Generator")]
public class ItemGenerator:Item{
    //on ajoute tableau de ce que le generateur peut generer
    public GeneratorComponent[] generatorComponents;
    //slider pour la vitesse de generation
    [Range(1,10)]
    public float generationSpeed;

    //on override GetItemType
    public override string GetItemType(){
        return "Generator";
    }

    private int GetTotalWeight(){
        int totalWeight = 0;
        foreach(GeneratorComponent component in generatorComponents){
            totalWeight += component.weight;
        }
        return totalWeight;
    }

    public string SelectRandomItem(){
        int random = Random.Range(0,GetTotalWeight());
        int totalWeight = 0;
        foreach(GeneratorComponent component in generatorComponents){
            totalWeight += component.weight;
            if(random < totalWeight){
                return component.itemName;
            }
        }
        return null;
    }
}
