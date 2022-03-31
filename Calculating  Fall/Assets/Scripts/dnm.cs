using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dnm : MonoBehaviour
{
    GameObject parentObj;

    void OnTriggerEnter(Collider other)
    {
        parentObj = other.gameObject;
        StartCoroutine(TriggerState());
    }

    IEnumerator TriggerState()
    {
        if(parentObj.transform.childCount != 0)
        {
            for (int i = 0; i < parentObj.transform.childCount; i++)
            {
                parentObj.transform.GetChild(i).GetComponent<MeshCollider>().isTrigger = false;
            }

            yield return new WaitForSeconds(8f);

            for (int i = 0; i < parentObj.transform.childCount; i++)
            {
                parentObj.transform.GetChild(i).GetComponent<MeshCollider>().isTrigger = true;
            }
        }
        else
        {
            Debug.LogError("Çocuk Obje Bulunamadý!!!");
        }
    }
}
