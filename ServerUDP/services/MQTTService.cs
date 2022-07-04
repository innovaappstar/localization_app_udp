using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using ServerUDP.utilities;

namespace ServerUDP.services
{
    public class MqttService
    {
        IMqttClient mqttClient;
        public async Task Start(string brokerIp, string clientId, Action<string> callback = null)
        {
            var factory = new MqttFactory();
            var options = new MqttClientOptionsBuilder()
            .WithTcpServer(brokerIp, DevConstants.HIVE_HOST_BROKER_MQTT.Item2)
            .WithClientId(clientId)
            .Build();
            mqttClient = factory.CreateMqttClient();
            mqttClient.UseConnectedHandler(async e =>
            {
                Debug.WriteLine("MQTT Connected");
                await mqttClient.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic(DevConstants.MQTT_RQ_TOPIC_SERVER_001).Build());
            });
            mqttClient.UseApplicationMessageReceivedHandler(e =>
            {
                Console.WriteLine("### RECEIVED APPLICATION MESSAGE ###");
                Console.WriteLine($"+ Topic = {e.ApplicationMessage.Topic}");
                Console.WriteLine($"+ Payload = {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}");
                Console.WriteLine($"+ QoS = {e.ApplicationMessage.QualityOfServiceLevel}");
                Console.WriteLine($"+ Retain = {e.ApplicationMessage.Retain}");
                Console.WriteLine();
                var message = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                callback?.Invoke(message);
            });
            mqttClient.UseDisconnectedHandler(async e =>
            {
                Console.WriteLine("MQTT Reconnecting");
                await Task.Delay(TimeSpan.FromSeconds(5));
                await mqttClient.ConnectAsync(options, CancellationToken.None);
            });
            await mqttClient.ConnectAsync(options, CancellationToken.None);
        }
        public async Task SendCode(string message, string? topic = null)
        {
            if(topic != null)
                await mqttClient.PublishAsync(topic, message);
        }
    }
}
