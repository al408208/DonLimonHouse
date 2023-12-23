using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f;

    Animator m_Animator;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;
    [SerializeField]AudioSource soundComer;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;
    public TextMeshProUGUI info;
    public int contcomida=0;


    [SerializeField] public cronometrojuego Temporizador;
    //INVISIBILIDAD
    public SkinnedMeshRenderer meshRendererToUse;
    public Material materialToUse;
    public Material materialToUse2;
    public bool invisible = false;
    public bool detectable = true;


    void Start()
    {

        materialToUse.SetColor("_Color", new Color(1f, 1f, 1f, 1f));
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();
    }

    //INVISIBILIDAD
    void Update()
    {
        if (invisible == false)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {

                CambiarMaterial();

            }
        }
    }

    void CambiarMaterial()
    {
        invisible = true;
        detectable = false;
        materialToUse.SetColor("_Color", new Color(1f, 1f, 1f, 0.05f));
        meshRendererToUse.material = materialToUse;
        Temporizador.ActivarTemporizador();
        Invoke("CambiarMaterial2", 3f);

    }

    void CambiarMaterial2()
    {
        detectable = true;
        materialToUse.SetColor("_Color", new Color(1f, 1f, 1f, 1f));
        meshRendererToUse.material = materialToUse2;

        Invoke("Cooldown", 10f);
    }

    void Cooldown()
    {
        invisible = false;

    }




    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool("IsWalking", isWalking);

        if (isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop();
        }

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);
    }

    void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Rotation);
    }

    public void SetInfoText(string newInfo)
	{        
		info.text =newInfo.ToString();
        
	}

    public void addComida(){
        contcomida+=1;
        soundComer.Play();
        SetInfoText(contcomida+"/3");
    }

    public int setComida(){
        return contcomida;
    }

}