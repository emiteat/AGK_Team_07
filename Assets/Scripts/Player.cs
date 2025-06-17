using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Blackjack : MonoBehaviour
{
    // ���� ���࿡ �ʿ��� ������
    [SerializeField]
    private List<string> deck; // ī�� ��
    [SerializeField]
    private List<string> playerHand; // �÷��̾��� ī��
    [SerializeField]
    private List<string> dealerHand; // ������ ī��
    [SerializeField]
    private int Turn = 0; // ���� �� ��
    [SerializeField]
    public bool playerWin;

    public TextMeshProUGUI hand;
    public TextMeshProUGUI Dealer;

    // UI ���
    public Button hitButton; // Hit ��ư
    public Button dieButton; // Die ��ư
    public Button stayButton; // Stay ��ư

    private bool isPlayerTurn = true; // ���� ���� �÷��̾��� ������ Ȯ��

    // ���� ���� �� ȣ��
    void Start()
    {
        InitializeGame();
    }

    // ���� �ʱ�ȭ: ī�� �� ���� �� �ʱ� ���� ���
    void InitializeGame()
    {
        deck = CreateDeck(); // �� ����
        ShuffleDeck(deck); // �� ����

        playerHand = new List<string>();
        dealerHand = new List<string>();

        // �÷��̾�� �������� ī�� 2�徿 ���
        playerHand.Add(DrawCard());
        playerHand.Add(DrawCard());
        dealerHand.Add(DrawCard());
        dealerHand.Add(DrawCard());

        // UI �� ���� ���� ǥ��
        UpdateUI();
        DisplayHands();
    }

    // �� ����: ī�� 52�� ����
    List<string> CreateDeck()
    {
        List<string> newDeck = new List<string>();
        string[] suits = { "Heart", "Diamond", "Clover", "Spade" }; // ī�� ���
        string[] values = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" }; // ī�� ��

        foreach (string suit in suits)
        {
            foreach (string value in values)
            {
                newDeck.Add(value + "_" + suit); // ��: "2_Heart"
            }
        }

        return newDeck;
    }

    // �� ����
    void ShuffleDeck(List<string> deck)
    {
        for (int i = 0; i < deck.Count; i++)
        {
            int randomIndex = Random.Range(0, deck.Count); // ���� �ε��� ����
            string temp = deck[i];
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

    // ī�� �̱�
    string DrawCard()
    {
        if (deck.Count == 0) return null; // ���� ī�尡 ������ null ��ȯ

        string card = deck[0]; // ���� ù ��° ī�� ��������
        deck.RemoveAt(0); // ���� ī��� ������ ����
        return card;
    }

    // ���� ���
    int CalculateScore(List<string> hand)
    {
        int score = 0;
        int aceCount = 0;

        foreach (string card in hand)
        {
            string value = card.Split('_')[0]; // ī�� ���� �и�
            if (int.TryParse(value, out int numericValue)) // ���� ī�� ó��
            {
                score += numericValue;
            }
            else if (value == "J" || value == "Q" || value == "K") // �׸� ī�� ó��
            {
                score += 10;
            }
            else if (value == "A") // ���̽� ó��
            {
                aceCount++;
                score += 11;
            }
        }

        // Ace�� 11�� ���Ǿ� 21�� �ʰ��� ���, Ace�� 1�� ���
        while (score > 21 && aceCount > 0)
        {
            score -= 10;
            aceCount--;
        }

        return score;
    }

    // �÷��̾ Hit ��ư�� ������ ��: ī�� �߰�
    public void Hit()
    {
        if (!isPlayerTurn) return;

        playerHand.Add(DrawCard()); // ī�� �� �� �߰�
        int score = CalculateScore(playerHand); // ���� ���
        Turn++; // �� ����

        Debug.Log("Player Hand: " + string.Join(", ", playerHand) + " (Score: " + score + ")");

        if (score > 21) // ������ 21�� �ʰ��ϸ� Bust ó��
        {
            Bust();
        }

        UpdateUI();
    }

    // �÷��̾ Bust�Ǵ� ���
    void Bust()
    {
        Debug.Log("Player Busted! Dealer Wins.");
        hitButton.interactable = false;
        dieButton.interactable = false;
        stayButton.interactable = false;
        isPlayerTurn = false; // �� ����
    }

    // Stay ��ư: ���� �ѱ�� ���
    public void Stay()
    {
        if (!isPlayerTurn) return;

        Debug.Log("Player Stays. Dealer's Turn.");
        isPlayerTurn = false; // �÷��̾� �� ����
        DealerTurn();
    }

    // ���� ��: 17 �̻��� �� ������ ī�� �߰�
    void DealerTurn()
    {
        while (CalculateScore(dealerHand) < 17)
        {
            dealerHand.Add(DrawCard());
        }

        Debug.Log("Dealer Hand : " + string.Join(", ", dealerHand) + " (Score: " + CalculateScore(dealerHand) + ")");
        DetermineWinner();
    }

    // ���� ����
    void DetermineWinner()
    {
        int playerScore = CalculateScore(playerHand);
        int dealerScore = CalculateScore(dealerHand);

        if (dealerScore > 21 || playerScore > dealerScore)
        {
            Debug.Log("Player Wins!");
        }
        else if (dealerScore > playerScore)
        {
            Debug.Log("Dealer Wins!");
        }
        else
        {
            Debug.Log("It's a Tie!");
        }

        hitButton.interactable = false;
        dieButton.interactable = false;
        stayButton.interactable = false;
    }

    // ���� ���� UI�� ���
    void DisplayHands()
    {
        hand.text = ("Player Hand: " + string.Join(", ", playerHand) + " (Score: " + CalculateScore(playerHand) + ")");
        Dealer.text = ("Dealer Hand: " + dealerHand[0] + ", [Hidden]"); // ������ ù ī�常 ����
    }

    // UI ���� ������Ʈ
    void UpdateUI()
    {
        int score = CalculateScore(playerHand);

        if (score > 21 || !isPlayerTurn) // Bust ���°ų� �� ���� �� ��ư ��Ȱ��ȭ
        {
            hitButton.interactable = false;
            dieButton.interactable = false;
            stayButton.interactable = false;
        }
        else
        {
            hitButton.interactable = true;
            dieButton.interactable = true;
            stayButton.interactable = true;
        }
    }

    // Die ��ư: ù �Ͽ��� Die�� ������ ���
    void Die()
    {
        if (Turn != 0) // ù ������ Ȯ��
        {
            return;
        }
        Debug.Log("Player Chooses to Die. Dealer Wins.");
        Bust();
    }
}
