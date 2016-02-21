using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;


public class Mov_Pelota : MonoBehaviour {
    public float speed = 3; //velocidad pelota por defecto
    public AudioClip choque,perdida;//declaramos el AudioClip como atributo del script para los diferentes sonidos
    private GameObject sceneManager;//atributos para el acceso al objeto sceneManager
    private SceneManager scriptSceneManager;

    //Inicializacion
    void Start() {    
        //Impulso inicial  
        GetComponent<Rigidbody2D>().velocity = Vector2.down * 1; //la pelota cae lentamente al principio
        //Cargo los sonidos desde la jerarquía de mi escena 
        //(la ruta depende de tu organización (no lleva extensión)
        choque = (AudioClip)Resources.Load("sonidos/choque_pelota");
        perdida = (AudioClip)Resources.Load("sonidos/perdida");
        //Obtenemos la referencia al script de sceneManager
        sceneManager = GameObject.Find("SceneManager");
        scriptSceneManager = sceneManager.GetComponent<SceneManager>();
    }

    // Update is called once per frame
    void Update(){
    }

    //Fisicas para rebote en la raqueta con efecto
    float reboteX(Vector2 bolaPos, Vector2 raquetaPos, float anchoRaqueta)  
    {    
        // -1 <- parte izq            0 <- parte media           1 <- parte derecha
        // ========================================================================
        return (bolaPos.x - raquetaPos.x) / anchoRaqueta;   
    }


    void OnCollisionEnter2D(Collision2D col) {
        //col es el objecto que recibe la colisión de la pelota
        //el suelo no es visible pues está desactivado su render
        //pero sigue recibiendo la colisión de la pelota
        if (col.gameObject.name == "MuroHorizontal_inf")
        {
            scriptSceneManager.restaVidas();
            //Reproducimos el sonido de pelota perdida
            AudioSource.PlayClipAtPoint(perdida, col.transform.position, 15);
        }
        //Golpea la raqueta
        else if (col.gameObject.name == "Raqueta")
        {
            //Reproducimos el sonido del choque
            AudioSource.PlayClipAtPoint(choque, col.transform.position, 15);
            // Calculate hit Factor
            float x = reboteX(transform.position,//posicion de la pelota 
                                col.transform.position,//posicion de la raqueta
                                col.collider.bounds.size.x);//tamaño de la raqueta
            //En este caso se mueve hacia arriba (y=1)
            Vector2 dir = new Vector2(x, 1).normalized;
            //Se aplica la velocidad
            GetComponent<Rigidbody2D>().velocity = dir * speed;
        }

        //Golpea el muro horizontal superior
        else if (col.gameObject.name == "MuroHorizontal_ladrillos")
        {
            //Reproducimos el sonido del choque
            AudioSource.PlayClipAtPoint(choque, col.transform.position, 15);
            // Calcula el factor de rebote
            float x = reboteX(transform.position,
                                col.transform.position,
                                col.collider.bounds.size.x);
            //En este caso se mueve hacia abajo (y=-1)
            Vector2 dir = new Vector2(x, -1).normalized;
            //Se aplica la velocidad
            GetComponent<Rigidbody2D>().velocity = dir * speed;
        }
        //Golpea los muros laterales
        else if (col.gameObject.name == "MuroVertical_der" && col.gameObject.name == "MuroVertical_izq")
        {
            //Reproducimos el sonido del choque
            AudioSource.PlayClipAtPoint(choque, col.transform.position, 15);
        }
    }


}


