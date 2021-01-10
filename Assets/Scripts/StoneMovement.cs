using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoneMovement : MonoBehaviour
{

    public Vector3 startingPos;

    public Tiles StartingTile;
    RollDice roller;
    Tiles currentTile;

    Vector3 targetPosition;
    Vector3 velocity = Vector3.zero;
    
    public Text player;

    bool scored = false;
    public bool alive = true;

    Tiles[] moveQueue;
    int index;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        roller = GameObject.FindObjectOfType<RollDice>();
        targetPosition = this.transform.position;

        startingPos = Vector3.zero;
    }

    void killStone()
    {
        alive = false;
    }

/*    bool checkLegalMove(Tiles destinationTile)
    {
        if (destinationTile.playerStone == null)
        {
            return true;
        }

        else
        {
            destinationTile.playerStone.killStone();
        }
        return false;
    }*/

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position != targetPosition)
        {
            this.transform.position = Vector3.SmoothDamp(this.transform.position, targetPosition, ref velocity, 15f * Time.deltaTime);
        } else
        {
            if (moveQueue != null && index < moveQueue.Length)
            {
                Tiles nextTile = moveQueue[index];
                
                if(nextTile == null)
                {
                    SetNewTargetPos(this.transform.position + Vector3.right * 100f);
                }

                SetNewTargetPos(nextTile.transform.position);
                index++;
            }
        }

        if (alive == false)
        {
            this.transform.position = Vector3.SmoothDamp(this.transform.position, startingPos, ref velocity, 15f * Time.deltaTime);

            if (this.transform.position == startingPos)
            {
                alive = true;
            }
        }
    }

    void SetNewTargetPos(Vector3 pos)
    {
        targetPosition = pos;
        velocity = Vector3.zero;
    }

    void OnMouseUp()
    {
        Debug.Log("click");

        int spacesToMove = roller.total;

        if (spacesToMove == 0)
        {
            if (player.text == "WHITE")
            {
                player.text = "BLACK";
            }
            else
            {
                player.text = "WHITE";
            }
            return;
        }

        moveQueue = new Tiles[spacesToMove];

        Tiles finalTile = currentTile;

        for (int i = 0; i < spacesToMove; i++)
        {
            if (finalTile == null && scored == false)
            {
                finalTile = StartingTile;
            } else
            {
                if (finalTile.NextTiles == null || finalTile.NextTiles.Length == 0)
                {
                    // MOVE TO THE STACK
                    finalTile = null;
                    scored = true;


                } else if (finalTile.NextTiles.Length > 1)
                {
                    if (player.text == "WHITE")
                    {
                        finalTile = finalTile.NextTiles[0];
                    }

                    else
                    {
                        finalTile = finalTile.NextTiles[1];
                    }

                } else
                {
                    finalTile = finalTile.NextTiles[0];
                }
            }


            moveQueue[i] = finalTile;
        }

        // checkLegalMove(finalTile);

        index = 0;
        currentTile = finalTile;
        currentTile.playerStone = this;

        if (finalTile.bonus == false)
        {

            if (player.text == "WHITE")
            {
                player.text = "BLACK";
            }
            else
            {
                player.text = "WHITE";
            }
        }
        else
        {
            animator.SetBool("bonus", true);
            animator.SetBool("bonus", false);
        }
    }
}
