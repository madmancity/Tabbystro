using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderBehavior : MonoBehaviour
{
    public TMP_Text orderField;
    public Image customer;
    private Order order;

    // Start is called before the first frame update
    void Start()
    {
        order = orders[Random.Range(0, orders.Length)];
        orderField.text = order.orderText;
    }

    public void orderUp(string foodItemName)
    {
        if (order == null) return;
        if (foodItemName.Equals(order.orderName))
        {
            orderField.text = correctResponses[Random.Range(0, correctResponses.Length)];
            ++LevelControls.completedOrders;
            StartCoroutine(waitToResetText(orders[Random.Range(0, orders.Length)]));
            order = null;
        }
        else
        {
            orderField.text = incorrectResponses[Random.Range(0, incorrectResponses.Length)];
            ++LevelControls.mistakes;
            StartCoroutine(waitToResetText(order));
            order = null;
        }
    }

    IEnumerator waitToResetText(Order order)
    {
        yield return new WaitForSeconds(3);
        orderField.text = order.orderText;
        this.order = order;
        int i = Random.Range(0, 3);
        switch (i)
        {
            case 0:
                customer.sprite = Resources.Load<Sprite>("Sprites/Boris");
                break;
            case 1:
                customer.sprite = Resources.Load<Sprite>("Sprites/Dolphin");
                break;
            case 2:
                customer.sprite = Resources.Load<Sprite>("Sprites/Terry");
                break;
        }
    }

    private Order[] orders = {
        new Order("I want a hot drink with no milk.", "Americano"),
        new Order("I just want a simple hot coffee.", "Americano"),
        new Order("I want an iced coffee, hold the milk.", "Iced Americano"),
        new Order("I just want a simple iced coffee.", "Iced Americano"),
        new Order("I知 craving a hot coffee with milk.", "Cafe au Lait"),
        new Order("I want a hot drink without water.", "Cafe au Lait"),
        new Order("I really want an iced coffee with milk.", "Iced Cafe au Lait"),
        new Order("I want an iced drink without water.", "Iced Cafe au Lait"),
        new Order("I want a frozen treat.", "Ice Cream"),
        new Order("I値l take a scoop of something cold.", "Ice Cream"),
        new Order("I知 craving a dessert coffee.", "Affogatto"),
        new Order("I値l take a coffee but with something extra cold in it.", "Affogatto")
    };

    private string[] correctResponses = {
        "Delicious, thank you!",
        "Perfect, thanks!",
        "This is just what I was craving!",
        "Thank you, this is perfect!",
        "Thanks!",
        "Thank you!"
    };

    private string[] incorrectResponses =
    {
        "That's not quite right...",
        "This isn't what I ordered, sorry.",
        "No, that isn't what I wanted.",
        "I didn't order that."
    };
}

public class Order
{
    public string orderText;
    public string orderName;

    public Order(string orderText, string orderName)
    {
        this.orderText = orderText;
        this.orderName = orderName;
    }
}