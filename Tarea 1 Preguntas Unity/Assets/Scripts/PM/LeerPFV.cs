using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using models;
using UnityEngine;

public class LeerPFV : MonoBehaviour
{
    string lineaLeida;
    
    private List<PreguntaBool> listaPB;
    private List<PreguntaBool> listaPBF;
    private List<PreguntaBool> listaPBD;
    public List<PreguntaBool> ListaPB { get => listaPB; set => listaPB = value; }
    public List<PreguntaBool> ListaPBF { get => listaPBF; set => listaPBF = value; }
    public List<PreguntaBool> ListaPBD { get => listaPBD; set => listaPBD = value; }

    void Start()
    {
        ListaPB = new List<PreguntaBool>();
        ListaPBF = new List<PreguntaBool>();
        ListaPBD = new List<PreguntaBool>();
        ListaPB.Clear();
        ListaPBF.Clear();
        ListaPBD.Clear();
        leerPreguntasBoolean();
    }

    // Update is called once per frame
    void Update()
    {
     
    }
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
                string versiculo = string.Join("-", lineapartida, 2, lineapartida.Length - 3);
                //en algunas preguntas 2 y 3 son el versiculo y 4 o 3 la dificultad
                string dicultad = lineapartida[lineapartida.Length - 1];

                PreguntaBool objPB = new PreguntaBool(pregunta, respuestaCorrecta, versiculo, dicultad);
                ListaPB.Add(objPB);
                if (objPB.Dificultad.Equals("facil", StringComparison.OrdinalIgnoreCase))
                {
                    ListaPBF.Add(objPB);
                }
                if (objPB.Dificultad.Equals("dificil", StringComparison.OrdinalIgnoreCase))
                {
                    ListaPBD.Add(objPB);
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
