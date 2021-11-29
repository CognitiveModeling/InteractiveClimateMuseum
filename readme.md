<img src="cm.png" align="right" />

# Exploring Climate Change Solutions in Unity
> An Interactive Museum for the En-ROADS Simulator

This project provides an interactive museum to learn about the [En-ROADS Simulator](https://www.climateinteractive.org/tools/en-roads/). The different topics featured in the simulator can be explored in different rooms, where the information is presented in terms of interactive panels. The simulator in terms of a browser window is integrated into the panels. In order to work with the code, you need the embedded browser provided by [zenfulcrum](https://assetstore.unity.com/packages/tools/gui/embedded-browser-55459) project. At the moment, the projects relies on this Unity plugin to integrate the En-ROADS simulator into the scene. Both projects were developed on Windows systems.

Binaries can be obtained from
> **TODO**

## Desktop Version

This version relies on mouse and keyboard for navigation. The project uses a mixed input setup, the embedded browser relies on Unity's classic input manager, while locomotion is realized with the [input system 1.0](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/index.html). Keep in mind to obtain the embedded browser plugin if you want to develop with this code. Technically, the project should work on most platforms, however, when using MacOS, you might have to re-configure gatekeeper to accept unsigned code (the binaries of the embedded browser).

## VR Version

The VR version relies on SteamVR. In order to work with the code, you need SteamVR as well as the embedded browser. Locomotion is realized by means of the teleportation functionality provided by the SteamVR [interaction system]{https://valvesoftware.github.io/steamvr_unity_plugin/articles/Interaction-System.html}. Again, the project should work on most platforms, as long as SteamVR and the embedded browser work.

## Bugs and Issues

This is not a complete list...

* only English at the moment, the option to change language in the browser panels has been disabled, as it interferes with our custom DOM changes

## License

[![CC0](https://licensebuttons.net/p/zero/1.0/88x31.png)](https://creativecommons.org/publicdomain/zero/1.0/)
