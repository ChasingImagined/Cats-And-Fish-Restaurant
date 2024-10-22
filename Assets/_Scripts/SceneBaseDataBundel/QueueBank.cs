using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueBank
{

    private Dictionary<Type, Queue<IQueueable>> _queues =new();

 

    public T DeQueueable<T>() where T : class, IQueueable
    {
        if (_queues.ContainsKey(typeof(T))){

            if (_queues[typeof(T)].Count>0) return (T)_queues[typeof(T)].Dequeue();

            Debug.LogWarning($"Queue<{typeof(T).Name}> is emty ");
            return null;

        }

        Debug.LogWarning($"Queue<{typeof(T).Name}> not found ");
        
        return null;
    }

    public void EnQueueable<T>(T queueable) where T : class, IQueueable
    {
        if(queueable == null) return;

        if (!_queues.ContainsKey(typeof(T)))
        {
            Queue<IQueueable> queue = new();
            queue.Enqueue(queueable);
            _queues.Add(typeof(T),queue);
        }
        else
        {
            _queues[typeof(T)].Enqueue(queueable);
        }
    }
}

public interface IQueueable { }