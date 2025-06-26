using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropShadowScore : MonoBehaviour
{
    public TextMeshProUGUI  scoreUI;
    private TextMeshProUGUI  dropScoreUI;

    // Start is called before the first frame update
    void Start()
    {
        dropScoreUI = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dropScoreUI.text != scoreUI.text)
            dropScoreUI.text = scoreUI.text;
    }
}
