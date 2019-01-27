using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerController : MonoBehaviour {

    private bool open = false;

    [SerializeField]
    private bool unlocked = false;

    public bool Unlocked
    {
        get
        {
            return unlocked;
        }

        set
        {
            unlocked = value;
        }
    }

    public bool Open
    {
        get
        {
            return open;
        }

        set
        {
            open = value;
        }
    }

    private void Start()
    {

    }

    private void OnMouseDown()
    {
        if (unlocked)
        {
            if (!open)
            {
                open = true;
                GetComponent<Animator>().SetTrigger("open");
                AudioManager.instance.CreateSFX("open");
            }

            else if (open)
            {
                open = false;
                GetComponent<Animator>().SetTrigger("close");
				AudioManager.instance.CreateSFX("close");
            }
        }

        else if (!unlocked && !GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Fail Open Drawer"))
        {
            GetComponent<Animator>().SetTrigger("fail open");
        }
    }
}
