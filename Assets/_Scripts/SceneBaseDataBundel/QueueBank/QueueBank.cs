using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueBank
{
    //kuyrukalrý türlereine göre depola.
    private Dictionary<Type, Queue<IQueueable>> _queues =new();

 
    // T tipi veri içeren kuyruktan elman çýkarýr.
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

    //T tipi veri içeren kuyruga elman ekleler.
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
            if(!_queues[typeof(T)].Contains(queueable))_queues[typeof(T)].Enqueue(queueable);
        }
    }
}

// Kuyruga alýnablirligi sýnrlama amçlý interface;
public interface IQueueable { }