using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraftBoard : MonoBehaviour
{
    public List<DraftSlot> slots = new List<DraftSlot>();

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

        ClearDraft();
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


}
