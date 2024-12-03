using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedFood : BaseFood
{
    public float speedIncreaseAmount = 0.2f; // Amount to increase speed per consumption

    public override void ApplyEffect(SnakeController snakeController)
    {
        SnakeHeadController headController = snakeController.GetComponent<SnakeHeadController>();

        if (headController != null )
        {
            headController.IncreaseSpeed(speedIncreaseAmount);
        }

        // Grow Snake
        snakeController.Grow();

        // Add Score
    }
}
