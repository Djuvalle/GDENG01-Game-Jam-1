using UnityEngine;
using GameEnum;
public class CustomerManager : MonoBehaviour
{
    [SerializeField] private GameObject customerStart;
    [SerializeField] private GameObject seat1;
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
        Debug.Log($"Running CustomerManager with {lenght} Customers");

        float MIN_ORDER_COOLDOWN = 20; // 60
        float MAX_ORDER_COOLDOWN = 40; // 120
        for (int i = 0; i < lenght; i++)
        {
            GameObject customerObj = this.Customers[i];
            //Renderer renderer = customerObj.GetComponent<MeshRenderer>();
            GameObject bone = customerObj.transform.Find("Bone").gameObject;
            Customer customer = customerObj.GetComponent<Customer>();
            customer.OnOrderReady += () =>
            {
                Debug.Log($"On Order Ready Invoked");
                //renderer.enabled = true;
                bone.SetActive(true);
                customer.StartOrdering();
                customer.orderCooldown = Random.Range(MIN_ORDER_COOLDOWN, MAX_ORDER_COOLDOWN);
                customer.MoveToPoint(seat1.transform.position, new Vector3(-90, 180),null);
                //renderer.enabled = true;
            };
            customer.OnLeaving += (orderState) =>
            {
                Debug.Log($"Customer {i}: Leaving with order state: {orderState}");
                customer.MoveToPoint(customerStart.transform.position, new Vector3(-90, 0), () => {
                    //renderer.enabled = false;
                    bone.SetActive(false);
                });
                
            };
            customer.orderCooldown = 10 + i * 30;
            customer.transform.position = customerStart.transform.position;
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