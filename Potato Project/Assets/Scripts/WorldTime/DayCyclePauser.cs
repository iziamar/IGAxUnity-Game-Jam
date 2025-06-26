using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCyclePauser : MonoBehaviour
{
    private TimeManager TM;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        TM = GameObject.Find("TimeManager").GetComponent<TimeManager>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TM.timeCanProgress == false)
            anim.speed = 0f;
        else
            anim.speed = 1f;
    }
}
