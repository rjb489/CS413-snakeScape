using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowFood : BaseFood
{
    public float slowFactor = -0.2f;
    public override void ApplyEffect(SnakeController snakeController)
    {
        SnakeHeadController headController = snakeController.GetComponent<SnakeHeadController>();

        if (headController != null)
        {
            headController.IncreaseSpeed(slowFactor);
        }

        // Grow Snake
        snakeController.Grow();
    }
}
