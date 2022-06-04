using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent", fileName = "New Game event")]
public class GameEvent : ScriptableObject
{
    HashSet<GameEventListener> _listeners = new HashSet<GameEventListener>();

    public void FireEvent()
    {
        foreach (var listener in _listeners)
        {
            listener?.FireEvent();
        }
    }

    public void Register(GameEventListener gameEventListener) => _listeners.Add(gameEventListener);
    public void Unregister(GameEventListener gameEventListener) => _listeners.Remove(gameEventListener);
}
