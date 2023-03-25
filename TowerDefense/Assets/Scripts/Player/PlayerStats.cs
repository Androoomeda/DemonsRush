using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{
    public int lives { get => _lives; }
    public int money { get => _money;}

    [SerializeField] private int startMoney;
    [SerializeField] private Text moneyText;

    [SerializeField] private Image healthBar;
    [SerializeField] private Sprite[] healthBarSprites;

    private int _lives = 5;
    private int _money;

    public UnityEvent Losed;

    private void Start()
    {
        ServiceLocator.instance.Register(this);

        _money = startMoney;
        moneyText.text = startMoney.ToString();
    }

    public void MinusLives()
    {
        if (_lives > 0)
        {
            _lives--;
            healthBar.sprite = healthBarSprites[_lives];

            if (_lives <= 0)
                Losed.Invoke();
        }
    }

    public void AddMoney(int money)
    {
        _money += money;
        moneyText.text = _money.ToString();
    }

    public void MinusMoney(int cost)
    {
        _money -= cost;
        moneyText.text = _money.ToString();
    }
}
