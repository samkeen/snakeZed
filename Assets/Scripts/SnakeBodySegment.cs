using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The individual segments of the snake body.
/// Each segment has a target to follow (head or other body segment)
/// </summary>
public class SnakeBodySegment : MonoBehaviour
{
    /// <summary>
    /// https://www.youtube.com/watch?v=TdiN18PR4zk
    /// </summary>
    public Transform FollowTarget { get; set; }
    public float SeparationDistance { get; set; }
    public float FollowSpeed { get; set; }
    public int SegmentIndex { get; set; }

    private void Update()
    {
        // Face the follow target and pursue
        transform.LookAt(FollowTarget.position, Vector3.forward);
        var distanceToHead = (transform.position - FollowTarget.position).magnitude;
        if (distanceToHead > SeparationDistance)
        {
            transform.Translate(Vector3.forward * FollowSpeed * Time.deltaTime);
        }
    }
}