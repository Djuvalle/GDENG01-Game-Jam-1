using UnityEngine;
using GameEnum;

public class ProximityPrompt : MonoBehaviour
{
    private static float ACTIVATION_RANGE = 3;
    public System.Action<Vector3> OnInteract;
    private Camera cam;
    private bool isEnabled = true;
    private bool isInView = false;
    public void OnInteracted(Parameters parameters)
    {
        float x = parameters.GetFloatExtra("x", 0);
        float y = parameters.GetFloatExtra("y", 0);
        float z = parameters.GetFloatExtra("z", 0);
        Vector3 sourcePos = new Vector3(x, y, z);
        Debug.Log($"ProximityPrompt received event with parameter value: {sourcePos}");

        // Do not accept if too far
        if ((this.transform.position - sourcePos).magnitude > ACTIVATION_RANGE)
        {
            return;
        }
    }
    private void Start()
    {
        Debug.Log("ProximityPrompt is running");
        this.cam = Camera.main;
        EventBroadcaster.Instance.AddObserver(ActionEvent.Interacted.ToString(), this.OnInteracted);



        Debug.Log("ProximityPrompt is finished");
    }

    private void Update()
    {
        Vector3 viewPos = cam.WorldToViewportPoint(this.transform.position);
        if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1)
            this.isInView = true;
        else
            this.isInView = false;

        if (this.isEnabled && this.isInView && (this.transform.position - cam.transform.position).magnitude <= ACTIVATION_RANGE)
            this.GetComponent<Canvas>().enabled = true;
        else
            this.GetComponent<Canvas>().enabled = false;

        Debug.Log($"Is in view: {this.isInView}; Is Enabled: {this.isEnabled}; Canvas is in range: {this.GetComponent<Canvas>().enabled}");
    }

    public void SetEnabled(bool state)
    {
        this.isEnabled = state;
    }
}