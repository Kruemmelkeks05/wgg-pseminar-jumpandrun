using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Coins : MonoBehaviour
{
    public int geld;
    public Text money;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        money.text = geld.ToString();
    }
    public void AddMoney()
    {
        geld++;
    }

    public int getGeld()
    {
        return geld;
    }
}