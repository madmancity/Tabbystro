using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OrderPanelBehavior : MonoBehaviour
{
    public TMP_Text orderField;
    public Image customer;
    private Order order;
    private string customerName;

    public Image ringBar;
    private float maxTime;
    private float halfTime;
    private float crunchTime;
    private float timeLeft;

    private bool isTalking;
    private bool isLeaving;
    private bool wasServed;

    void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("MainLevel"))
        {
            maxTime = 30;
        }
        else if (SceneManager.GetActiveScene().name.Equals("MainLevel1"))
        {
            maxTime = 40 - (0.125f * (LevelControls.maxTime - LevelControls.timeRemaining));
        }
        else if (SceneManager.GetActiveScene().name.Equals("MainLevel2"))
        {
            maxTime = 20 - (0.125f * (LevelControls.maxTime - LevelControls.timeRemaining));
        }
        else if (SceneManager.GetActiveScene().name.Equals("Freeplay"))
        {
            maxTime = 6000000;
            ringBar.enabled = false;
        }
        newArrival();
        ringBar.color = Color.green;
        halfTime = maxTime * 0.666f;
        crunchTime = maxTime * 0.333f;
        timeLeft = maxTime;

        isTalking = false;
        isLeaving = false;
        wasServed = false;
    }

    void Update()
    {
        if (isLeaving)
        {
            transform.position = Vector2.MoveTowards(transform.position, transform.position + new Vector3(-750, 0, 0), 50 * Time.deltaTime);
            if (transform.localPosition.x <= -700)
            {
                OrdersBehavior.orders.Remove(gameObject);
                if (!wasServed)
                {
                    ++LevelControls.mistakes;
                }
                Destroy(gameObject);
            }
        }
        if (!wasServed)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                ringBar.fillAmount = timeLeft / maxTime;
            }
            else
            {
                StartCoroutine(waitFor(1, () => {
                    isLeaving = true;
                }));
                isTalking = true;
            }
            if (timeLeft <= crunchTime && ringBar.color.Equals(Color.yellow))
            {
                ringBar.color = Color.red;
                customer.sprite = Resources.Load<Sprite>($"Sprites/Busts/{customerName} Upset Profile");
            }
            else if (timeLeft <= halfTime && ringBar.color.Equals(Color.green))
            {
                ringBar.color = Color.yellow;
                customer.sprite = Resources.Load<Sprite>($"Sprites/Busts/{customerName} Profile");
            }
        }
    }

    public void orderUp(string foodItemName)
    {
        if (isTalking) return;
        if (customerName.Equals("Leon") && foodItemName.Equals("Milk"))
        {
            orderField.text = "Oh, how did you know I loved milk? I'll put in a good word with Pierre...";
            LevelControls.mistakes = -1;
            StartCoroutine(waitFor(1, () => {
                isLeaving = true;
            }));
            isTalking = true;
            wasServed = true;
        }
        else if (foodItemName.Equals(order.orderName))
        {
            orderField.text = correctResponses[UnityEngine.Random.Range(0, correctResponses.Length)];
            StartCoroutine(waitFor(1, () => {
                isLeaving = true;
            }));
            isTalking = true;
            wasServed = true;
        }
        else
        {
            orderField.text = incorrectResponses[UnityEngine.Random.Range(0, incorrectResponses.Length)];
            StartCoroutine(waitFor(2, () =>
            {
                orderField.text = order.orderText;
                isTalking = false;
            }));
            isTalking = true;
        }
    }

    private void newArrival()
    {
        order = orders[UnityEngine.Random.Range(0, orders.Length)];
        orderField.text = order.orderText;
        int i = UnityEngine.Random.Range(0, 9);
        switch (i)
        {
            case 0:
                customerName = "BC1";
                break;
            case 1:
                customerName = "Boris";
                break;
            case 2:
                customerName = "Bun";
                break;
            case 3:
                customerName = "Gabriella";
                break;
            case 4:
                customerName = "Hugh";
                break;
            case 5:
                customerName = "Hughbert";
                break;
            case 6:
                customerName = "Hughie";
                break;
            case 7:
                customerName = "Leon";
                break;
            case 8:
                customerName = "Terry";
                break;
        }
        customer.sprite = Resources.Load<Sprite>($"Sprites/Busts/{customerName} Happy Profile");
    }

    IEnumerator waitFor(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action();
    }

    private Order[] orders = {
        new Order("I want a hot drink with no milk", "Americano"),
        new Order("I just want a simple hot coffee", "Americano"),
        new Order("Could you please prepare a hot drink without milk for me?", "Americano"),
        new Order("I would like a hot beverage that does not contain any milk, please", "Americano"),

        new Order("I want an iced coffee, hold the milk", "Iced Americano"),
        new Order("I just want a simple iced coffee", "Iced Americano"),
        new Order("I would like to have an iced coffee, but without milk, please", "Iced Americano"),
        new Order("Could you please give me an iced coffee without any milk?", "Iced Americano"),

        new Order("I'm craving a hot coffee with milk", "Cafe au Lait"),
        new Order("I want a hot drink without water", "Cafe au Lait"),
        new Order("I have a strong desire for a coffee that is hot and contains milk", "Cafe au Lait"),
        new Order("I really want a coffee that is both hot and has milk in it", "Cafe au Lait"),

        new Order("I really want an iced coffee with milk", "Iced Cafe au Lait"),
        new Order("I want an iced drink without water", "Iced Cafe au Lait"),
        new Order("I'm craving a coffee that is chilled and has milk in it", "Iced Cafe au Lait"),
        new Order("I really want a coffee that has milk in it and is served over ice", "Iced Cafe au Lait"),

        new Order("I want a frozen treat", "Ice Cream"),
        new Order("I'll take a scoop of something cold", "Ice Cream"),
        new Order("I would love some frozen milk", "Ice Cream"),
        new Order("May I please have that desert that is kept frozen?", "Ice Cream"),

        new Order("I'm craving a coffee with something extra cold and sweet", "Affogatto"),
        new Order("I'll take a coffee but with a frozen treat in it", "Affogatto"),
        new Order("I'd really love a scoop of something cold with some espresso poured over it", "Affogatto"),

        new Order("Could I please have that flaky bread?", "Croissant"),
        new Order("Could I get something shaped like the crescent moon?", "Croissant"),
        new Order("I'd like a small, fun-shaped bread please", "Croissant"),
        new Order("Could you give me the moon?", "Croissant"),

        new Order("I'd like a long bread please", "Baguette"),
        new Order("I'd like some bread please, but not flaky", "Baguette"),
        new Order("I'd love some bread but I'm terrified of crescent shapes..", "Baguette"),
        new Order("I'd like my food in the shape of a sword", "Baguette"),

        new Order("Can I have a meaty sandwich with cheese on top?", "Croque Monseiur"),
        new Order("Meaty sandwich please, with extra cheese!", "Croque Monseiur"),
        new Order("May I request a sandwich with meat and a layer of cheese on the top?", "Croque Monseiur"),
        new Order("Could I please have a hot sandwich with layers of flavors and a cheesy topping?", "Croque Monseiur"),

        new Order("Please give me a filling sandwich with all the fixings including an egg", "Croque Madame"),
        new Order("I'd love a ham and cheese sandwich but I'd like there to be something extra on top", "Croque Madame"),
        new Order("I'm hungry, I need something super filling, put an egg on top too", "Croque Madame"),

        new Order("Do you have anything I can eat in one bite without meat?", "Egg Bite"),
        new Order("I don't eat meat but hate bread, what do you have for me?", "Egg Bite"),
        new Order("I'm in a rush, need a quick bite but hate meat", "Egg Bite"),

        new Order("I'm in a rush, need a quick bite with lots of meat", "Ham Cheese Egg Bite"),
        new Order("Do you have anything that includes meat that I can eat in one bite?", "Ham Cheese Egg Bite"),
        new Order("I'm not a fan of bread but love meat and eggs, any suggestions?", "Ham Cheese Egg Bite"),
        new Order("Give me something high in protein with ham and low in carbs.", "Ham Cheese Egg Bite")
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