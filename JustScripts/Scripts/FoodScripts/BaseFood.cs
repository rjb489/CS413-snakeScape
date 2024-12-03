using UnityEngine;

public abstract class BaseFood : MonoBehaviour
{
    public int scoreVal = 1;
    public int rarity = 1; // Higer = more common
    public abstract void ApplyEffect(SnakeController snakeController);
}
