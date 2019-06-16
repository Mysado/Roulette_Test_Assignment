using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinController : MonoBehaviour
{
    public int randomNumber;
    private BetsController betsController;
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
        randomNumber = Random.Range(1, 2);
        betsController.CheckBetIfCorrect(randomNumber);
    }
}
