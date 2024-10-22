using System.Collections.Generic;
using UnityEngine;

public static class EventBus<T> where T : IEvent
{
    private static readonly HashSet<IEventBinding<T>> bindings = new HashSet<IEventBinding<T>>();
    private static bool isPublishing = false;
    private static readonly List<IEventBinding<T>> pendingAdds = new List<IEventBinding<T>>();
    private static readonly List<IEventBinding<T>> pendingRemoves = new List<IEventBinding<T>>();

    public static void Subscribe(EventBinding<T> binding)
    {
        if (isPublishing)
        {
            pendingAdds.Add(binding);
        }
        else
        {
            bindings.Add(binding);
        }
    }

    public static void UnSubscribe(EventBinding<T> binding)
    {
        if (isPublishing)
        {
            pendingRemoves.Add(binding);
        }
        else
        {
            bindings.Remove(binding);
        }
    }

    public static void Publish(T eventToRaise)
    {
        isPublishing = true;

        foreach (var binding in bindings)
        {
            binding.OnEvent.Invoke(eventToRaise);
            binding.OnEventNoArgs.Invoke();
        }

        isPublishing = false;

        // Apply pending additions and removals after publishing
        foreach (var binding in pendingAdds)
        {
            bindings.Add(binding);
        }
        pendingAdds.Clear();

        foreach (var binding in pendingRemoves)
        {
            bindings.Remove(binding);
        }
        pendingRemoves.Clear();
    }

    public static void Clear()
    {
        bindings.Clear();
        Debug.Log("Clearing " + typeof(T).Name + " bindings");
    }
}
