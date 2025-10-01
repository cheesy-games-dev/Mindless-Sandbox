using UnityEngine;

public class Car : MonoBehaviour
{
    public Transform seat;
    public bool isSeated;
    //[SerializeField] private Transform carCameraPosition;
    public float horizontal, vertical;
    public GameObject[] enableGameObjectWhenSeated;

    // Car Physics
    public Rigidbody rb;

    public float forwardAccel = 8, reverseAccel = 4f, maxSpeed = 50f, turnStrength = 180, gravityForce = 10f, dragOnGround = 3f;

    private float speedInput, turnInput;

    private bool grounded = false;

    public LayerMask groundLayer;

    public float rayLength = 0.5f;

    public Transform rayPoint;

    public AudioSource engineSound;

    private void Start() {
        isSeated = false;
        engineSound.enabled = false;
        rb = GetComponent<Rigidbody>();
        engineSound = GetComponent<AudioSource>();
        transform.rotation = Quaternion.Euler(0 , transform.rotation.eulerAngles.y, 0);
    }

    public void ToggleSeat(int playerId = -1) {
        isSeated = !isSeated;
        playerId = -1;
    }

    private void Update()
    {
        speedInput = 0f;
        //ManagePassanger();
        foreach (GameObject gameObject in enableGameObjectWhenSeated)
        {
            gameObject.SetActive(isSeated);
        }
        {
            if (isSeated)
            {

                if (vertical > 0)
                {
                    speedInput = vertical * forwardAccel * 1000f;
                }
                else if (vertical < 0)
                {
                    speedInput = vertical * reverseAccel * 1000f;
                }
                turnInput = horizontal;

                if (grounded)
                {
                    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnStrength * Time.deltaTime, 0f));
                }
            }
        }
        if (!isSeated)
        {
            engineSound.enabled = false;
            speedInput = 0f;
        }
    }

    private void FixedUpdate() {
        grounded = false;
        RaycastHit hit;
        if (Physics.Raycast(rayPoint.position, -transform.up, out hit, rayLength, groundLayer)) {
            grounded = true;

            // transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }

        if (grounded) {
            rb.linearDamping = dragOnGround;
            if (Mathf.Abs(speedInput) > 0.25f) {
                rb.AddForce(transform.forward * speedInput);
                engineSound.pitch = 1.5f;
            }
            else {
                engineSound.pitch = 1;
            }
        }
        else {
            rb.linearDamping = 0.25f;
            rb.AddForce(Vector3.up * -gravityForce * 75f);
        }
    }

}
