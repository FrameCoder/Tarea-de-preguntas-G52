using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using models;
using UnityEngine;

public class LeerPA : MonoBehaviour
{
    string lineaLeida;
    private List<PreguntaAbierta> listaPA;
    private List<PreguntaAbierta> listaPAF;
    private List<PreguntaAbierta> listaPAD;

    public List<PreguntaAbierta> ListaPA { get => listaPA; set => listaPA = value; }
    public List<PreguntaAbierta> ListaPAF { get => listaPAF; set => listaPAF = value; }
    public List<PreguntaAbierta> ListaPAD { get => listaPAD; set => listaPAD = value; }

    void Start()
    {
        listaPA = new List<PreguntaAbierta>();
        listaPAF = new List<PreguntaAbierta>();
        listaPAD = new List<PreguntaAbierta>();
        listaPA.Clear();
        listaPAF.Clear();
        listaPAD.Clear();
        leerPreguntaAbierta();
    }


    #region Leer Preguntas Multiples
    public void leerPreguntaAbierta()
        {
            try
            {
                StreamReader sr1 = new StreamReader("Assets/Resources/Files/ArchivoPreguntasAbiertas.txt");
                while ((lineaLeida = sr1.ReadLine()) != null)
                {
                    string[] lineapartida = lineaLeida.Split("-");
                    string pregunta = lineapartida[0];
                    string respuestaCorrecta = lineapartida[1];
                    string versiculo = string.Join("-", lineapartida, 2, lineapartida.Length - 3);
                    string dificultad = lineapartida[lineapartida.Length - 1];

                    PreguntaAbierta objPA = new PreguntaAbierta(pregunta,respuestaCorrecta,versiculo,dificultad);

                    listaPA.Add(objPA);
                    if (objPA.Dificultad.Equals("facil", StringComparison.OrdinalIgnoreCase))
                    {
                        listaPAF.Add(objPA);
                    }
                    if (objPA.Dificultad.Equals("dificil", StringComparison.OrdinalIgnoreCase))
                    {
                        listaPAD.Add(objPA);
                    }


                }

            }
            catch (Exception e)
            {
                Debug.Log("ERROR " + e.ToString());
            }
        }
        #endregion

    // Update is called once per frame
    void Update()
        {
        
        }
    }
