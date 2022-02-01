using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int _coins;

    public MenuUI _menuUI;

    private void Awake()
    {
        Application.targetFrameRate = 30;

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this); //Singleton
            return;
        }
        Destroy(this.gameObject);
    }

    private void Start()
    {
        _menuUI = GameObject.Find("GameCanvas").GetComponent<MenuUI>();   
    }

    [ContextMenu("Add Coin")]
    public void AddCoins()
    {
        int oldCoinValue = _coins;
        int newCoinValue = _coins + 100000;
        _menuUI.UpdateCoinField(oldCoinValue, newCoinValue, 1);
        _coins = newCoinValue;
    }
}

