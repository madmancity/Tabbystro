using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MachineBehavior : MonoBehaviour
{
    [SerializeField]
    public List<Recipe> recipes = new List<Recipe>();

    public string useMachine(GameObject[] foodItems)
    {
        int i = 0;
        foreach (GameObject item in foodItems)
        {
            if (item != null)
            {
                ++i;
            }
        }
        foreach (Recipe recipe in recipes)
        {
            int count = 0;
            if (recipe.ingredients.Length != i) continue;
            foreach (string ingredient in recipe.ingredients)
            {
                foreach (GameObject ingredientItem in foodItems)
                {
                    if (ingredientItem != null && ingredientItem.name.Equals(ingredient))
                    {
                        ++count;
                        break;
                    }
                }
            }
            if (count == recipe.ingredients.Length)
            {
                return recipe.result;
            }
        }
        LevelControls.mistakes++;
        return "Mush";
    }
}

[System.Serializable]
public class Recipe
{
    [SerializeField]
    public string[] ingredients;
    [SerializeField]
    public string result;
}