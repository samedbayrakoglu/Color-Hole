using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (GameData.isGameOver)
            return;

        string tag = other.tag;

        if (tag == "Object")
        {
            Level.Instance.RemoveOneObject();

            UIManager.Instance.UpdateLevelProgressBar();

            Destroy(other.gameObject);

            if (Level.Instance.objectsInLevel <= 0)
            {
                Level.Instance.LevelCompleted();
            }
        }

        if (tag == "Obstacle")
        {
            Level.Instance.LevelFailed();

            Destroy(other.gameObject);
        }
    }
}
