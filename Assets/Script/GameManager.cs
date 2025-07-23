using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Bird bird;
    public PipeSpawner pipeSpawner;
    public UIManager uiManager;
    public AudioSource AudioPlayer;
    public AudioClip PointAudio;

    private int score = 0;

    void Awake()
    {
        Instance = this;
        pipeSpawner.enabled = false;
    }

    void Start()
    {
        uiManager.ShowStart();
        bird.gameObject.SetActive(false);
    }

    /// <summary>
    /// When the player presses the Menu Button, Reset the player position, score, delete all Pipes, and show the StartScreen
    /// </summary>
    public void ResetGame()
    {
        Pipe[] pipes = FindObjectsByType<Pipe>(FindObjectsSortMode.None);
        foreach(Pipe pipe in pipes)
        {
            Destroy(pipe.gameObject);
        }
        score = 0;
        uiManager.UpdateScore(score);

        uiManager.ShowStart();
        pipeSpawner.enabled = false;
        bird.ResetBird();
        bird.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    /// <summary>
    /// When called upon, show the ReadyScreen and show the bird(Player) on screen.
    /// </summary>
    public void ReadyGame()
    {
        uiManager.ShowReady();
        bird.ResetBird();
        bird.gameObject.SetActive(true);
    }

    /// <summary>
    /// When called upon, begin spawning pipes and Start the game
    /// </summary>
    public void StartGame()
    {
        score = 0;
        uiManager.HideReady();
        pipeSpawner.enabled = true;
        bird.StartGame();
    }

    /// <summary>
    /// When the player dies, show the GameOver Screen
    /// </summary>
    public void GameOver()
    {
        Time.timeScale = 0f;
        uiManager.ShowGameOver();
    }

    /// <summary>
    /// Increase the score by 1 when called upon
    /// </summary>
    public void IncreaseScore()
    {
        score++;
        uiManager.UpdateScore(score);
        AudioPlayer.PlayOneShot(PointAudio);
    }
}
