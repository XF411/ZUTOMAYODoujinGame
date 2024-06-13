using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour
{
    public Text enemyHealthText;
    public Text[] cardTexts;
    private int enemyHealth = 100;
    private List<int> deck = new List<int>();
    private List<int> hand = new List<int>();

    void Start()
    {
        InitializeDeck();
        DrawCards();
        UpdateUI();
    }

    void InitializeDeck()
    {
        for (int i = 1; i <= 12; i++)
        {
            deck.Add(i);
        }
        ShuffleDeck();
    }

    void ShuffleDeck()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            int temp = deck[i];
            int randomIndex = Random.Range(0, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

    void DrawCards()
    {
        hand.Clear();
        int drawCount = Mathf.Min(4, deck.Count);
        for (int i = 0; i < drawCount; i++)
        {
            hand.Add(deck[0]);
            deck.RemoveAt(0);
        }

        if (deck.Count == 0)
        {
            InitializeDeck();
        }
    }

    public void PlayCard(int cardIndex)
    {
        if (cardIndex < 0 || cardIndex >= hand.Count) return;

        int damage = hand[cardIndex];
        enemyHealth -= damage;
        if (enemyHealth <= 0)
        {
            enemyHealth = 0;
            Debug.Log("Enemy defeated!");
        }

        DrawCards();
        UpdateUI();
    }

    void UpdateUI()
    {
        enemyHealthText.text = "Enemy Health: " + enemyHealth;

        for (int i = 0; i < cardTexts.Length; i++)
        {
            if (i < hand.Count)
            {
                cardTexts[i].text = "Card: " + hand[i];
            }
            else
            {
                cardTexts[i].text = "Card: -";
            }
        }
    }
}
