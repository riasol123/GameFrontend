using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{

    [SerializeField] private Transform character;
    [SerializeField] private BoxCollider2D collider;
    [SerializeField] private Text currentScore;
    [SerializeField] private Text recordScore;

    private float smoothTime = 1.0f;
    private Vector2 velocity = Vector2.zero;
    private int scoreCheck = 0;

    private void Start()
    {
        recordScore.text = (PlayerPrefs.GetInt("Score")).ToString();
    }


    private void Update()
    {
        Vector2 targetPos = new Vector2(character.position.x, 0.0f);
        transform.position = Vector2.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
        collider.offset = new Vector2(character.position.x - 10.5f, -4.159588f);

        if (((int)(character.position.x)) / 10 > scoreCheck)
        {
            currentScore.text = ((int)(character.position.x) / 10).ToString();
            scoreCheck = ((int)(character.position.x) / 10);
            if (scoreCheck > PlayerPrefs.GetInt("Score"))
            {
                PlayerPrefs.SetInt("Score", scoreCheck);
                recordScore.text = (PlayerPrefs.GetInt("Score")).ToString();
            }
        }
    }
}
