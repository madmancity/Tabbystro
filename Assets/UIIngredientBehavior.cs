using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIIngredientBehavior : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public GameObject IngredientPrefab;
    private GameObject Ingredient;

    void Drag()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
        Ingredient.transform.position = mousePosition;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Drag();
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        Ingredient = GameObject.Instantiate(IngredientPrefab);
        Drag();
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        //GameObject.Destroy(Ingredient);
        //Ingredient = null;
    }
}
