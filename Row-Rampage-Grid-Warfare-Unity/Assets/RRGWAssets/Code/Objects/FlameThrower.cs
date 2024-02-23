using FMOD.Studio;
using FMODUnity;
using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class FlameThrower : MonoBehaviour
{
    private bool isStart = false;
    private VisualEffect effect;

    public bool startAwake = false;
    public bool isAuto = false;
    [SerializeField] private Vector2 randomInterval = new Vector2(4f, 10f);
    [SerializeField] private EventReference flameThrowerSoundRef;
    private EventInstance eventInstance;

    private void Start()
    {
        effect = GetComponent<VisualEffect>();

        eventInstance = RuntimeManager.CreateInstance(flameThrowerSoundRef);

        FMOD.ATTRIBUTES_3D attributes = RuntimeUtils.To3DAttributes(transform.position);
        eventInstance.set3DAttributes(attributes);

        if (startAwake && isAuto)
        {
            StartCoroutine(FlameUpdate());
        }
    }

    private void OnDisable()
    {
        StopTrower(true);
    }

    private IEnumerator FlameUpdate()
    {
        while (isAuto)
        {
            yield return new WaitForSeconds(Random.Range(randomInterval.x, randomInterval.y));
            EnableTrower();
        }
    }

    public void StartTrower(bool startCoroutine = false)
    {
        isStart = true;

        effect.Play();

        eventInstance.setParameterByName("State", 0);
        eventInstance.start();

        if (isAuto && startCoroutine)
        {
            StartCoroutine(FlameUpdate());
        }
    }

    public void StopTrower(bool endCoroutine = false)
    {
        isStart = false;

        effect.Stop();

        eventInstance.setParameterByName("State", 1);

        if (endCoroutine)
        {
            StopAllCoroutines();
        }
    }

    public void EnableTrower()
    {
        if (isStart)
            StopTrower();
        else
            StartTrower();
    }
}
