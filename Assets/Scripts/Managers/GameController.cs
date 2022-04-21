using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private static GameController instance;  //singleton

    static Paddle paddle;
    static UIManager uiManager;

    [SerializeField] float addSizeAmmount;
    [SerializeField] float loadNextLevelSeconds;
    [SerializeField] float duplicateBallDivergence;
    [SerializeField] int ballDupFactor;
    [SerializeField] float ballSpeed = 6;
    [SerializeField] float ballSpeedUp;
    [SerializeField] int activeScene = 0;
    [SerializeField] int numBlocks = 0;
    [SerializeField] int damage = 1;
    [SerializeField] int playerLives = 3;
    [SerializeField] List<Ball> balls;

    private static float originalBallSpeed;
    private static bool pause = false;

    //====================================================================== lifecycle

    private void Awake() {
        //SINGLETON PATTERN
        //se non esiste ancora un game controller, inizializzo
        if (instance == null) {
            instance = this;                                                   
            balls = new List<Ball>();
            DontDestroyOnLoad(this); //potresti mettere anche gameObject 
        }
        else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        originalBallSpeed = instance.ballSpeed;
        PauseGame();
        uiManager.ShowNewGamePanel();
        uiManager.HideGameover();
        uiManager.HideGamePausedPanel();
    }

    //====================================================================== register objects

    public static void registerPaddle(Paddle _newPaddle) {
        paddle = _newPaddle;
    }
    public static void registerBall(Ball _newBall) {
        instance.balls.Add(_newBall);
    }
    public static void registerUIManager(UIManager _uiManager) {
        uiManager = _uiManager;
    }
    //====================================================================== levels handling
    public static void StartGame() {
        uiManager.HideNewGamePanel();
        uiManager.HideGameover();
        UnpauseGame();
        LoadNextLevel();
    }
    public static void RestartGame() {
        uiManager.HideGamePausedPanel();
        instance.playerLives = 3;
        instance.ballSpeed = originalBallSpeed;
        instance.balls.Clear();
        instance.activeScene = 1;
        instance.numBlocks = 0;
        SceneManager.LoadScene("Level 1");
        UnpauseGame();
    }
    private static void GameOver() {
        uiManager.ShowGameover();
        PauseGame();
    }

    public static void LoadNextLevel() {
        instance.balls = new List<Ball>();
        instance.ballSpeed = originalBallSpeed;
        instance.StartCoroutine(instance.LoadNextLevelWait());
    }
    IEnumerator LoadNextLevelWait() {
        yield return new WaitForSeconds(loadNextLevelSeconds);
        activeScene++;
        SceneManager.LoadScene("Level " + activeScene);
    }
    public static void CloseGame() {
        instance.StartCoroutine(instance.CloseGameWait());
    }
    IEnumerator CloseGameWait() {
        yield return new WaitForSeconds(loadNextLevelSeconds);
        Application.Quit();
    }
    public static void PauseGameShowMenu() {
        uiManager.ShowGamePausedPanel();
        PauseGame();
    }
    public static void PauseGame() {
        pause = true;
        Time.timeScale = 0;
    }
    public static void UnpauseGame() {
        pause = false;
        uiManager.HideGamePausedPanel();
        Time.timeScale = 1;
    }
    public static bool IsPaused() {
        return pause;
    }

    //====================================================================== 

    public static int GetDamage() {
        return instance.damage;
    }

    public static void BallLost(Ball _lostBall) {
        instance.balls.Remove(_lostBall);
        // if all balls went out, player lost a life
        if (instance.balls.Count <= 0) {

            //destroying all powers dropped
            Pickable[] powers = FindObjectsOfType<Pickable>();
            foreach (Pickable power in powers) Destroy(power.gameObject);

            //loos life
            instance.playerLives--;
            paddle.setLife(instance.playerLives);

            if (instance.playerLives <= 0) {
                GameOver();
            }

            //reset ball position
            PrepareLaunch();

        }
    }

    public static void PrepareLaunch() {
        paddle.prepareLaunch();
    }
    public static void countBlock() {
        instance.numBlocks++;
    }
    public static void blockDestroyed(int _score) {
        instance.ballSpeed += instance.ballSpeedUp;
        instance.numBlocks--;
        if (instance.numBlocks <= 0) {
            LoadNextLevel();
        }
    }

    public static int getPlayerLife() {
        return instance.playerLives;
    }

    public static float GetBallSpeed() {
        return instance.ballSpeed;
    }

    //============================================================================================ powers
    public static void duplicateBalls() {
        Debug.Log("DUPLICATE BALLS CALLED");
            foreach (Ball ball in instance.balls) {
                float xSpeed = ball.GetComponent<Rigidbody2D>().velocity.x;
                float ySpeed = ball.GetComponent<Rigidbody2D>().velocity.y;
                if(instance.ballDupFactor >= 2) {
                    Ball newBall = Instantiate(ball, ball.transform.position, Quaternion.identity);
                    newBall.GetComponent<Rigidbody2D>().velocity = new Vector2(xSpeed + instance.duplicateBallDivergence, ySpeed);
                }
                if (instance.ballDupFactor >= 3) {
                    Ball newBall = Instantiate(ball, ball.transform.position, Quaternion.identity);
                    newBall.GetComponent<Rigidbody2D>().velocity = new Vector2(xSpeed - instance.duplicateBallDivergence, ySpeed);
                }
            }
    }
    public static void increasePaddleSize() {
        paddle.increaseSize(instance.addSizeAmmount);
    }
    public static void createCompanion() {
        paddle.activateCompanion();
    }
    public static void DamageUp() {
        instance.damage++;
    }
    //============================================================================================
}
