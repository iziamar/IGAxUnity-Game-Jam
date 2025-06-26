using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveManager : MonoBehaviour
{
    private DayChecker dC;

    // Start is called before the first frame update
    void Start()
    {
        dC = GameObject.Find("TimeManager").GetComponent<DayChecker>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dC.Day >= 2)
            Destroy(this.gameObject);
    }
}
