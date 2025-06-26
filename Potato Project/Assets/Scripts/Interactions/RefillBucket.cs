using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefillBucket : MonoBehaviour
{
    private Player P;

    private bool canInteract = false;
    public GameObject waterIndicator;

    // Start is called before the first frame update
    void Start()
    {
        P = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
    
        if (canInteract && P.hasWateringCan && Input.GetKeyDown(KeyCode.F))
        {
            P.waterCount = P.maxWater;
            P.waterUI.text = "" + P.waterCount;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canInteract = true;
            waterIndicator.SetActive(true);
       
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canInteract = false;  
            waterIndicator.SetActive(false);
            
            
        }
    }


}
