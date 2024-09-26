using UnityEngine;

[RequireComponent (typeof(BoxCollider2D))]
[RequireComponent(typeof(LineRenderer))]

public class ObjectMatchingGame : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField] private int matchId;
    private bool isDragging;
    private Vector3 endPoint;
    private ObjectMatchForm objectMatchForm;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (Input.GetMouseButtonDown(0)) 
        {
            if(hit.collider != null && hit.collider)
            {
                isDragging = true;
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0f;
                lineRenderer.SetPosition(0, transform.position);
            }
        }
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            lineRenderer.SetPosition(1, mousePosition);
            endPoint = mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            RaycastHit2D hited = Physics2D.Raycast(endPoint, Vector2.zero);
            if(hited.collider != null && hit.collider.TryGetComponent(out objectMatchForm) && matchId == objectMatchForm.Get_ID())
            {
                lineRenderer.SetPosition(1, hited.transform.position);
                Debug.Log("Correct!");
                this.enabled = false;
            }
            else
            {
                lineRenderer.positionCount = 0;
            }

            lineRenderer.positionCount = 2;

        }
    }
}
