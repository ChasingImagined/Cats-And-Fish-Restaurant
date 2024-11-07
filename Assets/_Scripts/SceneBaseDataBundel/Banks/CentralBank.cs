using System.Collections.Generic;
using UnityEngine;

public class CentralBank : MonoBehaviour
{
    private Dictionary<Resource, double> _ResourceBundel = new();
    [SerializeField] private List<ResourceBankBase> _ResourceBanks = new List<ResourceBankBase>();

    private void Awake()
    {
        foreach(var bank in _ResourceBanks) { 

            if(bank == null) continue;
            foreach (var kvp in bank._GetAllList())
            {
                Add(kvp.Key, kvp.Value);
            }
        }

        Debug.Log(_ResourceBundel.Count);
    }

    // S�zl�ge kaynk ekler.
    public bool Add(Resource resource, double amount)
    {
        if (!ResourceNullCheck(resource)) return false;

        if (_ResourceBundel.ContainsKey(resource))
        {
            _ResourceBundel[resource] += amount;
        }
        else
        {
            _ResourceBundel.Add(resource, amount);
        }

        return true;
    }

    // S�zl�kten belirli bir kayna��n say�s�n� belirtelen miktarda eskiltitr;
    public bool TakeOut(Resource resource, double amount)
    {
        if (!ContainsAndNullCheck(resource)) return false;

        if (_ResourceBundel[resource] >= amount)
        {
            _ResourceBundel[resource] -= amount;
            return true;
        }

        return false;
    }

    // S�zl�kten belirli bir kayna��n tamama�n� s�f�rlar;
    public double TakeOutAll(Resource resource)
    {
        if (ContainsAndNullCheck(resource))
        {
            double temp = _ResourceBundel[resource];
            _ResourceBundel[resource] = 0d;
            return temp;
        }

        return 0d;
    }

    // belirtlen kaynag�n miktar�n� d�nenerir;
    public double GetAmount(Resource resource)
    {
        return ContainsAndNullCheck(resource) ? _ResourceBundel[resource] : 0d;
    }

    // belirtlen kaynag�n null olup olmad�g�n� �grenmeye yarar;
    private bool ResourceNullCheck(Resource resource, bool debug = true)
    {
        if (resource == null)
        {
            if (debug) Debug.LogWarning("Resource is null.");
            return false;
        }
        return true;
    }

    // belirtlen kaynag�n s�zl�kte olup olamd�g�n� kontrol eder ;
    private bool ContainsAndNullCheck(Resource resource, bool debug = false)
    {
        if (!ResourceNullCheck(resource, debug)) return false;

        if (!_ResourceBundel.ContainsKey(resource))
        {
            if (debug) Debug.LogWarning($"{resource?.name} Resource Not Found in bank.");
            return false;
        }

        return true;
    }
}
