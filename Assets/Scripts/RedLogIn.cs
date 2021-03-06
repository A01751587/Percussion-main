using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System;
using UnityEngine.SceneManagement;

/*Autores: Paulo Ogando
 *          Christian Parrish
 *          Eduardo Cortez
 *          Maximiliano Ben?tez
 *          Jorge Blanco */

public class RedLogIn : MonoBehaviour
{

    [SerializeField]
    public static RedLogIn instance;

    public TextMeshProUGUI resultado;

    //Los datos de entrada
    public TMP_InputField textoUsuario;
    public TMP_InputField textoContrasena;

    public string usuarioLI = "Null";


    private void Awake()
    {
        instance = this;
    }


    public string regresaUsuario()
    {
        instance = this;
        string usuario = textoUsuario.text;

        return usuario;
    }

    public string regresaContrasena()
    {
        instance = this;
        string contrasena = textoContrasena.text;
        return contrasena;
    }



    //Enviar los datos al servidor(click del boton)
    public void EnviarDatos()
    {
        StartCoroutine(SubirDatos());
    }

    private IEnumerator SubirDatos()
    {
        //Recuperar los datos
        string usuario = textoUsuario.text;
        string contrasena = textoContrasena.text;

        usuarioLI = usuario;

        PlayerPrefs.SetString("Usuario", usuarioLI);
        PlayerPrefs.Save();


        //Crear un objeto con los datos
        WWWForm forma = new WWWForm();
        forma.AddField("nombreUsuario", usuario);
        forma.AddField("contrasena", contrasena);

        //http ://143.198.157.129/CapturaDatos/IniciaSesion.php
        //https ://pwt-beta.000webhostapp.com/CapturaDatos/IniciaSesion.php



        UnityWebRequest request = UnityWebRequest.Post("http://143.198.157.129/CapturaDatos/IniciaSesion.php", forma);
        yield return request.SendWebRequest();
        //....despues de cierto tiempo
        if (request.result == UnityWebRequest.Result.Success)
        {
            string texto = request.downloadHandler.text;
            resultado.text = texto;

            print("*" + texto.Trim() + "*");


            if (texto.Trim() == "Success")
            {

                SceneManager.LoadScene("Menu_Principal");


            }
            else
            {
                print("Incorrecto");
            }
        }
        else
        {
            resultado.text = "Error: " + request.ToString();
        }
    }


}