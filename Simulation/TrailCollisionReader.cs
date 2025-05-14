using System;
using System.Collections;
using Unity.VisualScripting;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TrailCollisionReader : MonoBehaviour
{
    public TrailRenderer trailRenderer;
    public EdgeCollider2D edgeCollider2D;

    private List<Vector3> trailPositions = new List<Vector3>();

    public bool isRunning = false;


    public static Action<GameObject> OnAsteroidCollision;

    public void InitializeSelf()
    {
        //edgeCollider2D = GetComponent<EdgeCollider2D>();
        //trailRenderer = GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        if (!isRunning)
        {
            return;
        }

        SetColliderPointsFromTrail(trailRenderer, edgeCollider2D);
    }

    void SetColliderPointsFromTrail(TrailRenderer trail, EdgeCollider2D colider)
    {
        List<Vector2> points = new List<Vector2>();
        for (int position = 0; position < trail.positionCount; position++)
        {
            var point = trail.GetPosition(position);
            points.Add(point);
        }

        colider.SetPoints(points);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            return;
        }
        if (other.gameObject.CompareTag("Asteroid"))
        {
            OnAsteroidCollision?.Invoke(other.gameObject);
            Debug.Log("Asteroid Collision!");
        }
    }
}