using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    public Vector3 point1;
    public Vector3 point2;
    public float speed = 2f;
    public float pausetime = 1f;
    public float turnSpeed = 2f;

    private bool onetotwo = true;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(Pacing());
    }


    IEnumerator Pacing()
    {
        while (true)
        {

            yield return StartCoroutine(Turn180());

            Vector3 target = onetotwo ? point1 : point2;

            animator.SetBool("isWalking", true);

            yield return StartCoroutine(MoveToTarget(target));

            animator.SetBool("isWalking", false);

            animator.SetBool("isIdle", true);

            //yield return new WaitForSeconds(pausetime);

            animator.SetBool("isIdle", false);

            onetotwo = !onetotwo;
        }
    }

    IEnumerator MoveToTarget(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > 0.01f) 
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return null;        
        }

        transform.position = target;
    }

    IEnumerator Turn180()
    {
        float currentAngle = transform.eulerAngles.y;
        float targetAngle = currentAngle + 180f;

        while (Mathf.Abs(Mathf.DeltaAngle(currentAngle, targetAngle)) > 0.1f)
        {
            currentAngle = Mathf.LerpAngle(currentAngle, targetAngle, Time.deltaTime * turnSpeed);
            transform.eulerAngles = new Vector3(0, currentAngle, 0);
            yield return null;
        }

        transform.eulerAngles = new Vector3(0, targetAngle, 0);
    }
}
