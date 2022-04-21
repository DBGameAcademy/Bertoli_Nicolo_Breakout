using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerAfterTime : MonoBehaviour
{
    [SerializeField] float destroyAfterTime;
    void Start()
    {
        Destroy(gameObject,destroyAfterTime);
    }
}
