using UnityEngine;

public class GroundScroll : MonoBehaviour
{
    public float scrollSpeed = 2f;
    //public float resetPositionX; 
    //public float startPositionX;

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
            transform.Translate(Vector2.left * scrollSpeed * Time.deltaTime);
        }

        if (transform.position.x <= -groundPositionX)
        {
            RepositionGround();
        }
    }

    private void RepositionGround()
    {
        Vector2 newPos = new Vector2(groundPositionX * 2f, 0);
        transform.position = (Vector2)transform.position + newPos;
    }
}
