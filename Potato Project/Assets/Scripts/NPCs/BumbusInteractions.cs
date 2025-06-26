using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumbusInteractions : MonoBehaviour
{
    private bool canInteract = false;
    public GameObject[] dialogue;
    private DayChecker dC;
    private Player P;
    public GameObject chatIndicator;

    // Start is called before the first frame update
    void Start()
    {
        dC = GameObject.Find("TimeManager").GetComponent<DayChecker>();
        P = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E) )
        {
            dialogue[dC.Day].SetActive(true);
            P.inDialog = true;
            chatIndicator.SetActive(false);
        }
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canInteract = true;  
            chatIndicator.SetActive(true); 
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canInteract = false;   
            chatIndicator.SetActive(false);
        }
    }
}
