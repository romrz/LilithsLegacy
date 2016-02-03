using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MovingObject {

    public bool moveFast = false;

    private GameObject player;
    private Player playerScript;

    List<Vector2> positions = new List<Vector2>();

    protected override void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();

        base.Start();
    }

    void Update() {
        if(moveFast)
        {
            if (playerScript.HasMoved())
            {
                MoveEnemy();
            }
        }
        else
        {
            if (playerScript.HasMovedTwoTimes())
            {
                MoveEnemy();
            }
        }
    }

    public void MoveEnemy() {

        int moveX = 0;
        int moveY = 0;

        if (Mathf.Abs(player.transform.position.x - transform.position.x) < float.Epsilon) {
            moveY = player.transform.position.y > transform.position.y ? 1 : -1;
        }
        else {
            moveX = player.transform.position.x > transform.position.x ? 1 : -1;
        }

        int desiredX = moveX;

        if (positions.Contains(new Vector2(transform.position.x + moveX, transform.position.y + moveY)) || !Move(moveX, moveY))
        {
            if (moveX != 0)
            {
                moveX = 0;
                moveY = 1;

                if (positions.Contains(new Vector2(transform.position.x + moveX, transform.position.y + moveY)) || !Move(moveX, moveY))
                {
                    moveY *= -1;

                    if (positions.Contains(new Vector2(transform.position.x + moveX, transform.position.y + moveY)) || !Move(moveX, moveY))
                    {
                        positions.Add(new Vector2(transform.position.x, transform.position.y));
                        moveY = 0;
                        Move(desiredX * -1, moveY);
                    }
                }
            }
        }


    }

}
