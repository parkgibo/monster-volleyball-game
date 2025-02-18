using System.Collections;
using UnityEngine;

public class ball : MonoBehaviour
{
    public float maxVelocity = 15f;
    public float bounceDaping = 0.8f;
    public float netBounceForce = 5f;
    public GameManager GameManager;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxVelocity);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("AI"))
        {
            Vector2 force = new Vector2(rb.linearVelocity.x * 1.2f, Mathf.Abs(rb.linearVelocity.y) + 5f);
            rb.linearVelocity = force;
        }
        else if (collision.gameObject.CompareTag("Net"))
        {
            rb.linearVelocity = new Vector2(-rb.linearVelocity.x * bounceDaping, rb.linearVelocity.y);
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            if (transform.position.x > 0)
            {
                GameManager.AddScore(1, 0);
            }
            else
            {
                GameManager.AddScore(0, 1);
            }
            ResetBall();
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            HandleNetCollision(collision);
        }
    }
    void HandleNetCollision(Collision2D collision)
    {
        
        float netPositionX = collision.transform.position.x;
        float ballPositionX = transform.position.x;
        float direction = (ballPositionX > netPositionX) ? 1f : -1f;

        
        Vector2 bounceForce = new Vector2(direction * netBounceForce, netBounceForce);
        rb.linearVelocity = bounceForce;
    }
    void ResetBall()
    {
        transform.position = Vector2.zero;
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(new Vector2(Random.Range(-5, 5), 10f), ForceMode2D.Impulse);
    }
}


