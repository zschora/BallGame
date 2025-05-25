using UnityEngine;

public class Star : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Debug.Log("Star collected");
            LevelManager.Instance.CollectStar(this.gameObject);
        }
    }

}
