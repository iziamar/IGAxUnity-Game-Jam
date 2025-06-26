using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OptionPopUp : MonoBehaviour
{
    public GameObject popUpBox;
    public Animator animator;
    public TMP_Text popUpText;

    private TimeManager TM;

    public void PopUp(string text)
    {
        popUpText.text = text;
        popUpBox.SetActive(true);
        animator.SetTrigger("pop");
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            PopUp("Press E to Sleep");
        }
        /*if (Input.GetKeyDown(KeyCode.E))
       {
            TM.resetday
        }*/
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            popUpBox.SetActive(false);
    }

}


