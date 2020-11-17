using System;
using System.Collections;
using System.Collections.Generic;
using EZCameraShake;
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
    [SerializeField] private float deathMomentPause = 4f;

    /// <summary>
    /// Distance from wall where food will not spawn
    /// </summary>
    [SerializeField] private float foodSpawnWallBuffer = 2;


    private GameObject foodInstance;
    private const int NeckSegment = 1;

    private float planeMinX;
    private float planeMaxX;
    private float planeMinZ;
    private float planeMaxZ;

    void Awake()
    {
        CalculatePlaneBounds();
        SpawnFood();
        SubscribeToEvents();
    }

    private void CalculatePlaneBounds()
    {
        var planeMeshBounds = plane.GetComponent<MeshFilter>().mesh.bounds;
        var scaleX = plane.localScale.x;
        var scaleZ = plane.localScale.z;
        planeMinX = planeMeshBounds.min.x * scaleX + foodSpawnWallBuffer;
        planeMaxX = planeMeshBounds.max.x * scaleX - foodSpawnWallBuffer;
        planeMinZ = planeMeshBounds.min.z * scaleZ + foodSpawnWallBuffer;
        planeMaxZ = planeMeshBounds.max.z * scaleZ - foodSpawnWallBuffer;
    }

    private void SubscribeToEvents()
    {
        FindObjectOfType<Food>().EatenEvent += OnFoodEaten;
        FindObjectOfType<SnakeHead>().HeadHitBodySegmentEvent += OnBodySegmentImpactedByHeadEvent;
        FindObjectOfType<SnakeHead>().HeadHitWallEvent += OnWallImpactedByHeadEvent;
    }

    private void OnWallImpactedByHeadEvent(string wallIndex)
    {
        Debug.Log($"THe HEAD HIT WALL {wallIndex}");
        GameOver();
    }

    private void OnFoodEaten()
    {
        Debug.Log("**GameBoard saw food eaten event**");
        FindObjectOfType<AudioManager>().Play("Eat Food");
        CameraShaker.Instance.ShakeOnce(1, 5, .1f, .5f);
        SpawnFood();
    }

    private void SpawnFood()
    {
        // @TODO ensure the random position is not the snakehead position
        // https://app.clickup.com/t/e0rthb
        // random position within the bounds of the board
        var randomPosition = RandomPositionWithinBoard();
        Debug.Log($"SPAWNING FOOD AT: >>>>  {randomPosition} <<<<<");
        if (foodInstance != null)
        {
            foodInstance.transform.position = randomPosition;
        }
        else
        {
            foodInstance = Instantiate(fruit, randomPosition, Quaternion.identity);
        }
    }

    private Vector3 RandomPositionWithinBoard()
    {
        var randomPosition = new Vector3(Random.Range(planeMinX, planeMaxX), 0.5f, Random.Range(planeMinZ, planeMaxZ));
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
        CameraShaker.Instance.ShakeOnce(6f, 6f, .1f, 2f);
        var playerHead = FindObjectOfType<SnakeHead>();
        playerHead.IsFrozen = true;
        playerHead.GetComponentInChildren<ParticleSystem>().Play();
        AudioManager.instance.StopPlay("Game Music");
        AudioManager.instance.Play("Death explosion");
        StartCoroutine(LoadLevelAfterDelay(deathMomentPause));
    }

    IEnumerator LoadLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Scenes/Start Game");
        AudioManager.instance.Play("Menu Music");
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void UnsubscribeEvents()
    {
        // unsubscribe from events
        if (FindObjectOfType<Food>() != null)
        {
            FindObjectOfType<Food>().EatenEvent -= OnFoodEaten;
        }

        if (FindObjectOfType<SnakeHead>() != null)
        {
            FindObjectOfType<SnakeHead>().HeadHitBodySegmentEvent -= OnBodySegmentImpactedByHeadEvent;
        }
    }
}