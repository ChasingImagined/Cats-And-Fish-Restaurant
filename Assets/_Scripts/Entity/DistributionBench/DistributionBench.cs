using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistributionBench : MonoBehaviour
{
    
    [SerializeField] public List<DistributionBenchSlot> slots = new List<DistributionBenchSlot>();
    
    public bool AddFood(GameObject foodObj)
    {
        foreach (var slot in slots)
        {
            if(slot== null) continue;
            if (!slot.GetIsFul())
            {
                slot.SetMeall(foodObj);
                return true;
            }

        }

        return false;
    }

    

}
