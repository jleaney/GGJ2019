using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockController : MonoBehaviour {

    [SerializeField]
    private bool hasKey;

    public bool HasKey
    {
        get
        {
            return hasKey;
        }
    }

    // listen for event - key pickup

    public void PlayParticleFX()
    {
        transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        transform.GetChild(1).GetComponent<ParticleSystem>().Play();
    }

    private void OnMouseDown()
    {
        if (hasKey)
        {
            GetComponent<Animator>().SetTrigger("unlock");
            transform.parent.transform.parent.GetComponent<DrawerController>().Unlocked = true;
        }

        else if (!hasKey && !GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Fail Unlock"))
        {
            GetComponent<Animator>().SetTrigger("fail unlock");
        }
    }
}
