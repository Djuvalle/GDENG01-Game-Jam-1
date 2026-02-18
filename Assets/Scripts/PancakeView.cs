using UnityEngine;
using GameEnum;
public class PancakeView
{
    private static string MATERIAL_PATH = "Materials/";
    private static Material matRaw = Resources.Load<Material>(MATERIAL_PATH + "pancake_raw");
    private static Material matCooked = Resources.Load<Material>(MATERIAL_PATH + "pancake_cooked");
    private static Material matBurnt = Resources.Load<Material>(MATERIAL_PATH + "pancake_burnt");
    private GameObject MainObject;
    private GameObject Top;
    private GameObject Bot;
    public PancakeView(GameObject gameObject)
    {
        this.MainObject = gameObject;
        this.Top = gameObject.transform.Find("Top").gameObject;
        this.Bot = gameObject.transform.Find("Bot").gameObject;
        this.Reset();
    }

    public void Reset()
    {
        this.SetActive(false);
        this.SetFoodState(FoodState.Raw, false);
        this.SetFoodState(FoodState.Raw, true);
    }
    public void SetActive(bool state)
    {
        this.MainObject.SetActive(state);
    }
    public void SetFoodState(FoodState state, bool isFlipped)
    {
        Material targetMat;

        switch (state)
        {
            case FoodState.Raw:
                targetMat = matRaw;
                break;
            case FoodState.Cooked:
                targetMat = matCooked;
                break;
            case FoodState.Burnt:
                targetMat = matBurnt;
                break;
            default:
                targetMat = matRaw;
                break;
        }

        if (!isFlipped)
            this.Top.GetComponent<Renderer>().material = targetMat;
        else
            this.Bot.GetComponent<Renderer>().material = targetMat;
    }
}