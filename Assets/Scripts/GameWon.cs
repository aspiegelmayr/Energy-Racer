﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWon : MonoBehaviour
{
    public GameObject confettiObj;
    public Sprite sprite;
  

    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = Instantiate(confettiObj);

        /**
        GameObject spriteObj = new GameObject();
        SpriteRenderer renderer = spriteObj.AddComponent<SpriteRenderer>();
        renderer.sprite = sprite;
        */

        Destroy(obj, 6.0f);


    }
}
