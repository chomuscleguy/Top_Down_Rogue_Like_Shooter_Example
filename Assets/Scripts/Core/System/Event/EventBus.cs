using System;
using System.Collections.Generic;

public static class EventBus
{
    private static Dictionary<Type, Delegate> events = new();

    public static void Subscribe<T>(Action<T> callback)
    {
        var type = typeof(T);

        if (!events.ContainsKey(type))
            events[type] = null;

        events[type] = Delegate.Combine(events[type], callback);
    }

    public static void Unsubscribe<T>(Action<T> callback)
    {
        var type = typeof(T);

        if (!events.ContainsKey(type)) return;

        events[type] = Delegate.Remove(events[type], callback);
    }

    public static void Publish<T>(T evt)
    {
        var type = typeof(T);

        if (events.TryGetValue(type, out var del))
        {
            (del as Action<T>)?.Invoke(evt);
        }
    }
}