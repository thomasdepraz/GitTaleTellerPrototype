using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Object Event", menuName = "Event/Object Event", order = 1)]
public class ObjectEvent : StoryEvent
{
    public enum HeroProperty
    {
        None,
        MaxHP,
        CurrentHP,
        AttackDamage,
        AttackBonus
    }
    [System.Serializable]
    struct ObjectEffect
    {
        [SerializeField] public HeroProperty heroProperty;
        [SerializeField] public int value;
    }

    [Header("Effects")]
    [SerializeField] List<ObjectEffect> effects = new List<ObjectEffect>();

    //Private variables
    Hero currentHero;

    public override void InitializeEvent()
    {
        //Get hero from manager
        currentHero = GameManager.Instance.currentHero;

        //Initialize graphics on storyLine

    }

    public override void OnTriggerEnterEvent()
    {
        Debug.LogError($"Trigger event {eventName}");

        for (int i = 0; i < effects.Count; i++)
        {
            switch (effects[i].heroProperty)
            {
                case HeroProperty.None:
                    Debug.LogError("Object has no effect");
                    break;
                case HeroProperty.MaxHP:
                    currentHero.maxLifePoints += effects[i].value;
                    break;
                case HeroProperty.CurrentHP:
                    currentHero.lifePoints += effects[i].value;
                    break;
                case HeroProperty.AttackDamage:
                    currentHero.attackDamage += effects[i].value;
                    break;
                case HeroProperty.AttackBonus:
                    currentHero.bonusDamage += effects[i].value;
                    break;
                default:
                    Debug.LogError("Something went wrong");
                    break;
            }
        }

        //Keep going with the story
        GameManager.Instance.storyManager.MoveToNextEvent();
    }

    public override void OnTriggerExitEvent()
    {

    }
}
