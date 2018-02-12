using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {

	void OnTriggerEnter (Collider other)
    {
        TimeSpan tempo_recorde = TimeSpan.Parse(SettingsSingleton.Instance.tempo);
        if(GameObject.Find("Carro").GetComponent<Carrocontroller>().relogio != "")
        {
            TimeSpan tempo_actual = TimeSpan.Parse(GameObject.Find("Carro").GetComponent<Carrocontroller>().relogio);
            int resultado = TimeSpan.Compare(tempo_recorde, tempo_actual);
            if (resultado > 0)
            {
                SettingsSingleton.Instance.tempo = GameObject.Find("Carro").GetComponent<Carrocontroller>().ranktempo();
            }
            Debug.Log(resultado);
        }
        GameObject.Find("Carro").GetComponent<Carrocontroller>().timer_check = true;
        GameObject.Find("Carro").GetComponent<Carrocontroller>().timer = 0;
    }
}
