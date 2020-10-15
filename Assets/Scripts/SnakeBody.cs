using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBody : MonoBehaviour
{
    [SerializeField] private Transform snakeHead;
    [SerializeField] private float headToBodyDistance = .1f;
    [SerializeField] private float followSpeed = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(snakeHead.position);
        var distanceToHead = (transform.position - snakeHead.position).magnitude;
        if (distanceToHead > headToBodyDistance)
        {
            transform.Translate(0,0,followSpeed* Time.deltaTime);
        }
        
    }
}
