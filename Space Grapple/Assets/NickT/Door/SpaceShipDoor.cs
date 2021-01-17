using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipDoor : MonoBehaviour
{
    public bool Open = false;

    public bool playerNearby;

    public GameObject doorLeft;
    public GameObject doorRight;

    public void OpenDoor()
    {
        Open = true;
        doorLeft.transform.localScale = new Vector3(100, 100, 0);
        doorRight.transform.localScale = new Vector3(100, 100, 0);
    }

    public void CloseDoor()
    {
        Open = false;
        doorLeft.transform.localScale = new Vector3(100, 100, 100);
        doorRight.transform.localScale = new Vector3(100, 100, 100);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerNearby = true;
            if (playerNearby == true)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    scenenavigation.instance.LoadNextLevelByInt();
                }
            }
        }
    }
}
