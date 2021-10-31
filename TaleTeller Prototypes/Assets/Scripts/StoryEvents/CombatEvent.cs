using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CombatEvent", menuName = "Event/Combat Event", order = 1)]
public class CombatEvent : StoryEvent 
{

    //Get a reference to the enemy that must be beaten
    [Header("References")]
    public CharacterStats currentEnemy; //MAYBE I NEED *NEW* KEYWORD

    //Get private variables for combat
    Hero currentHero;
    private int enemyLifePoints;
    private int enemyAttackPoints;

    public override void InitializeEvent()
    {
        //Get hero from manager
        currentHero = GameManager.Instance.currentHero;

        //Initialize enemy on story line

        //Scale stats based on player progression 
        enemyLifePoints = currentEnemy.baseLifePoints + (int)Random.Range(GameManager.Instance.storyManager.chapterCount -2, GameManager.Instance.storyManager.chapterCount+2);
        enemyAttackPoints = currentEnemy.baseAttackDamage + (int)Random.Range((GameManager.Instance.storyManager.chapterCount * 0.75f) -1, (GameManager.Instance.storyManager.chapterCount * 0.75f) + 1);
        
        if (enemyAttackPoints < 1)//set min to 1
            enemyAttackPoints = 1;
        if (enemyLifePoints < 3)
            enemyLifePoints = 3;

        //Initialize graphics
    }

    public override void OnTriggerEnterEvent()
    {
        //Make something launch combat
        Debug.LogError($"Fight against {eventName}, HP : {enemyLifePoints}, DMG  : {enemyAttackPoints} ");
        GameManager.Instance.storyManager.StartEventCoroutine(Combat());
    }

    public override void OnTriggerExitEvent()
    {

    }

    IEnumerator Combat()
    {
        //Get a reference to the player and resolve the fight
        while(currentHero.lifePoints > 0 && enemyLifePoints > 0)
        {
            //Let the player attack
            enemyLifePoints -= (currentHero.attackDamage + currentHero.bonusDamage);

            yield return new WaitForSeconds(1f);//wait to show feedback

            if (enemyLifePoints <= 0)
                break;
            
            //If alive, monster attack
            currentHero.lifePoints -= currentEnemy.baseAttackDamage;

            //repeat if player alive
        }

        if(enemyLifePoints <= 0)
        {
            //End the fight and keep going 
            Debug.LogError("Enemy dead");
            GameManager.Instance.storyManager.MoveToNextStep();

        }
        else
        {
            //make a new Hero
            Debug.LogError("player dead, reviving hero");
            currentHero.ReviveHero();

            //keep going
            GameManager.Instance.storyManager.MoveToNextStep();
        }

        yield return null;
    }

}
