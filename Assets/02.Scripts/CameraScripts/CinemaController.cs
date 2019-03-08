using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemaController : MonoBehaviour {

    public static CinemaController instance;

    private void Awake()
    {
        instance = this;
    }

    public void SetFllowTarget(Transform target)
    {
        gameObject.GetComponent<CinemachineVirtualCamera>().Follow = target;
    }

}
