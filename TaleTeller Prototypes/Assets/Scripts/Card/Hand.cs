using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{

    public List<Card> hiddenHand = new List<Card>();
    [HideInInspector]public List<Card> currentHand = new List<Card>();
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

}
