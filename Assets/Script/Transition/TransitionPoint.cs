using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionPoint : MonoBehaviour
{
    public enum TransitionType
    {
        SameScene, DifferentScene
    }
    [Header("Transition Info")]
    public string sceneName;

    public TransitionType transitionType;

    public TransitionDestination.DesinationTag desinationTag;

    private bool canTrans;

    private void Update()
    {
        // Debug.Log("TransitionPoint canTrans:" + canTrans);
        if (Input.GetKeyDown(KeyCode.E) && canTrans)
        {
            Debug.Log("TransitionPoint GetKeyDown E.");
            SceneController.Instance.TransitionToDestination(this);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        // Debug.Log("TransitionPoint OnTriggerStay." + other.tag);
        if (other.CompareTag("Player"))
            canTrans = true;
    }

    private void OnTriggerExit(Collider other)
    {
        // Debug.Log("TransitionPoint OnTriggerExit." + other.tag);
        if (other.CompareTag("Player"))
            canTrans = false;
    }
}
