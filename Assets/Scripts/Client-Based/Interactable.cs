using System;
using System.Collections;
using System.Collections.Generic;
using UltEvents;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Interactable : MonoBehaviour {

    public UltEvent<Interactor> onPressedEvent;
    public UltEvent<Interactor> onPerformedEvent;
    public UltEvent<Interactor> onReleasedEvent;
    public UltEvent<Interactor> ToggleOn;
    public UltEvent<Interactor> ToggleOff;
    public UltEvent<Interactor, bool> Toggled;
    public bool isToggleInteractable;
    public bool isToggled = false;

    public void Pressed(Interactor interactor)
    {
        PressedCallback(interactor);
    }

    private void PressedCallback(Interactor interactor)
    {
        onPressedEvent.Invoke(interactor);
    }
    public void Performed(Interactor interactor)
    {
        PerformedCallback(interactor);
    }
    private void PerformedCallback(Interactor interactor) {
        onPerformedEvent.Invoke(interactor);
        if (isToggleInteractable) {
            Toggle(interactor);
        }
    }
    public void Released(Interactor interactor)
    {
        ReleasedCallback(interactor);
    }

    private void ReleasedCallback(Interactor interactor)
    {
        onReleasedEvent.Invoke(interactor);
    }

    public void Toggle(Interactor interactor)
    {
        ToggleCallback(interactor);
    }

    private void ToggleCallback(Interactor interactor)
    {
        isToggled = !isToggled;
        if (isToggled)
        {
            ToggleOn.Invoke(interactor);
        }
        else
        {
            ToggleOff.Invoke(interactor);
        }
        Toggled.Invoke(interactor,isToggled);
    }
}
