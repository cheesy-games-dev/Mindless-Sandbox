using UnityEngine;
using UnityEngine.InputSystem;

public class NoclipMode : MonoBehaviour
{
    private bool noclip = false;

    public InputActionProperty moveInput;

    public InputActionProperty noclipButton;

    public float speed = 2f;

    public Rigidbody rb;

    public Transform moveDirection;

    public bool canExecuteInFixedUpdate;

    private Vector2 moveVector = new Vector2();

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        noclip = false;    
    }

    Vector3 move;

    void LateUpdate()
    {
        if(noclipButton.action.WasPressedThisFrame()){
            noclip = !noclip;
        }
        canExecuteInFixedUpdate = true;
        rb.isKinematic = noclip;

        float x = Mathf.RoundToInt(moveInput.action.ReadValue<Vector2>().x);
        float z = Mathf.RoundToInt(moveInput.action.ReadValue<Vector2>().y);
        moveVector.x = x;
        moveVector.y = z ;
        move = (moveDirection.right * moveVector.x) + (moveDirection.forward * moveVector.y);
        //move = new Vector3(moveVector.x, 0 , moveVector.y);
        //move = moveDirection.right * moveVector.x + moveDirection.forward * moveVector.y;
    }

    void FixedUpdate(){
        if(!canExecuteInFixedUpdate) return;
        if(noclip) {
            
            //GetComponent<Rigidbody>().position += moveDirection.forward * (moveVector.y * speed) * Time.deltaTime;
            //GetComponent<Rigidbody>().position += moveDirection.right * (moveVector.x * speed) * Time.deltaTime;
            GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + (move * speed) * Time.deltaTime);
        }
    }
}
