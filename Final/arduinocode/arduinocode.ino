#include "DFRobot_BloodOxygen_S.h"
#include "WiFiS3.h"

char ssid[] = "Madagascar_105";
char pass[] = "Madagascar";
int status = WL_IDLE_STATUS;
WiFiClient client;

char server[] = "192.168.0.103";
int port = 5121;

unsigned long lastConnectionTime = 0;
const unsigned long postingInterval = 10L * 1000L;

#define I2C_COMMUNICATION
#define I2C_ADDRESS 0x57 

DFRobot_BloodOxygen_S_I2C MAX30102(&Wire, I2C_ADDRESS);

void setup() {
    Serial.begin(115200);
    while (!Serial) {
        ; // Wait for serial port to connect
    }

    Serial.println("Starting setup...");

    // Initialize MAX30102 sensor
    while (false == MAX30102.begin()) {
        Serial.println("Sensor initialization failed! Check your wiring.");
        delay(1000);
    }
    Serial.println("Sensor initialization successful!");

    // Check for WiFi module
    if (WiFi.status() == WL_NO_MODULE) {
        Serial.println("Communication with WiFi module failed!");
        while (true);
    }

    String fv = WiFi.firmwareVersion();
    if (fv < WIFI_FIRMWARE_LATEST_VERSION) {
        Serial.println("Please upgrade the firmware");
    }

    // Attempt to connect to WiFi
    while (status != WL_CONNECTED) {
        Serial.print("Attempting to connect to SSID: ");
        Serial.println(ssid);
        status = WiFi.begin(ssid, pass);
        delay(10000);
    }

    Serial.print("SSID: ");
    Serial.println(WiFi.SSID());

    // Print IP address
    IPAddress ip = WiFi.localIP();
    Serial.print("IP Address: ");
    Serial.println(ip);

    // Print signal strength
    long rssi = WiFi.RSSI();
    Serial.print("Signal strength (RSSI): ");
    Serial.print(rssi);
    Serial.println(" dBm");

    Serial.println("Starting measurements...");
    MAX30102.sensorStartCollect();
}

void loop() {
    // Get heart rate and SPO2 data
    MAX30102.getHeartbeatSPO2();

    if (MAX30102._sHeartbeatSPO2.SPO2 == -1 || MAX30102._sHeartbeatSPO2.Heartbeat == -1) {
        Serial.println("Calibrating...");
        delay(4000);
        return;
    }

    // Print sensor values
    Serial.print("SPO2: ");
    Serial.print(MAX30102._sHeartbeatSPO2.SPO2);
    Serial.println("%");

    Serial.print("Heart rate: ");
    Serial.print(MAX30102._sHeartbeatSPO2.Heartbeat);
    Serial.println(" BPM");

    // Construct JSON payload
    String jsonPayload = "{\"heartbeat\":" + String(MAX30102._sHeartbeatSPO2.Heartbeat) +
                         ", \"spo2\":" + String(MAX30102._sHeartbeatSPO2.SPO2) + "}";

    // Close any previous connection
    client.stop();

    // Try to connect to the server
    if (client.connect(server, port)) {
        Serial.println("Connecting to server...");

        // Send HTTP PATCH request
        client.println("PATCH /data/patients/P22XY-G-EMK50D6K HTTP/1.1");
        client.println("Host: " + String(server));
        client.println("User-Agent: ArduinoWiFi/1.1");
        client.println("Content-Type: application/json");
        client.print("Content-Length: ");
        client.println(jsonPayload.length());
        client.println();  // End of headers
        client.println(jsonPayload);  // Send JSON payload

        Serial.println("Data sent: " + jsonPayload);
        lastConnectionTime = millis();
    } else {
        Serial.println("Connection failed");
    }

    delay(4000);
}
