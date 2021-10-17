using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraftSlot : MonoBehaviour
     , IPointerEnterHandler
     , IPointerExitHandler
{
    public Card currentPlacedCard;
    public CanvasGroup canvasGroup;

    [Header("Sprites")]
    public Image image;
    public Sprite defaultSprite;
    public Sprite hoveredSprite;


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (CardManager.Instance.holdingCard)
        {
            CardManager.Instance.currentHoveredSlot = this;
        }

        image.sprite = hoveredSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (CardManager.Instance.holdingCard)
        {
            if(CardManager.Instance.currentHoveredSlot == this)
                CardManager.Instance.currentHoveredSlot = null;
        }

        image.sprite = defaultSprite;
    }
}
