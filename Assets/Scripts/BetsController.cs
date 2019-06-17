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
    private bool displayingResults = false;
    public GameObject winMessage;
    public GameObject loseMessage;

    public GameObject wonAmountObj;
    public GameObject numberDrawnObj;
    public Text wonAmountNumber;

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
    private void ResetCurrentBet()
    {
        currentBetAmount = 0;
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
        if(!displayingResults)
        {
            displayingResults = true;
            ResetPreviousWin();
            bool result = false;
            foreach (BetTypeController bet in betTypeControllers)
            {
                for (int i = 0; i < bet.numbers.Length; i++)
                {
                    if (bet.active)
                    {
                        if (winningNumber == bet.numbers[i])
                        {
                            result = true;
                            int win = CalculateWinnings(bet);
                            ChangePreviousWin(win);
                            balanceController.ChangeBalance(win);
                        }
                    }

                }
            }
            ShowMessage(result);
            CleanBoard();
            ResetSlots();
            ResetCurrentBet();
        }
    }

    private int CalculateWinnings(BetTypeController bet)
    {
        if(bet.betType == BetType.Single)
        {
            return Mathf.RoundToInt(bet.betValue * BetTypesEnum.single);
        }
        if (bet.betType == BetType.Split)
        {
            return Mathf.RoundToInt(bet.betValue * BetTypesEnum.Split);
        }
        if (bet.betType == BetType.Street)
        {
            return Mathf.RoundToInt(bet.betValue * BetTypesEnum.Street);
        }
        if (bet.betType == BetType.Corner)
        {
            return Mathf.RoundToInt(bet.betValue * BetTypesEnum.Corner);
        }
        if (bet.betType == BetType.Six_Line)
        {
            return Mathf.RoundToInt(bet.betValue * BetTypesEnum.Six_Line);
        }
        if (bet.betType == BetType.Trio)
        {
            return Mathf.RoundToInt(bet.betValue * BetTypesEnum.Trio);
        }
        if (bet.betType == BetType.First_Four)
        {
            return Mathf.RoundToInt(bet.betValue * BetTypesEnum.First_Four);
        }
        if (bet.betType == BetType.Dozen)
        {
            return Mathf.RoundToInt(bet.betValue * BetTypesEnum.Dozen);
        }
        if (bet.betType == BetType.Half)
        {
            return Mathf.RoundToInt(bet.betValue * BetTypesEnum.Half);
        }
        return 0;
    }

    private void CleanBoard()
    {
        GameObject[] usedChips = GameObject.FindGameObjectsWithTag("Used");
        foreach(GameObject chip in usedChips)
        {
            Destroy(chip);
        }
    }

    private void ResetSlots()
    {
        GameObject[] slots = GameObject.FindGameObjectsWithTag("Slot");
        foreach (GameObject slot in slots)
        {
            if(slot.GetComponent<BetTypeController>())
            {
                BetTypeController betTypeController = slot.GetComponent<BetTypeController>();
                betTypeController.active = false;
                betTypeController.betValue = 0;
            }

        }
    }

    private void ShowMessage(bool val)
    {
        if(val)
        {
            StartCoroutine(ToggleMessage(winMessage));
            StartCoroutine(ToggleMessage(wonAmountObj));
            wonAmountNumber.text = previousBetAmount.ToString();
        }
        else
        {
            StartCoroutine(ToggleMessage(loseMessage));
        }
        StartCoroutine(ToggleMessage(numberDrawnObj));
        
    }



    IEnumerator ToggleMessage(GameObject message)
    {
        message.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        message.SetActive(false);
        displayingResults = false;
    }
}
