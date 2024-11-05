using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Food : Item
{
    [Header(nameof(Food))]
    //yemek hazýrlama süresi temli
    [SerializeField] private float _preparationTime = 5f;

    public float GetPreparationTime()
    {
        return _preparationTime;
    }
}
