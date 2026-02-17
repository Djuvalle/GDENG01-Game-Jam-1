using UnityEngine;
using UnityEngine.InputSystem;

class PlayerController : MonoBehaviour
{
    private static Vector3 CAMERA_OFFSET = new Vector3(0, 1, 0);
    [SerializeField] private InputActionReference iarForward;
    [SerializeField] private InputActionReference iarRightward;

    private float speed = 5f;
    private Rigidbody2D rb;
    private Camera cam;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    void Update()
    {
        float step = speed * Time.deltaTime;
        float forwardAxis = iarForward.action.ReadValue<float>();
        float rightwardAxis = iarRightward.action.ReadValue<float>();

        if (forwardAxis != 0)
        {
            Vector3 faceDir = this.transform.forward;
            faceDir = Vector3.Normalize(new Vector3(faceDir.x, 0, faceDir.z)); // Removes vertical direction
            this.transform.Translate(forwardAxis * step * faceDir);
        }

        if (rightwardAxis != 0)
        {
            Vector3 rightDir = this.transform.right;
            rightDir = Vector3.Normalize(new Vector3(rightDir.x, 0, rightDir.z)); // Removes vertical direction
            this.transform.Translate(rightwardAxis * step *rightDir);
        }

        cam.transform.position = this.transform.position + CAMERA_OFFSET;
    }
}