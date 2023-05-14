using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class FreeComponent : MonoBehaviour
{
    private Rigidbody rb;
    
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    public void freeConstrains()
    {
        rb.constraints = RigidbodyConstraints.None;
        rb.isKinematic = false;
    }

    public void freeSyringe()
    {
        gameObject.transform.parent = null;
    }

    public void freeHands()
    {
        GameObject bottle = GameObject.Find($"Interactable_Bottle{gameObject.name[..25].Last()}(Clone)");
        StartCoroutine(bottleColliderUnlock(bottle));
        GameObject.Find("UserHands").transform.GetChild(1).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        
    }

    IEnumerator bottleColliderUnlock(GameObject bottle)
    {
        yield return new WaitForSeconds(1);
        bottle.GetComponent<Rigidbody>().isKinematic = false;
    }

}
