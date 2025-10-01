using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class SpawnMenu : MonoBehaviour
{
    public static SpawnMenu Instance;

    [SerializeField]
    GameObject spawnMenu;

    [SerializeField]
    public InputActionProperty spawnMenuInput;

    [SerializeField]
    private EventSystem eventSystem;

    [SerializeField]
    GameObject objectStarterButton;

    public bool isMenuOpened;

    private void Awake() {
        Instance = this;
        isMenuOpened = false;
        spawnMenu.SetActive(false);
    }
    void Update()
    {
        if (Pause.Instance.isPaused) {
            isMenuOpened = false;
            return;
        }
        spawnMenu.SetActive(isMenuOpened);
        if (spawnMenuInput.action.WasPressedThisFrame()) {
            isMenuOpened = !isMenuOpened;
            if (isMenuOpened) {
                eventSystem.SetSelectedGameObject(objectStarterButton);
                Cursor.lockState = CursorLockMode.None;
            }
            else {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        if (Pause.Instance.isPaused) {
            isMenuOpened = false;
        }
        if(eventSystem == null) {
            eventSystem = FindAnyObjectByType<EventSystem>();
        }       
    }

    public void ToggleInventory(bool value) {
        isMenuOpened = value;
        if (isMenuOpened) {
            eventSystem.SetSelectedGameObject(objectStarterButton);
            Cursor.lockState = CursorLockMode.None;
        }
        else {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
