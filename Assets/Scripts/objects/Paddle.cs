using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Paddle : BasicPaddle
{
    [SerializeField] float moveSpeed;
    [SerializeField] Ball ball;

    Rigidbody2D myRigidbody;
    bool isAlive;
    bool beginning;
    Vector2 moveInput;
    bool isLaunching;
    SpriteRenderer spriteRenderer;
    Vector2 originalPosition;
    //Vector3 originalScale;

    [SerializeField] GameObject ballLaunchPosition;
    [SerializeField] List<Sprite> sprites;
    [SerializeField] BasicPaddle companion;


    //============================================================================getter/setter

    public void prepareLaunch() {
        isLaunching = true;
    }

    public void setLife(int _val) {
        spriteRenderer.sprite = sprites[_val];
    }

    //============================================================================gameloop
    private void Start() {
        isAlive=true;
        beginning = true;
        myRigidbody = GetComponent<Rigidbody2D>();
        moveInput = new Vector2(0f, 0f);
        isLaunching = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        setLife(GameController.getPlayerLife());
        originalPosition = gameObject.transform.position;
        GameController.registerPaddle(this);
        companion.gameObject.SetActive(false);
        //originalScale = gameObject.transform.localScale;
    }

    public void resetPosition() {
        transform.position = originalPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive) {
            myRigidbody.velocity = new Vector2(moveInput.x * moveSpeed,0);
            if (isLaunching) {
                ball.transform.position = ballLaunchPosition.transform.position;
            }
        }
    }

    public void increaseSize(float increaseSize) {
        transform.localScale = new Vector3(transform.localScale.x+increaseSize,transform.localScale.y,transform.localScale.z);
    }

    public void activateCompanion() {
            companion.gameObject.SetActive(true);
    }

    //public void removePowers() {

    //    //removing companion
    //    companion.gameObject.SetActive(false );

    //    //removing increase size
    //    boxColliders[0].enabled=true;
    //    boxColliders[1].enabled=false;
    //    leftExtension.gameObject.SetActive(false);
    //    rightExtension.gameObject.SetActive(false);
    //}

    //===================================================================== player input

    public void OnMove(InputValue value)   //chiamato automaticamente quando l'utente preme un pulsante di spostamento
    {
        //per evitare pressione freccia automatica all'inizio
        if (isAlive && !beginning) {
            moveInput = value.Get<Vector2>();
        }
        else if (beginning) {
            moveInput = new Vector2(0f, 0f);
            beginning = false;
        }
    }
    public void OnLaunch(InputValue value)   //chiamato automaticamente quando l'utente preme un pulsante di spostamento
    {
        if (isAlive && isLaunching) {
            isLaunching = false;
            ball.Launch();
        }
    }

    public void OnPause(InputValue value) {
        if (!GameController.IsPaused()) {
            GameController.PauseGameShowMenu();
        }
        else
            GameController.UnpauseGame();
    }

}
