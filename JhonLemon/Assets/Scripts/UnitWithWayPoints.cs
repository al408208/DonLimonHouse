using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitWithWayPoints : MonoBehaviour
{
    public List<Transform> targets;
    public Transform comida;
    private int añadido=0;
    float speed = 1;
    Vector3[] path;
    int targetIndex;
    int currentTargetIndex = 0;
    float cubeSize = 0.2f;
    private PlayerMovement playermov;

    void Start()
    {
        playermov=FindObjectOfType<PlayerMovement>();

        SetDestination();
    }

    void SetDestination()
    {
        if (currentTargetIndex < targets.Count)
        {
            
            PathRequestManager.RequestPath(transform.position, targets[currentTargetIndex].position, OnPathFound);
        }
        
       /* if(playermov.setComida()==1 && añadido==0){//opcion 1 añado el punto aqui y modificio el index para que vaya al final
            targets.Add(comida);
            añadido = 1;
            currentTargetIndex= targets.Count-1;//aqui-2, si es en followpath -1
        }
        if(playermov.setComida()==1 && añadido==0){//opcion 2 ruta directa
            añadido = 1;
            PathRequestManager.RequestPath(transform.position, comida.position, OnPathFound);
        }*/
       
    }

    public void OnPathFound(Vector3[] newpath, bool pathSuccessful)
    {
        
        if (pathSuccessful)
        {
            path = newpath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
        else
        {
            // Manejar la falla en la solicitud de camino (opcional)
        }
    }

    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = path[0];

        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    targetIndex = 0; // Reiniciar al primer punto del camino
                    currentTargetIndex++;
                    if (currentTargetIndex >= targets.Count)
                    {
                        targets.Remove(comida); //Si ya ha ido y reinicia lo elimina
                        currentTargetIndex = 0; // Reiniciar al primer destino
                    }
                    
                    SetDestination();
                    yield break;
                }
                currentWaypoint = path[targetIndex]; 

            }
            if (playermov.setComida() == 1 && añadido == 0)
            {//opcion 3 meto la comida para que sea el siguiente punto
                targets.Insert(currentTargetIndex, comida);
                añadido = 1;
                SetDestination();
                yield break;
            }
            if (playermov.setComida() == 2 && añadido == 1)
            {
                targets.Insert(currentTargetIndex, comida);
                añadido = 2;
                SetDestination();
                yield break;
            }
            if (playermov.setComida() == 3 && añadido == 2)
            {
                targets.Insert(currentTargetIndex, comida);
                añadido = 3;
                SetDestination();
                yield break;
            }


            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);

            Vector3 direction = currentWaypoint - transform.position;
            if (direction != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 5f);
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
