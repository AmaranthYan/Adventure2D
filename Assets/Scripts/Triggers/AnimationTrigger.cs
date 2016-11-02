using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;

public class AnimationTrigger : MonoBehaviour {
    public LayerMask colliderMask = 0;
    public UnityEvent onTriggered = new UnityEvent();

    void OnTriggerEnter2D(Collider2D collider)
    {
        if ((colliderMask.value & (1 << collider.gameObject.layer)) != 0)
        {
            onTriggered.Invoke();
        }
    }
}
