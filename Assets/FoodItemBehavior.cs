using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public class FoodItemBehavior : MonoBehaviour
{
    [SerializeField]
    public Combination[] combinations;
    public string FoodItemName;

    [HideInInspector]
    public float lastDrag;
    private bool isPlaced = false; // will become true once the object is placed into the game world. This is a workaround for the way OnMouseDrag works when an object is first created
    

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!Input.GetMouseButton(0))
        {
            if (lastDrag > collision.GetComponent<FoodItemBehavior>().lastDrag)
            {
                string otherName = collision.GetComponent<FoodItemBehavior>().FoodItemName;
                if (FoodItemName.Equals("Mush") && otherName.Equals("Mush")) return;
                GameObject result;
                foreach (Combination combination in combinations)
                {
                    if (combination.ingredientName.Equals(otherName))
                    {
                        result = GameObject.Instantiate(Resources.Load<GameObject>("FoodItems/" + combination.resultName));
                        result.transform.position = transform.position;
                        Destroy(collision.gameObject);
                        Destroy(gameObject);
                        return;
                    }
                }
                result = GameObject.Instantiate(Resources.Load<GameObject>("FoodItems/Mush"));
                result.transform.position = transform.position;
                Destroy(collision.gameObject);
                Destroy(gameObject);
                //GameObject.Instantiate(RecipePrefab, collision.transform.localPosition, collision.transform.localRotation);
            }
        }
    }

    private void Update()
    {
        if (!isPlaced)
        {
            if (Input.GetMouseButton(0)) Drag();
            else isPlaced = true;
        }
    }

    private void Drag()
    {
        lastDrag = Time.time;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
        transform.position = mousePosition;
    }

    private void OnMouseDrag()
    {
        Drag();
    }
}
