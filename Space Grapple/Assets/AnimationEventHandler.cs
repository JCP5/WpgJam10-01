using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    public Collider2D hitBox;
    public CrawlerBehaviour cb;

    public void HitBoxActive()
    {
        hitBox.enabled = true;
    }

    public void HitBoxDeactive()
    {
        hitBox.enabled = false;
    }

    public void AttackingToFalse()
    {
        cb.attacking = false;
    }
}
