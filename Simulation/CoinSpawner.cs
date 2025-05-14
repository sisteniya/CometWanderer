using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    private List<GameObject> spawnedCoins = new List<GameObject>();

    public float time;
    
    private Vector3 spawnPosition;
    public float spawnY = 5f;
    
    public float _spawnInterval; 
    public int coinCount = 1;
    
    
    public bool isTimeRunning = false;


    private void OnEnable()
    {
        CoinController.OnCoinDestroyed += DeleteSpawnedCoin;
    }

    private void OnDisable()
    {
        CoinController.OnCoinDestroyed -= DeleteSpawnedCoin;
    }

  

    void Update()
    {
        if (!isTimeRunning){ return;}
        
        time += Time.deltaTime;
        SpawnCoins();
        
    }
    
    
    

    public void SpawnCoins()
    {
        if (CheckTimeToSpawn())
        {
            SpawnAddToListCoin();
            ResetTime();
        }
    }

    bool CheckTimeToSpawn()
    {
        if (time >= _spawnInterval)
        {
            return true;
        }

        return false;
    }
    void ResetTime()
    {
        time = 0;
    }
    
    
    void SpawnAddToListCoin()
    {
        for (int i = 0; i < coinCount; i++)
        {
            RandomizeSpawnPosition();
                
            var coin = GameObject.Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
            
            var component = coin.GetComponent<CoinController>();
            component._isRunning = true;
            
            
            spawnedCoins.Add(coin);
        }
    }
    void RandomizeSpawnPosition()
    {
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0));
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));

        float randomX = Random.Range(leftEdge.x, rightEdge.x);

        spawnPosition = new Vector3(randomX, spawnY, 0);
    }

    
    
    public void GameOver()
    {
        if (spawnedCoins == null || spawnedCoins.Count == 0)
        {
            return;
        }
        
        foreach (var coin in spawnedCoins)
        {
                Destroy(coin.gameObject);
        }
        
        spawnedCoins.Clear();
        
    }
    
    public void StopCoins()
    {
        foreach (var coin in spawnedCoins)
        {
            var component = coin.GetComponent<CoinController>();
            component._isRunning = false;
        }
    }

    public void StartMoving()
    {
        foreach (var coin in spawnedCoins)
        {
            var component = coin.GetComponent<CoinController>();
            component._isRunning = true;
        }
    }


   
    
    
    
    private void DeleteSpawnedCoin(GameObject obj)
    {
        spawnedCoins.Remove(obj);
    }
}