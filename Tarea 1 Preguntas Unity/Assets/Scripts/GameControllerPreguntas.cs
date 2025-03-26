using models;
using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = System.Random;



public class GameControllerPreguntas : MonoBehaviour
{
    public List<GameObject> listaGameObjects;
    List<PreguntaMultiple> listaPM;
    List<PreguntaMultiple> listaPMF;
    List<PreguntaMultiple> listaPMD;
    List<PreguntaBool> listaPB;
    List<PreguntaBool> listaPBF;
    List<PreguntaBool> listaPBD;
    List<PreguntaAbierta> listaPA;
    List<PreguntaAbierta> listaPAF;
    List<PreguntaAbierta> listaPAD;

    string lineaLeida;
    // Multiples
    public TextMeshProUGUI txtPreguntaM;
    public TextMeshProUGUI txtRespuesta1;
    public TextMeshProUGUI txtRespuesta2;
    public TextMeshProUGUI txtRespuesta3;
    public TextMeshProUGUI txtRespuesta4;

    // Abiertas
    public TextMeshProUGUI txtPreguntaA;
    public TextMeshProUGUI txtRespuestaA;

    // Falso Verdadero
    public TextMeshProUGUI txtPreguntaFV;
    public TextMeshProUGUI txtRespuestaF;
    public TextMeshProUGUI txtRespuestaV;

    //dif

    public TextMeshProUGUI txtDifucultad;
    
    //
    public TextMeshProUGUI txtAciertos;
    public TextMeshProUGUI txtErrores;

    public GameObject panelMultiples;
    public GameObject panelAbiertas;
    public GameObject panelFalsoVerdadero;
    public GameObject panelComenzar;
    public GameObject panelFinal;
    public string respuestaCorrecta;
    public int aciertos;
    public int errores;

    public int dificultadRonda;
    public string dificultad;
    public int canPreguntas;
    public int rondaActual;
    public int totalRondas;
    public int dificultadActual;
    public int cantidadClicks;
    public int contadorE;

    // Start is called before the first frame update
    void Start()
    {
        listaPM = new List<PreguntaMultiple>();
        listaPMF = new List<PreguntaMultiple>();
        listaPMD = new List<PreguntaMultiple>();
        listaPB = new List<PreguntaBool>();
        listaPBF = new List<PreguntaBool>();
        listaPBD = new List<PreguntaBool>();
        listaPA = new List<PreguntaAbierta>();
        listaPAF = new List<PreguntaAbierta>();
        listaPAD = new List<PreguntaAbierta>();

        listaPM = GameObject.Find("LeerPM").GetComponent<LeerPM>().ListaPM;
        listaPMF = GameObject.Find("LeerPM").GetComponent<LeerPM>().ListaPMF;
        listaPMD = GameObject.Find("LeerPM").GetComponent<LeerPM>().ListaPMD;
        listaPB = GameObject.Find("LeerPFV").GetComponent<LeerPFV>().ListaPB;
        listaPBF = GameObject.Find("LeerPFV").GetComponent<LeerPFV>().ListaPBF;
        listaPBD = GameObject.Find("LeerPFV").GetComponent<LeerPFV>().ListaPBD;
        listaPA = GameObject.Find("LeerPA").GetComponent<LeerPA>().ListaPA;
        listaPAF = GameObject.Find("LeerPA").GetComponent<LeerPA>().ListaPAF;
        listaPAD = GameObject.Find("LeerPA").GetComponent<LeerPA>().ListaPAD;


        canPreguntas = 20;
        rondaActual = 0;
        totalRondas = 2;
        aciertos = 0;
        errores = 0;
        dificultadActual = 0;
        cantidadClicks = 0;
        contadorE = 0;

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void comenzar()
    {
        panelComenzar.SetActive(false);
        mostrarPreguntas();
    }

    public void cantidadClick()
    {
        cantidadClicks++;
    }
    public void mostrarPreguntas()
    {
        if (listaPAF.Count == 0 && listaPBF.Count == 0 && listaPMF.Count == 0)
        {
            dificultadActual = 1;
            dificultad = "dificil";
            Debug.Log("Dificultad Actual: " + dificultadActual);
            txtDifucultad.text = dificultad;
        }

        if ((dificultadActual == 1 && listaPAD.Count == 0 && listaPBD.Count == 0 && listaPMD.Count == 0))
        {
            Debug.Log("¡No hay más preguntas disponibles!");
            // Puedes aquí activar una pantalla de fin de juego
            panelAbiertas.SetActive(false);
            panelMultiples.SetActive(false);
            panelFalsoVerdadero.SetActive(false);
            panelFinal.SetActive(true);
            txtAciertos.text = aciertos.ToString();
            txtErrores.text = errores.ToString();
            return;
            
        }



        Random random = new Random();
        int tipoPregunta = random.Next(0, listaGameObjects.Count);
        GameObject gameObject = listaGameObjects[tipoPregunta];



        if (gameObject.GetComponent<LeerPA>() != null)
        {
            List<PreguntaAbierta> listaFiltrada = dificultadActual == 0 ? listaPAF : listaPAD;
            if (listaFiltrada.Count > 0)
            {
                panelAbiertas.SetActive(true);
                panelMultiples.SetActive(false);
                panelFalsoVerdadero.SetActive(false);
                int pregunta = random.Next(0, listaFiltrada.Count);
                txtPreguntaA.text = listaFiltrada[pregunta].Pregunta;
                respuestaCorrecta = listaFiltrada[pregunta].RespuestaCorrecta;
                if (dificultadActual == 0)
                {
                    listaPAF.RemoveAt(pregunta);
                }
                else
                {
                    listaPAD.RemoveAt(pregunta);
                }
            }
            else
            {
                mostrarPreguntas();
            }

        }

        if (gameObject.GetComponent<LeerPFV>() != null)
        {
            List<PreguntaBool> listaFiltrada = dificultadActual == 0 ? listaPBF : listaPBD;

            if (listaFiltrada.Count > 0)
            {
                panelAbiertas.SetActive(true);
                panelAbiertas.SetActive(false);
                panelMultiples.SetActive(false);
                panelFalsoVerdadero.SetActive(true);

                int pregunta = random.Next(0, listaFiltrada.Count);
                txtPreguntaFV.text = listaFiltrada[pregunta].Pregunta;
                respuestaCorrecta = listaFiltrada[pregunta].RespuestaCorrecta;

                if (dificultadActual == 0)
                {
                    listaPBF.RemoveAt(pregunta);
                }
                else
                {
                    listaPBD.RemoveAt(pregunta);
                }
            }
            else
            {
                mostrarPreguntas();
            }
        }

        if (gameObject.GetComponent<LeerPM>() != null)
        {
            List<PreguntaMultiple> listaFiltrada = dificultadActual == 0 ? listaPMF : listaPMD;
            if (listaFiltrada.Count > 0)
            {
                panelAbiertas.SetActive(false);
                panelMultiples.SetActive(true);
                panelFalsoVerdadero.SetActive(false);
                int pregunta = random.Next(0, listaFiltrada.Count);
                txtPreguntaM.text = listaFiltrada[pregunta].Pregunta;
                txtRespuesta1.text = listaFiltrada[pregunta].Respuesta1;
                txtRespuesta2.text = listaFiltrada[pregunta].Respuesta2;
                txtRespuesta3.text = listaFiltrada[pregunta].Respuesta3;
                txtRespuesta4.text = listaFiltrada[pregunta].Respuesta4;
                respuestaCorrecta = listaFiltrada[pregunta].RespuestaCorrecta;

                if (dificultadActual == 0)
                {
                    listaPMF.RemoveAt(pregunta);
                }
                else
                {
                    listaPMD.RemoveAt(pregunta);
                }
            }
        }


    }

    public void mostrartamañolistas()
    {
        Debug.Log("Lista Preguntas Abiertas Facil: " + listaPAF.Count);
        Debug.Log("Lista Preguntas Multiples Facil: " + listaPMF.Count);
        Debug.Log("Lista Preguntas Falso Verdadero Facil: " + listaPBF.Count);
        Debug.Log("----------------------------------------------");
        Debug.Log("Lista Preguntas Abiertas Dificil: " + listaPAD.Count);
        Debug.Log("Lista Preguntas Multiples Dificil: " + listaPMD.Count);
        Debug.Log("Lista Preguntas Falso Verdadero Dificil: " + listaPBD.Count);
        Debug.Log("----------------------------------------------");
    }



    public void mostrarRespuesta()
    {
        try
        {
            txtRespuestaA.text = respuestaCorrecta;
        }
        catch (Exception e)
        {
            Debug.Log("ERROR AL MOSTRAR LA RESPUESTA" + e.ToString());
        }

    }

    public void verificarRespuestaMultiples()
    {
        GameObject botonPresionado = EventSystem.current.currentSelectedGameObject;
        TextMeshProUGUI textoRespuesta = botonPresionado.GetComponentInChildren<TextMeshProUGUI>();
        string respuesta = textoRespuesta.text;
        if (respuesta.Equals(respuestaCorrecta, StringComparison.OrdinalIgnoreCase))
        {
            aciertos++;
            Debug.Log("Respuesta Correcta");
            mostrarPreguntas();
        }
        else
        {
            if (contadorE == 1)
            {
                mostrarPreguntas();
                contadorE = 0;

            }
            errores++;
            contadorE++;
            Debug.Log("Respuesta Incorrecta");
        }


        /*if (txtRespuesta3 != null)
        {
            contadorE++;
            if(contadorE == 2)
            {
                mostrarPreguntas();
                contadorE = 0;
            }
            Debug.Log("Respuesta Incorrecta");
        }*/
    }

    public void verificarRespuestaFV()
    {
        GameObject botonPresionado = EventSystem.current.currentSelectedGameObject;
        TextMeshProUGUI textoRespuesta = botonPresionado.GetComponentInChildren<TextMeshProUGUI>();
        string respuesta = textoRespuesta.text;
        if (respuesta.Equals("Falso", StringComparison.OrdinalIgnoreCase))
        {
            respuesta = "false";
        }
        if (respuesta.Equals("Verdadero", StringComparison.OrdinalIgnoreCase))
        {
            respuesta = "true";
        }

        if (respuesta.Equals(respuestaCorrecta, StringComparison.OrdinalIgnoreCase))
        {
            aciertos++;
            Debug.Log("Respuesta Correcta");
            mostrarPreguntas();
        }
        else
        {
            mostrarPreguntas();
            errores++;
            Debug.Log("Respuesta Incorrecta");
        }

    }
}
