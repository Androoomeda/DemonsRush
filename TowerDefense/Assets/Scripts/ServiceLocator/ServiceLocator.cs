using System;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
    public static ServiceLocator instance;
    private Dictionary<Type, object> services = new Dictionary<Type, object>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void Register<T>(T newService)
    {
        var type = newService.GetType();

        if (!services.ContainsKey(type))
            services[type] = newService;
    }

    public void UnRegister<T>(T service)
    {
        var type = service.GetType();

        if (services.ContainsKey(type))
            services.Remove(type);
    }

    public T GetService<T>()
    {
        var type = typeof(T);
        if (!services.ContainsKey(type))
            return default;
        return (T)services[type];
    }
}
