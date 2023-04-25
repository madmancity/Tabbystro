using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrdersBehavior : MonoBehaviour
{
    public GameObject orderPanelPrefab;
    public static List<GameObject> orders;

    private Vector3 baseline = new Vector3(0, 397.5f, 0);

    void Start()
    {
        orders = new List<GameObject>();
        newCustomer();
    }

    float lastAdd = 0;
    void Update()
    {
        if (orders.Count < 4)
        {
            if (Time.time - lastAdd > 0.8f)
            {
                float rand = Random.Range(0, 10f);
                if (rand > 8)
                {
                    newCustomer();
                }
                lastAdd = Time.time;
            }
        }
        for (int i = 0; i < orders.Count; ++i)
        {
            Vector3 target = baseline - new Vector3(0, 265 * i, 0);
            if (Vector2.Distance(transform.localPosition, target) > 0.5f)
            {
                orders[i].transform.localPosition = Vector2.MoveTowards(orders[i].transform.localPosition, target, 1000 * Time.deltaTime);
            }
        }
    }

    void newCustomer()
    {
        GameObject obj = Instantiate(orderPanelPrefab);
        obj.transform.parent = transform;
        obj.transform.localScale = new Vector3(0.7614f, 0.7614f, 0.7614f);
        obj.transform.localPosition = baseline - new Vector3(0, 265 * 4, 0);
        orders.Add(obj);
    }
}
