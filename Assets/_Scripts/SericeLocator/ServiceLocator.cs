using System;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
    // Sahne bazl� singleton instance
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

    // Servis eklemek i�in
    public void RegisterService<T>(T service) where T : IService
    {
        Type type = typeof(T);
        if (!_services.ContainsKey(type))
        {
            _services[type] = service;
        }
    }

    // Servisi almak i�in
    public T GetService<T>() where T : IService
    {
        Type type = typeof(T);
        if (_services.ContainsKey(type))
        {
            return (T)_services[type];
        }
        return default(T);
    }

    //servis ��kamak i�in
    public void UnRegisterService<T>(T service) where T : IService
    {
        Type type = typeof(T);
        if (_services.ContainsKey(type))
        {
            _services.Remove(type);
        }
    }
}

//yan�l�l�ka hers�n�f� srvis gibi kulnmamak i�in
public interface IService
{
}
