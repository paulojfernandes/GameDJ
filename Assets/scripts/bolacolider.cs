using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class bolacolider : MonoBehaviour
{

    public WheelCollider RodaFE;
    public WheelCollider RodaTE;
    public WheelCollider RodaTD;
    public WheelCollider RodaFD;
    public Transform RodaFEtrans;
    public Transform RodaTEtrans;
    public Transform RodaTDtrans;
    public Transform RodaFDtrans;
    public float maxtorque;
    public float brake;
    public float angle;
    public float currentspeed = 0f;
    public float carrospeed = 0f;
    private int velocidadeint;
    public Material carroMap;
    public Material carroMap2;
    public float timer = 0;
    private int tempo = 0;
    private float arcont = 0;
    public String relogio;
    AudioSource audioSource;
    public GameObject top, back;
    public Text velocidade;
    public Text relogio_texto;
    public Text temporecord;
    public bool timer_check = false;
    private bool somestado;

    // Use this for initialization
    void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = new Vector3(0, -0.5f, 0);
        setcamera();
        //tempo ficticio para não bugar timers durante o jogo
        SettingsSingleton.Instance.tempo = "00:02:00";
        setCor();
        Debug.Log(SettingsSingleton.Instance.sound+" som");
        somestado=SettingsSingleton.Instance.sound;

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //direçao frontal do carro
        if (Input.GetAxis("Vertical") == 1)
        {
            RodaTE.motorTorque = maxtorque * Input.GetAxis("Vertical");
            RodaTD.motorTorque = maxtorque * Input.GetAxis("Vertical");

        }
        if (Input.GetButton("Vertical") == false)
        {
            RodaTE.brakeTorque = brake * 0.05f;
            RodaTD.brakeTorque = brake * 0.05f;
        }
        else
        {
            RodaTE.brakeTorque = 0;
            RodaTD.brakeTorque = 0;
        }

        if (Input.GetAxis("Vertical") == -1 && GetComponent<Rigidbody>().velocity != Vector3.zero && currentspeed >= 0)
        {
            RodaTE.brakeTorque = brake;
            RodaTD.brakeTorque = brake;
        }
        else
        {
            RodaTE.brakeTorque = 0;
            RodaTD.brakeTorque = 0;
            RodaTE.motorTorque = maxtorque * Input.GetAxis("Vertical");
            RodaTD.motorTorque = maxtorque * Input.GetAxis("Vertical");
        }

        // calculo da velocidade no instante e implementação do mesmo no ecra
        currentspeed = 2 * 22 / 7 * RodaTD.radius * RodaTD.rpm * 60 / 1000;
        carrospeed = GameObject.Find("Carro").GetComponent<Rigidbody>().velocity.magnitude * (3.6f); //3.6 para calcular km/h;
        velocidadeint = (int)carrospeed;
        velocidade.text = velocidadeint.ToString() + " KM/H";

        // direção do horizontal do carro
        RodaFE.steerAngle = angle * Input.GetAxis("Horizontal");
        RodaFD.steerAngle = angle * Input.GetAxis("Horizontal");

        // calculo do tempo
        if (timer_check == true)
        {
            contartempo();
        }

        //implementar tempo recorde

        temporecord.text = SettingsSingleton.Instance.tempo;

        //raycaster verificar se esta de pernas para o ar

        Vector3 fwd = transform.TransformDirection(Vector3.down);
        if ((Physics.Raycast(GetComponent<Rigidbody>().transform.position, fwd, 10)) == true)
        {
            arcont = 0;
        }
        else
        {
            arcont += Time.deltaTime;
            print("There isn´t something in front of the object!");
        }

        if (arcont >= 5)
        {
            ResetCarro();
        }
        //reset carro

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetCarro();
        }

        // ir para menu
        if (Input.GetKeyDown(KeyCode.Escape)){
            SceneManager.LoadScene("Menu");
        }

        Implsom();

    }

    // som
    public void Implsom ()
    {
        if (somestado == true)
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.pitch = carrospeed / 150 + 0.2f;
        }
        else
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.pitch = 0;
        }
    }

    public void contartempo()
    {
        timer += Time.deltaTime;
        tempo = (int)timer;
        TimeSpan ts = TimeSpan.FromSeconds(tempo);
        relogio = ts.ToString();
        relogio_texto.text = relogio;
    }

    // dar reset no carro
    public void ResetCarro()
    {
        timer = 0;
        timer_check = false;
        relogio = "";
        relogio_texto.text = "";
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        RodaTE.motorTorque = 0;
        RodaTD.motorTorque = 0;
        RodaFE.motorTorque = 0;
        RodaFD.motorTorque = 0;
        GetComponent<Rigidbody>().transform.position = new Vector3(-2401f, 3f, -4f);
        GetComponent<Rigidbody>().transform.eulerAngles = new Vector3(0, 33, 0);
    }

    void Update()
    {
        // dar rotação nas rodas visuais
        RodaFDtrans.Rotate(RodaFD.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        RodaTDtrans.Rotate(RodaTD.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        RodaFEtrans.Rotate(RodaFE.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        RodaTEtrans.Rotate(RodaTE.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        // dar rotação transversal nas rodas frontais, é subtraido o localeulerangle porque interferia com o eixo do z e com essa interferencia as rodas bugavam
        RodaFDtrans.localEulerAngles = new Vector3(RodaFDtrans.localEulerAngles.x, RodaFD.steerAngle - RodaFDtrans.localEulerAngles.z, RodaFDtrans.localEulerAngles.z);
        RodaFEtrans.localEulerAngles = new Vector3(RodaFEtrans.localEulerAngles.x, RodaFE.steerAngle - RodaFEtrans.localEulerAngles.z, RodaFEtrans.localEulerAngles.z);
    }

    public String ranktempo()
    {
        if(timer_check== true)
        {
                return relogio;
        }
        
        return null;
    }
    public void setCor()
    {
        if (SettingsSingleton.Instance.carColor == "Red")
        {
            GameObject.Find("car_main_part").GetComponent<Renderer>().material = carroMap;
        }
        else if(SettingsSingleton.Instance.carColor == "Blue")
        {
            GameObject.Find("car_main_part").GetComponent<Renderer>().material = carroMap2;
        }
    }

    public void setcamera()
    {
        if (SettingsSingleton.Instance.typeOfCamera == "Top")
        {
            top.SetActive(true);
            back.SetActive(false);
        }
        else
        {
            top.SetActive(false);
            back.SetActive(true);
        }
    }

    //  implementar todos os resultados graficos no ecra (velocidades, tempos)
    // dividir todos os scripts por ordem


}

