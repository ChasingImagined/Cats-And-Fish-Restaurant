using System;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
    // Sahne bazlý singleton instance
    private static ServiceLocator _instance;


    public static ServiceLocator Instance
    {
        

        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ServiceLocator>();
                if (_instance == null)
                {
                    GameObject locator = new GameObject("ServiceLocator");
                    _instance = locator.AddComponent<ServiceLocator>();
                }
            }
            return _instance;
        }
    }


    private Dictionary<Type, IService> _services = new Dictionary<Type, IService>();

    // Servis eklemek için
    public void RegisterService<T>(T service) where T : IService
    {
        Type type = typeof(T);
        if (!_services.ContainsKey(type))
        {
            _services[type] = service;
        }
    }

    // Servisi almak için
    public T GetService<T>() where T : IService
    {
        Type type = typeof(T);
        if (_services.ContainsKey(type))
        {
            return (T)_services[type];
        }
        return default(T);
    }

    //servis çýkamak için
    public void UnRegisterService<T>(T service) where T : IService
    {
        Type type = typeof(T);
        if (_services.ContainsKey(type))
        {
            _services.Remove(type);
        }
    }
}

//yanýlþlýka hersýnýfý srvis gibi kulnmamak için
public interface IService
{
}
