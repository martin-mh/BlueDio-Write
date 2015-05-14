/* Thought for an Arduino Mega. Need bluetooth CH-06 module on Serial1 (19/18).
 * Need a radio transmetter with DATA connected to digital pin 2.
 * See readme for more informations.
 */

#include "VirtualWire.h"

const char NOMBRE[10] = "BlueDio";
const char BPS = '4'; // 4 = 9600 bauds
const char PASS[10] = "0012";

struct Message
{
	String chunk1;
	String chunk2;
	String chunk3;
};

void setup() 
{
	for(int i = 3; i <= 13; ++i)
		pinMode(i, OUTPUT);

	vw_set_tx_pin(2);
	vw_setup(2000);

	// Set up bluetooth et Tx/Rx0
	Serial.begin(9600);
	Serial1.begin(9600);
	Serial1.setTimeout(5000);

	Serial1.print("AT");
	delay(1000);

	Serial1.print("AT+VERSION");
	delay(1000);

	Serial1.print("AT+NAME");
	Serial1.print(NOMBRE);
	delay(1000);

	Serial1.print("AT+BAUD");
	Serial1.print(BPS);
	delay(1000);

	Serial1.print("AT+PIN");
	Serial1.print(PASS);
	delay(1000);

	digitalWrite(13, HIGH);
}

/* If we receive a message in this format : aa:bb:cc -> chunk1 will have aa then chunk2 will have bb
 * and chunk3 will have cc
 */
Message proccessString(String &message)
{
	Message result;

	int firstIndex = message.indexOf(':');
	int lastIndex = message.lastIndexOf(':');

	for(int i = 0; i < firstIndex; ++i)
		result.chunk1.concat(message[i]);

	for(int i = firstIndex + 1; i < lastIndex; ++i)
		result.chunk2.concat(message[i]);

	for(int i = lastIndex + 1; i < message.length(); ++i)
		result.chunk3.concat(message[i]);

	return result;
}

void proccessCommand(Message &command)
{
	if(command.chunk1 == "master")
	{
		digitalWrite(command.chunk2.toInt(), command.chunk3.toInt());
	}

	if(command.chunk1 == "slave")
	{
		String message = "master:";
		message.concat(command.chunk2);
		message.concat(":");
		message.concat(command.chunk3);

		vw_send((uint8_t *)message.c_str(), strlen(message.c_str()));
		vw_wait_tx();
	}
}

void loop()
{
	while (Serial1.available() > 0)
	{
		String message = Serial1.readStringUntil(0x0D0A);
		Message command = proccessString(message);
		proccessCommand(command);
		Serial.println(message);
	}
}