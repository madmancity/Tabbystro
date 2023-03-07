using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIFoodItemBehavior : MonoBehaviour, IPointerDownHandler
{
    public GameObject FoodItemPrefab;

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
        GameObject obj = GameObject.Instantiate(FoodItemPrefab);
        obj.transform.position = mousePosition;
    }
}
