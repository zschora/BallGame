using System;
using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    public GameObject winText;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            winText.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
