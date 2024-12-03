using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularFood : BaseFood
{
 public override void ApplyEffect(SnakeController snakeController)
    {
        // Grow Snake
        snakeController.Grow();
    }
}
