using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MusicNotes : MonoBehaviour
{
    private Player P;
    public bool isPotato = true;



    //UI
    public TextMeshProUGUI  scoreUI;
    public int potatoScore = 1;

    //sfx
    [SerializeField] private AudioClip[] NoteClip = null;
    [SerializeField] private AudioSource Sounds;

    // Start is called before the first frame update
    void Start()
    {
        P = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (isPotato)
            {
                P.score += potatoScore;
                scoreUI.text = "Potatoes: " + P.score;
                int soundPlayed = Random.Range(0, 5);
                Sounds.PlayOneShot(NoteClip[soundPlayed], 0.5f);
                Destroy(this.gameObject);
            }
            else
            {
                //P.fertCount += potatoScore;
                //fertUI.text = "" + P.score;
                int soundPlayed = Random.Range(0, 5);
                Sounds.PlayOneShot(NoteClip[soundPlayed], 0.5f);
                Destroy(this.gameObject);
            }
            
        }

 
    }
    
}
