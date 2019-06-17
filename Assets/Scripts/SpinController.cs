using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpinController : MonoBehaviour
{
    public int randomNumber;
    private BetsController betsController;
    public Text numberDrawn;
    public GameObject drawnText;
    // Start is called before the first frame update
    void Start()
    {
        betsController = GameObject.FindGameObjectWithTag("GameController").GetComponent<BetsController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpinRoulette()
    {
        randomNumber = Random.Range(1, 37);
        drawnText.SetActive(true);
        numberDrawn.text = randomNumber.ToString();
        betsController.CheckBetIfCorrect(randomNumber);
        
    }
}
