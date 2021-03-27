#include <Sharer.h>
#include <RotaryEncoder.h>

#define PIN_IN1 15
#define PIN_IN2 16

RotaryEncoder encoder(PIN_IN1, PIN_IN2, RotaryEncoder::LatchMode::FOUR3);

void setup() {
  Sharer.init(115200); // Init Serial communication with 115200 bauds
}

void loop() {
  Sharer.run();
  Sharer.print(readRotatory());
}

int readRotatory(){
  static int pos = 0;
  encoder.tick();
  int newPos = encoder.getPosition();
  
  if (pos != newPos){
      pos = newPos;
      int dir =(int)(encoder.getDirection());
    if ( dir == -1) 
      return 1;
    else if(dir == 1) 
      return -1;
  }
  return 0;
}
