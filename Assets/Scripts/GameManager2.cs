using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager2 : MonoBehaviour
{
    [SerializeField] private int maxItems = 5;
    [SerializeField] private bool showWinScreen = false;
    private bool showLossScreen = false;
    public string labelText = "Collect all items and win your freedom!";
    private int _itemsCollected = 0;
    private int nextLvlIndex;
    [SerializeField] private int _playerHP = 5;
    public int Items
    {
        get { return _itemsCollected; }
        set
        {
            _itemsCollected = value;
            Debug.LogFormat("Items: {0}", _itemsCollected);

            if (_itemsCollected >= maxItems)
            {
                labelText = "You can go!";


            }
            else
            {
                labelText = "Item found, only " + (maxItems - _itemsCollected) + " more to go!";
            }
        }
    }
    public int HP
    {
        get { return _playerHP; }
        set
        {
            _playerHP = value;
            if (_playerHP <= 0)
            {
                Time.timeScale = 0f;
                showLossScreen = true;
            }
            else
            {
                labelText = "Ouch... That's got hurt";
            }
            Debug.LogFormat("Lives: {0}", _playerHP);
        }
    }
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt))
        {
            Cursor.visible = !Cursor.visible;
            Cursor.lockState = Cursor.visible ? CursorLockMode.None : CursorLockMode.Locked;
        }

    }
    void OnGUI()
    {
        GUI.Box(new Rect(20, 20, 150, 25),
            "Player Health:" + _playerHP);
        GUI.Box(new Rect(20, 50, 150, 25),
            "Items collected: " + _itemsCollected);
        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 50, 300, 50),
            labelText);

        if (showWinScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 200, 200, 100), "Next Level!"))
            {
                Utilities.RestartLevel(nextLvlIndex);
                showWinScreen = false;
            }
        }
        if (showLossScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 200, 200, 100), "You lose..."))
            {
                Utilities.RestartLevel();
            }
        }
    }
}
