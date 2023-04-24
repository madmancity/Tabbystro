using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseRecipeBook : MonoBehaviour
{
    public GameObject CloseBook;
    public void ClickX()
    {
        if (CloseBook != null)
        {
            CloseBook.SetActive(true);
        }
    }
}
