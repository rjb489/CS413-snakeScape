using UnityEngine;

public class GoldenApple : BaseFood
{
    public int removeCount = 2;
    public override void ApplyEffect(SnakeController snakeController)
    {
        // Remove half of the snake's length
        int segmentsToRemove = snakeController.GetBodySegmentCount() / removeCount;
        snakeController.RemoveSegments(segmentsToRemove);

        // Add to score
    }
}

