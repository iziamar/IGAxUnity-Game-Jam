using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PotatoFeeder : MonoBehaviour
{
    private Player P;
    private float potatoesTaken = 0;
    public float potatoesConsumed = 0;

    public TextMeshProUGUI potatoNumber;
    public TextMeshProUGUI scoreUI;

    private Animator anim;

    public GameObject BumbusNPC;
    public GameObject BumbusAnimation;
    public GameObject CutsceneCamera;

    public TimeManager TM;

    // Start is called before the first frame update
    void Start()
    {
        P = GameObject.Find("Player").GetComponent<Player>();
        TM = GameObject.Find("TimeManager").GetComponent<TimeManager>();
        potatoesTaken = P.score;
        potatoNumber.text = "x" + potatoesConsumed +"/"+ P.score;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!TM.timeCanProgress)
        {
            P.inDialog = true;
        }

       
    }

    public void IncreasePotatoes()
    {
        if(potatoesTaken >= 1)
        {
            potatoesTaken--;
            potatoesConsumed++;
            potatoNumber.text = "x" + potatoesConsumed +"/"+ P.score;
        }
    }

    public void DecreasePotatoes()
    {
        if(potatoesConsumed > 0)
        {
            potatoesTaken++;
            potatoesConsumed--;
            potatoNumber.text = "x" + potatoesConsumed +"/"+ P.score;
        }
    }

    public void IncreasePotatoesFive()
    {
        if(potatoesTaken >= 5)
        {
            potatoesTaken -= 5;
            potatoesConsumed += 5;
            potatoNumber.text = "x" + potatoesConsumed +"/"+ P.score;
        }
    }

    public void DecreasePotatoesFive()
    {
        if(potatoesConsumed > 5)
        {
            potatoesTaken += 5;
            potatoesConsumed -= 5;
            potatoNumber.text = "x" + potatoesConsumed +"/"+ P.score;
        }
    }

    public void FeedPotatoes()
    {
        if (potatoesTaken != P.score)
        {
            P.score -= potatoesConsumed;
            P.totalPotatoesFed += potatoesTaken; 
            scoreUI.text = "Potatoes: " + P.score;
            P.GetComponent<SpriteRenderer>().enabled = false;
            P.gameObject.transform.position = BumbusAnimation.transform.GetChild(0).transform.position;
            BumbusAnimation.SetActive(true);
            BumbusNPC.SetActive(false);
            CutsceneCamera.SetActive(true);
            anim.SetTrigger("Disappear");
            StartCoroutine(EndCutscene());
        }
            
    }

    public void ResetPotatoes()
    {
        
        potatoesConsumed = 0; 
        potatoNumber.text = "x" + potatoesConsumed +"/"+ P.score;
    }

    public void LeaveMenu()
    {
        potatoesConsumed = 0; 
        potatoNumber.text = "x" + potatoesConsumed +"/"+ P.score;
        anim.SetTrigger("Disappear");
        TM.timeCanProgress = true;
        P.inDialog = false;
        StartCoroutine(UIVanish());
    }

    IEnumerator UIVanish()
    {
        yield return new WaitForSeconds(1.75f);
        potatoesTaken = P.score;
        potatoesConsumed = 0;
        potatoNumber.text = "x" + potatoesConsumed +"/"+ P.score;
        this.gameObject.SetActive(false);
    }

    IEnumerator EndCutscene()
    {
        yield return new WaitForSeconds(3f);
        potatoesTaken = P.score;
        potatoesConsumed = 0;
        potatoNumber.text = "x" + potatoesConsumed +"/"+ P.score;
        P.GetComponent<SpriteRenderer>().enabled = true;
        BumbusNPC.SetActive(true);
        BumbusAnimation.SetActive(false);
        CutsceneCamera.SetActive(false);
        TM.timeCanProgress = true;
        P.inDialog = false;
        this.gameObject.SetActive(false);
    }


}
