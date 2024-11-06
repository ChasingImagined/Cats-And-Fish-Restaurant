using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

public abstract class Being : ScriptableObject
{
    [Header(nameof(Being))]

    [SerializeField] private string _id; 
    [SerializeField] private LocalizedString _name;
    [SerializeField] private LocalizedString _descripton; //açýklama

    public string GetId()
    {
        return _id;
    }

    public string GetName()
    {
        return _name.GetLocalizedString();
    }

    public string Descripton()
    {
        return _descripton.GetLocalizedString();
    }
    
   
}

