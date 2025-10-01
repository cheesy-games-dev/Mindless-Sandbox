using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    public InputActionProperty ClickInput;
    public Transform Target;
    public float Range;
    void Update()
    {
        Ray ray = new()
        {
            origin = Target.position,
            direction = Target.forward,
        };
        Physics.Raycast(ray, hitInfo: out var hit, Range, Physics.AllLayers, QueryTriggerInteraction.Collide);
        if (!hit.collider.TryGetComponent(out Interactable interactable)) return;
        if (ClickInput.action.WasPressedThisFrame()) Press(interactable);
        if (ClickInput.action.WasPerformedThisFrame()) Perform(interactable);
        if (ClickInput.action.WasReleasedThisFrame()) Release(interactable);
    }
    public void Press(Interactable interactable)
    {
        interactable.Pressed(this);
    }
    public void Perform(Interactable interactable)
    {
        interactable.Performed(this);
    }
    public void Release(Interactable interactable)
    {
        interactable.Released(this);
    }
}
