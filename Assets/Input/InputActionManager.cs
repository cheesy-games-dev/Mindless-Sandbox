using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputActionManager : MonoBehaviour
{
    public InputActionAsset[] Actions;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        foreach(var action in Actions)
        {
            action.Enable();
        }
    }
}
