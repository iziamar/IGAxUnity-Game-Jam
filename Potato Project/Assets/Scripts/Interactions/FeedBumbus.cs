using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedBumbus : MonoBehaviour
{
    //private happiness HP
    private bool canInteract = false;
    private Player P;
    public GameObject PotatoFeeder;
    public GameObject feedIndictator;


    // Start is called before the first frame update
    void Start()
    {
        P = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.F))
        {
            PotatoFeeder.SetActive(true);
            feedIndictator.SetActive(false);
            PotatoFeeder.GetComponent<PotatoFeeder>().TM.timeCanProgress = false;
            
            
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canInteract = true;   
            feedIndictator.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canInteract = false;   
            feedIndictator.SetActive(false);
        }
    }
}
