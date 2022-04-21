using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallOutDetector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Ball"){
            //removing ball from game controller list
            GameController.BallLost(other.gameObject.GetComponent<Ball>());
        }
        else {
            Destroy(other.gameObject);
        }
    }
}
