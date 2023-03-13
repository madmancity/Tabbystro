using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientBehavior : MonoBehaviour
{
    public GameObject RecipePrefab; // This will be expanded upon, this is just for testing of combination

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!Input.GetMouseButton(0))
        {
            GameObject.Instantiate(RecipePrefab, collision.transform.localPosition, collision.transform.localRotation);
            GameObject.Destroy(collision.gameObject);
            GameObject.Destroy(gameObject);
        }
    }
}
