using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationBehavior : MonoBehaviour
{
    public GameObject[] foodItems = new GameObject[4];
    public int i = 0;

    // Update is called once per frame
    void Update()
    {
        if (foodItems[0] != null)
        {
            foodItems[0].transform.parent = transform;
            foodItems[0].transform.localPosition = new Vector2(-0.5f, 0.5f);
        }
        if (foodItems[1] != null)
        {
            foodItems[1].transform.parent = transform;
            foodItems[1].transform.localPosition = new Vector2(0.5f, 0.5f);
        }
        if (foodItems[2] != null)
        {
            foodItems[2].transform.parent = transform;
            foodItems[2].transform.localPosition = new Vector2(-0.5f, -0.5f);
        }
        if (foodItems[3] != null)
        {
            foodItems[3].transform.parent = transform;
            foodItems[3].transform.localPosition = new Vector2(0.5f, -0.5f);
        }
    }
    private void Drag()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
        transform.position = mousePosition;
    }

    private void OnMouseDrag()
    {
        Drag();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!Input.GetMouseButton(0))
        {
            switch (collision.GetComponent<ObjectWithColliderUtil>().objectType)
            {
                case ObjectTypes.FoodItem:
                    break;
                case ObjectTypes.Machine:
                    string result_name = collision.GetComponent<MachineBehavior>().useMachine(foodItems);
                    GameObject result = Instantiate(Resources.Load<GameObject>($"FoodItems/{result_name}"));
                    result.transform.position = collision.transform.position + new Vector3(0, 3, 0);
                    Destroy(gameObject);
                    break;
                default:
                    Destroy(gameObject);
                    break;
            }
        }
    }
}