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
    // @TODO code smell that snake has reference to gameboard.
    //       needed in order to add an impact listener for new body segments to 
    //       the gameboard.
    [SerializeField] private GameBoard _gameBoard;
    
    private List<SnakeBodySegment> bodySegments = new List<SnakeBodySegment>();

    private void Awake()
    {
        // attach one body segment to the head.
        SpawnBodySegment(snakeHead);
    }

    private void Start()
    {
        // subscribe to Apple.eatenEvent
        FindObjectOfType<Apple>().EatenEvent += OnAppleEaten;
    }

    private void OnAppleEaten()
    {
        SpawnBodySegment(bodySegments[bodySegments.Count - 1].transform);
    }

    private void SpawnBodySegment(Transform followTarget)
    {
        var segment = Instantiate(
            snakeBodySegmentPrefab,
            NewSegmentPosition(followTarget),
            Quaternion.identity
        );
        this.bodySegments.Add(segment);
        SetSegmentState(followTarget, segment, this.bodySegments.Count);
    }

    private void SetSegmentState(Transform followTarget, SnakeBodySegment segment, int position)
    {
        segment.FollowTarget = followTarget;
        segment.FollowSpeed = this.bodySegmentFollowSpeed;
        segment.SeparationDistance = this.bodySegmentSeparation;
        segment.SegmentIndex = position;
        segment.ImpactedByHeadEvent += _gameBoard.OnBodySegmentImpactedByHeadEvent;
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
            FindObjectOfType<Apple>().EatenEvent -= OnAppleEaten;
        }
    }
}