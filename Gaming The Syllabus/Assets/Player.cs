using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class Player : Singleton<Player>
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rigidBody;

    [Header("Movement")]
    [SerializeField] private float movementSpeed = 5;
    public static bool CanMove = true;

    private Vector2 movementDirection;
    private List<IInteractable> interactablesWithinRange = new();

    private void Awake()
    {
        InitializeSingleton();
    }

    private void FixedUpdate()
    {
        if (CanMove)
        {
            rigidBody.MovePosition((Vector2)transform.position + movementDirection * movementSpeed * Time.deltaTime);
        }
    }

    public static void AddInteractable(IInteractable interactable)
    {
        _instance.interactablesWithinRange.Add(interactable);
    }

    public static void RemoveInteractable(IInteractable interactable)
    {
        _instance.interactablesWithinRange.Remove(interactable);
    }

    private void OnMove(InputValue inputValue)
    {
        movementDirection = inputValue.Get<Vector2>();
    }

    private void OnInteract()
    {
        if (interactablesWithinRange.Count > 0)
        {
            int highestInteractionPriority = interactablesWithinRange.Max((interactable) => interactable.InteractionPriority);
            interactablesWithinRange
                .First((interactable) => interactable.InteractionPriority == highestInteractionPriority)
                .Interact();
        }
    }
}
