using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControl : MonoBehaviour
{
    [SerializeField] private ButtonCheck RightButton;
    [SerializeField] private ButtonCheck LeftButton;
    [SerializeField] private ButtonCheck JumpButton;
    [SerializeField] private Animator anim;
    [SerializeField] private Login login;
    private bool direction = true;
    private float time = -1.0f;
    private float timeToAdd = 0.1f;
    private float move = 6.0f;
    private float jump = 13.0f;
    public float koef = 1.0f;
    private float generalKoef = 1.0f;
    private bool isJumping;
    private Rigidbody2D character;
    private Transform transform;
    public bool isPlaying = true;
    

    void Start()
    {
        character = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
     }

    void Update()
    {
        if (isPlaying == true)
        {
            if (Time.time > time)
            {
                anim.SetBool("Jump", false);
            }

            anim.SetBool("Run", (RightButton.IsHeldDown || LeftButton.IsHeldDown));

            if (LeftButton.IsHeldDown) koef = -1.0f;
            if (RightButton.IsHeldDown) koef = 1.0f;

            if (koef == -1.0f && transform.position.x < -1.0f) generalKoef = 0.0f;
            else generalKoef = 1.0f;

            transform.localScale = new Vector3(koef * 1.3f, 1.3f, 1.3f);

            if (JumpButton.IsHeldDown && isJumping == false)
            {
                anim.SetBool("Run", false);
                character.velocity = new Vector2(koef * generalKoef * (move + 2.0f), jump);
                anim.SetBool("Jump", true);
                time = Time.time + timeToAdd;
            }
            else if ((RightButton.IsHeldDown || LeftButton.IsHeldDown) && isJumping == false)
            {
                character.velocity = new Vector2(koef * generalKoef * move, 0.0f);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground")) isJumping = false;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground")) isJumping = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Puddle"))
        {
            anim.SetBool("Run", false);
            isJumping = false;
            anim.SetBool("Jump", false);
            login.Lost();
            PlayerPrefs.SetInt("reload", 1);
        }
    }

}
