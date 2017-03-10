using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class SpriteAnimation {

    public string name;
    public Sprite[] frames;
    public float fps;
    public bool isLoop;
    public bool isLoopEndActive;
    public bool isReverse;
    public bool ifChangeInit;

    [HideInInspector]
    public int _frameCount = 0;
    [HideInInspector]
    public float _animDelay = 0.0f;
    [HideInInspector]
    public bool _isAnimationEnd = false;
}
