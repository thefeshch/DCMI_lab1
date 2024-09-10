using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItem : MonoBehaviour
{
    public GameObject[] RandomItems;
    public delegate void GenerateItemEvent(int index);
    public event GenerateItemEvent randomItemEvent;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int choice = GenerateRandomItem();
            randomItemEvent(choice);
            Destroy(gameObject);
        }
    }
    int GenerateRandomItem()
    {
        int randomChoice = Random.Range(0, RandomItems.Length);
        GameObject currentAbility = RandomItems[randomChoice];
        currentAbility.SetActive(true);
        return randomChoice;
    }
}
