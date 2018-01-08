using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDirectionWithinBounds : MonoBehaviour {

    public float velocidadMax;

    public float xMax;
    public float zMax;
    public float xMin;
    public float zMin;

    private float x;
    private float z;
    private float tiempo;
    private float angulo;

    // Use this for initialization
    void Start() {


        x = Random.Range(-velocidadMax, velocidadMax);
        z = Random.Range(-velocidadMax, velocidadMax);
        angulo = Mathf.Atan2(x, z) * (180 / 3.141592f) + 90;
        transform.localRotation = Quaternion.Euler(0, angulo, 0);
    }

    // Update is called once per frame
    void Update() {

        tiempo += Time.deltaTime;

        if (transform.localPosition.x > xMax) {
            Debug.Log("inside if (transform.localPosition.x > xMax)");
            x = Random.Range(-velocidadMax, 0.0f);
            angulo = Mathf.Atan2(x, z) * (180 / 3.141592f) + 90;
            transform.localRotation = Quaternion.Euler(0, angulo, 0);
            tiempo = 0.0f;
        }
        if (transform.localPosition.x < xMin) {
            Debug.Log("inside if (transform.localPosition.x < xMin)");
            x = Random.Range(0.0f, velocidadMax);
            angulo = Mathf.Atan2(x, z) * (180 / 3.141592f) + 90;
            transform.localRotation = Quaternion.Euler(0, angulo, 0);
            tiempo = 0.0f;
        }
        if (transform.localPosition.z > zMax) {
            Debug.Log("inside if (transform.localPosition.z > zMax)");
            z = Random.Range(-velocidadMax, 0.0f);
            angulo = Mathf.Atan2(x, z) * (180 / 3.141592f) + 90;
            transform.localRotation = Quaternion.Euler(0, angulo, 0);
            tiempo = 0.0f;
        }
        if (transform.localPosition.z < zMin) {
            Debug.Log("inside if (transform.localPosition.z < zMin)");
            z = Random.Range(0.0f, velocidadMax);
            angulo = Mathf.Atan2(x, z) * (180 / 3.141592f) + 90;
            transform.localRotation = Quaternion.Euler(0, angulo, 0);
            tiempo = 0.0f;
        }


        if (tiempo > 1.0f) {
            x = Random.Range(-velocidadMax, velocidadMax);
            z = Random.Range(-velocidadMax, velocidadMax);
            angulo = Mathf.Atan2(x, z) * (180 / 3.141592f) + 90;
            transform.localRotation = Quaternion.Euler(0, angulo, 0);
            tiempo = 0.0f;
        }

        //transform.localPosition = new Vector3(transform.localPosition.x + x, transform.localPosition.y, transform.localPosition.z + z);
        transform.Translate(Vector3.forward * Random.Range(0.0f, 1.0f) * Time.deltaTime);
        GetComponent<Rigidbody>().MovePosition(transform.position);
    }
}
