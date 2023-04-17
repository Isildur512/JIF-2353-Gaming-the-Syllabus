using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.AI;

public class Player : Singleton<Player>
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rigidBody;

    [Header("Movement")]
    [SerializeField] private float movementSpeed = 5;
    public static bool CanMove = true;

    private Vector2 movementDirection;
    private List<IInteractable> interactablesWithinRange = new();

    private Vector2? destination;

    void OnEnable()
    {
        TouchSimulation.Enable();
        EnhancedTouchSupport.Enable();
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += OnTap;
    }

    private void OnTap(Finger finger)
    {
        Vector2 tappedLocation = Camera.main.ScreenToWorldPoint(finger.screenPosition);

        destination = tappedLocation;

        Collider2D interactableCollider = Physics2D.OverlapPoint(tappedLocation, LayerMask.GetMask("Interactable"));

        if (interactableCollider != null)
        {
            IInteractable interactable = interactableCollider.GetComponent<IInteractable>();
            if (interactablesWithinRange.Contains(interactable))
            {
                interactable.Interact();
            }
        }
    }

    private void Awake()
    {
        InitializeSingleton();
    }

    private void FixedUpdate()
    {
        if (CanMove)
        {
            if (destination == null)
            {
                rigidBody.MovePosition((Vector2)transform.position + movementDirection * movementSpeed * Time.deltaTime);
            } else
            {
                rigidBody.MovePosition(Vector2.MoveTowards(transform.position, (Vector2) destination, movementSpeed * Time.deltaTime));
            }
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
        destination = null;
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
