using System;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public Rigidbody2D rb;

    public float speed;
    public float rotation;
    
    public float screenPadding = 1f;
    
    
    
    public bool _isRunning = false;
    public static Action<GameObject> OnCoinDestroyed;
    
    
    
    private void OnEnable()
    {
        PlayerCollisionReader.OnCoinCollected += HandleCoinCollected;
    }
    private void OnDisable()
    {
        PlayerCollisionReader.OnCoinCollected -= HandleCoinCollected;
    }


    private void Update()
    {
        if (!_isRunning)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            return;
        }
        
        rb.constraints = RigidbodyConstraints2D.None;
        MoveCoin();
        CheckPosition();
    }

    
    void CheckPosition()
    {
        if (!IsOutOfBounds())
        {
            return;
        }
        Destroy(gameObject);
        OnCoinDestroyed.Invoke(gameObject);// Видаляємо об'єкт
    }

    private bool IsOutOfBounds()
    {
        Vector3 screenPos = Camera.main.WorldToViewportPoint(transform.position);

        // Якщо об'єкт за межами екрану (з додатковим запасом)
        return screenPos.x < -screenPadding || screenPos.x > 1 + screenPadding ||
               screenPos.y < -screenPadding || screenPos.y > 1 + screenPadding;
    }


    void HandleCoinCollected(GameObject coin)
    {
        if (coin.gameObject.CompareTag("Player"))
        {
            return;
        }
        Debug.Log("Coin collected");
        OnCoinDestroyed?.Invoke(gameObject);
        GameObject.Destroy(coin);
    }


    void MoveCoin()
    {
        rb.velocity = Vector2.down*speed;
    }
}