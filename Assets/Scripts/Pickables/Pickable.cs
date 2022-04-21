using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    [SerializeField] float fallSpeed;
    protected virtual void Start() {
        fallSpeed = 4;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0,-fallSpeed);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.tag == "Paddle") {
            OnPicked();
            Destroy(gameObject);
        }
    }

    protected virtual void OnPicked() {
        Debug.Log("PICKABLE: YOU SHOULD IMPLEMENT ON PICKED");
    }
}
