using UnityEngine;

public class TargetManipulatorScript : MonoBehaviour
{
    public Transform target;
    public Transform targetAttachPoint;

    private void Start()
    {
        if (targetAttachPoint == null) targetAttachPoint = transform;
    }

    private void LateUpdate()
    {
        if (!gameObject.activeSelf || !gameObject.activeInHierarchy) return;
        target.position = targetAttachPoint.position;
        target.rotation = targetAttachPoint.rotation;
    }
}
