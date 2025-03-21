using models;
using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = System.Random;



public class GameControllerPreguntas : MonoBehaviour
{
    List<PreguntasMultiples> listaPM;
    List<PreguntasMultiples> listaPMF;
    List<PreguntasMultiples> listaPMD;
    List<PreguntasBool> listaPB;

    string lineaLeida;
    public TextMeshProUGUI txtPRegunta;
    public TextMeshProUGUI txtRespuesta1;
    public TextMeshProUGUI txtRespuesta2;
    public TextMeshProUGUI txtRespuesta3;
    public TextMeshProUGUI txtRespuesta4;
    public TextMeshProUGUI txtDificultad;
    public object respuestaCorrecta;
    public string respuestas;
    public int aciertos;
    public int errores;

    // Start is called before the first frame update
    void Start()
    {
        listaPM = new List<PreguntasMultiples>();
        listaPMF = new List<PreguntasMultiples>();
        listaPMD = new List<PreguntasMultiples>();
        listaPB = new List<PreguntasBool>();

        txtDificultad.text = "MAURICIo";

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void leerPreguntasGeneral()
    {

        listaPB.Clear();
        listaPMF.Clear();
        listaPM.Clear();
        listaPMD.Clear();
        leerPreguntasMultiples();
        leerPreguntasBoolean();
    }

    public void mostrarPreguntas()
    {
        Random random = new Random();
        int tipoPregunta = random.Next(1, 3);

        if (tipoPregunta == 1)
        {
            int indiceAleatorioMul = random.Next(1, 40);

            txtPRegunta.text = listaPM[indiceAleatorioMul].Pregunta;
            txtRespuesta1.text = listaPM[indiceAleatorioMul].Respuesta1;
            txtRespuesta2.text = listaPM[indiceAleatorioMul].Respuesta2;
            txtRespuesta3.text = listaPM[indiceAleatorioMul].Respuesta3;
            txtRespuesta4.text = listaPM[indiceAleatorioMul].Respuesta4;
            respuestaCorrecta = listaPM[indiceAleatorioMul].RespuestaCorrecta;
        }
        else if (tipoPregunta == 2)
        {
            int indiceAleatorioBl = random.Next(1, 18);

            txtPRegunta.text = listaPB[indiceAleatorioBl].Pregunta;
            txtRespuesta1.text = "Verdadero";
            txtRespuesta2.text = "Falso";
            txtRespuesta3.text = "";
            txtRespuesta4.text = "";
            respuestaCorrecta = listaPB[indiceAleatorioBl].RespuestaCorrecta;
        }
    }

    #region respuestasMensaje(Anterior)
    public void respuesta1()
    {
        if (txtRespuesta1.text.Equals(respuestaCorrecta))
        {
            Debug.Log("Respuesta Correcta");
        }
        else
        {
            Debug.Log("Respuesta Incorrecta");
        }
    }

    public void respuesta2()
    {
        if (txtRespuesta2.text.Equals(respuestaCorrecta))
        {
            Debug.Log("Respuesta Correcta");
        }
        else
        {
            Debug.Log("Respuesta Incorrecta");
        }
    }
    public void respuesta3()
    {
        if (txtRespuesta3.text.Equals(respuestaCorrecta))
        {
            Debug.Log("Respuesta Correcta");
        }
        else
        {
            Debug.Log("Respuesta Incorrecta");
        }
    }
    public void respuesta4()
    {
        if (txtRespuesta4.text.Equals(respuestaCorrecta))
        {
            Debug.Log("Respuesta Correcta");
        }
        else
        {
            Debug.Log("Respuesta Incorrecta");
        }
    }

    #endregion

    public void VerificarRespuesta()
    {
        GameObject botonPresionado = EventSystem.current.currentSelectedGameObject;
        TextMeshProUGUI textoRespuesta = botonPresionado.GetComponentInChildren<TextMeshProUGUI>();

        if (respuestaCorrecta.Equals("true") || respuestaCorrecta.Equals("false"))
        {
            string textoRespuestaSuplemento = textoRespuesta.text.Equals("Verdadero") ? "true" : "false";

            if (textoRespuestaSuplemento.Equals(respuestaCorrecta))
            {
                aciertos++;
                Debug.Log("Respuesta Correcta" + "  - Aciertos: " + aciertos + "  - Errores: " + errores);
                mostrarPreguntas();
            }
            else
            {
                errores++;
                Debug.Log("Respuesta Incorrecta" + "  - Aciertos: " + aciertos + "  - Errores: " + errores);
                mostrarPreguntas();
            }
        }
        else
        {
            if (textoRespuesta.text.Equals(respuestaCorrecta))
            {
                aciertos++;
                Debug.Log("Respuesta Correcta" + "  - Aciertos: " + aciertos + "  - Errores: " + errores);
                mostrarPreguntas();
            }
            else
            {
                errores++;
                Debug.Log("Respuesta Incorrecta" + "  - Aciertos: " + aciertos + "  - Errores: " + errores);
                mostrarPreguntas();
            }
        }
    }


    #region Leer Preguntas Multiples
    public void leerPreguntasMultiples()
    {
        try
        {
            StreamReader sr1 = new StreamReader("Assets/Resources/Files/ArchivoPreguntasM.txt");
            while ((lineaLeida = sr1.ReadLine()) != null)
            {
                string[] lineapartida = lineaLeida.Split("-");
                string pregunta = lineapartida[0];
                string respuesta1 = lineapartida[1];
                string respuesta2 = lineapartida[2];
                string respuesta3 = lineapartida[3];
                string respuesta4 = lineapartida[4];
                string respuestaCorrecta = lineapartida[5];
                string versiculo = lineapartida[6];
                string dicultad = lineapartida[7];

                PreguntasMultiples objPM = new PreguntasMultiples(pregunta, respuesta1, respuesta2,
                    respuesta3, respuesta4, respuestaCorrecta, versiculo, dicultad);

                if (objPM.Dificultad.Equals("facil"))
                    {
                    listaPMF.Add(objPM);
                }
                if (objPM.Dificultad.Equals("dificil"))
                {
                    listaPMD.Add(objPM);
                }
                listaPM.Add(objPM);

            }
            Debug.Log("Tamaño de la Lista preguntas multiple: " + listaPM.Count);
        }
        catch (Exception e)
        {
            Debug.Log("ERROR " + e.ToString());
        }
    }
    #endregion


    #region Leer Preguntas Bool
    public void leerPreguntasBoolean()
    {
        try
        {
            StreamReader sr1 = new StreamReader("Assets/Resources/Files/preguntasFalso_Verdadero.txt");
            while ((lineaLeida = sr1.ReadLine()) != null)
            {
                string[] lineapartida = lineaLeida.Split("-");
                string pregunta = lineapartida[0];
                string respuestaCorrecta = lineapartida[1];
                string versiculo = lineapartida[2];
                string dicultad = lineapartida[3];

                PreguntasBool objPB = new PreguntasBool(pregunta, respuestaCorrecta, versiculo, dicultad);

                listaPB.Add(objPB);

            }
            Debug.Log("Tamaño de la Lista preguntas Bool: " + listaPB.Count);
        }
        catch (Exception e)
        {
            Debug.Log("ERROR " + e.ToString());
        }
    }
    #endregion

}
