using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    public Collider2D hitBox;

    public void HitBoxActive()
    {
        hitBox.enabled = true;
    }

    public void HitBoxDeactive()
    {
        hitBox.enabled = false;
    }
}
