using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardManager : Singleton<CardManager>
{
    private void Awake()
    {
        CreateSingleton();
    }

    public Deck cardDeck;
    public Hand cardHand;
    [HideInInspector]public DraftSlot currentHoveredSlot;
    [HideInInspector] public bool holdingCard;
    [HideInInspector] public Card currentCard;
    [HideInInspector] public Card hoveredCard;

    public GameObject movingCardsContainer;
    public GameObject cardHandContainer;

    public GameObject pointer;

    public CustomInputModule inputModule;
}
