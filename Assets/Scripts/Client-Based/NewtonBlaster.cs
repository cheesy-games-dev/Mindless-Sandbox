/*
using MindlessSDK;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewtonBlaster : MonoBehaviour
{
    public Animator holderAnimator;
    [SerializeField] float maxGrabDistance = 10f, throwForce = 20f;
    [SerializeField] Transform objectHolder;
    [SerializeField] InputActionProperty grabInput;
    [SerializeField] InputActionProperty launchInput;
    [SerializeField] InputActionProperty rotateInput;
    [SerializeField] ParticleSystem newtonParticle;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] Transform gunTip;
    [SerializeField] Material multiColorMaterial;
    [SerializeField] Color grabColor, deleteColor;
    public ConfigurableJoint grabJoint;
    private Vector3 currentLinePosition => grabJoint.connectedBody?grabJoint.connectedBody.position:gunTip.position;
    bool isGrabbed => grabJoint.connectedBody;
    bool deleteMode = false;
    AudioSource audioSource;

    private void Awake() {
        deleteMode = false;
        audioSource = GetComponent<AudioSource>();
    }
    private void OnDisable() {
        Stop();
        deleteMode = false;
    }
    private void LateUpdate() {
        if(grabJoint.connectedBody != null) {
            lineRenderer.enabled = true;
            DrawLine();
        }
        else {
            lineRenderer.SetPosition(0, gunTip.position);
            lineRenderer.SetPosition(1, gunTip.position);
            lineRenderer.enabled = false;
        }
    }
    void Update()
    {
        holderAnimator.SetBool("Reload", false);
        if (Pause.UIActive) return;
        if (rotateInput.action.phase == InputActionPhase.Performed)
        {
            var rotation = grabJoint.targetRotation;
            rotation.y += 150f * Time.deltaTime;
            grabJoint.targetRotation = rotation;
        }
        if (deleteMode)
        {
            multiColorMaterial.color = deleteColor;
            multiColorMaterial.SetColor("_EmissionColor", deleteColor);
        }
        else
        {
            multiColorMaterial.color = grabColor;
            multiColorMaterial.SetColor("_EmissionColor", grabColor);
        }

        if (grabInput.action.WasPerformedThisFrame())
        {
            if (grabJoint.connectedBody != null)
                Stop();
            else
                Grab();
        }

        if (launchInput.action.WasPerformedThisFrame())
        {
            if (grabJoint.connectedBody != null)
                LaunchObject();
            else
                Stop();
        }

    }

    void LaunchObject() {
        audioSource.Play();
        grabJoint.connectedBody.AddForce(-PlayerMovement.Instance.playerCam.eulerAngles * throwForce, ForceMode.VelocityChange);
    }

    void Stop()
    {
        if (!grabJoint.connectedBody)
        {
            return;
        }
        newtonParticle.Stop();
        grabJoint.connectedBody = null;
    }

    void Grab() {
        RaycastHit hit;
        if (Physics.Raycast(Pla.position, target.forward, out hit, maxGrabDistance)) {
            var body = hit.collider.attachedRigidbody.GetComponent<MindlessBody>();
            if (body != null) {
                if (deleteMode) {
                    Destroy(body.gameObject);
                }
                else {
                    newtonParticle.Play();

                    currentLinePosition = gunTip.position;
                }
            }
        }
    }

    void DrawLine() {
        currentLinePosition = Vector3.Lerp(currentLinePosition, linePoint, Time.deltaTime * 75f);

        lineRenderer.SetPosition(0, gunTip.position);
        lineRenderer.SetPosition(1, currentLinePosition);
    }
}

*/