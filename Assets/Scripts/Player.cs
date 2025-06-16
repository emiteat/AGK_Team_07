using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private List<string> deck;
    [SerializeField]
    private List<List<string>> playerHands; // 플레이어들의 손패
    [SerializeField]
    private List<string> dealerHand;
    [SerializeField]
    private int currentPlayer; // 현재 차례의 플레이어

    public Button hitButton; // Hit 버튼
    public Button standButton; // Stand 버튼
    public Text statusText; // 상태 메시지 출력용

    void Start()
    {
        InitializeGame();
    }

    // 게임 초기화
    void InitializeGame()
    {
        deck = CreateDeck();
        ShuffleDeck(deck);

        playerHands = new List<List<string>>();
        dealerHand = new List<string>();

        // 플레이어 생성
        for (int i = 0; i < 3; i++)
        {
            playerHands.Add(new List<string>());
            playerHands[i].Add(DrawCard());
            playerHands[i].Add(DrawCard());
        }

        // 딜러 카드 배분
        dealerHand.Add(DrawCard());
        dealerHand.Add(DrawCard());

        currentPlayer = 0; // 첫 번째 플레이어부터 시작

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
        if (deck.Count == 0) return null; // deck.Count가 0이라면 null값 반환

        string card = deck[0]; // string 타입의 card를 deck의 0번째 배열에 기입
        deck.RemoveAt(0); // deck에 있는 0번째 배열을 삭제
        return card; // card 값 반환
    }

    // 점수 계산
    int CalculateScore(List<string> hand) // 점수 계산
    {
        int score = 0;
        int aceCount = 0;

        foreach (string card in hand) // card 값만큼 
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
                score += 11; // Ace는 11로 계산
            }
        }

        while (score > 21 && aceCount > 0)
        {
            score -= 10; // Ace를 1로 계산
            aceCount--;
        }

        return score;
    }

    // 플레이어 카드 추가
    public void PlayerHit()
    {
        if (currentPlayer >= playerHands.Count) return; // 모든 플레이어가 턴을 끝냈다면 무시

        playerHands[currentPlayer].Add(DrawCard());
        Debug.Log("Player " + (currentPlayer + 1) + " Hand: " + string.Join(", ", playerHands[currentPlayer]) +
                  " (Score: " + CalculateScore(playerHands[currentPlayer]) + ")");

        if (CalculateScore(playerHands[currentPlayer]) > 21)
        {
            Debug.Log("Player " + (currentPlayer + 1) + " Busted!");
            NextTurn();
        }

        UpdateUI();
    }

    // 플레이어 차례 종료
    public void PlayerStand()
    {
        Debug.Log("Player " + (currentPlayer + 1) + " Stands.");
        NextTurn();
    }

    // 다음 차례로 이동
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
            UpdateUI();
            Debug.Log("Player " + (currentPlayer + 1) + "'s Turn.");
        }
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

    // 현재 손패 출력
    void DisplayHands()
    {
        for (int i = 0; i < playerHands.Count; i++)
        {
            Debug.Log("Player " + (i + 1) + " Hand: " + string.Join(", ", playerHands[i]) +
                      " (Score: " + CalculateScore(playerHands[i]) + ")");
        }

        Debug.Log("Dealer Hand: " + dealerHand[0] + ", [Hidden]");
    }

    // UI 상태 업데이트
    void UpdateUI()
    {
        if (currentPlayer >= playerHands.Count)
        {
            hitButton.interactable = false;
            standButton.interactable = false;
            statusText.text = "Dealer's Turn";
        }
        else
        {
            hitButton.interactable = true;
            standButton.interactable = true;
            statusText.text = "Player " + (currentPlayer + 1) + "'s Turn!";
        }
    }
}
