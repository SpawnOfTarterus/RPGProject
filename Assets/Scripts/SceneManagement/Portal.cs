using RPG.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public enum Destinations
    {
        A, B, C, D, E
    }

    public class Portal : MonoBehaviour
    {
        [SerializeField] string sceneToLoad;
        [SerializeField] Transform spawnPoint;
        [SerializeField] Destinations destination;
        [SerializeField] float fadeOutTime = 0;
        [SerializeField] float fadeInTime = 0;

        public Transform GetSpawnPoint()
        {
            return spawnPoint;
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject.transform.parent);
        }

        private void OnTriggerEnter(Collider other)
        {
            GameObject player = other.gameObject;
            StartCoroutine(Transition(player));
        }

        private IEnumerator Transition(GameObject player)
        {
            GetComponent<Collider>().enabled = false;
            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(fadeOutTime);
            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            Transform newSpawnPoint = GetNewSpawnPoint();
            SpawnPlayer(player, newSpawnPoint);
            yield return fader.FadeIn(fadeInTime);
            Destroy(gameObject.transform.parent.gameObject);
        }

        private Transform GetNewSpawnPoint()
        {
            var portals = FindObjectsOfType<Portal>();
            foreach (Portal portal in portals)
            {
                if(portal == this) { continue; }
                if(portal.destination != destination) { continue; }
    
                return portal.GetSpawnPoint();
            }
            return null;
        }

        private void SpawnPlayer(GameObject player, Transform spawnPoint)
        {
            var newPlayer = FindObjectOfType<PlayerController>();
            newPlayer.GetComponent<NavMeshAgent>().Warp(spawnPoint.position);
            newPlayer.transform.rotation = spawnPoint.rotation;
        }
    }
}
