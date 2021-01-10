using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gridlayoutscript : MonoBehaviour
{

    public Image[] jumpBars;

    public CharacterControl characterControlScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < jumpBars.Length; i++)
        {
            if(i < characterControlScript.extraJumps)
            {
                jumpBars[i].enabled = true;
            }
            else
            {
                jumpBars[i].enabled = false;
            }
        }

        if(characterControlScript.canJump == true)
        {
            for (int i = 0; i < jumpBars.Length; i++)
            {
                jumpBars[i].color = new Color(0, 185, 0);
            }
        }
        else
        {
            for (int i = 0; i < jumpBars.Length; i++)
            {
                jumpBars[i].color = new Color(150, 0, 0);
            }
        }
    }
}
