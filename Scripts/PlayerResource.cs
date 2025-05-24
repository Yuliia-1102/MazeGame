using UnityEngine;
using UnityEngine.Events;

public class PlayerResource : MonoBehaviour
{
    public int NumberOfKeys {  get; private set; }

    public UnityEvent<PlayerResource> OnKeysCollectedEvent;

    public void KeysCollected()
    {
        NumberOfKeys++;
        OnKeysCollectedEvent.Invoke(this);
    }
}
