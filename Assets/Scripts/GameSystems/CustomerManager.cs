using UnityEngine;
using GameEnum;
public class CustomerManager : MonoBehaviour
{
    public static CustomerManager Instance;
    private GameObject[] Customers;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    private void Start()
    {
        this.Customers = GameObject.FindGameObjectsWithTag("Customer");
        int lenght = this.Customers.Length;

        float MIN_ORDER_COOLDOWN = 20; // 60
        float MAX_ORDER_COOLDOWN = 40; // 120
        for (int i = 0; i < lenght; i++)
        {
            GameObject customerObj = this.Customers[i];
            Renderer renderer = customerObj.GetComponent<MeshRenderer>();
            Customer customer = customerObj.GetComponent<Customer>();
            customer.OnOrderReady += () =>
            {
                customer.orderCooldown = Random.Range(MIN_ORDER_COOLDOWN, MAX_ORDER_COOLDOWN);
                customer.StartOrdering();
                renderer.enabled = true;
            };
            customer.OnLeaving += (orderState) =>
            {
                Debug.Log($"Customer {i}: Leaving with order state: {orderState}");
                renderer.enabled = false;
            };
            customer.orderCooldown = i * 30;
            customer.StartInCooldown();
        }
    }
    public static void SetResumeCustomerOrdering(bool state)
    {
        foreach (GameObject customer in Instance.Customers)
        {
            customer.GetComponent<Customer>().resumeOrderCooldown = state;
        }
    }
}