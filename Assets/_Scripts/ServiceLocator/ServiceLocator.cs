using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ServiceLocator : MonoBehaviour
{
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

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }
        else if(_instance != this) Destroy(this.gameObject);
        
    }

    void OnSceneUnloaded(Scene scene)
    {
        if (_instance == this)
        {
            _services.Clear();
           
        }
        
    }

    private void OnDestroy()
    {
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

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
        Debug.LogWarning($"{type.Name}Service Not fonud");
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
