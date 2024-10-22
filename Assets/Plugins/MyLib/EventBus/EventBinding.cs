using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IEventBinding<T>
{
    public Action<T> OnEvent {  get; set; }
    public Action OnEventNoArgs { get; set; }
}

public class EventBinding<T> : IEventBinding<T> where T : IEvent
{
    private Action<T> onEvent    =  _ => {  };
    private Action onEventNoargs = () => { };

    Action<T> IEventBinding<T>.OnEvent
    {
        get => onEvent;
        set => onEvent = value;
    }

    Action IEventBinding<T>.OnEventNoArgs
    {
        get => onEventNoargs;
        set => onEventNoargs = value;
    }

    public EventBinding(Action<T>onEvent)     => this.onEvent = onEvent;
    public EventBinding(Action onEventNoArgs) => this.onEventNoargs = onEventNoArgs;

    public void  Add(Action<T> onEvent) => this.onEvent += onEvent;
    public void Remove(Action<T> onEvent)=> this.onEvent -= onEvent;


    public void Add(Action onEventNoArgs) => this.onEventNoargs += onEventNoArgs;
    public void Remove(Action onEventNoArgs)=> this.onEventNoargs -= onEventNoArgs;

}

