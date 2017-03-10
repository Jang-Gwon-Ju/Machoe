using UnityEngine;
using System.Collections;

public class BoxControl : MonoBehaviour {

    public float speed;

    DIRECTION _direction;
    Rigidbody2D _rigid2D;
    BoxCollider2D _collider;
    SpriteAnimator _animator;

	void Awake ()
    {
        _animator = GetComponent<SpriteAnimator>();
        _collider = GetComponent <BoxCollider2D>();
        _rigid2D = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        setType();
        setSpeed();
        setRandomDirection();
        setPerfectCollisionSize();
    }
	
	void FixedUpdate ()
    {
        if (_animator.isAnimationEnd)
        {
            if (_direction.Equals(DIRECTION.RIGHT))
                _rigid2D.velocity = speed * Time.smoothDeltaTime * Vector2.right;
            else
                _rigid2D.velocity = speed * Time.smoothDeltaTime * Vector2.left;
        }
    }

    void setSpeed()
    {
        switch(GameManager.getInstance.playerWeapon)
        {
            case PLAYER_WEAPON.PISOTL:
                speed = 1000;
                break;

            case PLAYER_WEAPON.UZI:
                break;

            case PLAYER_WEAPON.SHOTGUN:
                break;

            case PLAYER_WEAPON.MINIGUN:
                break;
        }
    }

    void setType()
    {
        if (GameManager.getInstance.dialogState.Equals(DIALOG.ATTACK))
        {
            switch (GameManager.getInstance.playerWeapon)
            {
                case PLAYER_WEAPON.PISOTL:
                    _animator.changeState("Pistol_Appear");
                    break;

                case PLAYER_WEAPON.UZI:
                    _animator.changeState("Uzi_Appear");
                    break;

                case PLAYER_WEAPON.SHOTGUN:
                    _animator.changeState("Shotgun_Appear");
                    break;

                case PLAYER_WEAPON.MINIGUN:
                    _animator.changeState("Minigun_Appear");
                    break;
            }
        }
        else
        {
            switch (GameManager.getInstance.enemyType)
            {
                case ENEMY_TYPE.NMM1:
                    break;

                case ENEMY_TYPE.NMM2:
                    break;

                case ENEMY_TYPE.NMM3:
                    break;

                case ENEMY_TYPE.NMM4:
                    break;

                case ENEMY_TYPE.NMM5:
                    break;
            }
        }
    }

    void setPerfectCollisionSize()
    {
        switch (GameManager.getInstance.playerWeapon)
        {
            case PLAYER_WEAPON.PISOTL:
                _collider.size = new Vector2(307, 127);
                break;

            case PLAYER_WEAPON.UZI:
                _collider.size = new Vector2(307, 127);
                break;

            case PLAYER_WEAPON.SHOTGUN:
                _collider.size = new Vector2(146, 127);
                break;

            case PLAYER_WEAPON.MINIGUN:
                _collider.size = new Vector2(307, 127);
                break;
        }
    }

    void setRandomDirection()
    {
        int random = Random.Range(0, 1);
        if(random.Equals(1))
        {
            _direction = DIRECTION.RIGHT;
        }
        else
        {
            _direction = DIRECTION.LEFT;
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Line"))
        {
            if (_direction.Equals(DIRECTION.LEFT))
                _direction = DIRECTION.RIGHT;
            else
                _direction = DIRECTION.LEFT;
        }
    }

    public void hideBox()
    {
        _rigid2D.velocity *= 0;
        if (GameManager.getInstance.dialogState.Equals(DIALOG.ATTACK))
        {
            switch (GameManager.getInstance.playerWeapon)
            {
                case PLAYER_WEAPON.PISOTL:
                    _animator.changeState("Pistol_Disappear");
                    break;

                case PLAYER_WEAPON.UZI:
                    _animator.changeState("Uzi_Disappear");
                    break;

                case PLAYER_WEAPON.SHOTGUN:
                    _animator.changeState("Shotgun_Disappear");
                    break;

                case PLAYER_WEAPON.MINIGUN:
                    _animator.changeState("Minigun_Disappear");
                    break;
            }
        }
        else
        {
            switch(GameManager.getInstance.enemyType)
            {
                case ENEMY_TYPE.NMM1:
                    break;

                case ENEMY_TYPE.NMM2:
                    break;

                case ENEMY_TYPE.NMM3:
                    break;

                case ENEMY_TYPE.NMM4:
                    break;

                case ENEMY_TYPE.NMM5:
                    break;
            }
        }
    }
}
