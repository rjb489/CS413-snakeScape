using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_Movement : MonoBehaviour
{
    public Vector3 minBounds = new Vector3(-10, 0, -10);
    public Vector3 maxBounds = new Vector3(10,0,10);
    public float speed = 3f;

    private Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        SetNewTarget();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        if(Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            // set new target
            SetNewTarget();
        }
    }

    void SetNewTarget()
    {
        targetPos = new Vector3(Random.Range(minBounds.x, maxBounds.x),
            minBounds.y,
            Random.Range(minBounds.z, maxBounds.z));
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        // Draw a wireframe cube to represent movement bounds
        Gizmos.DrawWireCube((minBounds + maxBounds) / 2, maxBounds - minBounds);
    }

}
