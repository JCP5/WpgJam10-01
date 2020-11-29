using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGrappled : MonoBehaviour
{
  public virtual void Grappled()
    {
        Debug.Log("Interacted with" + gameObject.ToString());
    }
}
