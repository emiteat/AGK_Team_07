using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blackjack : MonoBehaviour
{
    [SerializeField]
    private List<string> deck;
    [SerializeField]
    private List<string> playerHand;
    [SerializeField]
    private List<string> dealerHand;

    public Button hitButton; // Hit ��ư
    public Button standButton; // Stand ��ư
    public Button stayButton; // Stay ��ư
    public Text statusText; // ���� �޽��� ��¿�

    private bool isPlayerTurn = true; // �÷��̾� �� ����

    void Start()
    {
        InitializeGame();
    }

    // ���� �ʱ�ȭ
    void InitializeGame()
    {
        deck = CreateDeck();
        ShuffleDeck(deck);

        playerHand = new List<string>();
        dealerHand = new List<string>();

        // �÷��̾�� �������� ī�� ���
        playerHand.Add(DrawCard());
        playerHand.Add(DrawCard());
        dealerHand.Add(DrawCard());
        dealerHand.Add(DrawCard());

        UpdateUI();
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
                score += 11;
            }
        }

        while (score > 21 && aceCount > 0)
        {
            score -= 10;
            aceCount--;
        }

        return score;
    }

    // �÷��̾� ī�� �߰� (Hit)
    public void Hit()
    {
        if (!isPlayerTurn) return;

        playerHand.Add(DrawCard());
        int score = CalculateScore(playerHand);

        Debug.Log("Player Hand: " + string.Join(", ", playerHand) + " (Score: " + score + ")");

        if (score > 21)
        {
            Bust();
        }

        UpdateUI();
    }

    // �÷��̾ Bust�Ǵ� ���
    void Bust()
    {
        Debug.Log("Player Busted! Dealer Wins.");
        statusText.text = "Player Busted! Dealer Wins.";
        hitButton.interactable = false;
        standButton.interactable = false;
        stayButton.interactable = false;
        isPlayerTurn = false;
    }

    // �÷��̾� ���� ���� (Stand)
    public void Stand()
    {
        if (!isPlayerTurn) return;

        Debug.Log("Player Stands.");
        DealerTurn();
    }

    // �÷��̾ �ڽ��� ���� �ѱ�� ��� (Stay)
    public void Stay()
    {
        if (!isPlayerTurn) return;

        Debug.Log("Player Stays. Dealer's Turn.");
        isPlayerTurn = false;
        DealerTurn();
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
        int playerScore = CalculateScore(playerHand);
        int dealerScore = CalculateScore(dealerHand);

        if (dealerScore > 21 || playerScore > dealerScore)
        {
            statusText.text = "Player Wins!";
            Debug.Log("Player Wins!");
        }
        else if (dealerScore > playerScore)
        {
            statusText.text = "Dealer Wins!";
            Debug.Log("Dealer Wins!");
        }
        else
        {
            statusText.text = "It's a Tie!";
            Debug.Log("It's a Tie!");
        }

        hitButton.interactable = false;
        standButton.interactable = false;
        stayButton.interactable = false;
    }

    // ���� ���� ���
    void DisplayHands()
    {
        Debug.Log("Player Hand: " + string.Join(", ", playerHand) + " (Score: " + CalculateScore(playerHand) + ")");
        Debug.Log("Dealer Hand: " + dealerHand[0] + ", [Hidden]");
    }

    // UI ���� ������Ʈ
    void UpdateUI()
    {
        int score = CalculateScore(playerHand);

        if (score > 21 || !isPlayerTurn)
        {
            hitButton.interactable = false;
            standButton.interactable = false;
            stayButton.interactable = false;
        }
        else
        {
            hitButton.interactable = true;
            standButton.interactable = true;
            stayButton.interactable = true;
            statusText.text = "Player's Turn!";
        }
    }
}
