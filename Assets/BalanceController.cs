using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalanceController : MonoBehaviour
{
    private int balanceAmount = 1000;
    public Text balanceAmountText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeBalance(int amount)
    {
        balanceAmount += amount;
        balanceAmountText.text = balanceAmount.ToString();
    }

    public bool CheckIfCanSpawn(int amount)
    {
        if(balanceAmount-amount < 0)
        {
            return false;
        }
        return true;
    }
}
