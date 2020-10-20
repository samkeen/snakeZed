using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    public event Action<int> HeadHitBodySegemntEvent;
    
    [SerializeField] private float rotationSpeed = 500;
    [SerializeField] private float moveSpeed = 20;

    float z;
    void Update()
    {
        Rotate();
        Move();
    }

    private void Move()
    {
        var movement = new Vector3(0.0f, 0.0f, 1);
        transform.Translate(movement * moveSpeed * Time.deltaTime);
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
                HeadHitBodySegemntEvent?.Invoke(segmentIndex);
            }
        }
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