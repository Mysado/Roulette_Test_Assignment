using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetsController : MonoBehaviour
{
    public int currentBetAmount;
    public int previousBetAmount;
    public Text currentBetText;
    public Text previousBetText;
    private BetTypeController[] betTypeControllers;
    BalanceController balanceController;
    // Start is called before the first frame update
    void Start()
    {
        balanceController = GetComponent<BalanceController>();
        betTypeControllers = FindObjectsOfType<BetTypeController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaceBet(GameObject chip, GameObject bet)
    {
        BetTypeController betTypeController = bet.GetComponent<BetTypeController>();
        ChipController chipController = chip.GetComponent<ChipController>();
        betTypeController.active = true;
        betTypeController.betValue += chipController.value;
        chip.tag = "Used";
        ChangeCurrentBet(chipController.value);
    }
    private void ChangeCurrentBet(int amount)
    {
        currentBetAmount += amount;
        currentBetText.text = currentBetAmount.ToString();
    }
    private void ChangePreviousWin(int amount)
    {
        previousBetAmount += amount;
        previousBetText.text = previousBetAmount.ToString();
    }

    private void ResetPreviousWin()
    {
        previousBetAmount = 0;
        previousBetText.text = previousBetAmount.ToString();
    }

    public void CheckBetIfCorrect(int winningNumber)
    {
        ResetPreviousWin();
        foreach(BetTypeController bet in betTypeControllers)
        {
            for(int i = 0;i<bet.numbers.Length;i++)
            {
                if(winningNumber == bet.numbers[i])
                {
                    int win = CalculateWinnings(bet);
                    ChangePreviousWin(win);
                    balanceController.ChangeBalance(win);
                }
            }
        }
        //TODO reset chips and slots;
    }

    private int CalculateWinnings(BetTypeController bet)
    {
        if(bet.betType == BetType.Single)
        {
            return Mathf.RoundToInt(bet.betValue * BetTypesEnum.single);
        }
        return 0;
    }
}
