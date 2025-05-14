using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Simulation
{
    public class AsteroidSpawner  :MonoBehaviour
    {
        public List<GameObject> asteroidPrefabs;
        public List<GameObject> spawnedAsteroids = new List<GameObject>();

        public float time = 0f;
    
        private Vector3 spawnPosition;
        public float spawnY = 5f;
        
        public float _spawnInterval;
        public int _asteroidCount = 2;
        
        
        public bool IsTimerRunning = false;

        private void OnEnable()
        {
            AsteroidController.OnAsteroidDestroyed += ClearList;
            TrailCollisionReader.OnAsteroidCollision += DestroyAsteroid;
        }
        
        void OnDisable()
        {
            AsteroidController.OnAsteroidDestroyed -= ClearList;
            AsteroidController.OnAsteroidDestroyed -= ClearList;
        }
        
        
        
        void Update()
        {
            if (!IsTimerRunning){return;}
            
            time += Time.deltaTime;
            SpawnAsteroids();
        }

        
        
        

        
        
   
        public void SpawnAsteroids()
        {
            if (CheckTime())
            {
                SpawnAddToList();
                ResetTimer();
            }
        }

        bool CheckTime()
        {
            if (time >= _spawnInterval)
            {
                return true;
            }
            return false;
        }
        void ResetTimer()
        {
            time = 0;
        }
        
        
        

        void SpawnAddToList()
        {
            for (int i = 0; i < _asteroidCount; i++)
            {
                RandomizeSpawnPosition();
                var asteroidPrefab = RandomizePrefab();
                
                var asteroid = GameObject.Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
            
                var component = asteroid.GetComponent<AsteroidController>();
                component.rotation = RandomizeRotation();
                component.InirializeSelf();
                
                component._isRunning = true;
            
                spawnedAsteroids.Add(asteroid);
            }
        }
        void RandomizeSpawnPosition()
        {
            Vector3 leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0));
            Vector3 rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));

            float randomX = Random.Range(leftEdge.x, rightEdge.x);

            spawnPosition = new Vector3(randomX, spawnY, 0);
        }
        GameObject RandomizePrefab()
        {
            int index = Random.Range(0, asteroidPrefabs.Count);
            var asteroid = asteroidPrefabs[index];
            return asteroid;
        }

        float  RandomizeRotation()
        {
            float randomRotation = Random.Range(0f, 360f);
            return randomRotation;
        }




        void ClearList(GameObject goj)
        {
            spawnedAsteroids.Remove(goj);
        }
        
        
        public void StopAsteroids()
        {
            if (spawnedAsteroids == null)
            {
                return;
            }
            foreach (var asteroid in spawnedAsteroids)
            {
                if (asteroid != null)
                {
                    var component = asteroid.GetComponent<AsteroidController>();
                    component._isRunning = false;
                }
            }
        }
        public void StartMoving()
        {
            if (spawnedAsteroids == null)
            {
                return;
            }
            foreach (var asteroid in spawnedAsteroids)
            {
                if (asteroid != null)
                {
                    var component = asteroid.GetComponent<AsteroidController>();
                    component._isRunning = true;
                }
                
            }
        }
        
        public void GameOver()
        {
            if (spawnedAsteroids.Count > 0)
            {
                foreach (var asteroid in spawnedAsteroids)
                {
                    Destroy(asteroid.gameObject);
                    //Debug.Log(asteroid.name);
                }
                spawnedAsteroids.Clear();
            }
        }
        
        private void DestroyAsteroid(GameObject obj)
        {
            if (obj == null || obj.tag != "Asteroid")
            {
                return;
            }
            var comp = obj.GetComponent<AsteroidController>();
            comp.HandleTrailCollision();
        }
    }
}