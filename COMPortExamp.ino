void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
}

void loop() {
  // put your main code here, to run repeatedly:
  String recivedData =  Serial.readString();

  //Serial.println(recivedData);
  
  if (recivedData == "data\n") {
    Serial.println(random(999, 2999));
  }

  recivedData = "";

  delay(100);
  
}
