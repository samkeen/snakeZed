using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// https://generalistprogrammer.com/game-design-development/unity-spawn-prefab-at-position-tutorial/
/// https://answers.unity.com/questions/1590160/how-to-spawn-objects-within-a-planes-dimensions.html
/// </summary>
public class GameBoard : MonoBehaviour
{
    [SerializeField] private Transform plane;
    [SerializeField] private GameObject fruit;

    private GameObject appleInstance;
    private const int NeckSegment = 1;

    void Awake()
    {
        SpawnApple();
        // subscribe to Apple.eatenEvent
        FindObjectOfType<Apple>().EatenEvent += OnAppleEaten;
        // subscribe to SnakeBodySegment.ImpactedEvent
        FindObjectOfType<SnakeHead>().HeadHitBodySegemntEvent += OnBodySegmentImpactedByHeadEvent;
    }

    private void OnAppleEaten()
    {
        Debug.Log("**GameBoard saw apple eaten event**");
        SpawnApple();
    }

    private void SpawnApple()
    {
        // @TODO ensure the random position is not the snakehead position
        // https://app.clickup.com/t/e0rthb
        // random position within the bounds of the board
        var randomPosition = RandomPositionWithinBoard();
        if (appleInstance != null)
        {
            appleInstance.transform.position = randomPosition;
        }
        else
        {
            appleInstance = Instantiate(fruit, randomPosition, Quaternion.identity);
        }
    }

    private Vector3 RandomPositionWithinBoard()
    {
        var planeMeshBounds = plane.GetComponent<MeshFilter>().mesh.bounds;
        var scaleX = plane.localScale.x;
        var scaleZ = plane.localScale.z;
        var randomPosition = new Vector3(
            Random.Range(planeMeshBounds.min.x * scaleX, planeMeshBounds.max.x * scaleX),
            0.5f,
            Random.Range(planeMeshBounds.min.z * scaleZ, planeMeshBounds.max.z * scaleZ)
        );
        return randomPosition;
    }

    public void OnBodySegmentImpactedByHeadEvent(int segmentIndex)
    {
        if (segmentIndex == NeckSegment)
        {
            // @TODO hit neck, we are backing up, need to stop backward movement
            //       Or, allow slight reversing, count on eventual impact with 
            //       segments other than neck to end game.  Noticed if we make neck drag 
            //       0, it quickly moves aside and you hit a game ending segment.
            //       Wait, we need constant, automated fwd movement then collect only
            //       (GetAxis horizontal).  SOLVED!!!
            Debug.Log($"**You hit the Neck segment at {segmentIndex}**");
        }
        else
        {
            Debug.Log($"**You died, you hit index at {segmentIndex}**");
        }
    }

    private void OnDestroy()
    {
        UnsuscribeEvents();
    }

    private void UnsuscribeEvents()
    {
        // unsubscribe from events
        if (FindObjectOfType<Apple>() != null)
        {
            FindObjectOfType<Apple>().EatenEvent -= OnAppleEaten;
        }
        if (FindObjectOfType<SnakeHead>() != null)
        {
            FindObjectOfType<SnakeHead>().HeadHitBodySegemntEvent -= OnBodySegmentImpactedByHeadEvent;
        }
    }
}