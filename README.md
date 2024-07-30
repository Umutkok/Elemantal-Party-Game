# Elemantal-Party-Game
Overview
This project is an online multiplayer game developed as my final project in the third year of my studies. I was the sole developer, responsible for all aspects of the game including networking, mechanics, and design.

Game Description
The game is designed to be played by four players, each controlling a unique element. Initially, each element was planned to have distinct mechanics, but due to time constraints and insufficient level design, not all features were fully implemented. However, the elements can still interact with each other, and the game supports multiplayer functionality. We managed to create a simple test level for demonstration purposes.

Learning Experience
Through this project, I gained a deeper understanding of Unity's Netcode system and gained valuable experience in developing online multiplayer games. The character selection menu and network implementation were inspired by Dapper Dino's tutorials.

Playing the Game in Multiplayer
The game can be played in multiplayer mode. Here are the steps to set it up:

1. Playing on the Same Wi-Fi Network
If all devices are connected to the same Wi-Fi network, follow these steps:

The host player should start the game and obtain their computer's IPv4 address.
All other players should enter this IPv4 address in the "Address" field in the game menu.
2. Playing on Different Networks
If players are on different networks, there are two options:

Rent a server to host the game.
Use services like Hamachi to simulate a local network.
Alternative Method
If you prefer not to deal with network configurations, you can run multiple builds of the game on the same computer:

Open multiple instances of the game.
Set one instance as the host and the others as clients.
Do not change the "Address" and "Port" fields.
