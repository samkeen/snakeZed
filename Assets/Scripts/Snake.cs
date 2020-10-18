using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField] private Transform snakeHead;
    [SerializeField] private SnakeBodySegment snakeBodySegmentPrefab;
    [SerializeField] private float bodySegmentFollowSpeed = 50;
    [SerializeField] private float bodySegmentSeparation = 1.3f;
    private Stack<SnakeBodySegment> bodySegments = new Stack<SnakeBodySegment>();

    private void Awake()
    {
        // attach one body segment to the head.
        SpawnBodySegment(snakeHead);
    }

    private void Start()
    {
        // subscribe to Apple.eatenEvent
        FindObjectOfType<Apple>().eatenEvent += OnAppleEaten;
    }

    private void OnAppleEaten()
    {
        Debug.Log("**Snake saw apple eaten**");
        SpawnBodySegment(bodySegments.Peek().transform);
    }

    private void SpawnBodySegment(Transform followTarget)
    {
        var segment = Instantiate(
            snakeBodySegmentPrefab,
            NewSegmentPosition(followTarget),
            Quaternion.identity
        );
        SetSegmentState(followTarget, segment);
        this.bodySegments.Push(segment);
    }

    private void SetSegmentState(Transform followTarget, SnakeBodySegment segment)
    {
        segment.FollowTarget = followTarget;
        segment.FollowSpeed = this.bodySegmentFollowSpeed;
        segment.SeparationDistance = this.bodySegmentSeparation;
    }

    private Vector3 NewSegmentPosition(Transform followTarget)
    {
        var newSegmentPosition = followTarget.position;
        var spacing = followTarget.GetComponent<MeshFilter>().mesh.bounds.size.z + this.bodySegmentSeparation;
        return newSegmentPosition - followTarget.forward * spacing;
    }

    private void OnDestroy()
    {
        // unsubscribe to Apple.eatenEvent
        if (FindObjectOfType<Apple>() != null)
        {
            FindObjectOfType<Apple>().eatenEvent -= OnAppleEaten;
        }
    }
}