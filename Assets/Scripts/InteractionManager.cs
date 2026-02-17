using Unity.VisualScripting;
using UnityEngine;
public class InteractionManager : MonoBehaviour
{
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
