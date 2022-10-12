using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Vector3 offset;

    void Update()
    {
        mainCamera.gameObject.transform.position = gameObject.transform.position + offset;
    }
}
