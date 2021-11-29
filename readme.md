<img src="cm.png" align="right" />

# Exploring Climate Change Solutions in Unity
> An Interactive Museum for the En-ROADS Simulator

This project provides an interactive museum to learn about the [En-ROADS Simulator](https://www.climateinteractive.org/tools/en-roads/). The different topics featured in the simulator can be explored in different rooms, where the information is presented in terms of interactive panels. The simulator in terms of a browser window is integrated into the panels. In order to work with the code, you need the embedded browser provided by [zenfulcrum](https://assetstore.unity.com/packages/tools/gui/embedded-browser-55459) project. At the moment, the projects relies on this Unity plugin to integrate the En-ROADS simulator into the scene. Both projects were developed on Windows systems.

Binaries can be obtained from
> **TODO**

## Desktop Version

This version relies on mouse and keyboard for navigation. The project uses a mixed input setup, the embedded browser relies on Unity's classic input manager, while locomotion is realized with the [input system 1.0](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/index.html). Keep in mind to obtain the embedded browser plugin if you want to develop with this code. Technically, the project should work on most platforms, however, when using MacOS, you might have to re-configure gatekeeper to accept unsigned code (the binaries of the embedded browser).
The section [Documentation of the Desktop Version][1] provides an overview and explanations of the scripts used in the Desktop Version.

[1] ...

## VR Version

The VR version relies on SteamVR. In order to work with the code, you need SteamVR as well as the embedded browser. Locomotion is realized by means of the teleportation functionality provided by the SteamVR [interaction system]{https://valvesoftware.github.io/steamvr_unity_plugin/articles/Interaction-System.html}. Again, the project should work on most platforms, as long as SteamVR and the embedded browser work.

## Bugs and Issues

This is not a complete list...

* only English at the moment, the option to change language in the browser panels has been disabled, as it interferes with our custom DOM changes
* in VR the scroll bars on some of the 'Key Dynamics' panels will not work (the respective mouse events are no emulated)

## Documentation of the Desktop Version

### Contents:

0. General information about functions in Unity® scripts

1. Scripts managing the panels (in Assets - Scripts - PanelScripts)
  1.1 HideOnClick
  1.2 PanelRenderer
  1.3 OnEnableScript
  1.4 CorrelationOnEnable
  1.5 CorrectContentPosition
  1.6 SimulationRenderer
  1.7 LoadingCircle
  1.8 DestroyScriptsAndResetPanel (only for debugging)

2. Scripts managing the player (in Assets - Scripts - PlayerScripts)
  2.1 UserMovement
  2.2 Teleport
  2.3 TeleportCheck
  2.4 MovePlayerToOptimalPosition

3. Scripts managing the interactive table (in Assets - Scripts - TableScripts)
  3.1 ShowPathAndFrame
  3.2 BezierVisualizer
  3.3 BezierCurve
  3.4 ActivateCorrelationFrame
  3.5 CorrelationDictionary

4. Scripts managing the extraction of texts from the simulator website and
the import of images, materials and textures (in Assets - Editor)
  4.1 ObjectForJson
  4.2 EditorWebRequest
  4.3 MaterialProcessorWorking

5. Other scripts (in Assets - Scripts)
  5.1 QuitMuseum

### 0. General information about functions in Unity® scripts
### 1. Scripts managing the panels (in Assets - Scripts - PanelScripts)
### 2. Scripts managing the player (in Assets - Scripts - PlayerScripts)
### 3. Scripts managing the interactive table (in Assets - Scripts - TableScripts)
### 4. Scripts managing the extraction of texts from the simulator website and the import of images, materials and textures (in Assets - Editor)
### 5. Other scripts (in Assets - Scripts)

## License

[![CC0](https://licensebuttons.net/p/zero/1.0/88x31.png)](https://creativecommons.org/publicdomain/zero/1.0/)
