using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    public event Action<int> HeadHitBodySegmentEvent;
    public event Action<string> HeadHitWallEvent;
    
    [SerializeField] private float rotationSpeed = 500;
    [SerializeField] private float moveSpeed = 20;

    private bool isFrozen;
    public bool IsFrozen
    {
        set => isFrozen = value;
    }

    void Update()
    {
        Rotate();
        Move();
    }

    private void Move()
    {
        if (! isFrozen)
        {
            var movement = new Vector3(0.0f, 0.0f, 1);
            transform.Translate(movement * moveSpeed * Time.deltaTime);
        }
    }

    private void Rotate()
    {
        var rotation = new Vector3(0.0f, Input.GetAxis("Horizontal"), 0.0f);
        transform.Rotate(rotation * rotationSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (HitSnakeBody(other))
        {
            var segmentIndex = ParseSegmentIndex(other);
            if (segmentIndex > 1) // ignore hitting segment 1 (neck)
            {
                HeadHitBodySegmentEvent?.Invoke(segmentIndex);
            }
        }
        if (HitWall(other))
        {
            var wall = other.gameObject.GetComponent<Wall>();
            HeadHitWallEvent?.Invoke(wall.index);
        }
    }
    
    private bool HitWall(Collision other)
    {
        return other.gameObject.CompareTag("Wall");
    }

    private bool HitSnakeBody(Collision other)
    {
        return other.gameObject.CompareTag("SnakeBodySegment");
    }

    private int ParseSegmentIndex(Collision other)
    {
        string[] parts = other.gameObject.name.Split('-');
        int segmentIndex = Int32.Parse(parts[1]);
        return segmentIndex;
    }
}