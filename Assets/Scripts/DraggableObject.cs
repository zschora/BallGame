using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    private bool isHovered = false;
    private bool isDragging = false;
    private bool isSimulationRunning = false;
    private Vector3 offset;
    public ObjectSlotUI slot;

    private void OnMouseEnter()
    {
        if (!isSimulationRunning)
        {
            isHovered = true;
        }
    }

    private void OnMouseExit()
    {
        isHovered = false;
    }

    void Update()
    {
        if (isSimulationRunning) return;

        Delete();
        Move();
        Rotate();
    }

    private void Move()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider != null && hit.transform == transform)
            {
                isDragging = true;
                offset = transform.position - mousePos;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePos + offset;
        }
    }

    private void Rotate()
    {
        if (isHovered)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (Mathf.Abs(scroll) > 0.01f)
            {
                float rotatinSpeed = 50f;
                transform.Rotate(0,0, scroll * rotatinSpeed);
            }
        }
    }

    private void Delete()
    {
        if (isHovered && Input.GetMouseButtonDown(1))
        {
            slot.IncreaseCount();
            Destroy(gameObject);
        }
    }

    public void SetSimulationState(bool isRunning)
    {
        isSimulationRunning = isRunning;
    }
}