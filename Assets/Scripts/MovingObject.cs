using UnityEngine;
using System.Collections;

public class MovingObject : MonoBehaviour {

    public LayerMask collisionLayer;
    public float moveTime = 0.1f;

    private float inverseMoveTime;
    private bool canMove = true;
    private BoxCollider2D collider;

	protected virtual void Start () {
        collider = GetComponent<BoxCollider2D>();
        inverseMoveTime = 1f / moveTime;
    }

    public bool Move(int xDir, int yDir)
    {
        if (!canMove) return false;

        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition + new Vector3(xDir, yDir);

        collider.enabled = false;

        RaycastHit2D hit = Physics2D.Linecast(startPosition, endPosition, collisionLayer);

        collider.enabled = true;

        if(!hit)
        {
            //StartCoroutine(SmoothMovement(endPosition));
            transform.position = endPosition;
            return true;
        }
        else {
            if(tag == "Player") {
                if(hit.collider.tag == "Exit") {
                    //StartCoroutine(SmoothMovement(endPosition));
                    Map map = GameObject.FindGameObjectWithTag("Map").GetComponent<Map>();
                    if (map.GetCurrentLevel().HasFilledTraps()) {
                        transform.position = endPosition;
                        hit.collider.enabled = false;
                        map.SetNextLevel();
                        return true;
                    }
                    return false;
                }

                if(hit.collider.tag == "Demon") {
                    GameObject.FindGameObjectWithTag("Map").GetComponent<Map>().RestartLevel();
                    return false;
                }

                if (hit.collider.tag == "Movable" && hit.collider.gameObject.GetComponent<MovingObject>().Move(xDir, yDir)) {
                    //StartCoroutine(SmoothMovement(endPosition));
                    transform.position = endPosition;
                    return true;
                }

                if (hit.collider.tag == "Trap" && !hit.collider.gameObject.GetComponent<Trap>().HasDemon()) {
                    //StartCoroutine(SmoothMovement(endPosition));
                    hit.collider.gameObject.GetComponent<Trap>().SetPlayer();
                    transform.position = endPosition;
                    return true;
                }

                return false;
            }

            if(tag == "Demon") {
                if (hit.collider.tag == "Player") {
                    GameObject.FindGameObjectWithTag("Map").GetComponent<Map>().RestartLevel();
                }
                if (hit.collider.tag == "Trap" && !hit.collider.gameObject.GetComponent<Trap>().HasDemon()) {
                    //StartCoroutine(SmoothMovement(endPosition));
                    transform.position = endPosition;
                    canMove = false;
                    hit.collider.gameObject.GetComponent<Trap>().SetDemon();
                }
                return false;
            }            
        }

        return false;
    }

    protected IEnumerator SmoothMovement(Vector3 end) {
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPostion = Vector3.MoveTowards(transform.position, end, inverseMoveTime * Time.deltaTime);

            transform.position = newPostion;

            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            yield return null;
        }

        transform.position = end;
    }
}
