using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;

public class CentralBank : MonoBehaviour
{
    private Dictionary<Type, BankBase> _banks = new();
    public bool AddResource<T>(Resource resource,double  count) where T : Resource
    {
        if (resource == null) return false;

        if (_banks.ContainsKey(typeof(T)))
        {
           return ((ResourceBankBase<T>)_banks[typeof(T)]).AddResorge(resource,count);
        } 
        return false;
    }

    public bool AddResource<T>(ResourceBankBase<T> resourceBank) where T : Resource
    {
        if(resourceBank == null) return false;

        if (_banks.ContainsKey(resourceBank.GetStoredType()))
        {
            return false;
        }
        _banks.Add(resourceBank.GetStoredType(), resourceBank);
        return true;
    }

    public bool ExtractResource<T>(Resource resource,Double count) where T : Resource
    {
        if (resource == null) return false;

        return GetResourceBankBase<T>(resource)?.ExtractResource(resource,count) ?? false;
    }

    public bool ExtractAllResource<T>(Resource resource) where T : Resource
    {
        if (resource == null) return false;

        ResourceBankBase<T> rsbb = GetResourceBankBase<T>(resource);
        if (rsbb == null) return false;
        return rsbb.ExtractResource(resource, rsbb.GetResorgeCount(resource));

    }

    public double GetResorgeCount<T>(Resource resource) where T : Resource
    {  
        return GetResourceBankBase<T>(resource)?.GetResorgeCount(resource) ?? 0d;
    }

   
    private ResourceBankBase<T> GetResourceBankBase<T>(Resource resource) where T : Resource
    {
        if (_banks.ContainsKey(resource.GetType()))
        {
            return (ResourceBankBase<T>)_banks[resource.GetType()];
        }
        return null; 
    }

}
