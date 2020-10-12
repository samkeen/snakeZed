using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    void Start()
    {
        planeMeshBounds = plane.GetComponent<MeshFilter>().mesh.bounds;
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void Spawn()
    {
        Vector3 randPos = Vector3.zero;
        randPos.x = Random.Range(planeMeshBounds.min.x, planeMeshBounds.max.x);
        randPos.z = Random.Range(planeMeshBounds.min.z, planeMeshBounds.max.z);
        Debug.Log($"Placing apple at {randPos}");
        GameObject obj = Instantiate (fruit, randPos, Quaternion.identity,plane);

    }
}