using UnityEngine;
using System.Collections;

public class EffectControl : MonoBehaviour {

    SpriteAnimator _animator;

    void Awake()
    {
        _animator = GetComponent<SpriteAnimator>();
    }

	void OnEnable ()
    {
        _animator.changeState("PistolShotEffect");
	}
	
	void Update ()
    {
        if (_animator.isAnimationEnd)
            gameObject.SetActive(false);
	}
}
