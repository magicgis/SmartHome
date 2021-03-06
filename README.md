# SmartHome

The SmartHome project consists of multiple projects/platforms that form an intuitive and extendable system.
SmartHome tries to take different szenarios in daily life and packages them in one clean way of communication.
Besides custom systems like a clock or music streaming this project is also capable of communicating with a wide range of smart IoT devices like the Phillips Hue series of smart lights.

## Server

To bring everything together a permanently running server manages communication of all devices, saving of all data and directing all clients in the network. 
Through a lightweight API it is possible to write plugins in a very intuitive way to add more and more functionality.
The server exposes a network interface based on sockets which enabled platform independent communication to smart client applications running on PCs, mobile phones or tiny devices like Raspberry PI and Arduino.
If you need a guide how to set up the server and how plugins are written take a look at the wiki :)

## Embedded System

The Embedded System is a Python based Framework meant to run on a Raspberry PI connected to a touchscreen.
This system provides functionality of writing apps, so you can create and highly personalize touch wallpanels with ease
