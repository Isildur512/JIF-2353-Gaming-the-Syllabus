using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public int InteractionPriority { get; }

    public void Interact();
}
