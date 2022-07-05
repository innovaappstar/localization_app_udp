using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Web;
using System.Xml;
using ServerUDP.constants;
using ServerUDP.dto;
using ServerUDP.services;
using ServerUDP.utilities;
// {"type":"track_command", "content": "11|8~0|240|05/07/2022 14:03:14|||412|-11.98958|-77.06691|2.2489796|34"}
// {"type":"track_command", "content": "8|1|240|1~05/07/2022 13:00:18|-11.98938|-77.06687|0.9|15.0|88|2|412|A|0|0|0|0|1129"}
namespace ServerUDP.server
{
    public class UDPSocket
    {
        public UDPSocket()
        {
        }
        // defining the global variables
        private Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        private const int bufSize = 8 * 1024;
        private State state = new State();
        private EndPoint remoteEndpoint = new IPEndPoint(IPAddress.Any, 0);
        private AsyncCallback recv = null;
        private static MqttService mqttService;

        public class State
        {
            public byte[] buffer = new byte[bufSize];
        }

        public void Server(string address, int port, string? answerMessage = null)
        {
            mqttService = new MqttService();
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            mqttService.Start(DevConstants.HIVE_HOST_BROKER_MQTT.Item1, DevConstants.MQTT_ID_TOPIC_SERVER_001, message =>
            {
                Console.WriteLine("======");
                Console.WriteLine(message);
            });
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.ReuseAddress, true);
            socket.Bind(new IPEndPoint(IPAddress.Parse(address), port));
            Receive(answerMessage);
        }

        public void Client(string address, int port)
        {
            socket.Connect(IPAddress.Parse(address), port);
            Receive();
        }

        public void Send(string text)
        {
            byte[] data = Encoding.ASCII.GetBytes(text);
            socket.BeginSend(data, 0, data.Length, SocketFlags.None, (ar) =>
            {
                State so = (State)ar.AsyncState;
                int bytes = socket.EndSend(ar);
                Console.WriteLine("SEND: {0}, {1}", bytes, text);
            }, state);
        }

        private void Receive(string? answerMessage = null)
        {
            socket.BeginReceiveFrom(state.buffer, 0, bufSize, SocketFlags.None, ref remoteEndpoint, recv = (ar) =>
            {
                State so = (State)ar.AsyncState;
                int bytes = socket.EndReceiveFrom(ar, ref remoteEndpoint);
                socket.BeginReceiveFrom(so.buffer, 0, bufSize, SocketFlags.None, ref remoteEndpoint, recv, so);
                var dataFromClient = Encoding.ASCII.GetString(so.buffer, 0, bytes);
                //Console.WriteLine("dataFromClient => {0}", dataFromClient);
                try
                {
                    RequestCommandDTO requestCommandDTO = JsonSerializer.Deserialize<RequestCommandDTO>(dataFromClient);
                    var commandFound = Constants.Commands.FirstOrDefault(item => item != null && item.type != null & item.type == requestCommandDTO.type);
                    if(commandFound.type == "mobile-gps")
                    {
                        LocationDTO locationDTO = requestCommandDTO.GetLocationDTO();
                        Console.WriteLine("-----------------");
                        Console.WriteLine("RECIBIDO: Type {0}, Content {1}", requestCommandDTO.type, requestCommandDTO.content);
                        if (locationDTO.FechaHora == null || locationDTO.FechaHora.Length == 0)
                        { }
                        else
                        {
                            string message = JsonSerializer.Serialize(locationDTO);
                            string quoted = HttpUtility.JavaScriptStringEncode(message);
                            string unescapedJsonString = quoted.Replace("\\", ""); // JsonConvert.DeserializeObject<string>(quoted);
                            LocationDTO locationDTO2Unparsed = JsonSerializer.Deserialize<LocationDTO>(unescapedJsonString);
                            mqttService.SendCode(quoted, DevConstants.MQTT_RQ_TOPIC_WINDOWS_FORM_001);
                        }
                    }
                }
                catch ( Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                //Console.WriteLine("RECV: {0}: {1}, {2}", remoteEndpoint.ToString(), bytes, Encoding.ASCII.GetString(so.buffer, 0, bytes));
                //Console.WriteLine("answerMessage server > {0}", answerMessage);
                if (answerMessage != null)
                {
                    byte[] data = Encoding.ASCII.GetBytes(answerMessage);
                    socket.SendTo(data, data.Length, SocketFlags.None, remoteEndpoint);
                    //Console.WriteLine("socket send to > {0}", socket);
                }
            }, state);
        }
    }
}
