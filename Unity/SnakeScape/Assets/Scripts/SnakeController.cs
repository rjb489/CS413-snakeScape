using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    [Header("Dynamic")]
    public float speed;
    public GameObject bodyPrefab;
    public float bodySpacing = 0.5f;
    public int initialSize = 5;

    public List<Transform> bodySegments = new List<Transform>();
    private List<Vector3> positions = new List<Vector3>();

    void Start()
    {
        positions.Add(transform.position);

        // Initialize the snake with a set number of body segments
        for (int i = 0; i < initialSize; i++)
        {
            Grow();
            positions.Add(transform.position);
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, positions[0]);

        if (distance > bodySpacing)
        {
            positions.Insert(0, transform.position);

            // Remove excess positions
            if (positions.Count > bodySegments.Count + 1)
            {
                positions.RemoveAt(positions.Count - 1);
            }
        }

        // Ensure positions list is large enough
        while (positions.Count < bodySegments.Count + 1)
        {
            positions.Add(positions[positions.Count - 1]);
        }

        // Move body segments
        for (int i = 0; i < bodySegments.Count; i++)
        {
            Vector3 targetPos = positions[i + 1];
            Transform segment = bodySegments[i];

            Vector3 moveDirection = Vector3.MoveTowards(segment.position, targetPos, speed * Time.deltaTime);
            segment.position = new Vector3(moveDirection.x, segment.position.y, moveDirection.z);
        }
    }

    public void Grow()
    {
        GameObject segment = Instantiate(bodyPrefab);
        segment.transform.position = bodySegments.Count > 0 ? bodySegments[bodySegments.Count - 1].position : transform.position;
        bodySegments.Add(segment.transform);
    }

    public void RemoveSegments(int count)
    {
        for (int i = 0; i < count && bodySegments.Count > 0; i++)
        {
            Transform segment = bodySegments[bodySegments.Count - 1];
            bodySegments.RemoveAt(bodySegments.Count - 1);
            Destroy(segment.gameObject);
        }
    }

    public int GetBodySegmentCount()
    {
        return bodySegments.Count;
    }

    public void SetSpeed( float newSpeed )
    {
        speed = newSpeed;
    }

    public float GetSpeed()
    {
        return speed;
    }

}
