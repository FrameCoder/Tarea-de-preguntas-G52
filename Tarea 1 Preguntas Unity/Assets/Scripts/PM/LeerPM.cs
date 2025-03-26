using System;
using System.Collections;
using System.Collections.Generic;
using models;
using System.IO;
using UnityEngine;

public class LeerPM : MonoBehaviour
{
    string lineaLeida;
    public int cantidad;
    private List<PreguntaMultiple> listaPM;
    private List<PreguntaMultiple> listaPMF;
    private List<PreguntaMultiple> listaPMD;
    public List<PreguntaMultiple> ListaPM { get => listaPM; set => listaPM = value; }
    public List<PreguntaMultiple> ListaPMF { get => listaPMF; set => listaPMF = value; }
    public List<PreguntaMultiple> ListaPMD { get => listaPMD; set => listaPMD = value; }

    void Start()
    {
        ListaPM = new List<PreguntaMultiple>();
        ListaPMF = new List<PreguntaMultiple>();
        ListaPMD = new List<PreguntaMultiple>();
        ListaPM.Clear();
        ListaPMF.Clear();
        ListaPMD.Clear();
        leerPreguntasMultiples();
    }

    // Update is called once per frame
    void Update()
    {
        
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
                string versiculo = string.Join("-", lineapartida, 6, lineapartida.Length - 7);
                string dicultad = lineapartida[lineapartida.Length - 1];

                PreguntaMultiple objPM = new PreguntaMultiple(pregunta, respuesta1, respuesta2,
                    respuesta3, respuesta4, respuestaCorrecta, versiculo, dicultad);

                ListaPM.Add(objPM);
                if (objPM.Dificultad.Equals("facil", StringComparison.OrdinalIgnoreCase))
                {
                    ListaPMF.Add(objPM);
                }
                if (objPM.Dificultad.Equals("dificil", StringComparison.OrdinalIgnoreCase))
                {
                    ListaPMD.Add(objPM);
                }


            }
        }
        catch (Exception e)
        {
            Debug.Log("ERROR " + e.ToString());
        }
    }
    #endregion
}
