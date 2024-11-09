using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField]private List<Chair> _chairs = new List<Chair>();

    public Chair GetReadyChair()
    {
        foreach (Chair chair in _chairs)
        {
            if (chair != null) continue;
            if(!chair.GetIsUse()) return chair;
        }

        return null;
    }
}
