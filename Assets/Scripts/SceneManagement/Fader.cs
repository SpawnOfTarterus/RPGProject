using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        [SerializeField] Image fader = null;

        public IEnumerator FadeIn(float time)
        {
            while(fader.color.a > 0)
            {
                float newAlpha = fader.color.a - (Time.deltaTime / time);
                fader.color = new Color(0, 0, 0, newAlpha);
                yield return null;
            }
        }

        public IEnumerator FadeOut(float time)
        {
            while(fader.color.a < 1)
            {
                float newAlpha = fader.color.a + (Time.deltaTime / time);
                fader.color = new Color(0, 0, 0, newAlpha);
                yield return null;
            }
        }

    }
}
