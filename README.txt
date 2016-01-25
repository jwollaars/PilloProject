README

In this document we share important information regarding the use of the Pillo development kit 3.0.2 (Windows, Mac)

// 1. What does the PDK contain?
1. Pillo.dll
2. README.txt
3. Pillo Driver for Windows
4. Driver-install-for-Windows-8.1.txt

//2. Copyright notice
PDK 3.0.2 is created on 28-03-2015 by Salvatore CASTELLANO, Copyright is reserverd to PILLO games.
PILLO is a registered trademark. if you'd like to use PILLO in referral to your game, contact us with the adress below.

//3. Company info
Company: Pillo Games - by Ard Jacobs IxD, Kamperfoelielaan 33, 5643 BB, Eindhoven, The Netherlands
Website: www.pillogames.com
E-mail: info@pillogames.com

//4. Important notice for developers
1. 	First of all you need to put the Api Compatibility Level to .NET 2.0 in the Player Settings located under Edit>Project Settings>Player.
2. 	In order to make the PDK work create a gameobject in the first scene where you want to use the Pillo Game Controllers.
	Give this gameobject the name "PilloController".
3.	The next step is dragging the PilloController script onto the PilloController gameobject.
	This gameobject will only be necessary in the first scene where you use the Pillo Game Controller. 
	You can now access the PilloController functions from your other scripts.
4.	When accessing the PilloController in your script make sure to add the following line to your Start().
	PilloController.ConfigureSensorRange(0x50, 0x6f);
	This will redefine the available sensor range in the Pillo hardware.

Your game is now Pillo ready!
