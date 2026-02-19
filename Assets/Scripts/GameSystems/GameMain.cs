using UnityEngine;
[DefaultExecutionOrder(999999)] // We want this last, and get order systems ready
public class GameMain: MonoBehaviour
{
    void Start()
    {
        // Set up pan
        // Set up ingredients
        // Set up player
        // Set up customers
        // Start countdown
        CustomerManager.SetResumeCustomerOrdering(true);
    }
    
}