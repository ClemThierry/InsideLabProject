using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;

public class PlayAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource situation1;
    public AudioClip[] audioClipArray;

    public SerialController serialController;

    // Start is called before the first frame update
    void Start()
    {
        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
        //audioSource.PlayOneShot(audioClipArray[1]);
        //situation1.Play();
    }

    void Update()
    {
        /*Debug.Log("part 1 :"+!situation1.isPlaying);
        Debug.Log("part 2 :" + !audioSource.isPlaying);*/
        //if (!situation1.isPlaying && !audioSource.isPlaying)
        //{
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
                Debug.Log("Message arrived ici: " + message);
                Debug.Log("fgvhbjnk,l;");
                audioSource.PlayOneShot(audioClipArray[Int16.Parse(message)]);
            }
        //}

    }
}
