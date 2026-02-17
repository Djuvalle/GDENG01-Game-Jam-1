using UnityEngine;
using GameEnum;
using System;

public class GlobalObjectPools : MonoBehaviour
{
    public static GlobalObjectPools Instance { get; private set; }
    private ObjectPool BatterPool { get; set; }
    private ObjectPool EggPool { get; set; }
    private ObjectPool FlourPool { get; set; }
    private ObjectPool ButterPool { get; set; }
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        string PREFAB_PATH = "Prefabs/Ingredients/";

        this.BatterPool = new ObjectPool(Resources.Load<GameObject>(PREFAB_PATH + "Batter"));
        this.EggPool = new ObjectPool(Resources.Load<GameObject>(PREFAB_PATH + "Egg"));
        this.FlourPool = new ObjectPool(Resources.Load<GameObject>(PREFAB_PATH + "Flour"));
        this.ButterPool = new ObjectPool(Resources.Load<GameObject>(PREFAB_PATH + "Butter"));
        Instance = this;
    }

    public static ObjectPool GetPoolByIngredientType(IngredientType type)
    {
        switch (type)
        {
            case IngredientType.Batter:
                return Instance.BatterPool;
            case IngredientType.Egg:
                return Instance.EggPool;
            case IngredientType.Flour:
                return Instance.FlourPool;
            case IngredientType.Butter:
                return Instance.ButterPool;
            default:
                Debug.LogError($"No pool found for ingredient type: {type}");
                return null;
        }
    }
}
