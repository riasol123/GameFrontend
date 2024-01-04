using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BackgroundChange : MonoBehaviour
{
    [SerializeField] private Transform first;
    [SerializeField] private Transform second;
    [SerializeField] private Transform player;
    public float middle;
    public float difference;

    void Update()
    {
        difference = (float)Math.Max(first.position.x, second.position.x) - (float)Math.Min(first.position.x, second.position.x);
        middle = (difference / 2.0f);

        if (player.position.x > (first.position.x - 4.0f) && player.position.x < first.position.x)
        {
            second.position = new Vector3(first.position.x - difference, second.position.y, second.position.z);
        }
        else if (player.position.x > first.position.x && player.position.x < first.position.x + 4.0f)
        {
            second.position = new Vector3(first.position.x + difference, first.position.y, first.position.z);
        }
        else if (player.position.x > (second.position.x - 4.0f) && player.position.x < second.position.x)
        {
            first.position = new Vector3(second.position.x - difference, second.position.y, second.position.z);
        }
        else if (player.position.x > second.position.x && player.position.x < second.position.x + 4.0f)
        {
            first.position = new Vector3(second.position.x + difference, first.position.y, first.position.z);
        }

       
    }
}
