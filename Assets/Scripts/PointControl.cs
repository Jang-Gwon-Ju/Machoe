using UnityEngine;
using System.Collections;

public class PointControl : MonoBehaviour {

    public float speed;
    public DIRECTION direction;

    Collider2D _tempColl;
    Rigidbody2D _rigid2D;

	void Start ()
    {
        _rigid2D = GetComponent<Rigidbody2D>();
    }
	
    void OnEnable()
    {
        _tempColl = null;
    }

    void Update()
    {
        //if (Input.GetTouch(0).Equals(TouchPhase.Ended))
        if (Input.GetMouseButtonUp(0))
        {
            checkIsHit();
        }
    }

    void FixedUpdate ()
    {
        if (direction.Equals(DIRECTION.RIGHT))
            _rigid2D.velocity = Vector2.right * speed * Time.smoothDeltaTime;
        else
            _rigid2D.velocity = Vector2.left * speed * Time.smoothDeltaTime;
	}

    void checkIsHit()
    {
        if(_tempColl != null)
        {
            if (GameManager.getInstance.dialogState.Equals(DIALOG.ATTACK))
                GameManager.getInstance.enemyHit = true;
            else if (GameManager.getInstance.dialogState.Equals(DIALOG.AVOID))
                GameManager.getInstance.playerMiss = true;
        }
        else
        {
            if (GameManager.getInstance.dialogState.Equals(DIALOG.ATTACK))
                GameManager.getInstance.enemyMiss = true;
            else if (GameManager.getInstance.dialogState.Equals(DIALOG.AVOID))
                GameManager.getInstance.playerHit = true;
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Line"))
        {
            if (direction.Equals(DIRECTION.RIGHT))
                direction = DIRECTION.LEFT;
            else
                direction = DIRECTION.RIGHT;
        }

        if (coll.gameObject.CompareTag("Box"))
        {
            _tempColl = coll;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Box"))
        {
            _tempColl = null;
        }
    }
}
