using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{

    [Header("References")]
    public HeroBaseData heroData;

    //Private hero variables
    [HideInInspector]public int lifePoints;
    [HideInInspector]public int attackDamage;
    [HideInInspector]public int armor;
    [HideInInspector]public int bonusDamage;


    public void InitializeHero()
    {
        lifePoints = heroData.baseLifePoints;
        attackDamage = heroData.baseAttackDamage;
        armor = 0;
        bonusDamage = 0;

        //Initialize graphics on story line

    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
