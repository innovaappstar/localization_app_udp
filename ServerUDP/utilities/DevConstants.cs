using System;
using System.Collections.Generic;

namespace ServerUDP.utilities
{
    public class DevConstants
    {
        public DevConstants()
        {
        }
        // tòpicos REQUEST and RESPONSE
        public static String MQTT_RQ_TOPIC_SERVER_001 = "topic/server/0001/RQ";
        public static String MQTT_RP_TOPIC_DEVICE_001 = "topic/device/0001/RP";
        public static String MQTT_RQ_TOPIC_WINDOWS_FORM_001 = "topic/windows_form/0001/RQ";

        // public hosts
        public static Tuple<String, int> HIVE_HOST_BROKER_MQTT = new Tuple<String, int>("broker.mqttdashboard.com", 1883);
        public static String MQTT_ID_TOPIC_SERVER_001 = "topic/server/0001";

    }
}
