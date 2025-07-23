using UnityEngine;

public class Pipe : MonoBehaviour
{
    public float MoveSpeed = 2f;
    public float DestroyPipeAtX = -10f;
    public bool Scored = false;

    void Update()
    {
        transform.position += Vector3.left * MoveSpeed * Time.deltaTime;

        if (!Scored && transform.position.x < 0)
        {
            Scored = true;
            GameManager.Instance.IncreaseScore();

        }

        if (transform.position.x < DestroyPipeAtX)
        {
            Destroy(gameObject);
        }
    }
}
