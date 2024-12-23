# Placeo VR
 Placeo VR is a system for creating modular simulations in clinical settings using Unity.

<p xmlns:cc="http://creativecommons.org/ns#" xmlns:dct="http://purl.org/dc/terms/"><a property="dct:title" rel="cc:attributionURL" href="https://github.com/Shape94/Placeo-VR">Placeo VR</a> by <a rel="cc:attributionURL dct:creator" property="cc:attributionName" href="https://github.com/Shape94">Giuseppe Forma</a> is licensed under <a href="https://creativecommons.org/licenses/by-nc/4.0/?ref=chooser-v1" target="_blank" rel="license noopener noreferrer" style="display:inline-block;">CC BY-NC 4.0<img style="height:22px!important;margin-left:3px;vertical-align:text-bottom;" src="https://mirrors.creativecommons.org/presskit/icons/cc.svg?ref=chooser-v1" alt=""><img style="height:22px!important;margin-left:3px;vertical-align:text-bottom;" src="https://mirrors.creativecommons.org/presskit/icons/by.svg?ref=chooser-v1" alt=""><img style="height:22px!important;margin-left:3px;vertical-align:text-bottom;" src="https://mirrors.creativecommons.org/presskit/icons/nc.svg?ref=chooser-v1" alt=""></a></p>

This repository contains the Unity project files for Placeo VR, a system I developed for my master's thesis in Cinema and Media Engineering at Politecnico di Torino, in collaboration with the Department of Neuroscience (Università di Torino).

<p align="center">
  <img src="https://github.com/user-attachments/assets/550eacef-b2ba-45b8-b3fa-1b4114ed3502">
</p>

Placeo VR consists of two complementary applications: one for the desktop platform and one for the Meta Quest 3 virtual reality headset.

The desktop application allows for the creation of modular simulations, while the Meta Quest 3 application enables the visualization of the created simulations.

<p align="center">
  <video src="https://github.com/user-attachments/assets/2dc2f358-a7e3-4ea8-9288-56438b5855e8">
</p>

Placeo VR uses the PlayFab service to connect the two applications via the cloud and leverages Microsoft Azure's artificial intelligence for the speech synthesis of some NPCs present in the simulations.

<p align="center">
  <video src="https://github.com/user-attachments/assets/b3884361-16db-48be-a1d7-9a752e42d7b7">
</p>

## Setup
- Create a Microsoft Azure PlayFab account and install the plugin for Unity, as specified in this video: [Easy multiplayer in Unity Setup - How to use PlayFab in Unity tutorial (#1)](https://www.youtube.com/watch?v=DQWYMfZyMNU)

- Create a Microsoft Azure account, add a Resource Group, and enable MySpeechServices, keeping in mind the 'SubscriptionKey' and 'Region'.

- Download the repository and open it with Unity.

- Log in with your PlayFab credentials.

<p align="center">
  <img src="https://github.com/user-attachments/assets/dd6e2f66-5c4b-4b23-80d8-690fbb863226">
</p>


- Open the SimulationTTS.cs script and enter the SubscriptionKey and Region of MySpeechServices in the appropriate spaces. Then, open the Tutorial TTS script and do the same.

<p align="center">
  <img src="https://github.com/user-attachments/assets/eeb556b0-6c59-4745-a0ce-c4946d4a3947">
</p>


The external services should now be connected and ready for use.

## Build for desktop platform:

- Ensure that the '2D' button is activated for proper visualization.

<p align="center">
  <img src="https://github.com/user-attachments/assets/37a4cfc9-0780-4377-93f2-1084d3e52a8d">
</p>


- Change the development platform to 'Windows, Mac, Linux' and click on 'Switch Platforms'.

<p align="center">
  <img src="https://github.com/user-attachments/assets/134dce2b-86ab-42f6-9397-33e332d37b7e">
</p>


- In Player Settings, ensure that 'Active Input Handling' is set to 'Both’.

<p align="center">
  <img src="https://github.com/user-attachments/assets/1d65a769-a47b-4ed1-ba3d-e2dd8f5f0e6a">
</p>


- To build the desktop application, ensure that only the 'Placeo VR – PC' scene is selected in 'Scenes in Build'.

<p align="center">
  <img src="https://github.com/user-attachments/assets/934550a0-b00d-43c7-8351-20aab44e089c">
</p>


## Build for Meta Quest 3 headset:

- Ensure that the '2D' button is deactivated for proper visualization.

<p align="center">
  <img src="https://github.com/user-attachments/assets/37a4cfc9-0780-4377-93f2-1084d3e52a8d">
</p>


- Change the development platform to 'Android' and click on 'Switch Platforms'.

<p align="center">
  <img src="https://github.com/user-attachments/assets/134dce2b-86ab-42f6-9397-33e332d37b7e">
</p>


- In Player Settings, ensure that 'Active Input Handling' is set to 'Input System Package (New)'.

<p align="center">
  <img src="https://github.com/user-attachments/assets/1d65a769-a47b-4ed1-ba3d-e2dd8f5f0e6a">
</p>


- To build the application for Meta Quest 3, ensure that only the scenes in 'Scenes in Build' are selected in this order:<br/>
0 Placeo VR - Oculus - Intro<br/>
1 Placeo VR - Oculus<br/>
2 Placeo VR - Oculus - Sala attesa<br/>
3 Placeo VR - Oculus - Rovinato*<br/>
4 Placeo VR - Oculus - Sala attesa - Rovinato*<br/>

<p align="center">
  <img src="https://github.com/user-attachments/assets/fb850151-3254-4ac2-b054-bd220ca15b23">
</p>


Scenes marked with * should be activated if you intend to use only the simulations in the 'Examples' folder (custom lighting created).
If you want to create and use your own simulations, exclude the scenes with * and make sure to remove the 'LightMap assets' and 'Lightmaps' to avoid issues with the textures of the 3D models.
If you have included the scenes with * in the build, you can open the desktop application and in the ‘Simulazioni’ section use the 'Carica Simulazione’ button to load the .json files of the simulations present in the 'Examples' folder.

<p align="center">
  <img src="https://github.com/user-attachments/assets/46bddba1-2844-4178-b2dc-12fb134cad63">
</p>

## Create a Placeo VR account and enjoy
You can now use the desktop application to manage and start simulations, and the application loaded on the Meta Quest 3 headset to immerse yourself in the virtual reality simulation.
Remember to create a new Placeo VR account through the desktop application and log in with the same account from the Meta Quest 3 application to send and receive files between the two platforms.

<p align="center">
  <img src="https://github.com/user-attachments/assets/4c2b8db3-af81-4d2b-8cef-e4d41d5ea232">
</p>


In the 'Placeo VR - Oculus' scene (which is the main scene), there is a custom script created 
specifically for the experiments in my thesis.

<p align="center">
  <img src="https://github.com/user-attachments/assets/ebc503d4-dac2-42fb-ad99-53bf9690b1c4">
</p>


This script serves as the foundation for creating new scripts, correlating events, dialogues, animations, etc. Take a look at it if you intend to vary the context of the simulation.

## Extra – Adding/Modifying/Removing Parameters from the Simulation (Developers Only)
Placeo VR is ready for use, but if you need to modify the Simulation parameters, it is possible to do so. Currently, when you create a new Simulation, the parameters in the ‘Medico’ and ‘Comunicazione’ sections have no effect (they have been included in anticipation of future updates).

<p align="center">
  <img src="https://github.com/user-attachments/assets/c71e40ae-b5e1-4459-859d-2cef68f4c786">
</p>


If you want to modify the parameters displayed when creating a new Simulation, you can open the configParams.json file in the Resources folder and add, remove, or modify the parameters as you wish.
For example, in the macro category ‘Ambiente – Stanza del Medico', you can find the 
parameter to manage plants.

<p align="center">
  <img src="https://github.com/user-attachments/assets/13cc9e97-95d6-49f6-b077-1a10a82ae209">
</p>


In the .json file:
•	plant is the key.
•	name and descriptions are useful for helping users understand what the parameter does.
•	containerType identifies the type of container in which the parameter is contained, such as a normal container, an expandable one, …
•	type identifies the type of interactable (dropdown, toggle, checkbox, slider).
•	options identify the available options in the interactable (if available, for example, a toggle will not have any options).
Before modifying the configParams.json file, I recommend reviewing its structure, keeping the Placeo VR desktop application nearby to understand how the structure in the .json file is connected to the parameters displayed in the screen for creating a new Simulation.

<p align="center">
  <img src="https://github.com/user-attachments/assets/3a531b8b-5cb0-4922-9ad9-8257d433a2b3">
</p>


Anyway, once the configParams.json file has been modified, the changes can be updated in the Unity editor. Simply select the gameobject that contains the screen you want to modify, find the 'Element Manager' script, select the correct Macro Category, and click on ‘Aggiorna Parametri da JSON'.

<p align="center">
  <img src="https://github.com/user-attachments/assets/a07ea1b0-60b4-4dce-83ad-6c237ac28799">
</p>


In the example of plants, this is how the parameter described in the configParams.json file will 
be displayed in the editor:

<p align="center">
  <img src="https://github.com/user-attachments/assets/073ea79a-a541-4751-8608-74dcc48454ac">
</p>


Through this screen, it will be possible to insert an identifying image for the parameter.
Once a new parameter has been added, you will need to modify the SaveParametersJSON.cs file, adding the new parameter in the Parameters class. It is recommended to keep the same key for the parameter in the configParams.json file and the variable name in the SaveParametersJSON.cs file (for example, plant is both the key of the parameter in configParams.json and the variable name in SaveParametersJSON.cs).
Use an appropriate variable type (e.g., bool for toggle, checkbox; int for dropdown; float for slider)

<p align="center">
  <img src="https://github.com/user-attachments/assets/952d3e62-0a51-47dd-9cfd-dd42dca140f0">
</p>


Finally, you can modify the ReadNewSimulationJSONOculus.cs file to affect the simulation on the Meta Quest 3 headset based on the parameter values. For example, to manage the insertion and type of plants, simply check the value of parameters.plant, and then write the code to handle any new parameters.

<p align="center">
  <img src="https://github.com/user-attachments/assets/b2290e59-7073-487e-b660-3e6ec62f9d0d">
</p>


## Credits
- Music<br/>
by Nicko Gloire, Mykola Sosin, cybercutie, Harald, Yevhenii Kovalenko from Pixabay.<br/>
Eda to Gbemi re X - SuGar

- Plugin:<br/>
VR Keyboard - XRI Poke & Hands support - Black Whale Studio<br/>
[BFW]Simple Dynamic Clouds - Butterfly World<br/>
Magic effects pack - Hovl Studio<br/>
Living birds - Dinopunch<br/>
EasyCsv - RedScarf<br/>
StandaloneFileBrowser<br/>
uDialog - Digital Legacy Games<br/>
TabsUI - Hamza Herbou<br/>

- Avatar:<br/>
Microsoft Rocketbox

- 3D Models:<br/>
TurboSquid<br/>
CGTrader<br/>
AK Studio Art<br/>
Unity Asset Store<br/>
Dublin Chair - Aliosa<br/>
ArchViz Sofa Pack - Lite<br/>
Apartment kit - Brick Project Studio<br/>
Ghostly Hands - wolderado<br/>
Free Hospital Props- Mixail Team<br/>
MobileTreePacks - Next Spring<br/>
QA Books - QATMO<br/>
vrbn Studios Free Buildings Bundle - VRBN STUDIOS<br/>
Emperor Angelfish - Mikhal Nesterov<br/>

<p xmlns:cc="http://creativecommons.org/ns#" xmlns:dct="http://purl.org/dc/terms/"><a property="dct:title" rel="cc:attributionURL" href="https://github.com/Shape94/Placeo-VR">Placeo VR</a> by <a rel="cc:attributionURL dct:creator" property="cc:attributionName" href="https://github.com/Shape94">Giuseppe Forma</a> is licensed under <a href="https://creativecommons.org/licenses/by-nc/4.0/?ref=chooser-v1" target="_blank" rel="license noopener noreferrer" style="display:inline-block;">CC BY-NC 4.0<img style="height:22px!important;margin-left:3px;vertical-align:text-bottom;" src="https://mirrors.creativecommons.org/presskit/icons/cc.svg?ref=chooser-v1" alt=""><img style="height:22px!important;margin-left:3px;vertical-align:text-bottom;" src="https://mirrors.creativecommons.org/presskit/icons/by.svg?ref=chooser-v1" alt=""><img style="height:22px!important;margin-left:3px;vertical-align:text-bottom;" src="https://mirrors.creativecommons.org/presskit/icons/nc.svg?ref=chooser-v1" alt=""></a></p>
