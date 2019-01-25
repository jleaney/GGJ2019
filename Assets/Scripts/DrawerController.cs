using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerController : MonoBehaviour {

    private bool open = false;

    private void Start()
    {
    }

    private void OnMouseDown()
    {
        if (!open)
        {
            open = true;
            GetComponent<Animator>().SetTrigger("open");
        }

        else if (open)
        {
            open = false;
            GetComponent<Animator>().SetTrigger("close");
        }
    }

    private void OnMouseExit()
    {
        
        
    }

}
