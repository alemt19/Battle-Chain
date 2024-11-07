using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class returnMenu : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("InitialMenu"); // carga la escena usando su nombre
    }

    public void Salir()
    {
        Debug.Log("Salir...");
        Application.Quit(); // cierra la aplicación
    }
}