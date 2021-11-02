using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{

    public List<Card> hiddenHand = new List<Card>();
    public List<Card> currentHand = new List<Card>();
    public int maxHandSize;

    public void InitCard(CardData data)
    {
        for (int i = 0; i < hiddenHand.Count; i++)
        {
            if(!hiddenHand[i].gameObject.activeSelf)
            {
                currentHand.Add(hiddenHand[i]);
                hiddenHand[i].CardInit(data);
                break;
            }
        }
    }

    public void DiscardHand()
    {
        int cachedCount = currentHand.Count;
        for (int i = 0; i < cachedCount; i++)
        {
            CardManager.Instance.cardDeck.discardPile.Add(currentHand[0].data);
            currentHand[0].ResetCard();   
        }
    }

    public void UpdateKeyCardStatus()
    {
        for (int i = 0; i < currentHand.Count; i++)
        {
            if(currentHand[i].data.keyCardActivated)
            {
                currentHand[i].data.currentInterestCooldown -= 1;//Lower cooldown
                if(currentHand[i].data.currentInterestCooldown <= 0)
                {
                    GameManager.Instance.creativityManager.creativity -= currentHand[i].data.creativityBurn;//Affect creativity
                }    
            }
        }
    }

}
