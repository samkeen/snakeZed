using System;
using System.Collections;
using System.Collections.Generic;
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
    private Bounds planeMeshBounds;
    private const int NECK_SEGMENT = 1;


    // Start is called before the first frame update
    void Awake()
    {
        planeMeshBounds = plane.GetComponent<MeshFilter>().mesh.bounds;
        SpawnApple();
        // subscribe to Apple.eatenEvent
        FindObjectOfType<Apple>().EatenEvent += OnAppleEaten;
        // subscribe to SnakeBodySegment.ImpactedEvent
        FindObjectOfType<SnakeBodySegment>().ImpactedByHeadEvent += OnBodySegmentImpactedByHeadEvent;
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
        var randomPosition = new Vector3(
            Random.Range(planeMeshBounds.min.x, planeMeshBounds.max.x),
            0.5f,
            Random.Range(planeMeshBounds.min.z, planeMeshBounds.max.z)
        );
        if (appleInstance != null)
        {
            appleInstance.transform.position = randomPosition;
        }
        else
        {
            appleInstance = Instantiate(fruit, randomPosition, Quaternion.identity);
        }
    }

    public void OnBodySegmentImpactedByHeadEvent(int segmentIndex)
    {
        if (segmentIndex == NECK_SEGMENT)
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

        if (FindObjectOfType<SnakeBodySegment>() != null)
        {
            FindObjectOfType<SnakeBodySegment>().ImpactedByHeadEvent -= OnBodySegmentImpactedByHeadEvent;
        }
    }
}