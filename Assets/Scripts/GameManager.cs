using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour{
    public static GameManager instance;

    public GameObject itemPrefab;
    [SerializeField] private Canvas canvas;

    [Header("Items")]

    public Item[] items;
    public Recipe[] recipes;

    [Header("Background Sprites")]
    public Sprite itemBackground;
    public Sprite generatorBackground;

    [Header("Generators times")]
    public Sprite[] fills;

    [Header("Temporaire")]

    public TextMeshProUGUI currentItemText;
    string currentItem = "wood";
    int id = 0;

    private TreeCraft treeCraft;

    void Awake(){
        if(instance == null){
            instance = this;
        }else{
            Destroy(gameObject);
        }
    }

    void Start(){
        //on definit l'item courant
        GetNextItem();

        //on ajoute les recettes
        treeCraft = new TreeCraft("root");
        foreach(Recipe recipe in recipes){
            treeCraft.AddCraft(recipe.item1,recipe.item2,recipe.result);
        }
    }

    void Update(){
        //on detecte un click droit
        if(Input.GetMouseButtonDown(1)){
            //on fait apparaitre l'item courant
            SpawnItem(GetItem(currentItem),Input.mousePosition);
        }
        //si on appuis sur espace on change l'item courant
        if(Input.GetKeyDown(KeyCode.Space)){
            GetNextItem();
        }
    }

    //fonctions relatives aux items

    private Item GetItem(string name){
        foreach(Item item in items){
            if(item.itemName == name){
                return item;
            }
        }
        return null;
    }

    public void SpawnItem(Item item,Vector3 position){
        GameObject itemObject = Instantiate(itemPrefab,position,Quaternion.identity);
        itemObject.transform.SetParent(canvas.transform);
        itemObject.GetComponent<MovableComponent>().UpdateCanvas(canvas);
        itemObject.GetComponent<MovableComponent>().SetItem(item);
        itemObject.GetComponent<MovableComponent>().UpdateItem();
    }

    public bool Fuse(Item i1,Item i2,Vector3 position){
        //todo faire une vrai version

        string result = treeCraft.GetResult(i1.itemName,i2.itemName);
        if(result != null){
            SpawnItem(GetItem(result),position);
            return true;
        }
        return false;
    }


    //fonctions pour les textures;
    public Sprite GetBackground(string type){
        if(type == "Item"){
            return itemBackground;
        }else if(type == "Generator"){
            return generatorBackground;
        }
        return null;
    }

    public Sprite GetPercentageFill(float percentage){
        //on renvoie l'indice de fills avec percentage*fill.length
        return fills[(int)(percentage*fills.Length)];
    }

    public void SpawnAroundCoors(string itemName,Vector3 position){
        //on prends une position aleatoire autour de position
        Vector3 randomPosition = new Vector3(position.x + Random.Range(-300,300),position.y + Random.Range(-300,300),position.z);
        //on clamp
        randomPosition.x = Mathf.Clamp(randomPosition.x,0,Screen.width);
        randomPosition.y = Mathf.Clamp(randomPosition.y,0,Screen.height);
        //on fait apparaitre l'item courant
        SpawnItem(GetItem(itemName),randomPosition);
    }


    private void GetNextItem(){
        id++;
        if(id >= items.Length){
            id = 0;
        }
        currentItem = items[id].itemName;
        currentItemText.text = currentItem;

    }
}
