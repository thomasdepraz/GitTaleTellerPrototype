using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraftBoard : MonoBehaviour
{
    public List<DraftSlot> slots = new List<DraftSlot>();

    //Event Queues
    [HideInInspector] public List<IEnumerator> onStartQueue = new List<IEnumerator>();
    [HideInInspector] public List<IEnumerator> onEndQueue = new List<IEnumerator>();
    [HideInInspector] public List<IEnumerator> cardEffectQueue = new List<IEnumerator>();
    [HideInInspector] public List<IEnumerator> cardEventQueue = new List<IEnumerator>();

    #region OldLogic
    public void CreateStory()
    {
        
        for (int i = 0; i < slots.Count; i++)
        {
            //Create event based on card data
            if(slots[i].currentPlacedCard != null)
            {
                Debug.Log($"Created {slots[i].currentPlacedCard.data.cardName} at step {i+1}.");

                CardData cardData = slots[i].currentPlacedCard.data;
                CardToInit card = new CardToInit(cardData, i);
                GameManager.Instance.storyManager.cardsToInit.Add(card);
                GameManager.Instance.creativityManager.creativity -= GameManager.Instance.creativityManager.currentBoardCreativityCost;
                GameManager.Instance.creativityManager.currentBoardCreativityCost = 0;//reset board cost
            }
        }
    }

    public void ClearDraft()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            //place all data to discard Pile and reset card to hiddenhand
            if(slots[i].currentPlacedCard != null)
            {
                //Call reset method on card
                slots[i].currentPlacedCard.ResetCard();

                //Reset slot
                slots[i].currentPlacedCard = null;
                slots[i].canvasGroup.blocksRaycasts = true;
            }
        }
        //Clear Hand
        //CardManager.Instance.cardHand.DiscardHand();

        //launch story
        GameManager.Instance.storyManager.StartStory();
    }

    public bool IsEmpty()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].currentPlacedCard != null)
                return false;
        }
        return true;
    }
    #endregion

    public void InitBoard()
    {
        //Pour chaque slot, on appelle l'event OnStartStory
        for (int i = 0; i < slots.Count; i++)
        {
            if(slots[i].currentPlacedCard != null)
            {
                if (slots[i].currentPlacedCard.data.onStartEvent != null)
                {
                    slots[i].currentPlacedCard.data.onStartEvent();
                }
            }
        }
        //Normally have here a bg list of routines to run through
        StartCoroutine(onStartQueue[0]);
    }
    public void UpdateOnStartQueue()
    {
        //Unqueue
        onStartQueue.RemoveAt(0);

        //if still event continue
        if(onStartQueue.Count >0)
        {
            StartCoroutine(onStartQueue[0]);
        }
        //else stop and keep going with reading
        else
        {
           
        }
    }

    public void DiscardCardFromBoard(CardContainer card)
    {
        CardManager.Instance.cardDeck.discardPile.Add(card.data);
        card.ResetCard();

        //remove from board list
        card.currentSlot.currentPlacedCard = null;
        card.currentSlot = null;
    }

}
