using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cronometrojuego : MonoBehaviour
{

    [SerializeField] public float tiempoMaximo;
    [SerializeField] public Slider slider;


    public float tiempoActual;
    public bool tiempoActivado = false;



    public void Update()
    {
        if (tiempoActivado)
        {
            CambiarContador();
        }
    }

    public void CambiarContador()
    {
        tiempoActual -= Time.deltaTime;
        if (tiempoActual >= 0)
        {
            slider.value = tiempoActual;
        }
        if (tiempoActual <= 0)
        {
            Debug.Log("regenerado");
            tiempoActivado = false;
        }

    }

    public void CambiarTemporizador(bool estado)
    {
        tiempoActivado = estado;
    }

    public void ActivarTemporizador()
    {

        tiempoActual = tiempoMaximo;
        slider.maxValue = tiempoMaximo;
        CambiarTemporizador(true);

    }

    public void DesactivarTemporizador()
    {
        CambiarTemporizador(false);
    }
}