using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextBox : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI nameComponent;
    public string[] lines;
    public AudioClip[] vLines;
    public AudioSource audioPlayer;
    public float textSpeed;
    public string[] names;
    public GameObject cameraFocus; 

    private int index;

    private bool canStartTalk = true;
    private Animator anim;
    private Player P;

    void Start()
    {
        anim = GetComponent<Animator>();
        P = GameObject.Find("Player").GetComponent<Player>();
        //audioPlayer = this.gameObject.GetComponent<AudioSource>();
    }

    
    void Update() 
    {
        if (canStartTalk)
        {
            textComponent.text = string.Empty;
            nameComponent.text = string.Empty;
            if (cameraFocus != null)
                cameraFocus.SetActive(true);
            StartDialogue();
        }
        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();    
                textComponent.text = lines[index];
            }
        }   
    }

    void StartDialogue()
    {
        index = 0;
        canStartTalk = false;
        audioPlayer.clip = vLines[index];
        audioPlayer.Play();
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in names[index].ToCharArray())
        {
            nameComponent.text += c;
            //yield return new WaitForSeconds(textSpeed);
        }
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            
            textComponent.text = string.Empty;
            nameComponent.text = string.Empty;
            audioPlayer.clip = vLines[index];
            if (audioPlayer.isPlaying)
            {
                audioPlayer.Stop();

            }
            audioPlayer.Play();
            StartCoroutine (TypeLine());
        }
        else
        {
            
            anim.SetTrigger("EndDialogue");
            cameraFocus.SetActive(false);
            P.inDialog = false;
            StartCoroutine(EndDialogue());
            
        }
    }

    IEnumerator EndDialogue()
    {
        yield return new WaitForSeconds(2f);
        canStartTalk = true;
        gameObject.SetActive(false);
    }

    

}
