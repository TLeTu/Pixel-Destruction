using System;
using System.Collections.Generic;

/// <summary>
/// Một Event Bus generic, type-safe để giao tiếp giữa các hệ thống.
/// Sử dụng các struct kế thừa từ IGameEvent để định nghĩa sự kiện.
/// </summary>
public static class GameEvents
{
    // Dictionary lưu trữ các listener cho từng loại sự kiện.
    private static readonly Dictionary<Type, Action<IGameEvent>> s_events = new Dictionary<Type, Action<IGameEvent>>();

    // Dictionary tra cứu để giúp việc hủy đăng ký (Unsubscribe) dễ dàng hơn.
    private static readonly Dictionary<Delegate, Action<IGameEvent>> s_eventLookups = new Dictionary<Delegate, Action<IGameEvent>>();

    /// <summary>
    /// Đăng ký lắng nghe một loại sự kiện cụ thể.
    /// </summary>
    /// <param name="listener">Hàm sẽ được gọi khi sự kiện được phát.</param>
    /// <typeparam name="T">Loại sự kiện để lắng nghe.</typeparam>
    public static void Subscribe<T>(Action<T> listener) where T : IGameEvent
    {
        if (!s_eventLookups.ContainsKey(listener))
        {
            Action<IGameEvent> newAction = (e) => listener((T)e);
            s_eventLookups[listener] = newAction;

            if (s_events.TryGetValue(typeof(T), out var internalAction))
                s_events[typeof(T)] = internalAction + newAction;
            else
                s_events[typeof(T)] = newAction;
        }
    }

    /// <summary>
    /// Hủy đăng ký lắng nghe một sự kiện.
    /// </summary>
    public static void Unsubscribe<T>(Action<T> listener) where T : IGameEvent
    {
        if (s_eventLookups.TryGetValue(listener, out var action))
        {
            if (s_events.TryGetValue(typeof(T), out var tempAction))
            {
                tempAction -= action;
                if (tempAction == null)
                    s_events.Remove(typeof(T));
                else
                    s_events[typeof(T)] = tempAction;
            }
            s_eventLookups.Remove(listener);
        }
    }

    /// <summary>
    /// Phát một sự kiện đến tất cả các listener đã đăng ký.
    /// </summary>
    public static void Publish<T>(T e) where T : IGameEvent
    {
        if (s_events.TryGetValue(typeof(T), out var action))
            action?.Invoke(e);
    }
}