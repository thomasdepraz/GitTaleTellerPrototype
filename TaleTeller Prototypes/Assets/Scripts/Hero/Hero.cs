using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hero : MonoBehaviour
{

    [Header("References")]
    [SerializeField] public HeroBaseData heroData;

    [Header("UI")]
    public TextMeshProUGUI heroHpUI;
    public TextMeshProUGUI heroAttackUI;
    public TextMeshProUGUI heroBonusDamageUI;

    //Private hero variables
    private int _maxLifePoints;
    private int _lifePoints;
    private int _attackDamage;
    private int _bonusDamage;
    [HideInInspector]public int maxLifePoints
    { 
        get => _maxLifePoints ; 
        set 
        {
            _maxLifePoints = value;
            heroHpUI.text = "HP : " +  lifePoints + "/" + value.ToString();
        }
    }
    [HideInInspector]public int lifePoints 
    { 
        get => _lifePoints ; 
        set 
        {
            _lifePoints = value;

            if (_lifePoints  > maxLifePoints)
                _lifePoints = maxLifePoints;
            else if (_lifePoints < 0)
                _lifePoints = 0;


            heroHpUI.text = "HP : " + lifePoints.ToString() + "/" + maxLifePoints;

            //Different feedack if damage taken or if healed
        }
    }
    [HideInInspector]public int attackDamage
    {
        get => _attackDamage;
        set
        {
            _attackDamage = value;
            heroAttackUI.text = "Damage : " + value.ToString();
        }
    }
    [HideInInspector]public int bonusDamage
    {
        get => _bonusDamage;
        set
        {
            _bonusDamage = value;
            heroBonusDamageUI.text = "Bonus DMG : " + value.ToString();
        }
    }

    [HideInInspector]public int armor;


    public void InitializeHero()
    {
        maxLifePoints = heroData.baseLifePoints;
        lifePoints = heroData.baseLifePoints;
        attackDamage = heroData.baseAttackDamage;
        armor = 0;
        bonusDamage = 0;

        //Initialize graphics on story line

    }
    
    // Start is called before the first frame update
    void Start()
    {
        InitializeHero();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
