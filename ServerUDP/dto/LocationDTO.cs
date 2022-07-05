using System;
namespace ServerUDP.dto
{
    public class LocationDTO
    {
        public LocationDTO()
        {
        }

        public LocationDTO(string fechaHora, string latitud, string longitud, string velocidad, string bateria, string frecuenciaPosteo, string nomRuta, string lado, string codPersona, string iD)
        {
            FechaHora = fechaHora;
            Latitud = latitud;
            Longitud = longitud;
            Velocidad = velocidad;
            Bateria = bateria; 
            FrecuenciaPosteo = frecuenciaPosteo;
            NomRuta = nomRuta;
            Lado = lado;
            CodPersona = codPersona;
            ID = iD;
        }

        public string FechaHora { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public string Velocidad { get; set; }
        public string Bateria { get; set; }
        public string FrecuenciaPosteo { get; set; }
        public string NomRuta { get; set; }
        public string Lado { get; set; }
        public string CodPersona { get; set; }
        public string ID { get; set; }

    }
}
