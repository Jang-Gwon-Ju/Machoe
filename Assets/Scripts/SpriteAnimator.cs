using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SpriteAnimator : MonoBehaviour {

    [SerializeField]
    string state;

    [SerializeField]
    List<SpriteAnimation> animator;

    SpriteAnimation _nowAnimation;
    Image _renderer;

	void Start ()
    {
        _renderer = GetComponent<Image>();
        if (state != string.Empty)
            changeState(state);
    }

	void Update ()
    {
            _nowAnimation._animDelay += Time.deltaTime;
            _renderer.SetNativeSize();

            if (_nowAnimation.isReverse)
            {
                updateReverseAnimation();
            }
            else
            {
                updateAnimation();
            }
        //}
	}

    public void changeState(string stateName)
    {
        if(_nowAnimation != null)
        if(_nowAnimation.ifChangeInit)
        {
            _nowAnimation._frameCount = 0;
            _nowAnimation._animDelay = 0.0f;
        }

        state = stateName;
        for (int i = 0; i < animator.Count; i++)
        {
            if (animator[i].name.Equals(state))
            {
                _nowAnimation = animator[i];
                break;
            }
        }
    }


    void updateAnimation()
    {
        if (_nowAnimation._animDelay >= _nowAnimation.fps)
        {
            _nowAnimation._animDelay = 0.0f;
            _renderer.sprite = _nowAnimation.frames[_nowAnimation._frameCount];
            _nowAnimation._frameCount++;
            if (_nowAnimation._frameCount >= _nowAnimation.frames.Length)
            {
                if (_nowAnimation.isLoop)
                {
                    _nowAnimation._frameCount = 0;
                }
                else
                {
                    _nowAnimation._frameCount = _nowAnimation.frames.Length - 1;
                    _nowAnimation._isAnimationEnd = true;
                }

                if (_nowAnimation.isLoopEndActive)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }

    void updateReverseAnimation()
    {
        if (_nowAnimation._animDelay >= _nowAnimation.fps)
        {
            _nowAnimation._animDelay = 0.0f;
            _renderer.sprite = _nowAnimation.frames[_nowAnimation._frameCount];
            _nowAnimation._frameCount++;
            if (_nowAnimation._frameCount >= _nowAnimation.frames.Length)
            {
                if (!_nowAnimation.isLoop)
                {
                    _nowAnimation._frameCount = 0;
                    _nowAnimation._isAnimationEnd = true;
                }
                else
                {
                    _nowAnimation._frameCount = _nowAnimation.frames.Length - 1;
                }
            }
        }
    }

    public bool isAnimationEnd
    {
        get
        {
            return _nowAnimation._isAnimationEnd;
        }
    }
}
