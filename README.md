XtremeWorlds Game Engine
=================

Simple 2D MMORPG Game Engine written in C#!
Based on the Orion+ conversion and MirageBasic.

What is it?
===========
This is a tile-based 2D MMORPG game engine. It features a client and server application setup with a basic GUI. It has live editing built-in, with team-based features to allow fluid updating.

Setup
===========
Install PostgreSQL and use the password for the database as mirage. You can change this in the source code.
https://www.postgresql.org/

Build
===========
If you don't pull the repo with a proper Git client, you need to init the submodules by running the following command in the terminal.  
``git submodule update --init``

Game Features:
==============
Basic Character Creation/Class Selection
Movement/Attacking
NPC/Computer Characters for attacking
Items & Spells
Event System

Creation Features:
==================
The client has editors for the world (maps), items, spells, animations, NPCs, and more from the in-game admin panel.

How do I use this software?
===========================
If you are a programmer then you will probably prefer to compile the most recent version from source. Download the engine here, open up the solution in Visual Studio compile both projects, and start the client and server application. They should connect automatically. IP and Port options are stored in the root/data (files)/config.ini files.

How do I access the editors?
============================
Log into the game with the client. On the server, type the command /access name 5 to promote yourself to owner. Now, go back to the client and tap Insert for each of the editor options.

Support & Updates:
==================
Visit our website or go to our DIscord for support.
https://discord.gg/ARYaWbN6b2

I'm working on updating it to a more usable base.
