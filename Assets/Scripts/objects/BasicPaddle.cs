using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPaddle : MonoBehaviour
{
    [SerializeField] GameObject left;
    [SerializeField] GameObject right;
    [SerializeField] GameObject extremeLeft;
    [SerializeField] GameObject extremeRight;

    protected virtual void OnCollisionEnter2D(Collision2D other) {

        //check if other is Ball
        Ball otherBall = other.gameObject.GetComponent<Ball>();
        if (otherBall) {
            float speed = GameController.GetBallSpeed();
            if (other.transform.position.x < extremeLeft.transform.position.x) {
                other.rigidbody.velocity = new Vector2(-speed, speed);
            }
            else if (other.transform.position.x < left.transform.position.x) {
                other.rigidbody.velocity = new Vector2(-speed/2, speed);
            }
            else if (other.transform.position.x > extremeRight.transform.position.x) {
                other.rigidbody.velocity = new Vector2(speed, speed);
            }
            else if (other.transform.position.x > right.transform.position.x) {
                other.rigidbody.velocity = new Vector2(speed/2, speed);
            }
            else {
                other.rigidbody.velocity = new Vector2(0, speed);
            }

        }
    }
}
