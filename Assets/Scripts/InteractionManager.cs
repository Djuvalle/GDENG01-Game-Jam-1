using Unity.VisualScripting;
using UnityEngine;
public class InteractionManager : MonoBehaviour
{
    private const float DISTANCE_FROM_CAMERA = 10f;
    public static InteractionManager Instance { get; private set; }
    private Camera cam;
    [SerializeField] private InputManager input;
    private GameObject currentGrabable;
    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        cam = Camera.main;
    }

    private void Update()
    {
        if (currentGrabable != null)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = DISTANCE_FROM_CAMERA;
            Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);
            currentGrabable.transform.position = worldPos;
        }
    }

    private void OnEnable()
    {
        input.OnInputDown += HandleClickDown;
        input.OnInputUp += HandleClickUp;
    }

    private void OnDisable()
    {
        input.OnInputDown -= HandleClickDown;
        input.OnInputUp -= HandleClickUp;
    }

    private void HandleClickDown(Vector2 mousePos)
    {
        if (currentGrabable != null) return;
        Ray ray = cam.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Clickable clickable = hit.transform.GetComponent<Clickable>();
            clickable?.OnClicked();
        }
    }

    private void HandleClickUp(Vector2 mousePos)
    {
        if (currentGrabable != null) return;
        Clickable clickable = currentGrabable.transform.GetComponent<Clickable>();
        clickable?.OnClickRelease();
        currentGrabable = null;
    }

    public static void GrabObject(GameObject obj)
    {
        Instance.currentGrabable = obj;
    }
}
