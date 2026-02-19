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

        float MIN = 60, MAX = 120;
        for (int i = 0; i < lenght; i++)
        {
            Customer customer = this.Customers[i].GetComponent<Customer>();
            customer.OnOrderReady += () =>
            {
                customer.orderCooldown = Random.Range(MIN, MAX);
                customer.StartOrdering();
            };
            customer.OnLeaving += (orderState) =>
            {
                Debug.Log($"Customer {i}: Leaving with order state: {orderState}");
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