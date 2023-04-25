using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public class FoodItemBehavior : MonoBehaviour
{
    [SerializeField]
    public string FoodItemName;
    public AudioSource combine;
    public GameObject combinationPrefab;

    [HideInInspector]
    public float lastDrag;
    private bool isPlaced = false; // will become true once the object is placed into the game world. This is a workaround for the way OnMouseDrag works when an object is first created

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!Input.GetMouseButton(0))
        {
            CombinationBehavior b;
            SpriteRenderer r;
            switch (collision.GetComponent<ObjectWithColliderUtil>().objectType)
            {
                case ObjectTypes.IngredientsView:
                    break;
                case ObjectTypes.OrderView:
                    try
                    {
                        collision.GetComponent<OrderPanelBehavior>().orderUp(FoodItemName);
                    } catch { }
                    break;
                case ObjectTypes.CombinationItem:
                    b = collision.GetComponent<CombinationBehavior>();
                    if (b.i > 3) break;
                    b.foodItems[b.i] = new GameObject(FoodItemName);
                    b.foodItems[b.i].transform.localScale = new Vector2(0.25f, 0.25f);
                    r = b.foodItems[b.i].AddComponent<SpriteRenderer>();
                    r.sprite = Resources.Load<Sprite>($"Sprites/{FoodItemName}");
                    b.i++;
                    break;
                case ObjectTypes.FoodItem:
                    if (lastDrag > collision.GetComponent<FoodItemBehavior>().lastDrag)
                    {
                        GameObject combo = Instantiate(combinationPrefab);
                        b = combo.GetComponent<CombinationBehavior>();
                        b.foodItems[b.i] = new GameObject(FoodItemName);
                        b.foodItems[b.i].transform.localScale = new Vector2(0.25f, 0.25f);
                        r = b.foodItems[b.i].AddComponent<SpriteRenderer>();
                        r.sprite = Resources.Load<Sprite>($"Sprites/{FoodItemName}");
                        b.i++;
                        string otherName = collision.GetComponent<FoodItemBehavior>().FoodItemName;
                        b.foodItems[b.i] = new GameObject(otherName);
                        b.foodItems[b.i].transform.localScale = new Vector2(0.25f, 0.25f);
                        r = b.foodItems[b.i].AddComponent<SpriteRenderer>();
                        r.sprite = Resources.Load<Sprite>($"Sprites/{otherName}");
                        b.i++;
                        combo.transform.position = transform.position;
                    }
                    break;
                    //if (lastDrag > collision.GetComponent<FoodItemBehavior>().lastDrag)
                    //{
                    //    string otherName = collision.GetComponent<FoodItemBehavior>().FoodItemName;
                    //    if (FoodItemName.Equals("Mush") && otherName.Equals("Mush")) return;
                    //    GameObject result;
                    //    foreach (Combination combination in combinations)
                    //    {
                    //        if (combination.ingredientName.Equals(otherName))
                    //        {
                    //            combine.Play();
                    //            result = GameObject.Instantiate(Resources.Load<GameObject>("FoodItems/" + combination.resultName));
                    //            result.transform.position = transform.position;
                    //            Destroy(collision.gameObject);
                    //            Destroy(gameObject);
                    //            return;
                    //        }
                    //    }
                    //    ++LevelControls.mistakes;
                    //    result = GameObject.Instantiate(Resources.Load<GameObject>("FoodItems/Mush"));
                    //    result.transform.position = transform.position;
                    //    Destroy(collision.gameObject);
                    //}
            }
            Destroy(gameObject);
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
