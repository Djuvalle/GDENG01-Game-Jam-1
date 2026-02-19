using UnityEngine;
using System.Collections;
using GameEnum;
using System.Collections.Generic;
public class Customer: MonoBehaviour
{
    private static float MAX_PATIENCE_TIMER = 120f; //2 mins
    private List<FoodType> orderList;
    private float patienceTimer;
    public float orderCooldown { get; set; }
    public bool resumeOrderCooldown { get; set; } = false;
    private CustomerState state = CustomerState.Idle;
    public System.Action OnOrderReady;
    public System.Action<OrderState> OnLeaving;

    private void Update() {
        if (this.state == CustomerState.Order)
        {
            patienceTimer -= Time.deltaTime;
            if (patienceTimer <= 0) {
                this.Leave(OrderState.Failure);
            }
        }
        else if (this.state == CustomerState.Cooldown && this.resumeOrderCooldown)
        {
            orderCooldown -= Time.deltaTime;
            if (orderCooldown <= 0)
            {
                this.OrderReady();
            }
        }
    }
    private static List<FoodType> GenerateOrder() {
        List<FoodType> order = new List<FoodType>();
        order.Add(FoodType.Pancake); // Just a basic order for now
        return order;
    }
    public void StartOrdering()
    {
        this.orderList = GenerateOrder();
        this.patienceTimer = MAX_PATIENCE_TIMER;
        this.state = CustomerState.Order;
    }
    public void StartInCooldown()
    {
        this.state = CustomerState.Cooldown;
        this.resumeOrderCooldown = true;
    }
    private void OrderReady()
    {
        this.state = CustomerState.Idle;
        this.OnOrderReady?.Invoke();
    }
    private void Leave(OrderState orderState) {
        this.state = CustomerState.Leave;
        this.OnLeaving?.Invoke(orderState);
        this.state = CustomerState.Cooldown;
    }
    private void OnCollisionEnter(Collision collision)
    {
        Pancake pancake = collision.gameObject.GetComponent<Pancake>();
        // If it is not a food item or it is not cooked
        if (!pancake || !pancake.IsCookedProperly())
            return;
        
        FoodType food = FoodType.Pancake; // Change this later if we introduce more food
        // If customer is not ordering or the food is not in the list
        if (this.state != CustomerState.Order || !this.orderList.Contains(food))
        {
            // Do not accept food
            return;
        }

        this.orderList.Remove(food);
        if (this.orderList.Count == 0) {
            this.Leave(OrderState.Success);
        }
    }
}