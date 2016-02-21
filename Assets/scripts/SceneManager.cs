using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class SceneManager : MonoBehaviour {
    private GameObject pelota;
    private Vector3 pelota_pos;
    int speed = 3;

    public int vidas = 3;

    private GameObject canvasFinPartida;//referencia al canvas de fin de partida que ocultamos al inicio
    private GameObject labelResultado;
    private Text textoResultado;

	// Use this for initialization
	void Start () {
        //Escala de tiempo a 1 para que el juego arranque, se pone a 0 en fin de partida (se detiene el juego).
        Time.timeScale = 1;
        //Recogemos la referencia al objeto pelota dentro de la escena y su posicion de inicio
        pelota = GameObject.Find("Pelota");
        pelota_pos = pelota.transform.position;
        //Ocultamos el canvas que muestra el fin de partida
        canvasFinPartida = GameObject.Find("Canvas_fin");
        canvasFinPartida.SetActive(false);
        //Establezco el marcador de vidas
        GameObject labelVidas = GameObject.Find("Texto_cont_vidas");
        Text texto_vidas = labelVidas.GetComponent<Text>();
        texto_vidas.text = vidas.ToString();   
	}
	
	// Update is called once per frame
	void Update () {
        //Si no quedan bloques, partida ganada
        if (GameObject.FindGameObjectsWithTag("Bloque").Length == 0)
        {
            FinPartida(true); //partida ganada
        }
	}
   
    //Acciones al pulsar los botones de jugar y salir
    public void OnClick_jugar() {
        Application.LoadLevel("Principal");
    }
    public void OnClick_salir()
    {
        Application.Quit();
    }

    //Funcion para reiniciar la posicion de la pelota una vez perdida
    void reiniciarPelota()//situamos la bola en el centro y le damos movimiento inicial
    {
        pelota.gameObject.transform.position = pelota_pos;
        pelota.GetComponent<Rigidbody2D>().velocity = Vector2.down * 1; //Pelota cae lentamente en el reset
    }

    //Finaliza la partida en funcion de si se han destruido los bloques o se han acabado las vidas
    void FinPartida(Boolean resultado)
    {
        Time.timeScale = 0; //paramos la ejecución
        canvasFinPartida.SetActive(true); //Activamos el canvas de fin de partidas con el menu de opciones y el resultado
        GameObject labelResultado = GameObject.Find("Texto_resultado");
        Text textoResultado = labelResultado.GetComponent<Text>();
        if (resultado)
        {
            textoResultado.text = "VICTORIA";

        }else
        {
            textoResultado.text = "GAME OVER";
        }
    }

    public void restaVidas()
    {
        //Hallo las vidas actuales y resto del marcador
        vidas--;
        GameObject labelVidas = GameObject.Find("Texto_cont_vidas");
        Text texto_vidas = labelVidas.GetComponent<Text>();
        texto_vidas.text = vidas.ToString();  
        //Compruebo que no sea la ultima vida
        if (vidas == 0)
        {
            FinPartida(false); //derrota
        }
        reiniciarPelota();
    }

}
