using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;
using UnityEngine.SceneManagement;

public class QTEvent : MonoBehaviour
{
    public SerialController serialController;

    public string nextSceneName;

    public AudioSource choiceSoundSource;
    public AudioSource introSound;
    public AudioSource QTTimerSoundSource;
    public AudioClip[] audioClipArray;


    private bool isQTSoudPlayed = false;
    private bool isQTCatched = false;
    private bool isSceneEnd = false;

    // Initialization
    void Start()
    {
        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
        Debug.Log("Push the button");
        introSound.Play();
    }

    // Executed each frame
    void Update()
    {

        if (!introSound.isPlaying && !QTTimerSoundSource.isPlaying && !choiceSoundSource.isPlaying && !isSceneEnd && !isQTSoudPlayed)
        {
            QTTimerSoundSource.Play();
            isQTSoudPlayed = true;
        }
        
        if (QTTimerSoundSource.isPlaying)
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
                        isQTCatched = true;
                        QTTimerSoundSource.Stop();                    
                    }
        }

        if (!introSound.isPlaying && !QTTimerSoundSource.isPlaying && !choiceSoundSource.isPlaying && !isSceneEnd)
        {
                if (!isQTCatched)
                {
                    choiceSoundSource.PlayOneShot(audioClipArray[0]);
                }
                else
                {
                    choiceSoundSource.PlayOneShot(audioClipArray[1]);
                }
                isSceneEnd = true;
        }      
        
        if (isSceneEnd && !introSound.isPlaying && !QTTimerSoundSource.isPlaying && !choiceSoundSource.isPlaying)
        {
            Invoke("changeScene", 2);
        }

    }

    void changeScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}