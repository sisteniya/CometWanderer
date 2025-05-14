using System;
using System.Collections.Generic;
using Simulation;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public GameObject asteroidsSpawnerFolder;
    public GameObject coinSpawnerFolder;

    private List<AsteroidSpawner> asteroidSpawners = new List<AsteroidSpawner>();
    private List<CoinSpawner> coinSpawners = new List<CoinSpawner>();

    public float timeScale;


    private void OnEnable()
    {
        ScoreController.OnScoreChange += ScoreProcessing;
    }

    private void OnDisable()
    {
        ScoreController.OnScoreChange -= ScoreProcessing;
    }


    public void InitializeSelf()
    {
        AsteroisSpawnersInitialization();
        CoinSpawnersInitialization();
    }

    
    void AsteroisSpawnersInitialization()
    {
        for (int i = 0; i < asteroidsSpawnerFolder.transform.childCount; i++)
        {
            var child = asteroidsSpawnerFolder.transform.GetChild(i);
            var component = child.GetComponent<AsteroidSpawner>();
            asteroidSpawners.Add(component);
        }
    }
    void CoinSpawnersInitialization()
    {
        for (int i = 0; i < coinSpawnerFolder.transform.childCount; i++)
        {
            var child = coinSpawnerFolder.transform.GetChild(i).gameObject;
            var component = child.GetComponent<CoinSpawner>();
            coinSpawners.Add(component);
        }
    }


    
    


    

    public void GameOverProcess()
    {
        StopSpawning();
        

        foreach (var asteroidSpawner in asteroidSpawners) {
            asteroidSpawner.GameOver(); }
        
        foreach (var coinSpawner in coinSpawners) {
            coinSpawner.GameOver(); }
        
        FasterTime(1f);
        Debug.Log("Time renewd");
    }

    
    public void StopMovingObjects()
    {
        foreach (var spawner in asteroidSpawners) { 
            spawner.StopAsteroids(); }

        foreach (var spawner in coinSpawners) {
            spawner.StopCoins(); }
    }
    public void StartMovingObjects()
    {
        foreach (var spawner in asteroidSpawners) {
            spawner.StartMoving(); }

        foreach (var spawner in coinSpawners) {
            spawner.StartMoving(); }
    }
    
    public void StopSpawning()
    {
        foreach (var spawner in asteroidSpawners) {
            spawner.IsTimerRunning = false; }
        
        foreach (var spawner in coinSpawners) {
            spawner.isTimeRunning = false; }
    }
    public void StartSpawning()
    {
        
        foreach (var spawner in asteroidSpawners) {
            spawner.IsTimerRunning = true; }
        
        foreach (var spawner in coinSpawners) {
            spawner.isTimeRunning = true; }
        
        
    }



    void ScoreProcessing(int score)
    {
        switch (score)
        {
            case 10: FasterTime(1.1f); break;
            case 20: FasterTime(1.3f); break;
            case 30: FasterTime(1.5f); break;
            case 40: FasterTime(1.7f); break;
            case 50: FasterTime(2f); break;
        }
        
        Debug.Log("Score Proccesed" + score);
    }

    void FasterTime(float index)
    {
        timeScale = index;
        Time.timeScale = index; 
        Time.fixedDeltaTime = 0.02f * Time.timeScale; 
        
        
    }
}