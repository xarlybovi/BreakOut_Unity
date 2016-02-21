using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Impacto_bloque : MonoBehaviour {
    public int dureza = 1; //dureza por defecto
    private int impactos;
    public AudioClip choque, explosion;//declaramos el AudioClip como atributo del script para los diferentes sonidos

	// Use this for initialization
	void Start () {
        impactos = 0;
        //Cargo el sonido desde la jerarquía de mi escena 
        explosion = (AudioClip)Resources.Load("sonidos/explosion");
        choque = (AudioClip)Resources.Load("sonidos/choque_pelota");

	}
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D col) {
        /////////////////////////////////
        /////Destruccion de bloques//////
        //Detecta el impacto de la pelota
        if (col.gameObject.tag == "Pelota"){
            impactos++;
            if (impactos == dureza){
                //Añadir sonido de explosion
                AudioSource.PlayClipAtPoint(explosion, col.transform.position, 15);
                //Destruyo el bloque
                Destroy(this.gameObject);
                //Añado puntuacion en funcion del tipo de bloque
                var tipo_bloque = this.transform.parent.name;
                //Hallo los puntos actuales y los añado al marcador
                GameObject puntuacion = GameObject.Find("Texto_cont_puntos");
                Text texto_puntuacion = puntuacion.GetComponent<Text>();
                if (tipo_bloque == "Bloques_blandos")
                {
                    int newScore = Int32.Parse(texto_puntuacion.text) + 5;
                    texto_puntuacion.text = newScore.ToString();         
                }
                else if (tipo_bloque == "Bloques_medios")
                {
                    int newScore = Int32.Parse(texto_puntuacion.text) + 10;
                    texto_puntuacion.text = newScore.ToString(); 
                }
                else if (tipo_bloque == "Bloques_duros")
                {
                    int newScore = Int32.Parse(texto_puntuacion.text) + 20;
                    texto_puntuacion.text = newScore.ToString(); 
                }

            }
            else
            {//En caso de no romper el bloque
                //Reproducimos el sonido del choque con la pelota
                AudioSource.PlayClipAtPoint(choque, col.transform.position, 15);
            }
        }
    }

}
