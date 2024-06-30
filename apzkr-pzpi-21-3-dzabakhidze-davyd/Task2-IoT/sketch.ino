#include <Adafruit_MPU6050.h>
#include <Adafruit_Sensor.h>
#include <Wire.h>
#include <Adafruit_SSD1306.h>
#include <Adafruit_GFX.h>
#include <WiFi.h>
#include <HTTPClient.h>
#include <WebServer.h>
#include <uri/UriBraces.h>
#include <ArduinoJson.h>


#define SCREEN_WIDTH 128
#define SCREEN_HEIGHT 64
#define OLED_RESET 4
#define BUTTON_PIN 13
Adafruit_SSD1306 display(SCREEN_WIDTH, SCREEN_HEIGHT, &Wire);
Adafruit_MPU6050 mpu;
const char WIFI_SSID[] = "Wokwi-GUEST";
const char WIFI_PASSWORD[] = "";    


WebServer server(80);
boolean fall = false;
String patientId = "";

void addPatientId() {
  Serial.println("Add Patient request received");
  
  if (server.hasArg("plain")) {
    String jsonString = server.arg("plain");

    DynamicJsonDocument doc(200); 
    DeserializationError error = deserializeJson(doc, jsonString);

    if (error) {
      Serial.print("Failed to parse JSON: ");
      Serial.println(error.c_str());
      server.send(400, "text/plain", "Error parsing JSON");
      return;
    }

    if (doc.containsKey("id")) {
      patientId = doc["id"].as<String>();

      Serial.println("[AddPatient] Пацієнт був під'єднаний до браслету");
      server.send(200, "text/plain", "Patient ID added: " + patientId);
    } else {
      Serial.println("[AddPatient] ID пацієнт не було вказано");
      server.send(400, "text/plain", "ID пацієнт не було вказано");
    }
  } else {
    Serial.println("[AddPatient] ID пацієнт не було вказано");
    server.send(400, "text/plain", "ID пацієнт не було вказано");
  }
}

void checkStatus() {
 if(patientId == ""){
  Serial.println("[AddPatient] ID пацієнт не було вказано");
   server.send(400, "text/plain", "ID пацієнт не було вказано");
 }

 // Create a JSON object
  StaticJsonDocument<200> doc;

  // Populate the JSON object
  JsonObject root = doc.to<JsonObject>();
   Serial.println(patientId);
  root["PatientId"] = patientId;
  root["Location"] = "Your_Location_Description";
  root["Status"] = 0;
  root["Type"] = fall ? 1 : 9;

  // Convert the JSON object to a JSON string
  String responseJson;
  serializeJson(doc, responseJson);

  server.send(200, "application/json", responseJson);
}


void setup() {
  Serial.begin(115200);

  // Connect to Wi-Fi
  Serial.println("Connecting to WiFi...");
  WiFi.begin(WIFI_SSID, WIFI_PASSWORD);

  unsigned long startTime = millis(); // Record the start time

  while (WiFi.status() != WL_CONNECTED && millis() - startTime < 5000) {
    delay(1000);
    Serial.println("Still trying to connect...");
  }

  Serial.println("Connected to WiFi");

  pinMode(BUTTON_PIN, INPUT_PULLUP);

  if (!display.begin(SSD1306_SWITCHCAPVCC, 0x3C)) {
    Serial.println(F("SSD1306 allocation failed"));
    for (;;)
      ;
  }

  display.clearDisplay();
  display.setTextSize(2);
  display.setTextColor(WHITE, 0);
  display.setCursor(0, 0);
  display.println("Accelerometer:");
  display.display();

  if (!mpu.begin()) {
    Serial.println("Failed to find MPU6050 chip");
    while (1) {
      delay(10);
    }
  }

  mpu.setAccelerometerRange(MPU6050_RANGE_8_G);
  mpu.setGyroRange(MPU6050_RANGE_250_DEG);
  mpu.setFilterBandwidth(MPU6050_BAND_21_HZ);

  ledcSetup(15, 5000, 8);
  ledcAttachPin(15, 15);

  server.on("/add-patient", HTTP_POST, addPatientId);
  server.on("/check-status", HTTP_GET, checkStatus);

  server.begin();

  delay(1000);
}

void loop() {
  sensors_event_t a, g, temp;
  mpu.getEvent(&a, &g, &temp);

  if (digitalRead(BUTTON_PIN) == LOW) {
    fall = false;
    display.clearDisplay();
    display.setTextSize(1);
    display.setTextColor(WHITE, 0);
    display.setCursor(0, 0);
    display.println("Resetting...");
    display.display();
    noTone(15);
    delay(1000);
  } else {
    display.clearDisplay();
    display.setTextSize(1);
    display.setTextColor(WHITE, 0);
    display.setCursor(0, 16);
    display.print("X: ");
    display.print(a.acceleration.x);
    display.setCursor(0, 32);
    display.print("Y: ");
    display.print(a.acceleration.y);
    display.setCursor(0, 48);
    display.print("Z: ");
    display.print(a.acceleration.z);
    display.setCursor(64, 16);
    display.print("X°: ");
    display.print(g.gyro.x);
    display.setCursor(64, 32);
    display.print("Y°: ");
    display.print(g.gyro.y);
    display.setCursor(64, 48);
    display.print("Z°: ");
    display.print(g.gyro.z);

    if (!fall) {
      display.display();
    } else {
      display.clearDisplay();
      display.setTextSize(2);
      display.setTextColor(WHITE, 0);
      display.setCursor(0, 20);
      display.println("FALL DETECTED");
      tone(15, 262, 100);
      display.display();
    }

    if (a.acceleration.z < 0.6 && g.gyro.z < 45) {
      fall = true;
    }
  }

  server.handleClient();
  delay(2);
}