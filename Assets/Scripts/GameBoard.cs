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

    // Plane Properties
    private Bounds planeMeshBounds;


    // Start is called before the first frame update
    void Awake()
    {
        planeMeshBounds = plane.GetComponent<MeshFilter>().mesh.bounds;
        Spawn();
    }

    private void Start()
    {
        // subscribe to Apple.eatenEvent
        FindObjectOfType<Apple>().eatenEvent += OnAppleEaten;
    }

    // Update is called once per frame
    private void OnAppleEaten()
    {
        Debug.Log("**GameBoard Event Trigger**");
    }

    private void OnDisable()
    {
        // unsubscribe to Apple.eatenEvent
        if (FindObjectOfType<Apple>() != null)
        {
            FindObjectOfType<Apple>().eatenEvent -= OnAppleEaten;
        }
    }

    private void Spawn()
    {
        var randomPosition = new Vector3(
            Random.Range(planeMeshBounds.min.x, planeMeshBounds.max.x),
            0.5f,
            Random.Range(planeMeshBounds.min.z, planeMeshBounds.max.z)
        );
        Debug.Log($"Placing apple at {randomPosition}");
        GameObject obj = Instantiate(fruit, randomPosition, Quaternion.identity, plane);
    }
}