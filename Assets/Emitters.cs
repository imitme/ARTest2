using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Emitters : MonoBehaviour
{
    public AnimationCurve curve = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(1f, 1f));

    public AnimationCurve heightCurve =
   new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(0.5f, 1f), new Keyframe(1f, 0f));

    public void Initialize(float duration, float lifeTime, float height, Vector3 beginPos, Transform target)
    {
        transform.position = beginPos;
        InitializeParticleSystem(duration, lifeTime, target);
        StartCoroutine(Process(duration, height, target));
    }

    private void InitializeParticleSystem(float duration, float lifeTime, Transform target)
    {
        var ps = GetComponent<ParticleSystem>();
        var main = ps.main;
        main.duration = duration;
        main.startLifetime = lifeTime;
        var shapeModule = ps.shape;
        shapeModule.rotation = target.rotation.eulerAngles;
        ps.Play();
    }

    private IEnumerator Process(float duration, float height, Transform target)
    {
        var beginTime = Time.time;
        var t = 0f;
        var beginPos = transform.position;
        var pos = beginPos;
        var heightPos = Vector3.zero;
        for (; ; )
        {
            t = (Time.time - beginTime) / duration;
            if (t >= 1f)
                break;
            if (target != null)
                pos = Vector3.Lerp(beginPos, target.position, curve.Evaluate(t));
            heightPos = Vector3.Lerp(Vector3.zero, target.up * height, heightCurve.Evaluate(t));
            transform.position = pos + heightPos;
            yield return null;
        }
        if (target != null)
            transform.position = target.position;
        else
            transform.position = beginPos;
    }
}