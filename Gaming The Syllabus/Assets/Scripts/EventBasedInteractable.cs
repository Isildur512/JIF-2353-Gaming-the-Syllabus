using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class EventBasedInteractable : MonoBehaviour, IInteractable
{
    [Tooltip("Higher values are interacted with first.")]
    [SerializeField] private int interactionPriority;
    [SerializeField] private UnityEvent onInteract;

    public int InteractionPriority { get => interactionPriority; }

    public void Interact()
    {
        onInteract?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player.AddInteractable(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player.RemoveInteractable(this);
        }
    }
}
