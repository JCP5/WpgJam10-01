using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnGrappled_UnityEvents : OnGrappled
{
    [SerializeField] UnityEvent GrappledEvent;
    public override void Grappled()
    {
        GrappledEvent.Invoke();
        base.Grappled();
    }
}
