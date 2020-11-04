using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

/// <summary>
/// https://generalistprogrammer.com/game-design-development/unity-spawn-prefab-at-position-tutorial/
/// https://answers.unity.com/questions/1590160/how-to-spawn-objects-within-a-planes-dimensions.html
/// </summary>
public class GameBoard : MonoBehaviour
{
    [SerializeField] private Transform plane;
    [SerializeField] private GameObject fruit;

    /// <summary>
    /// Distance from wall where apples will not spawn
    /// </summary>
    [SerializeField] private float appleSpawnWallBuffer = 2;


    private GameObject appleInstance;
    private const int NeckSegment = 1;

    void Awake()
    {
        SpawnApple();
        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        FindObjectOfType<Apple>().EatenEvent += OnAppleEaten;
        FindObjectOfType<SnakeHead>().HeadHitBodySegmentEvent += OnBodySegmentImpactedByHeadEvent;
        FindObjectOfType<SnakeHead>().HeadHitWallEvent += OnWallImpactedByHeadEvent;
    }

    private void OnWallImpactedByHeadEvent(string wallIndex)
    {
        Debug.Log($"THe HEAD HIT WALL {wallIndex}");
        GameOver();
    }

    private void OnAppleEaten()
    {
        Debug.Log("**GameBoard saw apple eaten event**");
        FindObjectOfType<AudioManager>().Play("Eat Apple");
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
        var minX = planeMeshBounds.min.x * scaleX + appleSpawnWallBuffer;
        var maxX = planeMeshBounds.max.x * scaleX - appleSpawnWallBuffer;
        var minZ = planeMeshBounds.min.z * scaleZ + appleSpawnWallBuffer;
        var maxZ = planeMeshBounds.max.z * scaleZ - appleSpawnWallBuffer;
        var randomPosition = new Vector3(Random.Range(minX, maxX), 0.5f, Random.Range(minZ, maxZ));
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
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log($"@@@ GAME OVER @@@");
        var snakeHead = FindObjectOfType<SnakeHead>();
        snakeHead.IsFrozen = true;
        snakeHead.GetComponentInChildren<ParticleSystem>().Play();
        AudioManager.instance.Play("Death explosion");
        StartCoroutine(LoadLevelAfterDelay(5f));
    }
    
    IEnumerator LoadLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Scenes/End Game");
    }

    private void OnDisable()
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
            FindObjectOfType<SnakeHead>().HeadHitBodySegmentEvent -= OnBodySegmentImpactedByHeadEvent;
        }
    }
}