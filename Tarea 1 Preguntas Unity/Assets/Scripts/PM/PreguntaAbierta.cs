using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace models
{
    public class PreguntaAbierta
    {
        private String pregunta;
        private String respuestaCorrecta;
        private String versiculo;
        private String dificultad;

        public PreguntaAbierta()
        {
        }
        public PreguntaAbierta(string pregunta, string respuestaCorrecta, string versiculo, string dificultad)
        {
            this.Pregunta = pregunta;
            this.RespuestaCorrecta = respuestaCorrecta;
            this.Versiculo = versiculo;
            this.Dificultad = dificultad;
        }

        public string Pregunta { get => pregunta; set => pregunta = value; }
        public string RespuestaCorrecta { get => respuestaCorrecta; set => respuestaCorrecta = value; }
        public string Versiculo { get => versiculo; set => versiculo = value; }
        public string Dificultad { get => dificultad; set => dificultad = value; }
    }
}