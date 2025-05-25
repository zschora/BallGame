using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ball")) {
            Debug.Log("������� �������");
            GameManager.Instance.OnLevelComplete();

            SceneManager.LoadScene("Main menu");
        }
    }
}
