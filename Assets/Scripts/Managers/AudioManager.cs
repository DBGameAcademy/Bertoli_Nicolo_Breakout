using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip genericCollisionRead;
    [SerializeField] AudioClip paddleCollisionRead;
    [SerializeField] AudioClip blockBreakRead;

    public static AudioClip genericCollision;
    public static AudioClip paddleCollision;
    public static AudioClip blockBreak;
    static AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        genericCollision = genericCollisionRead;
        paddleCollision = paddleCollisionRead;
        blockBreak = blockBreakRead;
    }

    public static void PlaySound(AudioClip _audio) {
        audioSource.PlayOneShot(_audio);
    }

}
