using System.Collections;
using System.Linq;
using UnityEngine;

public class Stands : MonoBehaviour
{
    [SerializeField] private Material[] rdmMaterials;
    [SerializeField] private Transform[] peoples;
    private Coroutine[] coroutines;

    private IEnumerator Start()
    {
        coroutines = new Coroutine[peoples.Length];

        foreach (var person in peoples)
        {
            if (person == null)
                continue;

            if (Random.Range(0, 10) == 0)
            {
                DestroyImmediate(person.gameObject);
                continue;
            }


            MeshRenderer renderer = person.GetComponent<MeshRenderer>();
            if (renderer != null)
                renderer.material = rdmMaterials[Random.Range(0, rdmMaterials.Length)];
            else
                Debug.LogWarning("MeshRenderer component not found on person: " + person.name);
        }

        peoples = peoples
            .Where(x => x != null)
            .ToArray();

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
