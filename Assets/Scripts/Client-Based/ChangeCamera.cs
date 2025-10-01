using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeCamera : MonoBehaviour
{
    [SerializeField] private GameObject fpsCamera;
    [SerializeField] private GameObject tpsCamera;
    bool thirdPerson;

    public InputActionProperty changeCameraInput;

    private void Update() {
        fpsCamera.SetActive(!thirdPerson);
        tpsCamera.SetActive(thirdPerson);
        tpsCamera.transform.position = fpsCamera.transform.position;
        tpsCamera.transform.eulerAngles = new Vector3(fpsCamera.transform.eulerAngles.x, fpsCamera.transform.eulerAngles.y, 0);
        if (changeCameraInput.action.WasPressedThisFrame()) TogglePerspective(!thirdPerson);
    }

    public void TogglePerspective(bool toggleTPS) {
        thirdPerson = toggleTPS;
    }
}
