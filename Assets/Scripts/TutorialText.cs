using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class TutorialText : MonoBehaviour
{

    public TextMeshProUGUI text;

    public string keyboardText;

    [Tooltip("The way you can set specific controls is by typing in the general button inbetween a **.")]
    public string gamePadText;

    GameMaster gm;

    Controls controls;

    // Start is called before the first frame update
    void Start()
    {
        controls = new Controls();
        controls.Enable();
        gm = FindObjectOfType<GameMaster>();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.usingController)
        {
            string finalText = "";

            string[] textList = gamePadText.Split('*');

            bool isOnControl = false;

            foreach (string part in textList)
            {
                if (!isOnControl)
                {
                    finalText += part;
                } else
                {
                    finalText += Gamepad.current[part].shortDisplayName;
                }

                isOnControl = !isOnControl;
            }

            text.text = finalText;
        } else
        {
            text.text = keyboardText;
        }
    }
}
