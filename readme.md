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

The Unity® Documentation provides overviews of important classes and their
execution order:
[class MonoBehaviour]{https://docs.unity3d.com/Manual/class-MonoBehaviour.html}
[Execution Order]{https://docs.unity3d.com/Manual/ExecutionOrder.html}
(last access: November/26/2021)

Particularly important are:
- Awake()
  - is always called before any Start functions

- Start()
  - is called before the first frame update

- Update()
  - is called once per frame

### 1. Scripts managing the panels (in Assets - Scripts - PanelScripts)

#### 1.1 Script HideOnClick

##### 1.1.1 Description
This script hides the start screen of a panel if the user clicks on it.
It is assigned to the StartScreen of each panel in the editor.

##### 1.1.2 Attributes
- a public GameObject TabPanel, the corresponding panel with different tabs that
  appears instead of the start screen is assigned to it in the editor

##### 1.1.3 Methods
- OnMouseDown()
  - if user clicks onto the start screen, panel's start screen is deactivated
    and tab panel is activated

################################################################################

#### 1.2 Script PanelRenderer

##### 1.2.1 Description
This script handles the rendering of the panels.
It itself is not assigned to any objects in the editor.

##### 1.2.2 Attributes
- public GameObjects for the start screen
- public GameObjects for the general panel tab "General Information"
- public GameObjects for the general panel tab "Correlations"
- public GameObjects for the parent, helping to get the right header and
  background
- public text file with panel text
- private contentScript, an instance of the helper script OnEnableScript for
  en-/disabling the header background
- private string that holds the child of the panel

##### 1.2.3 Methods
- Start()
  - instantiates start screen, its background image and header with the help
    of contentScript
  - if the general tab "Correlations" is not active at the moment:
    - background image, header and text of general tab "General Info" is
      instantiated for each tab inside "General Info" (Information, Examples,
      Key Dynamics, ...)
    - if there is no text available, it deactivates this specific child tab
    - activates the container of General Info (for script CorrelationOnEnable)
  - if images of correlations exist, instantiate child tabs of "Correlations"
    (differ from panel to panel)
    - for each image (each child tab, respectively):
      - instantiate child tabs of "Correlations" (differ from panel to panel)
      - if it is the first image/child, set its toggle to true (so its content
        is shown in the panel first after clicking on "Correlations"), else
        toggle is false
      - write name of child into its label (tab heading)
      - define size of the child tab's toggle (s. SizeOfToggle() below)
      - write content from text file (TMPro.TextMeshProUGUI) into Content part
        of child tab (s. GetTextFromFile(string) below)
  - activate the container of Correlations (for script CorrelationOnEnable)
  - instantiate correlation background and header for each child tab, depending
    on the given panel name (s. InstantiateHeaderBackground(image, header)
    below)

- InstantiateHeaderBackground(GameObject img, GameObject header)
  - instantiates the background of a header and its image
  - deactivates header and image

- GetTextFromFile(string name)
  - returns the panel text depending on the given panel name
  - saves panel text as new string and makes it better readable

- SizeOfToggle(string name)
  - returns a specific size of a toggle depending on the given panel name

################################################################################

#### 1.3 Script OnEnableScript

##### 1.3.1 Description
This helper script manages the en- and disabling of header and image in a panel
tab. Thus, it is assigned to every tab (Key Dynamics, Potential Co-Benefits,
...) and Content object in a panel in the editor.

##### 1.3.2 Attributes
- public GameObjects for an image and a header, assigned in the editor 

##### 1.3.3 Methods
- OnEnable()
  - activates image and header
- OnDisable()
  - deactivates image and header

################################################################################

#### 1.4 Script CorrelationOnEnable

##### 1.4.1 Description
This script sets each child/sub tab's toggle to the correct (clickable) position.
It itself is not assigned in the editor.

##### 1.4.2 Attributes
- public boolean initialized that indicates if positioning has already
  happened, initially false
- private float currentToggleOffset, difference of a tab's toggle to its
  neighbouring toggle, initially 0    

##### 1.4.3 Methods
- OnEnable()
  - if initialization has not happened yet:
    - sets initialization boolean to true
    - activates the initialization coroutine (below)
- IEnumerator initializationCoroutine()
  - waits for 1 frame
  - for each child tab, set content to correct position:
    - if it is not the first (left-most) child tab: calculate and apply toggle
      offset of correlation tabs
      - calculate toggle offset for the current child tab dependent on the one
        before (left of it)
      - save it in an rectangle with anchored position (x-coordinates with the
        calculated toggle offset)
  - deactivate general tab "Correlations" (activation happens in Start() of
    script PanelRenderer)

################################################################################

#### 1.5 Script CorrectContentPosition

##### 1.5.1 Description
This script sets the content of each child/sub tab (tabs inside "General Info"
or "Correlation") to the correct (readable) position in the panels.
It itself is not assigned to any objects in the editor.

##### 1.5.2 Attributes
- public boolean initialized that indicates if positioning has already
  happened, initially false

##### 1.5.3 Methods
- OnEnable()
  - if initialization has not happened yet:
    - sets initialization boolean to true
    - activates the initialization coroutine (below)
- IEnumerator initializationCoroutine()
  - waits for 1 frame
  - for each child tab, set content to correct position:
    - every content gets same global position (the position of the content of
      the first element of General Information)

################################################################################

#### 1.6 Script SimulationRenderer

##### 1.6.1 Description
This class renders the simulator from the En-ROADS website in the panels.
Therefore, in the editor it is assigned to every panel (in the tab "Key
Dynamics", except for the full-version simulator).

##### 1.6.2 Attributes
- a private variable browser, an instance of the class Browser
- a public simulator type (enumeration is assigned in the editor)
- a public enumeration type holding all simulator types (full or
  downscaled versions

##### 1.6.3 Methods
- Awake()
  - initializes browser
  
- Start()
  - loads En-ROADS simulator into Browser instance browser
  - defines a javascript function (JS) to deactivate listeners on browser
  - defines a JS to set browser's opacity to 0.3

- Update()
  - calls RenderingCoroutine() if the website has loaded

- RenderingCoroutine()
  - adjusts the top toolbar for every simulator instance (in every panel) with
    auxiliary method adjustTopToolbar()
  - if the full version of the simulator is rendered, design and interactions
    are not changed
  - if the downscaled version of the simulator is rendered, design and inter-
    actions are changed (specified in switch-case by simulator type), i.e.
    nonrelevant sliders are deactivated and grayed out with the two JS functions 
    defined in Start()

- auxiliary methods:
	string getFirstByClassName (string className)
		- takes an HTML class name className
		- returns the first element of this class
	string hideElement(string elem)
		- takes an HTML element name elem
		- hides this element
	string addPadding(string elem, string pos, int px)
		- takes an HTML element name elem, a position pos and a size px.
		- adds a padding of size px to the HTML element elem at defined 
                  position pos (top, bottom, left, right) 
	string addPadding(string elem, int top, int right, int bottom, int left)
		- takes an HTML element name elem and the integers top, right,
                  bottom and left
		- adds each a padding of defined sizes top, right, bottom and
                  left to the respective position at elem
	void adjustTopToolbar():
		- hides initial welcome screen, logo in the left upper corner,
                  button "share your scenario" in top right corner, help tab,
                  full-screen button and help button in toolbar at the top as
                  well as logos in right lower corner
		- adds padding at the bottom of the simulator

##### 1.6.4 Open issues
- Tab "Language" in the top toolbar (currently hidden)
    This tab allows switching between languages. However, if the language is
    changed, none of the sliders can be used anymore. In the
    RenderingCoroutine()'s switch-case, also the relevant sliders' listeners are
    deactivated incorrectly by the JS function deactivateListeners(elem)
    defined in Start(). This happens because the sliders' names have changed
    (they are not in English anymore) which are requested in the if-statement
    of each case for deactivating and graying out the nonrelevant sliders.
    Because the JS function destroys the listeners irrecoverably, all of the
    sliders can not be used in the following, not even when changing the
    language back to English again.
    This could be fixed with rewriting the JS function
    deactivateListeners(elem) in such a way that the listeners will not be
    destroyed irrecoverably.

################################################################################

#### 1.7 Script Loading Circle

##### 1.7.1 Description
This script manages the rotation of the loading circle while the simulator
website is loaded in the simulator panel. It is assigned to the Progress object
of the Loading Circle in the Simulator panel.

##### 1.7.2 Attributes
- a private RectTransform rectComponent, the Transform of a rectangle
- a private float rotateSpeed, the speed of rotation for the loading circle

##### 1.7.3 Methods
- Start()
  - initializes the rectangle Transform
- Update()
  - rotates the rectangle with the defined speed and the time from the last to
    the current frame

################################################################################

#### 1.8 Script DestroyScriptsAndResetPanel (only for debugging)

##### 1.8.1 Description
This script destroys all scripts that are used to initialize the panels, if the
user presses the Delete-key. It is not assigned to any object in the editor
because it was only used for debugging purposes during the implementation.

##### 1.8.2 Methods
- Update()
  - if user presses the Delete-key:
    - destroys all scripts used to correctly initialize panel: PanelRenderer,
      each use of CorrectContentPosition
    - resets start screen and tab panel
    - resets toggles such that only first is active:
      - for general/chapter tabs: only General Information toggle is on
      - for sub/child tabs: only first active tab toggle (left-most) is on
    - destroys itself (script DestroyScriptsAndResetPanel) at last

### 2. Scripts managing the player (in Assets - Scripts - PlayerScripts)
### 3. Scripts managing the interactive table (in Assets - Scripts - TableScripts)
### 4. Scripts managing the extraction of texts from the simulator website and the import of images, materials and textures (in Assets - Editor)
### 5. Other scripts (in Assets - Scripts)

## License

[![CC0](https://licensebuttons.net/p/zero/1.0/88x31.png)](https://creativecommons.org/publicdomain/zero/1.0/)
