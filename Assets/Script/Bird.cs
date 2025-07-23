using UnityEngine;
using UnityEngine.InputSystem;

public class Bird : MonoBehaviour
{
    public AudioSource AudioPlayer;
    public AudioClip DieAudio;
    public AudioClip HitAudio;
    public AudioClip SwooshingAudio;
    public AudioClip WingAudio; // The sound when the player jumps
    public static bool IsAlive = false;
    public float MaxJumpVelocity = 6f;
    public float MaxUpwardAngle = 45f;
    public float MaxDownwardAngle = -90f;
    public float RotationLerpSpeed = 5f;
    public float GravityScale = 3f;

    private Rigidbody2D birdRB;
    private Animator birdAnimator;
    private Vector3 initialBirdPosition;
    private Quaternion initialBirdRotation;

    void Start()
    {
        initialBirdPosition = transform.position;
        initialBirdRotation = transform.rotation;
        birdAnimator = GetComponent<Animator>();
        birdRB = GetComponent<Rigidbody2D>();
        birdRB.gravityScale = 0f; 
    }

    void Update()
    {
        if (IsAlive)
        {
            if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1"))
            {
                Jump();
            }
            RotateBasedOnVelocity();
        }
    }

    /// <summary>
    /// When the player presses spacebar or the left mouse button, the player go up by 6 (on the y axis).
    /// </summary>
    private void Jump()
    {
        birdRB.linearVelocity = Vector2.up * MaxJumpVelocity;
        AudioPlayer.PlayOneShot(WingAudio);
        birdAnimator.SetTrigger("Jump");
    }

    /// <summary>
    /// Turn the player(Bird) to be facing either up or down. When falling, face down; when jumping, face up.
    /// </summary>
    void RotateBasedOnVelocity()
    {
        float verticalVelocity = birdRB.linearVelocity.y;

        float rotation = 0f;
        if (verticalVelocity > 0)
        {
            rotation = Mathf.InverseLerp(0, MaxJumpVelocity, verticalVelocity);
        }
        else
        {
            rotation = Mathf.InverseLerp(0, -MaxJumpVelocity, verticalVelocity);
            if (rotation < 0)
            {
                rotation = 0;
            }
        }
        float targetAngle = 0f;

        if(verticalVelocity > 0)
        {
            targetAngle = Mathf.Lerp(0, MaxUpwardAngle, rotation);
        }
        else
        {
            targetAngle = Mathf.Lerp(0, MaxDownwardAngle, rotation);
        }
        
        float currentZ = transform.eulerAngles.z;
        if (currentZ > 180)
        {
            currentZ -= 360;
        }

        float newZ = Mathf.Lerp(currentZ, targetAngle, Time.deltaTime * RotationLerpSpeed);
        transform.rotation = Quaternion.Euler(0f, 0f, newZ);
    }

    /// <summary>
    /// When the game starts, make the player begin to fall down.
    /// </summary>
    public void StartGame()
    {
        IsAlive = true;
        birdRB.gravityScale = GravityScale;
        birdRB.linearVelocity = Vector2.zero;
    }

    /// <summary>
    /// Reset the player to the original position
    /// </summary>
    public void ResetBird()
    {
        IsAlive = false;
        birdRB.gravityScale = 0f;
        transform.position = initialBirdPosition;
        transform.rotation = initialBirdRotation;
    }

    /// <summary>
    /// When the player dies, show the GameOverScreen
    /// </summary>
    public void Die()
    {
        IsAlive = false;
        birdRB.linearVelocity = Vector2.zero;
        GameManager.Instance.GameOver();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioPlayer.PlayOneShot(HitAudio);
        AudioPlayer.PlayOneShot(DieAudio);
        AudioPlayer.PlayOneShot(SwooshingAudio);
        Die();
    }
}
