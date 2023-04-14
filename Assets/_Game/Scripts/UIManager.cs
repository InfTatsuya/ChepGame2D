using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public const string COIN_KEY = "CoinNumber";

    private static UIManager instance;

    public static UIManager Instance
    {
        get { return instance; }
        private set { instance = value; }
    }

    public event EventHandler onAttackButtonDown;
    public event EventHandler onJumpButtonDown;
    public event EventHandler onThrownButtonDown;

    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] Button attackButton;
    [SerializeField] Button jumpButton;
    [SerializeField] Button throwButton;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        attackButton.onClick.AddListener(OnAttack);
        jumpButton.onClick.AddListener(OnJump);
        throwButton.onClick.AddListener(OnThrow);
    }

    private void OnAttack()
    {
        onAttackButtonDown?.Invoke(this, EventArgs.Empty);
    }

    private void OnJump()
    {
        onJumpButtonDown?.Invoke(this, EventArgs.Empty);
    }

    private void OnThrow()
    {
        onThrownButtonDown?.Invoke(this, EventArgs.Empty);
    }

    public void SetCoinText(int coinNumber)
    {
        coinText.text = coinNumber.ToString();
        PlayerPrefs.SetInt(COIN_KEY, coinNumber);
    }
}
