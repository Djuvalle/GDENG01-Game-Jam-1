namespace GameEnum
{
    public enum ActionEvent
    {
        IngredientAdded,
    }
    public enum ParameterKey
    {
        IngredientType,
    }
    public enum IngredientType
    {
        Flour,
        Egg,
        Batter,
        Butter
    }
    public enum FoodType
    {
        Pancake,
        ButteredPancake
    }
    public enum CustomerState
    {
        Idle,
        Order,
        Leave,
    }
    public enum ActionState
    {
        Success,
        Failure
    }
}