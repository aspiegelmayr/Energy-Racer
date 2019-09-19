﻿using System.Collections; using UnityEngine;  public class Dot : MonoBehaviour {     private Vector2 firstTouch;     private Vector2 secondTouch;     public float swipe = 0; //angle     public int row;     public int col;     private GameObject swapWith;     private Board board;     public int targetX;     public int targetY;     private Vector2 tempPos;     public bool isMatched = false;     public float swipeResist = 1f;     private FindMatches findMatches;         // Start is called before the first frame update     void Start()     {         board = FindObjectOfType<Board>();         targetX = (int)transform.position.x;         targetY = (int)transform.position.y;         row = targetY;         col = targetX;         findMatches = FindObjectOfType<FindMatches>();     }      // Update is called once per frame     void Update()     {         //FindMatches();         if (isMatched)
        {
            SpriteRenderer spriteRender = GetComponent<SpriteRenderer>();             spriteRender.color = new Color(0f, 0f, 0f, .2f);
        }         targetX = col;         targetY = row;         if(Mathf.Abs(targetX-transform.position.x) > .1)         {             tempPos = new Vector2(targetX, transform.position.y);             transform.position = Vector2.Lerp(transform.position, tempPos, .6f);             if(board.allDots[col,row]!= this.gameObject)
            {
                board.allDots[col, row] = this.gameObject;
            }             findMatches.findAllMatches();         }         else         {             tempPos = new Vector2(targetX, transform.position.y);             transform.position = tempPos;             board.allDots[col, row] = this.gameObject;         }          if(Mathf.Abs(targetY - transform.position.y) > .1) {             tempPos = new Vector2(transform.position.x, targetY);              transform.position = Vector2.Lerp(transform.position, tempPos, .6f);             if (board.allDots[col, row] != this.gameObject)             {                 board.allDots[col, row] = this.gameObject;             }
            findMatches.findAllMatches();          } else         {             tempPos = new Vector2(transform.position.x, targetY);             transform.position = tempPos;         }     }      void OnMouseDown()     {        
            firstTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);     }      private void OnMouseUp()     {         
            secondTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GetSwipeAngle();
             }      void GetSwipeAngle()     {         if (Mathf.Abs(secondTouch.y - firstTouch.y) > swipeResist || Mathf.Abs(secondTouch.x - firstTouch.x) > swipeResist)
        {
            swipe = Mathf.Atan2(secondTouch.y - firstTouch.y, secondTouch.x - firstTouch.x) * 180 / Mathf.PI;
            SwapPieces();             //board.curState = GameState.wait;
        }     }      void SwapPieces()     {
            //move right
            if (swipe > -45 && swipe <= 45 && col < board.width - 1)
            {
                swapWith = board.allDots[col + 1, row];
                swapWith.GetComponent<Dot>().col -= 1;
                col += 1;   
            }
            //move up
            else if (swipe > 45 && swipe <= 135 && row < board.height - 1)
            {
                swapWith = board.allDots[col, row + 1];
                swapWith.GetComponent<Dot>().row -= 1;
                row += 1;   
            }
            //move left
            else if ((swipe > 135 || swipe <= -135) && col > 0)
            {
                swapWith = board.allDots[col - 1, row];
                swapWith.GetComponent<Dot>().col += 1;
                col -= 1;
            }
            // move down
            else if (swipe < -45 && swipe >= -135 && row > 0)
            {
                swapWith = board.allDots[col, row - 1];
                swapWith.GetComponent<Dot>().row += 1;
                row -= 1;
            }

        board.remainingMoves--;
        if (board.remainingMoves == 1)
        {
            board.movesText.text = "1 Zug übrig";
        }
        else
        {
            board.movesText.text = board.remainingMoves + " Züge übrig";
        }

        StartCoroutine(CheckMoveCo());           board.updateCoins();     }      void FindMatches()
    {         //horizontal
        if (col > 0 && col < board.width - 1)
        {
            GameObject leftDot1 = board.allDots[col - 1, row];             GameObject rightDot1 = board.allDots[col + 1, row];             if (leftDot1 != null && rightDot1 != null)
            {
                if (leftDot1.tag == this.gameObject.tag && this.gameObject.tag == rightDot1.tag)
                {
                    leftDot1.GetComponent<Dot>().isMatched = true;
                    rightDot1.GetComponent<Dot>().isMatched = true;
                    this.isMatched = true;
                }
            }
        }          //vertical         if (row > 0 && row < board.height - 1)         {             GameObject upDot1 = board.allDots[col, row + 1];             GameObject downDot1 = board.allDots[col, row - 1];             if (upDot1 != null & downDot1 != null)
            {
                if (upDot1.tag == this.gameObject.tag && this.gameObject.tag == downDot1.tag)
                {
                    upDot1.GetComponent<Dot>().isMatched = true;
                    downDot1.GetComponent<Dot>().isMatched = true;
                    this.isMatched = true;
                }             }         }

    }      public IEnumerator CheckMoveCo()
    {
        yield return new WaitForSeconds(.5f);         if(swapWith != null)
        {                         if(!isMatched && !swapWith.GetComponent<Dot>().isMatched)
            {
                board.checkGameOver();
                yield return new WaitForSeconds(.4f);
            }             else
            {
                board.DestroyMatches();
            }
        }
    } } 