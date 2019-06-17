using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnChip : MonoBehaviour
{
    public bool empty;
    public int spawnValue;
    public GameObject chipToSpawn;
    public GameObject currentChipInside;
    private BalanceController balanceController;
    // Start is called before the first frame update
    void Start()
    {
        balanceController = GameObject.FindGameObjectWithTag("GameController").GetComponent<BalanceController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Draggable"))
        {
            if (other.GetComponent<ChipController>().value == spawnValue)
            {
                if(other.gameObject == currentChipInside)
                {
                    if(balanceController.CheckIfCanSpawn(spawnValue))
                    {
                        currentChipInside = Instantiate(chipToSpawn, transform);
                        balanceController.ChangeBalance(-spawnValue);
                    }

                }
                
            }
        }
    }
}
