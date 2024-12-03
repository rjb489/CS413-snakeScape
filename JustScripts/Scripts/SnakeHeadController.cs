using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnakeHeadController : MonoBehaviour
{
    [Header("Dynamic")]
    public float speed; // Movement speed

    public float maxSpeed = 20f;
    public bool allowConstantMovement = true; // Option to enable/disable constant movement
    private Vector3 moveDirection;       // Current movement direction
    private Vector3 lastMousePosition;   // Last known mouse position
    private bool isMouseMoving = true;   // Tracks if the mouse is moving
    private float stationaryTimer = 0f;  // Timer for stationary mouse
    private const float stationaryThreshold = 2f; // Time before moving forward
    private float fixedYPosition;
    private SnakeController snakeController;

    private void Start()
    {
        fixedYPosition = transform.position.y;

        snakeController = GetComponent<SnakeController>();
    }
    void Update()
    {
        // Convert mouse position to a point in world space
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        float distance;

        if (plane.Raycast(ray, out distance))
        {
            Vector3 currentMousePosition = ray.GetPoint(distance);

            // If the mouse has moved, update direction
            if ((currentMousePosition - lastMousePosition).sqrMagnitude > 0.01f)
            {
                isMouseMoving = true;
                stationaryTimer = 0f; // Reset the timer
                lastMousePosition = currentMousePosition;

                // Update movement direction toward the mouse
                moveDirection = (lastMousePosition - transform.position).normalized;
            }
            else
            {
                isMouseMoving = false;
            }
        }

        if (!isMouseMoving)
        {
            stationaryTimer += Time.deltaTime;

            if (allowConstantMovement)
            {
                // Continue moving forward after the threshold
                if (stationaryTimer >= stationaryThreshold)
                {
                    // Keep moving forward in the last direction
                    isMouseMoving = false; // Disable mouse tracking
                }
            }
            else
            {
                // Follow the mouse even if stationary
                moveDirection = (lastMousePosition - transform.position).normalized;
            }
        }

        // Apply movement
        Vector3 newPosition = transform.position + moveDirection * speed * Time.deltaTime;

        // Fix the y position to prevent unintended changes
        transform.position = new Vector3(newPosition.x, fixedYPosition, newPosition.z);
    }

    public void IncreaseSpeed(float amount)
    {
        // Ensuring a speed cap
        speed = Mathf.Min(speed + amount, maxSpeed);

        if (snakeController != null)
        {
            snakeController.SetSpeed(speed);
        }
    }


    void OnTriggerEnter(Collider other)
    {
        // Check if the object is a food type
        BaseFood food = other.GetComponent<BaseFood>();
        if (food != null)
        {
            food.ApplyEffect(GetComponent<SnakeController>());
            Score score = Camera.main.GetComponent<Score>();
            score.UpdateScore(food.scoreVal);
            Destroy(other.gameObject);

            // Optionally spawn a new food item
            FindObjectOfType<FoodSpawner>().SpawnFood();
        }

        // Hit Wall or Border
        if (other.CompareTag("Obstacle"))
        {
            // Ending a Game Functionality
            //SceneManager.LoadScene("Easy_Scene");
            ReloadCurrentScene();
        }

        // Hit Yourself
        if (other.CompareTag("Body"))
        {
            // Check if the body segment is the first one
            Transform firstSegment = snakeController.bodySegments.Count > 0 ? snakeController.bodySegments[0] : null;

            // Check for grace period (length < 3)
            // here to stop weird little bug
            if (snakeController.bodySegments.Count < 3)
            {
                // Debug.Log("Grace period active: Cannot die yet.");
                return; // Ignore collision
            }

            if (other.transform == firstSegment)
            {
                // Debug.Log("Head collided with the first body segment. Ignoring.");
            }
            else
            {
                // Ending a Game Functionality
                // Debug.Log("You Hit Yourself! Game Over.");
                //SceneManager.LoadScene("Easy_Scene");
                ReloadCurrentScene();
            }
        }
    }

    void ReloadCurrentScene()
    {
        string scene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(scene);
    }
}

