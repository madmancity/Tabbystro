using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeBook : MonoBehaviour
{
    public GameObject OpenBook;
    public void ClickBook()
    {
        if (OpenBook != null)
        {
            OpenBook.SetActive(true);
        }
    }
}
