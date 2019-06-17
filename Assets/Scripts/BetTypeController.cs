using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetTypeController : MonoBehaviour
{
    public int betValue;
    public BetType betType;
    public bool active;
    public int[] numbers;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeState()
    {
        active = !active;
    }
}
