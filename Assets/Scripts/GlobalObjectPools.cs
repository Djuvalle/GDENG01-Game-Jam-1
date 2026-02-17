using UnityEngine;
using GameEnum;
using System.Collections.Generic;

public class GlobalObjectPools : MonoBehaviour
{
    public static GlobalObjectPools Instance { get; private set; }
    public static ObjectPool BatterPool { get; private set; }
    public static ObjectPool EggPool { get; private set; }
    public static ObjectPool FlourPool { get; private set; }
    public static ObjectPool ButterPool { get; private set; }
    
    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject); // Optional: keep between scenes
        
        // Load prefabs directly - no 'type' variable needed
        string PREFAB_PATH = "Prefabs/Ingredients/";
        
        BatterPool = new ObjectPool(Resources.Load<GameObject>(PREFAB_PATH + "Batter"));
        EggPool = new ObjectPool(Resources.Load<GameObject>(PREFAB_PATH + "Egg"));
        FlourPool = new ObjectPool(Resources.Load<GameObject>(PREFAB_PATH + "Flour"));
        ButterPool = new ObjectPool(Resources.Load<GameObject>(PREFAB_PATH + "Butter"));
    }

    public static ObjectPool GetPoolByIngredientType(IngredientType type)
    {
        switch (type)
        {
            case IngredientType.Batter:
                return BatterPool;
            case IngredientType.Egg:
                return EggPool;
            case IngredientType.Flour:
                return FlourPool;
            case IngredientType.Butter:
                return ButterPool;
            default:
                Debug.LogError($"No pool found for ingredient type: {type}");
                return null;
        }
    }
}
