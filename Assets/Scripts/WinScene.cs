﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScene : MonoBehaviour
{
    public Text winnerText;
    public Text winText;
    public static string winner;

    // Start is called before the first frame update
    void Start()
    {
        if (Board.isMultiplayer)
        {
            winnerText.text = Board.curPlayer;
            winText.text = "gewinnt!";
        } else
        {
            winnerText.text = "Du hast";
            winText.text = "verloren!";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
