using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Food : Item
{
    [Header(nameof(Food))]
    //yemek haz�rlama s�resi temli
    [SerializeField] private float _preparationTime;

    public float GetPreparationTime()
    {
        return _preparationTime;
    }
}
