using GameEnum;
using UnityEngine;
public class Ingredient : MonoBehaviour, Clickable
{
    [SerializeField] private IngredientType ingredientType;
    public IngredientType IngredientType { get {return ingredientType; } set { ingredientType = value; }}
    public void ReturnIngredient()
    {
        ObjectPool pool = GlobalObjectPools.GetPoolByIngredientType(ingredientType);
        pool.ReturnObject(this.gameObject);
    }

    public void OnClicked()
    {
        Debug.Log($"Ingredient {this.gameObject.name} was clicked");
        InteractionManager.GrabObject(this.gameObject);
    }

    public void OnClickRelease()
    {
        Debug.Log($"Ingredient {this.gameObject.name} click released");
        InteractionManager.ReleaseObject(this.gameObject);
    }
}