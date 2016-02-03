using UnityEngine;
using System.Collections;

public class Player : MovingObject {

    private bool hasMoved = false;
    private bool hasMovedTwoTimes = false;
    private int moveCount = 0;
    public int steps;

    private int invalidMoveCount = 0;
    private bool invalidMoves = true;

    protected override void Start() {
        base.Start();
    }

    public void Reset()
    {
        steps = 0;
        hasMoved = false;
        hasMovedTwoTimes = false;
        moveCount = 0;
        invalidMoveCount = 0;
        invalidMoves = true;
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

        if(hasMoved == true)
        {
            steps++;
        }

        bool move = Move(moveX, moveY);

        if (hasMoved) {
            invalidMoves = invalidMoves && !move;
            if (invalidMoves) {
                invalidMoveCount++;
                if (invalidMoveCount >= 3) {
                    GameObject.FindGameObjectWithTag("Map").GetComponent<Map>().RestartLevel();
                    invalidMoveCount = 0;
                    invalidMoves = true;
                }
            }
            else {
                invalidMoves = true;
                invalidMoveCount = 0;
            }
        }
    }

    public bool HasMoved() {
        return hasMoved;
    }

    public bool HasMovedTwoTimes()
    {
        return hasMovedTwoTimes;
    }
}
