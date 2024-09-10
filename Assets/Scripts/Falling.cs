using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MonoBehaviour
{
    private GameManager2 _gameBehavior;

    private void Awake()
    {
        _gameBehavior = FindFirstObjectByType<GameManager2>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _gameBehavior.HP = 0;
        }
    }
    
}
