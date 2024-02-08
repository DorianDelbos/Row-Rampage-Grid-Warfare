using System.Collections;
using UnityEngine;

public class Stands : MonoBehaviour
{
    [SerializeField] private Transform[] peoples;
    private Coroutine[] coroutines;

    private IEnumerator Start()
    {
        coroutines = new Coroutine[peoples.Length];

        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            UpdatePeoples();
        }
    }

    private void UpdatePeoples()
    {
        for (int i = 0; i < peoples.Length; i++)
        {
            if (coroutines[i] == null)
            {
                if (Random.Range(0, 5) == 0)
                {
                    coroutines[i] = StartCoroutine(PeopleMove(peoples[i], i));
                }
            }
        }
    }

    private IEnumerator PeopleMove(Transform person, int coroutineIndex)
    {
        Vector3 startPos = person.position;
        float elapsedTime = 0f;
        float maxTime = 0.25f;

        while (elapsedTime < maxTime)
        {
            yield return null;

            float factor = elapsedTime / maxTime;
            float ease = Mathf.Sin(factor * Mathf.PI);
            person.position = Vector3.Lerp(startPos, startPos + Vector3.up * 0.5f, ease);

            elapsedTime += Time.deltaTime;
        }

        person.position = startPos;
        coroutines[coroutineIndex] = null;
    }
}
