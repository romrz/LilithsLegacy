using UnityEngine;
using System.Collections;

public class Player : MovingObject {

    private bool hasMoved = false;
    private bool hasMovedTwoTimes = false;
    private int moveCount = 0;

    protected override void Start() {
        base.Start();
    }

    void Update() {
        hasMoved = false;
        hasMovedTwoTimes = false;

        int moveX = 0;
        int moveY = 0;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveX = -1;
            hasMoved = true;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveX = 1;
            hasMoved = true;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            moveY = -1;
            hasMoved = true;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            moveY = 1;
            hasMoved = true;
        }

        if (hasMoved) {
            moveCount++;
        }

        if (moveCount == 2) {
            moveCount = 0;
            hasMovedTwoTimes = true;
        }

        Move(moveX, moveY);
    }

    public bool HasMoved() {
        return hasMoved;
    }

    public bool HasMovedTwoTimes()
    {
        return hasMovedTwoTimes;
    }
}
