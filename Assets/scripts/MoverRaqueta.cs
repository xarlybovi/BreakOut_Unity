using UnityEngine;
using System.Collections;


public class MoverRaqueta : MonoBehaviour {
    public float speed = 5; //velocidad de la raqueta plr defecto
    public string axis = "Horizontal";
    private Vector2 touchDeltaPosition;
	// Update 60 veces por segundo en un intervalo de tiempo fijo
	void FixedUpdate () {
        float v = Input.GetAxisRaw(axis);
        GetComponent<Rigidbody2D>().velocity = new Vector2(v, 0) * speed; //Desplazamiento eje X

        //Control tactil de la raqueta
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary)
        {
            Vector2 touchPosition = Input.GetTouch(0).position;
            double halfScreen = Screen.width / 2.0;
            //Compruebo la posicion del toque en la pantalla y muevo la raqueta 
            if(touchPosition.x < halfScreen){
                this.transform.Translate(Vector3.left * 5 * Time.deltaTime);
            } else if (touchPosition.x > halfScreen) {
                this.transform.Translate(Vector3.right * 5 * Time.deltaTime);
            }
	    }
    }
}
