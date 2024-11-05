using ChasingImagined.Uitls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Item : Being
{
    [Header(nameof(Item))]
    [SerializeField] private GameObject _obj;

    [ContextMenuItem("Number TO ABC Number",nameof(NumberToAbcNumber))]
    [SerializeField] private double _price;

    [ContextMenuItem("ABC Number TO Number", nameof(AbcNumberToNumber))]
    [SerializeField] private string _abcNumer;
 

    public GameObject GetObj()
    {
        return _obj;
    }
    public double GetPrice()
    {
    
        return _price;
    }
    
    

    public string GetABCNumber()
    {
        return _abcNumer;
    }

    
    protected void AbcNumberToNumber()
    {

        (double number, bool numbercheck) = ABCNumbers.AbcToNumber(_abcNumer);
        if (numbercheck)
        {
            _price = number;
        }
        else ResetValeue();
    }

    protected void NumberToAbcNumber()
    {
        (string abc, bool abcCheck) = ABCNumbers.NumberToAbc(_price);
        if (abcCheck)
        {
            _abcNumer = abc;
        }
        else ResetValeue();
    }

    private void ResetValeue()
    {
        _abcNumer = "";
        _price = 0;
    }
}

public interface IItemFrendlyable { }