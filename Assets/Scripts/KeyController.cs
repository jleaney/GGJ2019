using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour {

    [SerializeField]
    private LockController lockController;

    private void OnMouseDown()
	{
		var locks = FindObjectsOfType<LockController>();
		foreach (var l in locks)
			l.Unlock();
		GetComponent<Animator>().SetTrigger("get key");
    }

    public void PlayParticleFX()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(1).GetComponent<ParticleSystem>().Play();
    }
}
