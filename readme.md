# BlueDio Write
This is a really simple project.

First, we have a master arduino card with two modules : bluetooth and radio transmitter.
Secondly, we have a slave arduino card with only one radio receiver.

From bluetooth we send message to the master card. There is the message :

    to:pin:state

Just replace `to` with `master` or `slave`.
Replace `pin` with the digital pin you want to set.
And change state by 1 or 0.

Example :
    
    master:13:0

This will shutdown the digital pin 13 on the master card.

    slave:9:1

This will turn on the digital pin 9 on slave card.

Slave card and master card communicate with radio without securities.
The bluetooth pin is 0012.

Slave card and Master card have not same code so there is two folders. Look at them they are really
easy.