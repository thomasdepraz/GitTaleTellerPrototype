using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Deck : MonoBehaviour
{
    public List<CardData> cardDeck;
    public List<CardData> discardPile;


    public void Start()
    {
        ShuffleCards(cardDeck);
        DealCards(CardManager.Instance.cardHand.maxHandSize);//Deal First hand
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

                    //Remove 1 creativity per recycled card
                    GameManager.Instance.creativityManager.creativity--;
                }
                discardPile.Clear();
                ShuffleCards(cardDeck);
                Debug.LogError("Shuffling discard pile in deck");

                i--;
            }
        }
    }
}
