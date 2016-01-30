using UnityEngine;
using System.Collections;

public class Enemy : MovingObject {

    private GameObject player;
    private Player playerScript;

    protected override void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();

        base.Start();
    }

    void Update() {
        if(playerScript.HasMovedTwoTimes()) {
            MoveEnemy();
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

        if(!Move(moveX, moveY)) {
            moveX *= -1;
            moveY *= -1;

            Move(moveX, moveY);
        }
    }

}
