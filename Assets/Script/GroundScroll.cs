using UnityEngine;

public class GroundScroll : MonoBehaviour
{
    public float ScrollSpeed = 2f;

    private BoxCollider2D groundCollider;
    private float groundPositionX;

    private void Start()
    {
        groundCollider = GetComponent<BoxCollider2D>();
        groundPositionX = groundCollider.size.x;
    }

    void Update()
    {
        if(Bird.IsAlive)
        {
            transform.Translate(Vector2.left * ScrollSpeed * Time.deltaTime);
        }

        if (transform.position.x < -groundPositionX)
        {
            RepositionGround();
        }
    }

    /// <summary>
    /// This functions handles the Ground moving off screen.
    /// When the BoxCollider of the current Ground-object is no longer in view of the camera, it will move to the right.
    /// </summary>
    private void RepositionGround()
    {
        Vector2 newPos = new Vector2(groundPositionX * 2f, 0);
        transform.position = (Vector2)transform.position + newPos;
    }
}
