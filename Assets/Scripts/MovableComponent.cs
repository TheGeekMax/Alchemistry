using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovableComponent : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler{
    
    [SerializeField] private Canvas canvas;
    public Item holdingItem;
    public GameObject itemPicture;
    public GameObject loadingBar;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private bool flowingTime = false;
    public float time = 0;

    void Awake(){
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Update(){
        if(flowingTime){
            time += Time.deltaTime*0.1f;
            if(time > 1){
                time = 0;
                GameManager.instance.SpawnAroundCoors(((ItemGenerator)holdingItem).SelectRandomItem(),transform.position);
            }
            loadingBar.GetComponent<UnityEngine.UI.Image>().sprite = GameManager.instance.fills[(int)(time * 10)];
        }
    }

    public void UpdateCanvas(Canvas canvas){
        this.canvas = canvas;
    }

    //fonctions relatives au drag and drop

    public void OnBeginDrag(PointerEventData eventData){
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData){
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData){
        canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerDown(PointerEventData eventData){}

    public void OnDrop(PointerEventData eventData){
        bool isFuzed = GameManager.instance.Fuse(holdingItem,eventData.pointerDrag.GetComponent<MovableComponent>().holdingItem,eventData.position);
        if(isFuzed){
            Destroy(eventData.pointerDrag);
            Destroy(gameObject);
        }
    }
    
    //fonctions relatives au Item
    public void SetItem(Item item){
        holdingItem = item;
    }

    public void setPosition(Vector3 position){
        rectTransform.position = position;
    }

    public void UpdateItem(){
        if(holdingItem != null){
            itemPicture.GetComponent<UnityEngine.UI.Image>().sprite = holdingItem.itemSprite;
            //on change le background
            GetComponent<UnityEngine.UI.Image>().sprite = GameManager.instance.GetBackground(holdingItem.GetItemType());
            //on change le flowing time
            if(holdingItem.GetItemType() == "Generator"){
                loadingBar.SetActive(true);
                flowingTime = true;
            }else{
                loadingBar.SetActive(false);
                flowingTime = false;
            }
        }
    }
}
