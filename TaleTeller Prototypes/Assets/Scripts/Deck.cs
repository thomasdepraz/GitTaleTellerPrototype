using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Deck : MonoBehaviour
    ,IPointerClickHandler
{
    public List<CardData> cardDeck;
    public List<CardData> discardPile;


    public void Start()
    {
        ShuffleCards(cardDeck);
    }

    public List<CardData> ShuffleCards(List<CardData> deckToShuffle) //FisherYates Shuffle
    {
        int n = deckToShuffle.Count;
        for (int i = 0; i < (n - 1); i++)
        {
            int r = i + Random.Range(0, n-i);
            CardData t = deckToShuffle[r];
            deckToShuffle[r] = deckToShuffle[i];
            deckToShuffle[i] = t;
        }
        return deckToShuffle;
    }

    public void DealCards(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if(cardDeck.Count > 0)
            {
                CardManager.Instance.cardHand.InitCard(cardDeck[0]);
                cardDeck.RemoveAt(0);
            }
            else
            {
                for (int j = 0; j < discardPile.Count; j++)
                {
                    cardDeck.Add(discardPile[j]);
                }
                discardPile.Clear();
                ShuffleCards(cardDeck);
                i--;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        int numberToDeal = CardManager.Instance.cardHand.maxHandSize - CardManager.Instance.cardHand.currentHand.Count;
        DealCards(numberToDeal);
    }
}
