using UnityEngine;
public class BallMonitor : MonoBehaviour
{
    private Rigidbody2D rb;
    private float stillTime = 0f;
    private const float stillThreshold = 0.05f;
    private const float maxStillTime = 4f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!LevelManager.Instance.isSimulationRunning)
        {
            return;
        }
        Debug.Log(transform.position);
        if (transform.position.y < -8 ||
            transform.position.y > 8 ||
            transform.position.x < -10 ||
            transform.position.x < -10)
        {
            GameManager.Instance.Lose();
            Destroy(gameObject);
        }

        if (rb.linearVelocity.magnitude < stillThreshold)
        {
            stillTime += Time.deltaTime;
            if (stillTime > maxStillTime)
            {
                GameManager.Instance.Lose();
                Destroy(gameObject);
            }
        }
        else
        {
            stillTime = 0;
        }
    }
}
