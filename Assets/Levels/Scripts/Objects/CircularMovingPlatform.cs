using UnityEngine;

public class CircularMovingPlatform : MonoBehaviour
{
    public Transform platform; // The platform to move
    public Transform centerPoint; // The center of the circular path
    public Transform radiusPoint; // A point that defines the radius from the center
    public float speed = 1.5f; // Speed of the movement
    private float angle = 0f; // Current angle for circular motion
    private float radius; // Radius of the circular path

    // Additional Code 
    private void OnDrawGizmos()
    {
        // Draw the circle path for visualization in the editor
        if (centerPoint != null && radiusPoint != null)
        {
            float tempRadius = Vector2.Distance(centerPoint.position, radiusPoint.position);
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(centerPoint.position, tempRadius);

            // Draw a line from the center to the radius point for better visualization
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(centerPoint.position, radiusPoint.position);
        }
    }

    void Start()
    {
        // Calculate the radius once at the start based on the distance between centerPoint and radiusPoint
        radius = Vector2.Distance(centerPoint.position, radiusPoint.position);
    }
    void Update()
    {
        // Increment the angle based on speed and time
        angle += speed * Time.deltaTime;
        // Ensure angle stays within 0 to 360 degrees (or 0 to 2π radians)
        if (angle >= 360f)
        {
            angle -= 360f;
        }
        // Calculate the new position on the circle
        float x = centerPoint.position.x + Mathf.Cos(angle) * radius;
        float y = centerPoint.position.y + Mathf.Sin(angle) * radius;
        // Update the platform's position
        platform.position = new Vector2(x, y);
    }


    // Ensure player becomes a child of the platform on collision
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.transform.SetParent(platform.transform, true);
        }
    }
    // Ensure player stops following the platform when exiting collision
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (col.gameObject.transform.parent == platform.transform)
            {
                col.gameObject.transform.SetParent(null, true);
            }
        }
    }
}
    // End of Additional Code


