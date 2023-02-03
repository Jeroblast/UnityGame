using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class MyScript : MonoBehaviour
{
    public float speed = 5f;
    public float jumpSpeed = 9f;
    private float direction = 0f;
    public Rigidbody2D player;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGround;

    private Vector3 respawnPoint;
    public GameObject fallDetector;

    public TextMeshProUGUI scoreText;

    

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position;
        scoreText.text = "Score: " + Scoring.totalScore;
    }

    // Update is called once per frame
    void Update()
    {
        isTouchingGround = Physics2D.OverlapCircle(transform.position, groundCheckRadius, groundLayer);
        direction = Input.GetAxis("Horizontal");

        if (direction > 0f)
        {
            player.velocity = new Vector2(direction * speed, player.velocity.y);
        }
        else if (direction < 0f)
        {
            player.velocity = new Vector2(direction * speed, player.velocity.y);
        }
        else
        {
            player.velocity = new Vector2(0, player.velocity.y);
        }
        if(Input.GetButtonDown("Jump") && isTouchingGround)
        {
            player.AddForce(new Vector2(0,speed), ForceMode2D.Impulse);
        }

        fallDetector.transform.position = new  Vector2 (transform.position.x, fallDetector.transform.position.y);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "FallDetector")
        {
            transform.position = respawnPoint;
        }
        else if (collision.tag =="Checkpoint")
        {
            respawnPoint = transform.position;
        }
         else if(collision.tag == "NextLevel")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            respawnPoint = transform.position;
        }
        else if (collision.tag == "PreviousLevel")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            respawnPoint = transform.position;
        }
        else if(collision.tag == "Cristal")
        {
            Scoring.totalScore += 1;
            scoreText.text = "Score: " + Scoring.totalScore;
            collision.gameObject.SetActive(false);
        }

    }
}
