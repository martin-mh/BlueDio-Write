/* Thought for an Arduino Mega.
 * Need a radio receiver with DATA connected to digital pin 2.
 * See readme for more informations.
 */

#include <VirtualWire.h>

struct Message
{
	String chunk1;
	String chunk2;
	String chunk3;
};

void setup()
{
	Serial.begin(9600);
	Serial.println("Tuto VirtualWire");

	vw_setup(2000);
	vw_set_rx_pin(2);
	vw_rx_start();

	for(int i = 3; i <= 13; ++i)
		pinMode(i, OUTPUT);
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
}

void loop()
{
	uint8_t buflen = VW_MAX_MESSAGE_LEN;
	uint8_t buf[VW_MAX_MESSAGE_LEN];

	if(vw_wait_rx_max(200))
	{
		if(vw_get_message(buf, &buflen))
		{
			String stringReceived;

			for (byte i = 0; i < buflen; ++i)
				stringReceived.concat((char)buf[i]);
			
			Message command = proccessString(stringReceived);
			proccessCommand(command);
			Serial.println(stringReceived);
		}
	}
}