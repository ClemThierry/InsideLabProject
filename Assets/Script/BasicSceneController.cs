/**
 * Ardity (Serial Communication for Arduino + Unity)
 * Author: Daniel Wilches <dwilches@gmail.com>
 *
 * This work is released under the Creative Commons Attributions license.
 * https://creativecommons.org/licenses/by/2.0/
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;
using UnityEngine.SceneManagement;

/**
 * Sample for reading using polling by yourself, and writing too.
 */
public class BasicSceneController : MonoBehaviour
{
    public SerialController serialController;

    public string nextSceneName;

    public AudioSource introSound;

    public AudioSource choiceSoundSource;
    public AudioClip[] audioClipArray;

    private bool isChoiceDone = false;

    // Initialization
    void Start()
    {
        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
        introSound.Play();
    }

    // Executed each frame
    void Update()
    {
        if (!introSound.isPlaying && !choiceSoundSource.isPlaying)
        {
            string message = serialController.ReadSerialMessage();

            if (message == null)
                return;

            // Check if the message is plain data or a connect/disconnect event.
            if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_CONNECTED))
                Debug.Log("Connection established");
            else if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_DISCONNECTED))
                Debug.Log("Connection attempt failed or disconnection detected");
            else
            {
                choiceSoundSource.PlayOneShot(audioClipArray[Int16.Parse(message)]);
                isChoiceDone = true;
            }
        }

        if(isChoiceDone && !choiceSoundSource.isPlaying)
        {
            Invoke("changeScene", 2);
        }
    }

    void changeScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}