using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private List<EventInstance> playingEventInstances;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            playingEventInstances = new List<EventInstance>();

            transform.parent = null;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void PlayAudio(EventReference eventRef)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventRef);

        FMOD.ATTRIBUTES_3D attributes = RuntimeUtils.To3DAttributes(transform.position);
        eventInstance.set3DAttributes(attributes);

        eventInstance.start();

        playingEventInstances.Add(eventInstance);
    }

    public void PlayAudio(string eventRefName)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventRefName);

        FMOD.ATTRIBUTES_3D attributes = RuntimeUtils.To3DAttributes(transform.position);
        eventInstance.set3DAttributes(attributes);

        eventInstance.start();

        playingEventInstances.Add(eventInstance);
    }

    public void StopAllAudio()
    {
        foreach (var eventInstance in playingEventInstances)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }

        playingEventInstances.Clear();
    }
}
