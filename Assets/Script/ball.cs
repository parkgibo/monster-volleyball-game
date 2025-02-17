using System.Collections;
using UnityEngine;

public class ball : MonoBehaviour
{
    public float maxVelocity = 15f;
    public float bounceDaping = 0.8f;
    public float netBounceForce = 5f;
    public ScoreManager scoreManager;
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
        else if (collision.gameObject.CompareTag("Wall"))
        {
            rb.linearVelocity = new Vector2(-rb.linearVelocity.x * bounceDaping, rb.linearVelocity.y);
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            if (transform.position.x > 0)
            {
                scoreManager.AddScore(1, 0);
            }
            else
            {
                scoreManager.AddScore(0, 1); 
            }
            ResetBall();
        }
        else if (collision.gameObject.CompareTag("Net"))
        {
            HandleNetCollision(collision);
        }
    }
    void HandleNetCollision(Collision2D collision)
    {
        // 네트 충돌 위치 계산
        float netPositionX = collision.transform.position.x;
        float ballPositionX = transform.position.x;
        float direction = (ballPositionX > netPositionX) ? 1f : -1f;

        // 네트에 맞으면 튕겨나가는 방향 적용
        Vector2 bounceForce = new Vector2(direction * netBounceForce, netBounceForce);
        rb.velocity = bounceForce;
    }
    void ResetBall()
    {
        transform.position = Vector2.zero;
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(new Vector2(Random.Range(-5, 5), 10f), ForceMode2D.Impulse);
    }
}
