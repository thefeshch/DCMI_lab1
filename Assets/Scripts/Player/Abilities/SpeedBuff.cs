using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBuff : MonoBehaviour
{
    [SerializeField] private float speedMultiplier = 2f;
    [SerializeField] private GameObject _player;

    private void Start()
    {
        _player = transform.parent.gameObject;
        ApplyBuff(_player);
    }

    public void ApplyBuff(GameObject player)
    {
        
        PlayerController playerMovement = player.GetComponent<PlayerController>();
        if (playerMovement != null)
        {
            playerMovement.ApplySpeedBuff(speedMultiplier);
        }
    }

    public void DisableBuff()
    {
        gameObject.SetActive(false);
    }
}
