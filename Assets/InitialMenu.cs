using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialMenu : MonoBehaviour
{
    public void Jugar()
    {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // carga la siguiente escena
    }

    public void Salir()
    {
        Debug.Log("Salir...");
        Application.Quit(); // cierra la aplicación
    }
}
