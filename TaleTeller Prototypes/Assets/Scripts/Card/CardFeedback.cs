using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardFeedback : MonoBehaviour
{
    [Header("References")]
    public Image feedbackImage;
    public GameObject statsContainer;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI attackText;

    public void InitCardFeedback(CardData card)
    {
        gameObject.SetActive(true);
        card.feedback = this;
        feedbackImage.sprite = card.cardGraph;

        //Init the rest of the stats
        switch (card.type)
        {
            case CardType.Character:
                statsContainer.SetActive(true);
                hpText.text = card.characterStats.baseLifePoints.ToString();
                attackText.text = card.characterStats.baseAttackDamage.ToString();
                break;
            case CardType.Object:
                break;
            case CardType.Location:
                break;
            default:
                break;
        }
    }

    public void UnloadCardFeedback(CardData card)
    {
        gameObject.SetActive(false);
        statsContainer.SetActive(false);
        card.feedback = null;
        feedbackImage.sprite = null;
    }

    public void UpdateText(CardData card)
    {
        hpText.text = card.characterStats.baseLifePoints.ToString();
        attackText.text = card.characterStats.baseAttackDamage.ToString();
    }
}
