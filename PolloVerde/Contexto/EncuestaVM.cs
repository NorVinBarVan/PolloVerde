using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PolloVerde.Contexto
{
    public class EncuestaVM
    {
        public int id_cliente { get; set; }
        public List<RespuestaVM> respuestas { get; set; }
    }

    public class RespuestaVM
    {
        public int id_pregunta { get; set; }
        public string descripcion_respuesta { get; set; }
    }

    public class PreguntaVM
    {
        public int id_pregunta_encuesta { get; set; }
        public string descripcion_pregunta { get; set; }
    }
}