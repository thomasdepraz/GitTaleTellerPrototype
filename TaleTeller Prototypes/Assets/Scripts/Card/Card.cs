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
    [Header("References")]
    public RectTransform rectTransform;
    public Image selfImage;
    private Transform targetTransform;
    private Vector3 origin;
    private Vector3 basePosition;

    [HideInInspector] public DraftSlot currentSlot;
    [HideInInspector] public CardData data;
    public TextMeshProUGUI cardName;
    public TextMeshProUGUI cardDescription;
    public TextMeshProUGUI cardCreativityCost;

    public GameObject attackUI, healthUI, timerUI;
    public TextMeshProUGUI attackText, healthText, timerText;

    delegate void UIEvent();

    List<RaycastResult> results = new List<RaycastResult>();
    List<GameObject> cachedObjects = new List<GameObject>();
    bool pointerDown;
    PointerEventData currentData;
    private Vector2 originPosition;
    private int siblingIndex;

    //Variables for dragging management
    public CanvasGroup canvasGroup;
    private bool isDragging;
    
    private void Update()
    {
        if(isDragging && Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            canvasGroup.blocksRaycasts = true;
            CardManager.Instance.holdingCard = false;
            CardManager.Instance.currentCard = null;
        }
        if(isDragging)
        {
            //follow 
            rectTransform.position = targetTransform.position;
            rectTransform.rotation = Quaternion.Lerp(rectTransform.rotation, new Quaternion(CardManager.Instance.pointerRef.pointerDirection.y/3, -CardManager.Instance.pointerRef.pointerDirection.x/3, 0, 1), Time.deltaTime);
        }
    }

    public void CardInit(CardData data)
    {
        this.data = data;

        attackUI.SetActive(false);
        healthUI.SetActive(false);
        timerUI.SetActive(false);

        //Change effect if needed
        if (data.keyCardActivated)
        {
            data.effect = data.deadCardEffect;
        }

        //load Data and activate gameobject
        basePosition = transform.position;
        data.currentInterestCooldown = data.interestCooldown;
        cardName.text = data.cardName;

        if(data.type == CardType.Character)
        {
            attackUI.SetActive(true);
            attackText.text = data.characterStats.baseAttackDamage.ToString();

            healthUI.SetActive(true);
            healthText.text = data.characterStats.baseLifePoints.ToString();
        }

        if(data.isKeyCard && data.keyCardActivated)
        {
            timerUI.SetActive(true);
            timerText.text = data.currentInterestCooldown.ToString();
        }


        if (data.effect == null)
            cardDescription.text = data.description;
        else
            cardDescription.text = data.effect.effectName;

        cardCreativityCost.text = data.creativityCost.ToString();

        gameObject.SetActive(true);
        originPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y);

        //add random rotation 
        rectTransform.rotation = new Quaternion(0,0,Random.Range(-0.1f,0.1f),1);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(!GameManager.Instance.storyManager.isReadingStory)
        {
            origin = rectTransform.anchoredPosition;
            targetTransform = CardManager.Instance.pointer.transform;
            CardManager.Instance.holdingCard = true;
            CardManager.Instance.currentCard = this;
            transform.SetParent(CardManager.Instance.movingCardsContainer.transform);
            transform.SetAsLastSibling();

            //new event methods
            isDragging = true;
            canvasGroup.blocksRaycasts = false;
            rectTransform.pivot = new Vector2(rectTransform.pivot.x, 0.5f);
            rectTransform.localScale = Vector3.one;

            if (currentSlot != null)
                currentSlot.canvasGroup.blocksRaycasts = true;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        //rectTransform.position = targetTransform.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rectTransform.rotation = new Quaternion(0, 0, 0, 0);
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

            //For now add cost to creativity
            GameManager.Instance.creativityManager.currentBoardCreativityCost += data.creativityCost;

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
        if(!LeanTween.isTweening(gameObject))
            originPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y);


        if (CardManager.Instance.holdingCard && CardManager.Instance.currentCard != this && currentSlot!=null)
        {
            CardManager.Instance.hoveredCard = this;
        }
        else if(transform.parent == CardManager.Instance.cardHandContainer.transform && !CardManager.Instance.holdingCard)//Check if in hand
        {
            //Reset card rotation
            rectTransform.rotation = new Quaternion(0,0,0,0);

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
        cardCreativityCost.text = string.Empty;

        data = null;
        currentSlot = null;
        cachedObjects.Clear();

        rectTransform.position = basePosition;

        CardManager.Instance.cardHand.currentHand.Remove(this);

        gameObject.SetActive(false);

        transform.SetParent(CardManager.Instance.cardHandContainer.transform);
    }

}
