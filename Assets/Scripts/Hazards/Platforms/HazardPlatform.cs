using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HazardPlatform : MonoBehaviour
{
    [SerializeField] protected float disappearTime;
    [SerializeField] protected float stayHazardousTime;
    [SerializeField] protected int shakeTime = 1;
    [SerializeField] protected Collider2D mainCollider;
    [SerializeField] protected Collider2D triggerCollider;

    private Transform startPoint;
    protected bool isAlreadyActive;


    protected virtual void Start()
    {
        GameObject startPointGO = new GameObject(this.gameObject.name + "_StartPoint");
        startPoint = startPointGO.transform;

        startPoint.position = transform.position;
        startPoint.rotation = transform.rotation;
        startPoint.localScale = transform.localScale;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("test");
            TiggerAction();
        }
    }

    protected virtual void TiggerAction()
    {
        LeanTween.cancel(gameObject);

        LeanTween.rotateAround(gameObject, Vector3.forward, 3f, 0.02f).setEaseInOutSine().setLoopCount(shakeTime * 10).setOnComplete(() =>
        {
            LeanTween.cancel(gameObject);
            transform.rotation = Quaternion.identity;
            ActivateAction();
        });

        
    }

    protected virtual void ActivateAction()
    {
        LeanTween.cancel(gameObject);

        Debug.Log("Started Action");
        LeanTween.delayedCall(gameObject, stayHazardousTime, () => ReActivateAgain());

    }

    protected virtual void ReActivateAgain()
    {
        ResetToSpwanPoint();
        triggerCollider.enabled = true;

        Debug.Log("Reactivated");
    }

    protected virtual void ResetToSpwanPoint()
    {
        transform.position = startPoint.position;
        transform.localScale = startPoint.localScale;
        transform.rotation = startPoint.rotation;

    }

    private void OnDisable()
    {
        LeanTween.cancel(gameObject);
    }
}