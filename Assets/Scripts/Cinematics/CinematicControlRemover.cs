using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Control;
using UnityEngine.Playables;
using RPG.Core;

namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        float cinematicLength = 0;
        PlayerController player = null;

        public void StopPlayerControl(GameObject gameObject)
        {
            player = gameObject.GetComponent<PlayerController>();
            player.enabled = false;
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            cinematicLength = (float)GetComponent<PlayableDirector>().duration;
            StartCoroutine(GiveBackControls());
        }

        private IEnumerator GiveBackControls()
        {
            yield return new WaitForSeconds(cinematicLength);
            player.enabled = true;
        }

    }
}
