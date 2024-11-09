using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistributionBenchSlot : MonoBehaviour
{
    
    [SerializeField] private GameObject _foodObj;

    [SerializeField] private Transform _cehefTarget;
    [SerializeField] private Transform _waiterTarget;


    [SerializeField] private bool _isFull;

    public bool GetIsFul()
    {
        return _isFull;
    }

    public void SetIsFull(bool isFull)
    {
        _isFull = isFull;
    }


    public void SetMeall(GameObject foodObj)
    {
        if (_foodObj != null)
        {
            _foodObj = foodObj;
            foodObj.transform.position = this.transform.position;
            foodObj.transform.parent = this.transform;
            _isFull =true;
        }
    }

    public GameObject GetMeall() 
    { 
    
        _isFull = false;
        return _foodObj;
    
    }

    public GameObject GetMeallObj()
    {
        return _foodObj;
    }

    public void SetMeallObj(GameObject foodObj)
    {
        _foodObj = foodObj;
    }

    private Transform GetChefTarget()
    {
        return _cehefTarget;
    }
    public Transform GetWaiterTarget()
    {
        return _waiterTarget;
    }
}
