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
                Debug.Log($"Created event of type {slots[i].currentPlacedCard.data.type} at story index {i}.");
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
                CardManager.Instance.cardDeck.discardPile.Add(slots[i].currentPlacedCard.data);

                //Call reset method on card
                slots[i].currentPlacedCard.ResetCard();

                //Reset slot
                slots[i].currentPlacedCard = null;
                slots[i].canvasGroup.blocksRaycasts = true;
            }
        }
    }


}
