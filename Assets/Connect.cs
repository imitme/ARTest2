using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connect : MonoBehaviour
{
    public GameObject emitterPrefab;
    public float interval = 0.5f;
    public float duration = 1f;
    public float lifeTime = 0.5f;
    public float height = 10f;

    public static Connect other = null;

    private void OnEnable()
    {
        other = this;
        StartCoroutine(SpawnProcess());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator SpawnProcess()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(interval);
            if (other.enabled == false)
                other = this;
            var newEmitter = Instantiate(emitterPrefab);
            var emitters = newEmitter.GetComponent<Emitters>();
            emitters.Initialize(duration, lifeTime, height, transform.position, other.transform);
        }
    }
}