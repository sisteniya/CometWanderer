using System;
using UnityEngine;

namespace Simulation
{
    public class PlayerController : MonoBehaviour
    {
        public GameObject _player;
        public GameObject _playerPrefab;
        public Rigidbody2D _rb;
        public TrailCollisionReader _tralCollisionReader;

        public Vector3 direction;
        
        public float _radius;
        public float _speed;
        public float maxStillTime;
        public float timer = 0f;    
    
        public bool isRunning = false;
        
        public static Action OnBOOM;

        public void InitializeSelf()
        {
            _rb = GetComponent<Rigidbody2D>();
            _tralCollisionReader.InitializeSelf();
        }
    
    
        private void Update()
        {
            if (!isRunning)
            {
                _tralCollisionReader.isRunning = false;
                return; 
            }
        
            _tralCollisionReader.isRunning = true;
            var distance = Vector3.Distance(_rb.transform.position, direction);
            if (distance < _radius)
            {
                BOOMifStill(); 
            }

            else
            {
                timer = 0;
                MoveInPurpose();
            }
        }


        private void OnEnable()
        {
            PlayerCollisionReader.OnAsteroidCrashed += HandleCrash;
            PlayerCollisionReader.OnBordersTouched += HandleBorders;
        }
        private void OnDisable()
        {
            PlayerCollisionReader.OnAsteroidCrashed -= HandleCrash;
            PlayerCollisionReader.OnBordersTouched -= HandleBorders;
        }

        private void HandleBorders()
        {
            DisablePLayer();
            OnBOOM?.Invoke();
        }


        void HandleCrash()
        {
            isRunning = false;
            DisablePLayer();
            timer = 0;
        }
    
    
    
    
    

        void MoveInPurpose()
        {
            Vector3 rb = new Vector3(_rb.position.x, _rb.position.y, 0);
            Vector3 difference = (direction - rb);
            _rb.MovePosition(_rb.transform.position + difference * _speed);
        }

        void BOOMifStill()
        {
            timer += Time.deltaTime;
        
            if (timer >= maxStillTime)
            {
                DisablePLayer();
                OnBOOM?.Invoke();
                timer = 0;
            }
        }
   

        public void EnablePLayer()
        {
            /*_playerPrefab.transform.position = Vector3.zero;
            _player.transform.position = Vector3.zero;*/
            
            _playerPrefab.SetActive(true);
        }
        public void DisablePLayer()
        {
            _playerPrefab.SetActive(false);
        }
    }
}