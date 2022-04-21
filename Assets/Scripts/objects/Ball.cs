using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D myRigidbody;
    //=====================================================game
    private void Start() {
        myRigidbody = GetComponent<Rigidbody2D>();
        GameController.registerBall(this);
    }

    private void OnCollisionExit2D(Collision2D collision) {
        float speed = GameController.GetBallSpeed();
        if (Mathf.Abs(myRigidbody.velocity.x) > speed) {
            myRigidbody.velocity = new Vector2(Mathf.Sign(myRigidbody.velocity.x)*speed,myRigidbody.velocity.y);
        }
        if (Mathf.Abs(myRigidbody.velocity.y) > speed) {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, Mathf.Sign(myRigidbody.velocity.y) * speed);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag != "Block") {
            
            if (other.gameObject.tag == "Paddle") {
                AudioManager.PlaySound(AudioManager.paddleCollision);
            }
            else {
                AudioManager.PlaySound(AudioManager.genericCollision);
            }
        }
    }

    public void Launch() {
        myRigidbody.velocity = new Vector2(0, GameController.GetBallSpeed());
    }

}
