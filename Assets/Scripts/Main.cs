using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    // 게임 진행에 필요한 변수들
    [SerializeField]
    public List<string> deck; // 카드 덱
    [SerializeField]
    public List<string> playerHand; // 플레이어의 카드
    [SerializeField]
    public List<string> dealerHand; // 딜러의 카드
    [SerializeField]
    public int Turn = 0; // 현재 턴 수
    [SerializeField]
    public bool playerWin;
    [SerializeField]
    public bool dealerWin;
    [SerializeField]
    public bool Tied;
    [SerializeField]
    public bool Dead = false;
    [SerializeField]
    public int Sec;
    [SerializeField]
    public int Blood = 2;

    [SerializeField]
    public GameObject Settings;


    public HP HPscript;
    [SerializeField]
    public Cards cardScript1;
    [SerializeField]
    public Cards cardScript2;
    [SerializeField]
    public Dealer DCScript1;
    [SerializeField]
    public Dealer DCScript2;
    [SerializeField]
    public newHP hpScript;

    // UI 요소
    public Button hitButton; // Hit 버튼
    public Button dieButton; // Die 버튼
    public Button stayButton; // Stay 버튼

    [SerializeField]
    public bool isPlayerTurn = true; // 현재 턴이 플레이어의 턴인지 확인

    // 게임 시작 시 호출
    public void Start()
    {
        InitializeGame();
    }

    public void Update()
    {
        // UI 및 현재 상태 표시
        UpdateUI();
        HPscript.HPf();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Settings.SetActive(true);
        }
    }

    // 카드 덱 생성 및 초기 손패 배분, 게임 초기화
    public void InitializeGame()
    {
        hpScript.Checker();
        deck.Clear();
        // 변수 초기화
        Turn = 0;
        playerWin = false;
        dealerWin = false;
        Tied = false;
        Dead = false;
        isPlayerTurn = true;
        hitButton.interactable = true;
        dieButton.interactable = true;
        stayButton.interactable = true;

        CreateDeck(); // 덱 생성 * 4 총 52개의 카드 생성 / 기존에 존재하던 카드 기호 삭제
        CreateDeck();
        CreateDeck();
        CreateDeck();

        ShuffleDeck(deck); // 덱 섞기

        playerHand = new List<string>();
        dealerHand = new List<string>();

        // 플레이어, 딜러 카드 삭제
        playerHand.Clear();
        dealerHand.Clear();
        // 플레이어와 딜러에게 카드 2장씩 배분
        playerHand.Add(DrawCard());
        playerHand.Add(DrawCard());
        dealerHand.Add(DrawCard());
        dealerHand.Add(DrawCard());
        cardScript1.cardChange();
        cardScript2.cardChange();
        DCScript1.DCChange();
        DCScript2.DCChange();
    }

    // 덱 생성: 카드 52장 생성
    public List<string> CreateDeck()
    {
        List<string> newDeck = new List<string>();
        string[] values = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" }; // 카드 값
        foreach (string value in values)
        {
            deck.Add(value); // 예: "2_Heart"
        }

        return newDeck;
    }

    // 덱 섞기
    public void ShuffleDeck(List<string> deck)
    {
        for (int i = 0; i < deck.Count; i++)
        {
            int randomIndex = Random.Range(0, deck.Count); // 랜덤 인덱스 선택
            string temp = deck[i];
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

    // 카드 뽑기
    public string DrawCard()
    {
        if (deck.Count == 0)
        {
            Debug.LogError("덱에 남아있는게 없어요");
            return "empty";
        }

        string card = deck[0]; // 덱의 첫 번째 카드 가져오기
        deck.RemoveAt(0); // 뽑은 카드는 덱에서 제거
        return card;
    }

    // 점수 계산
    public int CalculateScore(List<string> hand)
    {
        int score = 0;
        int aceCount = 0;

        foreach (string card in hand)
        {
            string value = card.Split('_')[0]; // 카드 값만 분리
            if (int.TryParse(value, out int numericValue)) // 숫자 카드 처리
            {
                score += numericValue;
            }
            else if (value == "J" || value == "Q" || value == "K") // 그림 카드 처리
            {
                score += 10;
            }
            else if (value == "A") // 에이스 처리
            {
                aceCount++;
                score += 11;
            }
            cardScript1.cardChange();
            cardScript2.cardChange();
            DCScript1.DCChange();
            DCScript2.DCChange();
        }

        // Ace가 11로 계산되어 21을 초과할 경우, Ace를 1로 계산
        while (score > 21 && aceCount > 0)
        {
            score -= 10;
            aceCount--;
        }
        cardScript1.cardChange();
        cardScript2.cardChange();
        DCScript1.DCChange();
        DCScript2.DCChange();

        return score; // 너무 스파게티식으로 코딩해서 꼬였다 가 맞겠네요 ㅋㅋ..;;
    }

    // 플레이어가 Hit 버튼을 눌렀을 때: 카드 추가 
    /*    public void Hit()
        {
            if (!isPlayerTurn) return;

            playerHand.Add(DrawCard()); // 카드 한 장 추가
            int score = CalculateScore(playerHand); // 점수 계산
            Turn++; // 턴 증가

            Debug.Log("Player Hand: " + string.Join(", ", playerHand) + " (Score: " + score + ")");

            if (score > 21) // 점수가 21을 초과하면 Bust 처리
            {
                Bust();
            }

            UpdateUI();
        }*/


    // 플레이어가 Bust되는 경우
    public void Bust()
    {
        Dead = true;
        Debug.Log("Player Busted! Dealer Wins.");
        Blood--;
        InitializeGame(); 
    }

    // Stay 버튼: 턴을 넘기는 기능
    //public void Stay()
    //{
    //    if (!isPlayerTurn) return;

    //    Debug.Log("Player Stays. Dealer's Turn.");
    //    isPlayerTurn = false; // 플레이어 턴 종료
    //    DealerTurn();
    //    Turn++;
    //}

    // 딜러 턴: 17 이상이 될 때까지 카드 추가
    public void DealerTurn()
    {
        while (CalculateScore(dealerHand) < 17)
        {
            dealerHand.Add(DrawCard());
        }

        Debug.Log("Dealer Hand : " + string.Join(", ", dealerHand) + " (Score: " + CalculateScore(dealerHand) + ")");
        DetermineWinner();
    }

    // 승자 결정
    public void DetermineWinner()
    {
        int playerScore = CalculateScore(playerHand);
        int dealerScore = CalculateScore(dealerHand);

        if (dealerScore > 21 || playerScore > dealerScore)
        {
            Debug.Log("Player Wins!");
            InitializeGame();
        }
        else if (dealerScore > playerScore)
        {
            Debug.Log("Dealer Wins!");
            InitializeGame();
        }
        else
        {
            Debug.Log("It's a Tie!");
            InitializeGame();
        }
        hitButton.interactable = false;
        dieButton.interactable = false;
        stayButton.interactable = false;
    }

    // UI 상태 업데이트
    public void UpdateUI()
    {
        int score = CalculateScore(playerHand);

        if (score > 21 || !isPlayerTurn) // Bust 상태거나 턴 종료 시 버튼 비활성화
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

    // Die 버튼: 첫 턴에서 Die를 선택할 경우
    public void Die()
    {
        if (Turn == 0) // 첫 턴인지 확인
        {
            Debug.Log("Player Chooses to Die. Dealer Wins.");
            Bust();
        }
        return;
    }
}
