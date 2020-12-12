using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Saving
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "save";

        private void Update()
        {
            InputTriggeredSave();
            InputTriggeredLoad();
        }

        private void InputTriggeredSave()
        { 
            if(Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
            {
                if(Input.GetKeyDown(KeyCode.S))
                {
                    GetComponent<SavingSystem>().Save(defaultSaveFile);
                }
            }
        }

        private void InputTriggeredLoad()
        {
            if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
            {
                if (Input.GetKeyDown(KeyCode.L))
                {
                    GetComponent<SavingSystem>().Load(defaultSaveFile);
                }
            }
        }
    }
}
