using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{
    GameObject player;
    public void TransitionToDestination(TransitionPoint transitionPoint)
    {
        switch (transitionPoint.transitionType)
        {
            case TransitionPoint.TransitionType.SameScene:
                StartCoroutine(Transition(SceneManager.GetActiveScene().name, transitionPoint.desinationTag));
                break;
            case TransitionPoint.TransitionType.DifferentScene:

                break;
        }
    }

    IEnumerator Transition(string sceneName, TransitionDestination.DesinationTag desinationTag)
    {
        player = GameManager.Instance.playerStats.gameObject;

        NavMeshAgent plagerAgent = player.GetComponent<NavMeshAgent>();
        plagerAgent.enabled = false;

        TransitionDestination targetDestination = GetDestination(desinationTag);
        player.transform.SetPositionAndRotation(targetDestination.transform.position, targetDestination.transform.rotation);

        plagerAgent.enabled = true;

        yield return null;
    }

    private TransitionDestination GetDestination(TransitionDestination.DesinationTag desinationTag)
    {
        var entrances = FindObjectsOfType<TransitionDestination>();
        for (int i = 0; i < entrances.Length; i++)
        {
            if (entrances[i].desinationTag == desinationTag)
            {
                return entrances[i];
            }
        }

        return null;
    }

}
