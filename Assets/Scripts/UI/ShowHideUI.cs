using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameDevTV.UI
{
    public class ShowHideUI : MonoBehaviour
    {
        [SerializeField] Vector3 rectPosition;
        [SerializeField] GameObject uiContainer = null;
        [SerializeField] Button toggle;
        [SerializeField] Button cancel;

        private Vector3 defaultPosition;

        // Start is called before the first frame update
        void Start()
        {
            defaultPosition = uiContainer.transform.position;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                Toggle();
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cancel();
            }
        }

        public void Toggle()
        {
            uiContainer.transform.position = rectPosition;
            toggle.gameObject.SetActive(false);
            cancel.gameObject.SetActive(true);
        }

        public void Cancel()
        {
            uiContainer.transform.position = defaultPosition;
            toggle.gameObject.SetActive(true);
            cancel.gameObject.SetActive(false);
        }
    }
}