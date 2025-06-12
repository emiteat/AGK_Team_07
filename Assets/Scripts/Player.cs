using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    public List<string> deck;
    [SerializeField]
    public List<List<string>> playerHands; // �÷��̾���� ����
    [SerializeField] 
    public List<string> dealerHand;
    [SerializeField]
    private int currentPlayer; // ���� ������ �÷��̾�

    void Start()
    {
        InitializeGame();
    }

    // ���� �ʱ�ȭ
    void InitializeGame()
    {
        deck = CreateDeck();
        ShuffleDeck(deck);

        playerHands = new List<List<string>>();
        dealerHand = new List<string>();

        // 3���� �÷��̾� ����
        for (int i = 0; i < 3; i++)
        {
            playerHands.Add(new List<string>());
            playerHands[i].Add(DrawCard());
            playerHands[i].Add(DrawCard());
        }

        // �������� ī�� 2�� ���
        dealerHand.Add(DrawCard());
        dealerHand.Add(DrawCard());

        currentPlayer = 0; // ù ��° �÷��̾���� ����

        Debug.Log("Game Start!");
        DisplayHands();
    }

    // �� ����
    List<string> CreateDeck()
    {
        List<string> newDeck = new List<string>();
        string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };
        string[] values = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };

        foreach (string suit in suits)
        {
            foreach (string value in values)
            {
                newDeck.Add(value + " of " + suit);
            }
        }

        return newDeck;
    }

    // �� ����
    void ShuffleDeck(List<string> deck)
    {
        for (int i = 0; i < deck.Count; i++)
        {
            int randomIndex = Random.Range(0, deck.Count);
            string temp = deck[i];
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

    // ī�� �̱�
    string DrawCard()
    {
        if (deck.Count == 0) return null;

        string card = deck[0];
        deck.RemoveAt(0);
        return card;
    }

    // ���� ���
    int CalculateScore(List<string> hand)
    {
        int score = 0;
        int aceCount = 0;

        foreach (string card in hand)
        {
            string value = card.Split(' ')[0];
            if (int.TryParse(value, out int numericValue))
            {
                score += numericValue;
            }
            else if (value == "J" || value == "Q" || value == "K")
            {
                score += 10;
            }
            else if (value == "A")
            {
                aceCount++;
                score += 11; // Ace�� 11�� ���
            }
        }

        while (score > 21 && aceCount > 0)
        {
            score -= 10; // Ace�� 1�� ���
            aceCount--;
        }

        return score;
    }

    // �÷��̾� ī�� �߰�
    public void PlayerHit()
    {
        if (currentPlayer >= playerHands.Count) return; // ��� �÷��̾ ���� ���´ٸ� ����

        playerHands[currentPlayer].Add(DrawCard());
        Debug.Log("Player " + (currentPlayer + 1) + " Hand: " + string.Join(", ", playerHands[currentPlayer]) +
                  " (Score: " + CalculateScore(playerHands[currentPlayer]) + ")");

        if (CalculateScore(playerHands[currentPlayer]) > 21)
        {
            Debug.Log("Player " + (currentPlayer + 1) + " Busted!");
            NextTurn();
        }
    }

    // �÷��̾� ���� ����
    public void PlayerStand()
    {
        Debug.Log("Player " + (currentPlayer + 1) + " Stands.");
        NextTurn();
    }

    // ���� ���ʷ� �̵�
    void NextTurn()
    {
        currentPlayer++;

        if (currentPlayer >= playerHands.Count)
        {
            Debug.Log("All players have finished. Dealer's Turn.");
            DealerTurn();
        }
        else
        {
            Debug.Log("Player " + (currentPlayer + 1) + "'s Turn.");
        }
    }

    // ���� ����
    void DealerTurn()
    {
        while (CalculateScore(dealerHand) < 17)
        {
            dealerHand.Add(DrawCard());
        }

        Debug.Log("Dealer Hand: " + string.Join(", ", dealerHand) + " (Score: " + CalculateScore(dealerHand) + ")");
        DetermineWinner();
    }

    // ���� ����
    void DetermineWinner()
    {
        int dealerScore = CalculateScore(dealerHand);

        for (int i = 0; i < playerHands.Count; i++)
        {
            int playerScore = CalculateScore(playerHands[i]);

            if (playerScore > 21)
            {
                Debug.Log("Player " + (i + 1) + " Busted! Dealer Wins.");
            }
            else if (dealerScore > 21 || playerScore > dealerScore)
            {
                Debug.Log("Player " + (i + 1) + " Wins!");
            }
            else if (dealerScore > playerScore)
            {
                Debug.Log("Dealer Wins against Player " + (i + 1) + "!");
            }
            else
            {
                Debug.Log("Player " + (i + 1) + " and Dealer Tie!");
            }
        }
    }

    // ���� ���� ���
    void DisplayHands()
    {
        for (int i = 0; i < playerHands.Count; i++)
        {
            Debug.Log("Player " + (i + 1) + " Hand: " + string.Join(", ", playerHands[i]) +
                      " (Score: " + CalculateScore(playerHands[i]) + ")");
        }

        Debug.Log("Dealer Hand: " + dealerHand[0] + ", [Hidden]");
    }
}
