using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreativityManager : MonoBehaviour
{
    [Header("Stats")]
    private int _creativity;
    public int creativity
    {
        get => _creativity;
        set
        {
            _creativity = value;
            if (_creativity > maxCreativity)
                _creativity = maxCreativity;
            else if(_creativity<0)
            {
                _creativity = 0;
                //Trigger game over
            }

            creativityBar.fillAmount = _creativity / maxCreativity;
        }
    }
    public int maxCreativity;
    [HideInInspector] public int currentBoardCreativityCost;

    [Header("References")]
    public Image creativityBar;


    // Start is called before the first frame update
    void Start()
    {
        //set creativity to max
        creativity = maxCreativity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
