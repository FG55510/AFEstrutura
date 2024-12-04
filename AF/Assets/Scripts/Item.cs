using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, I_Collectable
{
    public GameObject itemHand;
    public GameObject itemScene;
    public Transform socket;

    BoxCollider myCollider;

    private void Start()
    {
        myCollider = GetComponent<BoxCollider>();
    }

    public void Get()
    {
        myCollider.enabled = false;

        itemHand.SetActive(true);
        itemScene.SetActive(false);

        transform.SetParent(socket);
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = new Vector3(0, 0, 0);
    }

    public void Drop(Transform transParent)
    {
        myCollider.enabled = true;

        itemScene.SetActive(true);
        itemHand.SetActive(false);

        transform.SetParent(null);
        transform.localPosition = transform.position + transParent.forward * 2;
        transform.localEulerAngles = new Vector3(0,0,0);
    }
}
