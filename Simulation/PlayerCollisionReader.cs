using System;
using UnityEngine;

public class PlayerCollisionReader : MonoBehaviour
{
    
    public static event Action<GameObject> OnCoinCollected;
    public static event Action OnAsteroidCrashed;
    public static event Action OnBordersTouched;
    
    
    
    void OnTriggerEnter2D(Collider2D triggeredObject) //check triggered object
    {
        if (triggeredObject.CompareTag("Coin"))
        {
            Debug.Log("Player touched coin");
            OnCoinCollected?.Invoke(triggeredObject.gameObject);
 
        }
        else if(triggeredObject.CompareTag("Asteroid") & !this.gameObject.CompareTag("Manager"))
        {
            Debug.Log("Player touched asteroid");
            OnAsteroidCrashed?.Invoke();
        }else if (triggeredObject.CompareTag("Borders"))
        {
            Debug.Log("Player touched border");
            OnBordersTouched?.Invoke();
        }
        
        
    }
    
}