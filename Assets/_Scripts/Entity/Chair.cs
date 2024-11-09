using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    [SerializeField] private bool _isUse;

    public bool GetIsUse()
    {
        return _isUse;
    }

    public void SetIsUse(bool use)
    {
       _isUse = use;
    }

}
