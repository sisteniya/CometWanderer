using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Simulation
{
    public class AsteroidController : MonoBehaviour
    {
        public GameObject asteroid;
        public Rigidbody2D _rigidbody;
    
        //public Vector2 _direction;
        public float _speed;
        public float rotation = 0f;
        
        public float screenPadding = 1f; //for camera borders

        
        
        public bool _isRunning = false;
        
        public static Action<GameObject> OnAsteroidDestroyed;

    
    
        private void OnEnable()
        {
            PlayerCollisionReader.OnAsteroidCrashed += HandleCrash;
        }
        private void OnDisable()
        {
            PlayerCollisionReader.OnAsteroidCrashed -= HandleCrash;
        }
        
        
        public void InirializeSelf()
        {
            asteroid = gameObject;
            _rigidbody = asteroid.GetComponent<Rigidbody2D>();
            transform.rotation = Quaternion.Euler(0f, 0f, rotation);
            Debug.Log(rotation);
        }

        
        
        void Update()
        {
            
            if (!isActiveAndEnabled)
            {
                return;
            }
            
            if (_isRunning== false)
            {
                _rigidbody.constraints = RigidbodyConstraints2D.FreezePosition;
                return;
            }
            
            

            _rigidbody.constraints = RigidbodyConstraints2D.None;
            CheckPosition();
            MoveAsteroid();
        }


        
        
        void HandleCrash()
        {
            OnAsteroidDestroyed?.Invoke(asteroid);
            GameObject.Destroy(asteroid);
            Debug.Log(asteroid.name);
        }

        public void HandleTrailCollision()
        {
            OnAsteroidDestroyed?.Invoke(asteroid);
            Destroy(asteroid);
            Debug.Log(asteroid.name);
        }
        

        void CheckPosition()
        {
            if (IsOutOfBounds())
            {
                Destroy(asteroid);
                Debug.Log(asteroid.name);// Видаляємо об'єкт
            }
            else
            {
                return;
            }
        }
    
        bool IsOutOfBounds()
        {
            Vector3 screenPos = Camera.main.WorldToViewportPoint(transform.position);

            // Якщо об'єкт за межами екрану (з додатковим запасом)
            return screenPos.x < -screenPadding || screenPos.x > 1 + screenPadding ||
                   screenPos.y < -screenPadding || screenPos.y > 1 + screenPadding;
        }
    
 



        public void MoveAsteroid()
        {
            _rigidbody.velocity = Vector3.down *_speed;
            //_rigidbody.angularVelocity = rotation;
        }
    }
}