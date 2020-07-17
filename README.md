![.NET Core](https://github.com/jo3bingham/TibiaAPI/workflows/.NET%20Core/badge.svg)

# TibiaAPI
TibiaAPI is an open-source, cross-platform proxy written in C#. You will need to install .NET Core 3.1 to take full advantage of the code in this repository.

This library serves two main purposes:
1. Keeping track of protocol changes.
1. Serving as a means to help and advance Open-Tibia servers (via abilities like map tracking).

TibiaAPI can be used on any platform that supports .NET Standard 2.0. 
That means, by utilizing .NET Core, TibiaAPI can be used on Windows, Linux, and macOS. 
Because of BattlEye, Windows support is limited to official servers not protected by BattlEye (Zuna and Zunera). 
However, currently, TibiaAPI can be used with ANY official server on Linux and macOS. (TibiaAPI is compatible with Open-Tibia servers.) 
Note that because this library is now open-source, and publicly known, it's quite possible that CipSoft/BattlEye will eventually block its use on official BattlEye-protected servers on Linux and macOS. 
I also do NOT recommend using this library with your main account(s) when using it on official servers (always use throwaway accounts) as the risk of banishment/deletion is now possible because of it being open-source.

## Apps
There are, currently, four official, command-line apps included: `Record`, `Redact`, `Extract`, and `Watch`. 
All of these apps target .NET Core 3.1 so that they can be used cross-platform. 
These apps (including TibiaAPI itself) can be built/run using `dotnet` on the command-line, or with Visual Studio on Windows and macOS.

### Record
The `Record` app can be used to record your gaming session. 
The recorded session file includes the client version, both client and server packets, and timestamps for each packet. 
The client version is so that the correct .dat file can be loaded, and the timestamps are, mostly, for the `Watch` app. 
Recordings are saved in the following format (UTC time): Day_Month_Year__Hour_Minute_Second.oxr

Unlike `SharpMapTracker`, the Record app only records your session and doesn't create an OTBM file at the same time (you can use the `Extract app` to do that). 
This has two advantages:
1. You don't get any unncessary latency overhead.
1. You can reuse your recordings if you want/need to get more data without having to log back in to the game.

By default, the `Record` app listens on port 7171 on your localhost, so all you need to do is modify the `loginWebService` of your client to point to `http://127.0.0.1:7171/`, and change the RSA key to the Open-Tibia public key. 
You can override the listening port by passing `-p/--port` as a command-line paramater. 
For example, `-p=1234`.

By default, the `Record` app will attempt to login to CipSoft's official loginWebService. 
You can override this for use with Open-Tibia servers back passing `-l/--login`.
For example, `--login=https://my-ot-server.com/login.php`.

By default, the `Record` app will attempt to locate your Tibia package directory based on the default installation location for your operating system. 
This is needed to get the client version and load the .dat file. 
You can override this by passing `-t/--tibiadirectory`. 
For example. `-t=C:\Tibia\packages\Tibia\`. 
Note that if you override this directory you need to target the package directory, not the main directory. 
The package directory includes the `package.json` file and `assets` directory.

Open-Tibia Public RSA Key:
```
9B646903B45B07AC956568D87353BD7165139DD7940703B03E6DD079399661B4A837AA60561D7CCB9452FA0080594909882AB5BCA58A1A1B35F8B1059B72B1212611C6152AD3DBB3CFBEE7ADC142A75D3D75971509C321C5C24A5BD51FD460F01B4E15BEB0DE1930528A5D3F15C1E3CBF5C401D6777E10ACAAB33DBE8D5B7FF5
```

### Redact
**WARNING** The `Redact` app needs further development to make it completely anonymous. 
For example, player IDs aren't anonymized which means it's possible a CipSoft employee could still identify your character since player IDs are now static. 
It's also possible that I've missed other areas that need to be scrubbed.

The main purpose of the `Redact` app is to anonymize your recording(s) before sharing them. 
It will create a new recording with the same file name as the one you provide with "_redacted" appended to the end. 
Of course, you don't have to redact your recordings before sharing them, but if you don't want others to know your character name you can use this app. 
The `Redact` app will change all player names to "Redacted" in the game window, battlelist, channels, and even NPC messages. 
It also omits various packets that aren't necessary to keep. 
One thing to keep in mind is that redacted recordings may not work properly with the `Watch` app, but should always work with the `Extract` app.

By default, the `Redact` app will remove all client packets. 
However, you can pass `--keepclientpackets` on the command-line to keep client packets in the redacted recording. 
Note that, even with `--keepclientpackets`, some client packets will still be redacted (i.e., the `Login` packet that includes your character name).

Pass `-r/--recording` on the command-line with the recording you want to redact. 
For example, `-r=C:\Recordings\1_2_3__4_5_6.oxr`.

### Extract
The `Extract` app can be used to convert recordings into OTBM files and extract various information such as messages when looking at items. 

Like the `Record` app, the `Extract` app needs to load the .dat file so it will first try locate it by version in the `ClientData` directory, followed by the default directory. 
You can override this with the `-t/--tibiadirectory` parameter.

You can specify a single recording, or a directory of recordings, with the `-r/--recording/--recordings` parameter. 
If specifying a directory of recordings, a new OTBM file will be created for reach recording. 
The format of created OTBM files is (XYZ is login position and date is UTC time): `RecordingFilename__X_Y_Z__Day_Month_Year__Hour_Minute_Second.otbm`

The Extract app uses the `ItemsIgnore.xml` and `ItemsReplace.xml` files in the `Content` directory for ignoring/replacing items when creating OTBM files. 

By default, the `Extract` app outputs created OTBM files to the current directory. 
You can override this by passing `-o/--outdirectory` on the command-line. 
e.g., `-o=C:\ExtractedMaps\`.

If you want to stop extraction after a specific time, you can use the `--time/--timestamp` parameter. 
Recording timestamps are in milliseconds, but the time parameter is in seconds. 
So, for example, if you have a recording from a 20 minute session but you only want to extract from the first 5 minutes you would pass: `--time=300`.

Pass `-h/--help` on the command-line to get helpful information.

Pass `--map` on the command-line to create an OTBM file from the recording.

Pass `--items` to extract item data to a local file named `items.txt`. 
Item data is in the format: `ID Position`. 
e.g., `3031 12345,67890,7`.

Pass `--monsters` to extract monster data to a local file named `monsters.txt`. 
Monster data is in the format: `Name Position`. 
e.g., `Rat 54321,09876,8`. 
Note that monsters are tracked by ID, so it's possible to have more than one creature at/near a position if a monster tracked at that position dies and another spawns.

Pass `--npcs` to extract NPC data to a local file named `npcs.txt`. 
NPC data is in the format: `Name Position`. 
e.g., `Cipfried 23545,87986,3`. 

Pass `--lookitemtext` to extract the message returned when looking at an item to a local file named `lookitemtext.txt`. 
Look item data is in the format: `ID::Position::Text`. 
e.g., `1234::56789,01234,5::You see an object.\nIt weighs 1 oz.`.

### Watch
The `Watch` app can be used to watch recordings back in real-time. 
There are currently no controls to pause/play, rewind, fast-forward, etc. 
Once the `Watch` app is running, all you need to do is launch a client that has its `loginWebService` pointing at the same port that the `Watch` app is listening on, and has the RSA key overwritten with the Open-Tiba one. 
You can use any account/email and password to connect and get a character list. 
The character list will contain one entry, your recording. 
Select it and playback will commence.

By default, the `Watch` app (like the `Record` app) listens to login connections from the client on localhost on port 7171. 
You can override this by passing `-p/--port` on the command-line.

Use `-r/--recording` to specify the recording you would like to watch.

## Donate
If you enjoy the project, and like the work that has been put into it, feel free to donate. Donations aren't necessary, but anything is appreciated. Thanks!

Paypal: PayPal.Me/jo3bingham

Revolut: @josephlh4

Bitcoin: 1JWuyfeCV4SJtmDv16d3SadV4Nq8xtCB5v

Ethereum: 0xaD4d3650A89a2786B60F86c2980d772aa412741F

XRP: Address: rw2ciyaNshpHe7bCHo4bRWq6pqqynnWKQg | Tag: 1431977163
