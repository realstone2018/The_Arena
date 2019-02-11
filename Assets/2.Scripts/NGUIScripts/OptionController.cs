using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionController : MonoBehaviour {

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        TweenScale tween = TweenScale.Begin(this.gameObject, 0.5f, new Vector3(1, 1, 1));
        tween.method = UITweener.Method.EaseIn;
    }

    public void RemoveOption()
    {
        TweenScale.Begin(this.gameObject, 0.5f, new Vector3(0, 0, 1));
        Invoke("SetActiveFalse", 0.5f);
    }

    public void SetActiveFalse()
    {
        gameObject.SetActive(false);
    }
}
