using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMenu : MonoBehaviour {

    private bool shown = false;

    public void ShowCredits()
    {
        if (!shown)
        {
            shown = true;
            GetComponent<Animator>().SetTrigger("show");
        }

        else
        {
            shown = false;
            GetComponent<Animator>().SetTrigger("hide");
        }
        
    }


}
