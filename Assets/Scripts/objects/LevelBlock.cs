using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBlock : MonoBehaviour
{
    [SerializeField] protected int life;
    [SerializeField] List<Sprite> sprites;
    [SerializeField] int score;
    [SerializeField] int coins;
    [SerializeField] Pickable pickable;
    [SerializeField] ParticleSystem destroyedParticleEffect;
    SpriteRenderer spriteRenderer;

    private void Start() {
        // adding this block to the count of total blocks
        GameController.countBlock();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[sprites.Count-1];
    }

    protected void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Ball") {
            life -= GameController.GetDamage();
            if (life <= 0) {
                AudioManager.PlaySound(AudioManager.blockBreak);
                Instantiate(destroyedParticleEffect,transform.position,Quaternion.identity);
                GameController.blockDestroyed(score);
                if(pickable)
                    Instantiate(pickable,transform.position,Quaternion.identity);
                Destroy(gameObject);
            }
            else {
                AudioManager.PlaySound(AudioManager.genericCollision);
                spriteRenderer.sprite = sprites[life-1];
            }
        }
    }
}
