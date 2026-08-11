using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboController : MonoBehaviour
{
    private int comboStep = 0;
    private bool canCombo = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EnableCombo()
    {
        canCombo = true;
    }
    public void DisableCombo() 
    { 
        canCombo = false;
    }
}
