using System;
using Simulation;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public int score = 0; 
    
    
    
    public static Action<int> OnScoreChange;
    
    
    private void OnEnable()
    {
        PlayerCollisionReader.OnCoinCollected += HandleCoinCollected;
        TrailCollisionReader.OnAsteroidCollision += HandleTrail;
    }
    private void OnDisable()
    {
        PlayerCollisionReader.OnCoinCollected -= HandleCoinCollected;
        TrailCollisionReader.OnAsteroidCollision -= HandleTrail;
    }

    void HandleCoinCollected(GameObject coin)
    {
        score ++;
        ScoreChanged();
    }

    void HandleTrail(GameObject asteroid)
    {
        Debug.Log("Trail collision score");
        score ++;
        ScoreChanged();
    }


    void ScoreChanged()
    {
        Debug.Log("Score changed");
        OnScoreChange?.Invoke(score);
    }
    
}