using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectWithColliderUtil : MonoBehaviour
{
    public ObjectTypes objectType;

    
}

public enum ObjectTypes
{
    FoodItem,
    IngredientsView,
    OrderView,
    CombinationItem,
    Machine
}