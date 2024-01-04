using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    private Material material;
    private float distance;

    [Range(0.0f, 0.5f)]
    public float speed = 0.2f;

    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        distance += Time.deltaTime * speed;
        material.SetTextureOffset("MainTex", Vector2.right * distance);
    }
}
