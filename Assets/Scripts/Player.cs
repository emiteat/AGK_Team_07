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

    public Button hitButton; // Hit 버튼
    public Button standButton; // Stand 버튼
    public Button stayButton; // Stay 버튼
    public Text statusText; // 상태 메시지 출력용

    private bool isPlayerTurn = true; // 플레이어 턴 여부

    void Start()
    {
        InitializeGame();
    }

    // 게임 초기화
    void InitializeGame()
    {
        deck = CreateDeck();
        ShuffleDeck(deck);

        playerHand = new List<string>();
        dealerHand = new List<string>();

        // 플레이어와 딜러에게 카드 배분
        playerHand.Add(DrawCard());
        playerHand.Add(DrawCard());
        dealerHand.Add(DrawCard());
        dealerHand.Add(DrawCard());

        UpdateUI();
        DisplayHands();
    }

    // 덱 생성
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

    // 덱 섞기
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

    // 카드 뽑기
    string DrawCard()
    {
        if (deck.Count == 0) return null;

        string card = deck[0];
        deck.RemoveAt(0);
        return card;
    }

    // 점수 계산
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

    // 플레이어 카드 추가 (Hit)
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

    // 플레이어가 Bust되는 경우
    void Bust()
    {
        Debug.Log("Player Busted! Dealer Wins.");
        statusText.text = "Player Busted! Dealer Wins.";
        hitButton.interactable = false;
        standButton.interactable = false;
        stayButton.interactable = false;
        isPlayerTurn = false;
    }

    // 플레이어 차례 종료 (Stand)
    public void Stand()
    {
        if (!isPlayerTurn) return;

        Debug.Log("Player Stands.");
        DealerTurn();
    }

    // 플레이어가 자신의 턴을 넘기는 경우 (Stay)
    public void Stay()
    {
        if (!isPlayerTurn) return;

        Debug.Log("Player Stays. Dealer's Turn.");
        isPlayerTurn = false;
        DealerTurn();
    }

    // 딜러 차례
    void DealerTurn()
    {
        while (CalculateScore(dealerHand) < 17)
        {
            dealerHand.Add(DrawCard());
        }

        Debug.Log("Dealer Hand: " + string.Join(", ", dealerHand) + " (Score: " + CalculateScore(dealerHand) + ")");
        DetermineWinner();
    }

    // 승자 결정
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

    // 현재 손패 출력
    void DisplayHands()
    {
        Debug.Log("Player Hand: " + string.Join(", ", playerHand) + " (Score: " + CalculateScore(playerHand) + ")");
        Debug.Log("Dealer Hand: " + dealerHand[0] + ", [Hidden]");
    }

    // UI 상태 업데이트
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
