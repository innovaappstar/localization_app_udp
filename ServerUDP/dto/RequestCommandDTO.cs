using System;
using System.Collections.Generic;
using ServerUDP.interfaces;

namespace ServerUDP.dto
{
    public class RequestCommandDTO
    {
        public RequestCommandDTO(string type, string content)
        {
            this.type = type;
            this.content = content;
        }

        public string type { get; set; }
        public string content { get; set; }

        public LocationDTO GetLocationDTO() {
            if (content == null) return new LocationDTO();
            String[] rowList = content.Split("~");

            if(rowList.Length < 2) return new LocationDTO();
            String[] indexList = rowList[0].Split("|");
            if(indexList[0] != "8") return new LocationDTO();   // indice de tipo tracking
            String[] bodyColumnList = rowList[1].Split("|");

            if (bodyColumnList.Length < 13) return new LocationDTO();

            String[] paramsList = rowList[1].Split("|");
            string FechaHora = paramsList[0];
            string Latitud = paramsList[1];
            string Longitud = paramsList[2];
            string Velocidad = paramsList[3];
            string Precision = paramsList[4];
            string Bateria = paramsList[5];
            string FrecuenciaPosteo = paramsList[6];
            string NomRuta = paramsList[7];
            string Lado = paramsList[8];
            //intToString(tracking.getCodSalidaServidor()),
            //intToString(tracking.getCodRegistroDespachoLocal()),
            string CodPersona = paramsList[11];
            //intToString(tracking.getCodServicio()),
            string ID = paramsList[13];
            return new LocationDTO(FechaHora, Latitud, Longitud, Velocidad, Bateria, FrecuenciaPosteo, NomRuta, Lado, CodPersona, ID);
        }

    }
}

/**
 *       tracking.getFechaHora(),
                        tracking.getLatitud(),
                        tracking.getLongitud(),
                        tracking.getVelocidad(),
                        tracking.getPrecision(),
                        intToString(tracking.getBateria()),
                        intToString(tracking.getFrecuenciaPosteo()),
                        tracking.getNomRuta(),
                        tracking.getLado(),
                        intToString(tracking.getCodSalidaServidor()),
                        intToString(tracking.getCodRegistroDespachoLocal()),
                        intToString(tracking.getCodPersona()),
                        intToString(tracking.getCodServicio()),
                        intToString(tracking.getID())
*/
