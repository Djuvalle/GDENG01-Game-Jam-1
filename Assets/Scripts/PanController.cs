using System.Collections.Generic;
using UnityEngine;
using GameEnum;
using DG.Tweening;
public class PanController : MonoBehaviour
{
    private static float MIN_COOK_TIME = 5f;
    private static float MAX_COOK_TIME = 10f;
    private static List<IngredientType> ACCEPTABLE_INGREDIENTS = new List<IngredientType>() { IngredientType.Batter, IngredientType.Butter };
    
    private static List<IngredientType> currentIngredients = new List<IngredientType>();
    private GameObject pancake;
    private PancakeView pancakeView;
    private float cookTime1 = 0;
    private float cookTime2 = 0;
    private bool isFlipped = false;
    private void Start()
    {
        Debug.Log($"PanController {this.gameObject.name} is running");
        this.pancake = this.transform.Find("Pancake").gameObject;
        this.pancakeView = new PancakeView(this.pancake);
        this.ResetPan();
    }
    private void Update()
    {
        if (!currentIngredients.Contains(IngredientType.Batter)) {return;}
        if (!isFlipped)
        {
            cookTime1 += Time.deltaTime;
        }
        else
        {
            cookTime2 += Time.deltaTime;
        }

        if (!isFlipped && cookTime1 >= MAX_COOK_TIME || isFlipped && cookTime2 >= MAX_COOK_TIME)
        {
            Debug.Log("Pancake is burnt!");
            this.pancakeView.SetFoodState(FoodState.Burnt, isFlipped);
        }
        else if (!isFlipped && cookTime1 >= MIN_COOK_TIME || isFlipped && cookTime2 >= MIN_COOK_TIME)
        {
            Debug.Log("Pancake is cooked!");
            this.pancakeView.SetFoodState(FoodState.Cooked, isFlipped);
        }
    }
    private void ResetPan()
    {
        currentIngredients.Clear();
        this.pancakeView.Reset();
        this.cookTime1 = 0;
        this.cookTime2 = 0;
        this.isFlipped = false;
    }
    private void HandleIngredientAdded(IngredientType ingredientType)
    {
        if (ingredientType == IngredientType.Batter)
        {
            this.pancakeView.SetActive(true);
        }
        else if (ingredientType == IngredientType.Butter)
        {
            // TODO: Add butter visual effect
        }
    }   
    private void OnCollisionEnter(Collision collision)
    {
        Ingredient ingredient = collision.gameObject.GetComponent<Ingredient>();
        // Do not accept if ingredient is null, not acceptable, or already in the pan
        if (
            ingredient == null ||
            !ACCEPTABLE_INGREDIENTS.Contains(ingredient.IngredientType) ||
            currentIngredients.Contains(ingredient.IngredientType)
        )
        {
            return;
        }
        currentIngredients.Add(ingredient.IngredientType);
        this.HandleIngredientAdded(ingredient.IngredientType);
    }
    private void OnTriggerEnter(Collider other)
    {
        Ingredient ingredient = other.gameObject.GetComponent<Ingredient>();
        // Do not accept if ingredient is null, not acceptable, or already in the pan
        if (
            ingredient == null ||
            !ACCEPTABLE_INGREDIENTS.Contains(ingredient.IngredientType) ||
            currentIngredients.Contains(ingredient.IngredientType)
        )
        {
            return;
        }
        ingredient.ReturnIngredient();
        currentIngredients.Add(ingredient.IngredientType);
        this.HandleIngredientAdded(ingredient.IngredientType);
    }
    
}