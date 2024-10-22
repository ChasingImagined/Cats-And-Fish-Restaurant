using ChasingImagined.Uitls;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bank : MonoBehaviour
{
    [SerializeField] private List<ResorceAndCount> _resorceAndCount = new List<ResorceAndCount>();

    private void AbcNumberToNumber()
    {
        for (int i = 0; i < _resorceAndCount.Count; i++)
        {


            (double number, bool numberCheck) = ABCNumbers.AbcToNumber(_resorceAndCount[i].abcCount);
            if (numberCheck)
            {
                _resorceAndCount[i].count = number;
            }
            else
            {
                ResetValue(_resorceAndCount[i]);
            }
        }
    }

    private void NumberToAbcNumber()
    {
        for (int i = 0; i < _resorceAndCount.Count; i++)
        {


            (string abc, bool abcCheck) = ABCNumbers.NumberToAbc(_resorceAndCount[i].count);
            if (abcCheck)
            {
                _resorceAndCount[i].abcCount = abc;
            }
            else
            {
                ResetValue(_resorceAndCount[i]);
            }
        }
    }

    private void ResetValue( ResorceAndCount resorceAndPrice)
    {
        resorceAndPrice.abcCount = "";
        resorceAndPrice.count = 0;
    }


 

    [System.Serializable]
    public class ResorceAndCount
    {
        public Resource resource;
        [ContextMenuItem("Price To ABC price", nameof(NumberToAbcNumber))]
        public double count;
        [ContextMenuItem("ABC Price To Price", nameof(AbcNumberToNumber))]
        public string abcCount;
    }


    public (double,bool) GetResorgeCount(Resource resorge)
    {
        foreach (ResorceAndCount rc in _resorceAndCount)
        {
            if(rc.resource == resorge) return(rc.count,true);
        }

        return(0,false);
    }

    public bool AddResorge(Resource resource,double count)
    {
        int index = -1;
     
        foreach (ResorceAndCount rc in _resorceAndCount)
        {
            index ++;
            if (rc.resource == resource)
            {
                break;
            }
        }
        if(index >= 0)
        {
            _resorceAndCount[index].count = count;
            return true;

        }

        return false;
    }


    public bool ExcartResorge(Resource resource, double count)
    {
        int index = -1;

        foreach (ResorceAndCount rc in _resorceAndCount)
        {
            index++;
            if (rc.resource == resource)
            {
                break;
            }
        }
        if (index >= 0)
        {
            if(_resorceAndCount[index].count >= count)
            {
                _resorceAndCount[index].count -= count;
                return true;
            }

        }

        return false;
    }
}
