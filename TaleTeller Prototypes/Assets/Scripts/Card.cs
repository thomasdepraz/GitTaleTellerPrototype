using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour
     , IPointerUpHandler
     , IBeginDragHandler
     , IEndDragHandler
     , IDragHandler
     , IPointerDownHandler
     , IPointerEnterHandler
     , IPointerExitHandler
 
{
    public RectTransform rectTransform;
    private Transform targetTransform;
    private Vector3 origin;
    private Vector3 basePosition;

    [HideInInspector] public DraftSlot currentSlot;
    [HideInInspector] public CardData data;
    public TextMeshProUGUI cardName;
    public TextMeshProUGUI cardDescription;
    public TextMeshProUGUI cardWeight;

    delegate void UIEvent();

    List<RaycastResult> results = new List<RaycastResult>();
    List<GameObject> cachedObjects = new List<GameObject>();
    bool pointerDown;
    PointerEventData currentData;
    private Vector2 originPosition;
    private int siblingIndex;

    private void Update()
    {
        if(pointerDown)
        {
            currentData = CardManager.Instance.inputModule.GetPointerEventData();
            if(currentData != null)
            {
                //Start RayCasting for underneathObjects
                EventSystem.current.RaycastAll(currentData, results);
                if(results.Count >0)
                {
                    if(cachedObjects.Count < results.Count - 1)
                    {
                        cachedObjects.Add(results[results.Count - 1].gameObject);
                        ExecuteEvents.Execute(cachedObjects[cachedObjects.Count - 1], currentData, ExecuteEvents.pointerEnterHandler);
                    }
                    else if(cachedObjects.Count > results.Count-1 && cachedObjects.Count>0)
                    {
                        int difference = cachedObjects.Count - (results.Count - 1);
                        for (int i = 0; i < difference; i++)
                        {
                            ExecuteEvents.Execute(cachedObjects[cachedObjects.Count - 1], currentData, ExecuteEvents.pointerExitHandler);
                            cachedObjects.RemoveAt(cachedObjects.Count - 1);
                        }
                        cachedObjects.Clear();
                    }
                }
            }
        }
    }

    public void CardInit(CardData data)
    {
        //load Data and activate gameobject
        basePosition = transform.position;
        this.data = data; 

        cardName.text = data.cardName;
        cardDescription.text = data.description;
        cardWeight.text = data.weight.ToString();

        gameObject.SetActive(true);
        originPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        origin = rectTransform.anchoredPosition;
        targetTransform = CardManager.Instance.pointer.transform;
        CardManager.Instance.holdingCard = true;
        CardManager.Instance.currentCard = this;
        transform.SetParent(CardManager.Instance.movingCardsContainer.transform);
        transform.SetAsLastSibling();

        if (currentSlot != null)
            currentSlot.canvasGroup.blocksRaycasts = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = targetTransform.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Enter open slot
        if (CardManager.Instance.currentHoveredSlot != null)
        {
            if (currentSlot != null)
            {
                currentSlot.currentPlacedCard = null;
                currentSlot = null;
            }

            CardManager.Instance.currentHoveredSlot.currentPlacedCard = this;
            currentSlot = CardManager.Instance.currentHoveredSlot;
            currentSlot.canvasGroup.blocksRaycasts = false;
            rectTransform.position = CardManager.Instance.currentHoveredSlot.transform.position;
        }
        else
        {
            targetTransform = null;
            rectTransform.anchoredPosition = origin;
            if(currentSlot!=null)
                currentSlot.canvasGroup.blocksRaycasts = false;
        }

        //Swap with other card
        if(CardManager.Instance.hoveredCard!=null)
        {
            //Swap cards from the board
            if(currentSlot!=null)
            {
                if(CardManager.Instance.hoveredCard.currentSlot!=null)
                {
                    DraftSlot tempSlot = CardManager.Instance.hoveredCard.currentSlot;
            
                    CardManager.Instance.hoveredCard.currentSlot = currentSlot;
                    CardManager.Instance.hoveredCard.currentSlot.currentPlacedCard = CardManager.Instance.hoveredCard;
                    CardManager.Instance.hoveredCard.rectTransform.position = CardManager.Instance.hoveredCard.currentSlot.transform.position;
                    CardManager.Instance.hoveredCard.currentSlot.canvasGroup.blocksRaycasts = false;


                    tempSlot.currentPlacedCard = this;
                    currentSlot = tempSlot;
                    rectTransform.position = currentSlot.transform.position;
                }
                else
                {
                    DraftSlot tempSlot = currentSlot;

                    tempSlot.currentPlacedCard = CardManager.Instance.hoveredCard;
                    CardManager.Instance.hoveredCard.currentSlot = tempSlot;

                    currentSlot = null;

                    rectTransform.position = CardManager.Instance.hoveredCard.rectTransform.position;
                    CardManager.Instance.hoveredCard.rectTransform.position = tempSlot.transform.position;
                    CardManager.Instance.hoveredCard.transform.SetParent(CardManager.Instance.movingCardsContainer.transform);
                    transform.SetParent(CardManager.Instance.cardHandContainer.transform);
                }
                    CardManager.Instance.hoveredCard = null;

            }
            else
            {
                DraftSlot tempSlot = CardManager.Instance.hoveredCard.currentSlot;

                tempSlot.currentPlacedCard = this;
                CardManager.Instance.hoveredCard.currentSlot = null;
                currentSlot = tempSlot;
                CardManager.Instance.hoveredCard.rectTransform.position = rectTransform.position;
                CardManager.Instance.hoveredCard.transform.SetParent(CardManager.Instance.cardHandContainer.transform);
                rectTransform.position = tempSlot.transform.position;

                CardManager.Instance.hoveredCard = null;
            }
        }
        CardManager.Instance.currentHoveredSlot = null;
        CardManager.Instance.holdingCard = false;
        CardManager.Instance.currentCard = null;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDown = true;


    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pointerDown = false;
        cachedObjects.Clear();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (CardManager.Instance.holdingCard && CardManager.Instance.currentCard != this)
        {
            CardManager.Instance.hoveredCard = this;
        }
        else if(transform.parent == CardManager.Instance.cardHandContainer.transform)//Check if in hand
        {
            //Scale up and bring to front;
            LeanTween.cancel(gameObject);
            siblingIndex = transform.GetSiblingIndex();
            transform.SetAsLastSibling();

            rectTransform.anchoredPosition = originPosition;
            originPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y);

            rectTransform.pivot = new Vector2(rectTransform.pivot.x, 0);
            rectTransform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

            LeanTween.move(rectTransform, rectTransform.anchoredPosition + new Vector2(0, 10f), 0.5f).setEaseOutSine();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(CardManager.Instance.holdingCard && CardManager.Instance.currentCard != this)
        {
            CardManager.Instance.hoveredCard = null; 
        }
        else if (transform.parent == CardManager.Instance.cardHandContainer.transform)//Check if in hand
        {
            //Scale up and bring to front;
            LeanTween.cancel(gameObject);
            rectTransform.pivot = new Vector2(rectTransform.pivot.x, 0.5f);
            rectTransform.localScale = Vector3.one;
            LeanTween.move(rectTransform, originPosition, 0.5f).setEaseOutSine();
            //rectTransform.anchoredPosition = originPosition;
            transform.SetSiblingIndex(siblingIndex);
        }
    }

    public void ResetCard()
    {
        //unload Data and activate gameobject
        cardName.text = string.Empty;
        cardDescription.text = string.Empty;
        cardWeight.text = string.Empty;

        data = null;
        currentSlot = null;
        cachedObjects.Clear();

        rectTransform.position = basePosition;

        CardManager.Instance.cardHand.currentHand.Remove(this);
        CardManager.Instance.cardHand.hiddenHand.Add(this);

        gameObject.SetActive(false);

        transform.SetParent(CardManager.Instance.cardHandContainer.transform);
    }

}
