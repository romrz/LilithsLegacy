using UnityEngine;
using System.Collections;

public class MovingObject : MonoBehaviour {

    public LayerMask collisionLayer;

    private bool canMove = true;
    private BoxCollider2D collider;

	protected virtual void Start () {
        collider = GetComponent<BoxCollider2D>();
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
            transform.position = endPosition;
            return true;
        }
        else {
            if(tag == "Player") {
                if(hit.collider.tag == "Exit") {
                    GameObject.FindGameObjectWithTag("Map").GetComponent<Map>().SetNextLevel();
                    Debug.Log("Salida");
                }
            }

            if(tag == "Demon") {
                if(hit.collider.tag == "Trap" && !hit.collider.gameObject.GetComponent<Trap>().HasDemon()) {
                    transform.position = endPosition;
                    canMove = false;
                    hit.collider.gameObject.GetComponent<Trap>().SetDemon();
                }
                return false;
            }

            if(hit.collider.tag == "Trap") {
                if(tag == "Player") {
                    if (!hit.collider.gameObject.GetComponent<Trap>().HasDemon()) {
                        transform.position = endPosition;
                        return true;
                    }
                }
            }

            if(hit.collider.tag == "Movable") {
                if (tag == "Movable") return false;
                if (hit.collider.gameObject.GetComponent<MovingObject>().Move(xDir, yDir)) {
                    transform.position = endPosition;
                    return true;
                }
            }
        }

        return false;
    }
}
