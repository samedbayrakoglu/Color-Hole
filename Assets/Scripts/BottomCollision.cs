using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        string tag = other.tag;

        if (tag == "Object")
        {
            Debug.Log("object");
        }

        if (tag == "Obstacle")
        {
            Debug.Log("obstacle");

        }
    }
}
