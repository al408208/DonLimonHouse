using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class UnitStatic : MonoBehaviour
{
    public Transform target;
    public Transform startPosition;
    float speed = 0.5f;
    Vector3[] path;
    int targetIndex;
    float cubeSize = 0.2f;
    private PlayerMovement playermov;
    int añadido = 0;

    Quaternion startRotation;

    void Start()
    {
        playermov = FindObjectOfType<PlayerMovement>();
        startRotation = transform.rotation;
        StartCoroutine("UpdatePath");
    }

    IEnumerator UpdatePath()
    {

        while (true)
        {
            if (playermov.setComida() == 1 && añadido == 0)
            {
                PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
                yield return new WaitForSeconds(0.5f); // Actualiza la ruta cada 0.5 segundos (ajusta seg�n sea necesario)
            }
            else
            {
                PathRequestManager.RequestPath(transform.position, startPosition.position, OnPathFound);//si pones dos transform se quedará en el sitio
           
                yield return new WaitForSeconds(0.5f);

            }
            if(Vector3.Distance(target.position, transform.position) <= 0.8f)
            {
                añadido = 1;
            }

        }
    }

    public void OnPathFound(Vector3[] newpath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newpath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath()
    {
        targetIndex = 0;

        while (true)
        {
            if (targetIndex >= path.Length)
            {
                float t = 0;
                while (t < 1)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, startRotation, t);
                    t += Time.deltaTime * 2f; // Ajusta la velocidad de giro según sea necesario
                    yield return null;
                }
                transform.rotation = startRotation;
                yield break;
            }

            Vector3 currentWaypoint = path[targetIndex];

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);

            Vector3 direction = currentWaypoint - transform.position;
            if (direction != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 5f);
            }

            if (transform.position == currentWaypoint)
            {
                targetIndex++;
            }

            yield return null;
        }
        

    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one * cubeSize);

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}
