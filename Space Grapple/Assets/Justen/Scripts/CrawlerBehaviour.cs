using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerBehaviour : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    float moveSpeed = 0;
    public bool standing = false;
    public bool attacking = false;

    public float adjustWalkSpeed = 1;
    public float adjustRunSpeed = 50;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponentInChildren<Animator>();
        anim.SetBool("Standing", false);
    }

    private void Update()
    {
        rb.velocity = CheckLookDirection() * Vector3.right * moveSpeed;
        CheckGroundInFront();
        CheckForEntitiesInFront();
        CheckAnimations();
    }

    void CheckForEntitiesInFront()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, CheckLookDirection() * Vector2.right, Mathf.Infinity, 9);

        try
        {
            if (attacking == false)
            {
                if (hit.collider.tag == "Player" || hit.collider.tag == "Prey")
                {
                    if (Vector3.Distance(this.transform.position, hit.transform.position) < 5)
                    {
                        attacking = true;
                        moveSpeed = 0;
                    }
                    else
                    {
                        attacking = false;
                        moveSpeed = adjustRunSpeed;
                    }
                }
                else
                {
                    moveSpeed = adjustWalkSpeed;
                    attacking = false;
                }
            }
        }
        catch
        {
            moveSpeed = adjustWalkSpeed;
            attacking = false;
        }
    }

    void CheckGroundInFront()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + transform.right, Vector2.down, 2);

        if (hit.collider == null)
        {
            if (transform.rotation.eulerAngles.y == 0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
            else
            {
                transform.rotation = Quaternion.Euler(Vector3.zero);
            }
        }
    }

    float CheckLookDirection()
    {
        if (transform.rotation.eulerAngles.y == 0)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }

    void CheckAnimations()
    {
        anim.SetInteger("Speed", Mathf.Abs((Mathf.RoundToInt(moveSpeed))));
        anim.SetBool("Standing", standing);
        anim.SetBool("Attacking", attacking);

        AnimatorClipInfo[] aci = anim.GetCurrentAnimatorClipInfo(0);
        string animClipName = aci[0].clip.name;
        Transform meshTransform = transform.Find("Crawler");

        switch (animClipName)
        {
            case "Armature_WalkTall":
                meshTransform.localPosition = new Vector3(0, -0.4f, 0);
                break;

            case "Armature_Scuttle":
                meshTransform.localPosition = new Vector3(0, -1.8f, 0);
                break;

            default:
                meshTransform.localPosition = new Vector3(0, -1.25f, 0);
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (transform.rotation.eulerAngles.y == 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        else
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }
}
