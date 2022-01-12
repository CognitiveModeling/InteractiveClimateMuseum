<img src="cm.png" align="right" />

# Exploring Climate Change Solutions in Unity
> An Interactive Museum for the En-ROADS Simulator

This project provides an interactive museum to learn about the [En-ROADS Simulator](https://www.climateinteractive.org/tools/en-roads/). The different topics featured in the simulator can be explored in different rooms, where the information is presented in terms of interactive panels. The simulator in terms of a browser window is integrated into the panels. In order to work with the code, you need the embedded browser provided by [zenfulcrum](https://assetstore.unity.com/packages/tools/gui/embedded-browser-55459) project. At the moment, the projects relies on this Unity plugin to integrate the En-ROADS simulator into the scene. Both projects were developed on Windows systems.

We host the binaries in the microsoft cloud of the university of Tübingen. They come in zip archives, just download and extract them and run the executable file (ClimateMuseum.exe).
> [Desktop Version](https://unitc-my.sharepoint.com/:u:/g/personal/iivbu01_cloud_uni-tuebingen_de/ERHgrVf_9uBAijgg83No9KMBMCksn7Mvwc6vV8jOGcAWTg?e=l46cfs)

> [VR Version](https://unitc-my.sharepoint.com/:u:/g/personal/iivbu01_cloud_uni-tuebingen_de/EVgmqDklko1BlpGGXMttOUYBONEjttdPXgw5wW4IDNziCg?e=hshSWc)

## Desktop Version

This version relies on mouse and keyboard for navigation. The project uses a mixed input setup, the embedded browser relies on Unity's classic input manager, while locomotion is realized with the [input system 1.0](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/index.html). Keep in mind to obtain the embedded browser plugin if you want to develop with this code. Technically, the project should work on most platforms, however, when using MacOS, you might have to re-configure gatekeeper to accept unsigned code (the binaries of the embedded browser).
The section [Additional scripts for the VR version](#VRScripts) in the documentation provides an overview and explanations of the scripts used in the VR Version.

## VR Version

The VR version relies on SteamVR. In order to work with the code, you need SteamVR as well as the embedded browser. Locomotion is realized by means of the teleportation functionality provided by the SteamVR [interaction system]{https://valvesoftware.github.io/steamvr_unity_plugin/articles/Interaction-System.html}. Again, the project should work on most platforms, as long as SteamVR and the embedded browser work. The section [Documentation](https://github.com/CognitiveModeling/InteractiveClimateMuseum#documentation) provides an overview and explanations of the scripts used in the Desktop Version.

## Bugs and Issues

This is not a complete list...

* only English at the moment, the option to change language in the browser panels has been disabled, as it interferes with our custom DOM changes
* in VR the scroll bars on some of the 'Key Dynamics' panels will not work (the respective mouse events are no emulated)

## Documentation

### Contents:

0. [General information about functions in Unity® scripts](#general)

1. [Scripts managing the panels (in Assets - Scripts - PanelScripts)](#PanelScripts)

	1\.1 HideOnClick

	1\.2 PanelRenderer
	
	1\.3 OnEnableScript
	
	1\.4 CorrelationOnEnable
	
	1\.5 CorrectContentPosition
	
	1\.6 SimulationRenderer
	
	1\.7 URLLanguageCheck
	
	1\.8 LoadingCircle
	
	1\.9 DestroyScriptsAndResetPanel (only for debugging)

2. [Scripts managing the player (in Assets - Scripts - PlayerScripts)](#PlayerScripts)
	
	2\.1 UserMovement
	
	2\.2 Teleport
	
	2\.3 TeleportCheck
	
	2\.4 MovePlayerToOptimalPosition

3. [Scripts managing the interactive table (in Assets - Scripts - TableScripts)](#TableScripts)
	
	3\.1 ShowPathAndFrame
	
	3\.2 BezierVisualizer
	
	3\.3 BezierCurve
	
	3\.4 ActivateCorrelationFrame
	
	3\.5 CorrelationDictionary

4. [Scripts managing the extraction of texts from the simulator website and the import of images, materials and textures (in Assets - Editor)](#EditorScripts)
	
	4\.1 ObjectForJson
	
	4\.2 EditorWebRequest
	
	4\.3 MaterialProcessorWorking

5. [Additional scripts for the VR version](#VRScripts)
	
	5\.1 [Additional scripts managing the panels](#VRPanelScripts)
		
	5.1.1 ToggleVRSupport

	5.1.2 ToggleVRSupportHelper

	5.1.3 ScrollbarVRSupport

	5.1.4 CameraRayCast
	
	5\.2 [Important scripts for the interaction between VR and browser](#VRBrowserScripts)

	5.2.1 HandleLoadingScreen

	5.2.2 VRControllerInputProxy

	5.2.3 VRBrowserHand

6. [Other scripts (in Assets - Scripts)](#OtherScripts)
	
	6\.1 QuitMuseum

################################################################################
################################################################################

### 0. General information about functions in Unity® scripts <a name="general"></a>

The Unity® Documentation provides overviews of important classes and their
execution order:

[class MonoBehaviour](https://docs.unity3d.com/Manual/class-MonoBehaviour.html) (last access: November/26/2021)

[Execution Order](https://docs.unity3d.com/Manual/ExecutionOrder.html) (last access: November/26/2021)

Particularly important are:
- Awake()
  - is always called before any Start functions

- Start()
  - is called before the first frame update

- Update()
  - is called once per frame

################################################################################
################################################################################

### 1. Scripts managing the panels (in Assets - Scripts - PanelScripts) <a name="PanelScripts"></a>

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
- 15.12.21: Tab "Language" can be used now. However, sliders that are deactivated
	    when the simulator is initially rendered do not change their name to the 
	    chosen language, but are still displayed in English.
- 11/21: Tab "Language" in the top toolbar (currently hidden)
	This tab allows switching between languages. However, if the language is
	changed in the tabs "Key Dynamics", none of the sliders can be used anymore. In the
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

#### 1.7 URLLanguageCheck

##### 1.7.1 Description
This script checks if the language was changed and reloads the simulator page in the panel.

##### 1.7.2 Attributes
- a public browser, assigned in the editor
- a string holding its url
- a string holding the language in which its content is presented

##### 1.7.3 Methods
- Start()
  - initializes English language
  - checks the url for language changes
  - sets current url to browser's url
- Update()
  - if the urls (current and browser) are not the same:
  	- take browser's url as current
  	- if language was changed (s. method checkLanguage()), reload page
- bool checkLanguage(string url)
	- if given url contains specific language suffix:
		- takes index of suffix
		- extracts the language to which was changed from the suffix
		- if current language is not the one extracted from the url, language change has happened (bool set to true)
		- sets current language to changed language
	- if url does not contain specific language suffix:
		- if current language is not English, language change has happened (bool set to true)
		- sets current language to English
	- returned bool shows if language change has happened

################################################################################

#### 1.8 Script Loading Circle

##### 1.8.1 Description
This script manages the rotation of the loading circle while the simulator
website is loaded in the simulator panel. It is assigned to the Progress object
of the Loading Circle in the Simulator panel.

##### 1.8.2 Attributes
- a private RectTransform rectComponent, the Transform of a rectangle
- a private float rotateSpeed, the speed of rotation for the loading circle

##### 1.8.3 Methods
- Start()
  - initializes the rectangle Transform
- Update()
  - rotates the rectangle with the defined speed and the time from the last to
    the current frame

################################################################################

#### 1.9 Script DestroyScriptsAndResetPanel (only for debugging)

##### 1.9.1 Description
This script destroys all scripts that are used to initialize the panels, if the
user presses the Delete-key. It is not assigned to any object in the editor
because it was only used for debugging purposes during the implementation.

##### 1.9.2 Methods
- Update()
  - if user presses the Delete-key:
    - destroys all scripts used to correctly initialize panel: PanelRenderer,
      each use of CorrectContentPosition
    - resets start screen and tab panel
    - resets toggles such that only first is active:
      - for general/chapter tabs: only General Information toggle is on
      - for sub/child tabs: only first active tab toggle (left-most) is on
    - destroys itself (script DestroyScriptsAndResetPanel) at last

################################################################################
################################################################################

### 2. Scripts managing the player (in Assets - Scripts - PlayerScripts) <a name="PlayerScripts"></a>

#### 2.1 Script UserMovement

##### 2.1.1 Description
This script manages the user's control of the player and is assigned to the
Player in the editor.

##### 2.1.2 Attributes
- public floats movementSpeed, rotateSpeed and scrollSpeed for the player's
  movement, rotation and scrolling speed
- a public GameObject playerCamera, the camera from the player's point of view

##### 2.1.3 Methods
- FixedUpdate()
  - is called once per frame
  - specifies user's commands to move the player with given movement speed: key
    arrows (+ Shift) forwards (normal and fast speed) / backwards / left /
    right
  - specifies user's commands to rotate the player with given rotation speed:
    hold right mouse button and move mouse to let the player
    "look" left / right / up / down
  - specifies user's command to move player slightly forward with given
    scrolling speed: scroll mouse wheel

################################################################################

#### 2.2 Script Teleport

##### 2.2.1 Description
This script manages the player's teleportation to the target (the full-version
simulator) and back to the original position where the player stood before this
teleportation. It is assigned to the Player in the editor.

##### 2.2.2 Attributes
- variables needed for the teleportation
  - a public teleportation target (position in front of the full-version
    simulator)
  - the public player's original position before the teleportation to the
    simulator takes place
  - a public boolean for the teleportation state (true if teleportation to
    target happened, false if teleportation back to original position happened)

- variables needed for fade-in/-out of the scene
  - a public canvas, object can also be found in editor as "Fader Canvas"
  - a public Fader, object can also be found in editor as "Fader"
  - a private animator for realizing the scene's fading in/out during a
    teleportation

##### 2.2.3 Methods
- Awake()
  - initializes the animator as a component of the Fader

- Update()
  - if user presses "Return":
    - canvas is activated and scene fades out
    - player is teleported to
        - full-version simulator or
        - original position again (player's position before teleportation to
          simulator happened)
    -  canvas is deactivated and scene fades in again with small temporal delay

- auxiliary methods:
  - activateCanvas()
    - method for deactivating the Fader Canvas
  - deactivateCanvas()
    - method for deactivating the Fader Canvas
  - FadeOut()
    - method for scene's fade-out, uses Fader's Animator anim
  - FadeIn()
    - method for scene's fade-in, uses Fader's Animator anim
  - GoToDestination()
    - changes the player's position to the target position (full-version
      simulator) and faces player and its camera towards the panel
  - GoToOldPos()
    - changes the player's position to the original position before player was
      teleported to the simulator

################################################################################

#### 2.3 Script TeleportCheck

##### 2.3.1 Description
This script checks the teleportation state if the player moves through the door
of the Simulator room. Thus, it is assigned to the object TeleportCheck of the
Simulator room's door frame in the editor.

##### 2.3.2 Methods
- OnTriggerEnter()
  - is called if user moves the player through the door to the Simulator room,
    so the player hits the object TeleportCheck
  - if the teleportation already happened and thus, the state "teleported" in
    the script 2.2 Teleport is true:
    - the teleportation state is set to false so that the next teleportation can
      happen

##### 2.3.3 Note
- This script was implemented because without it, the teleportation did not work
properly. When the player was teleported to the simulator and then moved away
with the arrow keys, the next teleportation took the player back to its old
position where it was before the first teleportation instead of back to the
simulator again. This script checks if the player has already entered/exited the
Simulator room and if the teleportation has already happened. Then the boolean
is set to false again so the Script Teleport can perform the next teleportation.
- The Layer IgnoreRaycast at the object TeleportCheck ensures that the click onto
the green Floor Position point in the Simulator room still works properly.

################################################################################

#### 2.4 Script MovePlayerToOptimalPosition

##### 2.4.1 Description
This script moves the player to the optimal position in front of a panel if user
clicks at the green button on the floor. It is assigned to each Floor Position
in front of a panel in the editor.

##### 2.4.2 Attributes
- public GameObject for the player which is assigned in the editor
- public GameObject for the panel which is assigned in the editor

##### 2.4.3 Methods
- OnMouseDown()
  - is called if user clicks on the optimal position in front of a panel (green
    button on the floor)
  - if VR version is run:
	- get VR player
	- set player's position to the position of the green Floor Position tile (regarding feet offset)
	- if player has objects attached to its hands, reset attachments
  - else (if Desktop Version is run):
	- x- and z-position of the optimal position are assigned to the player
	- player and player camera are rotated towards the panel
  
  - CallOnMouseDown()
	- calls OnMouseDown in Desktop version ???
  
  - HandleVivePointerEvent(object sender, PointerEventArgs e)
	- if an event from the VR controllers comes in and its target is the current Floor Position where this script is assigned to, call OnMouseDown


################################################################################
################################################################################

### 3. Scripts managing the interactive table (in Assets - Scripts - TableScripts) <a name="TableScripts"></a>

#### 3.1 Script ShowPathAndFrame

##### 3.1.1 Description
This script handles the orientation with the interactive table.
If the user clicks onto a specific content card on the table (Afforestation,
Technological Carbon Removal, ...), a frame appears around it and a path to the
corresponding panel is shown. The script is assigned to the specific Content
Cards on the interactive Table in the editor (s. Table - Content Cards).

##### 3.1.2 Attribute
- a public GameObject Path (s. Table - Paths in the editor)
- a public GameObject Frame (s. Table - Content Card - [Specific Content Card
  Name] - Frame in the editor)

##### 3.1.3 Methods
- OnMouseDown()
  - if user clicks on the content card, a frame appears/disappears around it and
    a path to the corresponding panel appears/disappears
  - more concrete:
    if frame is not active:
      - all active frames and paths are collected and deactivated
      - only the frame and path of the clicked content card are activated
    if frame is active:
      - frame and path of the clicked content card are deactivated

################################################################################

#### 3.2 Script BezierVisualizer

##### 3.2.1 Definition
This script extracts the points from the Bezier segments, concatenates them and
sets them as positions for the line renderer. It is assigned to every
BezierPoints object at the interactive Table in the editor (Table - Paths -
[specific panel/Content Card name] - BezierPoints).

##### 3.2.2 Attributes
- a public array of Bezier curves called BezierSegments, assigned in the editor
- a public LineRenderer called Renderer, assigned in the editor

##### 3.2.3 Methods
- Start()
  - create a new list of points with 3-dimensional coordinates and save all
    points of each Bezier segment in it
  - create new array of 3-dimensional positions with same length as pointList
  - fill array with points from pointList (copy pointList into array positions)
  - set positions as positions for the line renderer

################################################################################

#### 3.3 BezierCurve

##### 3.3.1 Definition
This script creates a Bezier curve for the paths which reach from the
interactive table to the panels. It is assigned to every BezierPoints child
Segment 1 at the interactive Table in the editor (Table - Paths - [specific
panel/Content Card name] - BezierPoints - Segment 1) and an array of
BezierCurves is assigned to each BezierVisualizer.
The idea is to have a controllable path segment, that can be concatenated by the
BezierVisualizer. This implementation is close to the tutorial version here:
https://www.habrador.com/tutorials/interpolation/2-bezier-curve/

##### 3.3.2 Attributes
- 4 public control points: Transform Start, ControlPointStart,
  ControlPointEnd, End
- 4 private 3-dimensional vectors: A, B, C, D

##### 3.3.3 Methods
- OnDrawGizmos()
  - draws line segments successively
  - initializes point positions A-D with positions of the 4 Transforms
  - defines Bezier curve's color
  - defines start position of the line
  - defines resolution of the line (has to add up to 1, so 0.02 is chosen)
  - defines amount of loops or line segments, respectively
  - for each line segment:
    - calculates current step along line
    - calculates coordinates between control points with a Catmull-Rom spline
      (s. DeCasteljausAlgorithm(step) below)
    - draws the calculates line segment
  - saves this position so the next line segment can be drawn

- GetPoints() (similar to OnDrawGizmo, except of the drawing)
  - returns a list of points of one Bezier Curve by calculating different line
    segments
  - creates new list of 3-dimensional points called points
  - initializes point positions A-D with positions of the 4 Transforms
  - defines start position of the line
  - defines resolution of the line (has to add up to 1, so 0.02 is chosen)
  - defines amount of loops or line segments, respectively
  - for each line segment:
    - adds current position on the line to the list points
    - calculates current step along line
    - calculates coordinates between control points with a Catmull-Rom spline
      (s. DeCasteljausAlgorithm(step) below)
    - saves this position so the next line segment can be calculated
  - return the filled list of points

- DeCasteljausAlgorithm(float step)
  - is private
  - implements De Casteljau's Algorithm
  - gets the float step (current position/segment on the line)
  - returns an interpolated position, a 3-dimensional vector

################################################################################

#### 3.4 Script ActivateCorrelationFrames

##### 3.4.1 Description
This script activates the frames around the Content Cards and sub topics that
correlate to the clicked Content Card at the interactive table. It is assigned
to each Content Card (Carbon Removal, ...) and their sub topics (Afforestation,
...) at the Table in the editor.

##### 3.4.2 Attributes
- a public and static new dictionary of correlations between Content Cards and
  their sub topics (s. script CorrelationDictionary)
- a public and static event OnFrameStateChangeEvent of type OnFrameStateChange
  (s. method below) that describes the state of a frame (on/off)

##### 3.4.3 Methods
  - OnFrameStateChange(string tableCardName)
    - delegate method describing which content card is changed (on/off)
  - OnMouseDown()
    - if user clicks on Content Card or sub topic, event is provoked and state
      of correlative Content Card is changed (on/off)
  - Start()
    - listens to event of a state change of the frame and calls the Response
      function
  - ResponseToFrameStateChange(string correlationTableCardName)
    - as response to a frame state change, frame of deactivated correlative
      Content Cards are activated and active ones are deactivated
    - more concrete:
      - array of correlative Cards is created with the help of the
        correlation dictionary correlations
      - each entry is visited and its frame is activated/deactivated if it
        equals the correlative Card name (input string)

################################################################################

#### 3.5 Script CorrelationDictionary

##### 3.5.1 Description
This script creates and fills a dictionary with all correlations between the
Content Cards (rooms and panels, respectively). The script
ActivateCorrelationFrames uses this dictionary to draw frames around the Content
Cards which correlate to the Content Card that the user has clicked on the
interactive table.

##### 3.5.2 Attributes
- a public variable correlationDictionary of the type Dictionary which holds a
  string (Content Card/panel name) and a list of strings (correlative Content
  Card/panel names)

##### 3.5.3 Methods
 - CorrelationDictionary()
    - creates the correlation dictionary
    - fills the correlation dictionary with entries holding a Content Card name
      and its correlative Content Card names (Carbon Removal, Emissions,
      Energy Supply, Transport, Buildings and Industry)

################################################################################
################################################################################

### 4. Scripts managing the extraction of texts from the simulator website and the import of images, materials and textures (in Assets - Editor) <a name="EditorScripts"></a>

#### 4.1 Script ObjectForJson

##### 4.1.1 Description
This script is not a class, but a struct and supports the script EditorWebRequest (below).
It is not assigned to any object in the editor.

##### 4.1.2 Attributes
- public strings for the panel name and each child/sub tab name in the panel
  (information, examples, ...)

##### 4.1.3 Methods
- ObjectForJson(string name, string information, ...)
  - public constructor for the struct
- ObjectClear()
  - is public
  - empties all strings

################################################################################

#### 4.2 EditorWebRequest

##### 4.2.1 Description
not yet commented...

##### 4.2.2 Attributes

##### 4.2.3 Methods

################################################################################

#### 4.3 MaterialProcessorWorking

##### 4.3.1 Description
not yet commented...

##### 4.3.2 Attributes

##### 4.3.3 Methods

################################################################################
################################################################################

### 5. Additional scripts for the VR version  <a name="VRScripts"></a>

#### 5.1 Additional scripts managing the panels <a name="VRPanelScripts"></a>

##### 5.1.1 ToggleVRSupport

###### 5.1.1.1 Description
not yet commented...

###### 5.1.1.2 Attributes

###### 5.1.1.3 Methods

##### 5.1.2 ToggleVRSupportHelper

###### 5.1.2.1 Description
not yet commented...

###### 5.1.2.2 Attributes

###### 5.1.2.3 Methods

##### 5.1.3 ScrollbarVRSupport

###### 5.1.3.1 Description
not yet commented...

###### 5.1.3.2 Attributes

###### 5.1.3.3 Methods

##### 5.1.4 CameraRayCast

###### 5.1.4.1 Description
not yet commented...

###### 5.1.4.2 Attributes

###### 5.1.4.3 Methods

#### 5.2 Scripts for the interaction between VR and browser <a name="VRBrowserScripts"></a>

##### 5.2.1 HandleLoadingScreen

###### 5.2.1.1 Description
This script handles the presentation of a loading screen while the browser is loaded.
The script is assigned to the general simulator's tab panel and its start screen.

###### 5.2.1.2 Attributes
- a game object LoadingScreen representing the loading screen, assigned in the editor (Simulator - Tab Panel - Loading Screen)
- an instance of the type Browser, assigned in the editor (Simulator - Tab Panel - Browser)
- a boolean indicating if the query is running, initially false

###### 5.2.1.3 Methods
- Update()
	- if the loading screen is active and the query does not run:
		- boolean is set to true, query runs now
		- starts the coroutine that checks if the browser is ready
- checkBrowserReady()
  - coroutine that checks if the browser is ready
  - initializes a node and tries to fill it with an element on the website
    (here: the temperature increase)
  - not a nice solution, but it works:
    - until the element promise is not defined (i.e. we get an error): keep the loading screen
    - if we get a value in the variable promise: deactivates the loading screen
  - query is set to false again

##### 5.2.2 VRControllerInputProxy

##### 5.2.2.1 Description
This script communicates between the SteamVR and the scripts managing the
browser (PonterUIMesh, PointerUIBase, VRBrowserHand). Like the script
VRBrowserHand, it is assigned to the object VRBrowser (VRPlayer - SteamVRObjects - RightHand), represented by a cube in the information room.

##### 5.2.2.2 Attributes
- a VRBrowserHand
- a specific boolean from SteamVR, indicating the trigger click, named
  TriggerClick
- a specific input source from SteamVR (SteamVR_Input_Sources.RightHand can
  also be used)

##### 5.2.2.3 Methods
- OnEnable()
  - adds listener for pressing to the input source (s. Press() below)
  - adds listener for releasing to the input source (s. Release() below)

- OnDisable()
  - removes listener for pressing to the input source (s. Press() below)
  - removes listener for releasing to the input source (s. Release() below)

- Press(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
  - sets the amplitude of the VR browser hand to 1

- Release(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
  - sets the amplitude of the VR browser hand to 0

##### 5.2.3 VRBrowserHand

###### 5.2.3.1 Description
This script tracks the tracked controllers. This can be used for feeding the VR input to the
browser by the method FeedVRPointers() in the script PointerUIBase.
Like the script VRControllerInputProxy, this script is assigned to the object VRBrowser
(VRPlayer - SteamVRObjects - RightHand), represented by a cube in the information
room.

###### 5.2.3.2 Attributes
- a public XRNode called hand indicating which hand we should look to track, set to left
  hand
- a public GameObject for optional visualization of the hand. It should be a child of the VRHand object and will be set active when the controller is tracking.
- a public scroll threshold (0.1) indicating how much we must slide a finger/joystick before we start scrolling
- a public trackpad scroll speed (0.05) indicating how fast the page moves as we move our finger across the touchpad. Set to a negative number to enable that infernal ""natural scrolling"" that's been making so many trackpads unusable lately.
- a public joystick scroll speed (75) indicating how fast the page moves as we scroll with a joystick
- a private 2-dimensional vector holding the last point that was touched
- a private boolean indicating that the touch is currently scrolling
- a bool Tracked indicating if we are currently tracking (has a getter and
  setter)
- a public MouseButton showing the currently depressed buttons (has a getter
  and setter)
- a public 2-dimensional vector showing how much we've scrolled since the
  last frame (same units as Input.mouseScrollDelta, has a getter
  and setter)
- a private XRNodeState nodeState
- a private VRInput input
- 3 public floats for left, middle and right click amplitudes

- private int lastFrame
- private List<XRNodeState> states = new List<XRNodeState>()
- a private boolean hasTouchpad indicating if a touchpad exists (currently not used)

###### 5.2.3.3 Methods
- OnEnable()
  - initialize VR input
  - VR poses update after LateUpdate and before OnPreCull
  - onPreCull of camera is updated by adding UpdatePreCull (s. below)
  - if visualization of the hand is active, deactivate it

- OnDisable()
  - onPreCull of camera is updated by subtracting UpdatePreCull (s. below)

- Update()
  - if more than 5 frames have passed (giving the SteamVR SDK a chance to start
    up), ReadInput() is called
  
- ReadInput()
  - depressed buttons and scroll delta is set to 0
  - if the left click amplitude is greater than 0.9, the number of depressed
    buttons and the left mouse button is compared
  - if the middle click amplitude is greater than 0.5, the number of depressed
    buttons and the middle mouse button is compared
  - if the right click amplitude is greater than 0.5, the number of depressed
    buttons and the right mouse button is compared
  - boolean Tracked is set to the tracking state of the XRNodeState
  - amplitude of left, middle and right click is reported (see console)

- ReadTouchpad()
  - defines a 2-dimensional touchPoint with x- and y-coordinate of the touch
    pad
  - defines a boolean touchButton indicating if the axis between nodeState
    (XRNodeState) and the touch on the pad is greater than 0.5
  - if touchButton is true:
    - difference between last and current touch point is calculated (delta)
    - if player is not scrolling yet (boolean touchIsScrolling false)
      - if delta's magnitude multiplied by the scroll speed of the pad is
        greater than the scroll threshold
        - player is scrolling -> sets boolean touchIsScrolling to true
        - the last touch point is updated to the current one
      - if scroll threshold is not exceeded: does nothing, especially does not
        updating the touch pad yet
    - if player is already scrolling:
      - scrollDelta is updated by adding the product of the delta and the
        scroll speed of the pad
  - if touchButton is false:
    - the last touch point is updated to the current one
    - player is not scrolling -> boolean touchIsScrolling is set to false

- ReadJoystick()
  - defines a 2-dimensional position with x- and y-coordinate of the joy stick
  - if x-coordinate (y-coordinate) is greater than the scroll threshold,
    change its value (add/substract threshold), else set it to 0
  - changes position by multiplicaiton with its magnitude, the scroll speed of
    the joy stick and the time passed since the last frame
  - scrollDelta is updated by adding the calculated position

- UpdatePreCull(Camera cam)
  - if we are still in the same frame, return
  - sets lastFrame to current frame counter
  - get all tracked states (VR browser hands, ...)
  - for each state:
    - if it is not the hand, continue
    - if it is the hand (hand was found):
      - get its pose and set it as local position
      - get its rotation and set it as local rotation (if we are in the
        hierarchy, we do not want to change the orientation)
      - if the visualization of the hand is active, keep it activated if the
        current hand is tracked and deactivate it if it is not
	
##### 5.2.4 PointerUIBase

###### 5.2.4.1 Description
This script handles the input for different inputs (mouse, touch, pointer, VR,
nose) makes them able to interact with the browser.
It is not assigned in the editor.

###### 5.2.4.2 Attributes important for VR part
- boolean enableVRInput, indicating if VR controllers are used, initially
  false
- a list of VR browser hands vrHands
- a struct PointerState with:
  - a unique id
  - a boolean indicating if it is 2d or 3d
  - a 2-dimensional position or a 3-dimensional position (Ray)
  - a MouseButton activeButtons holding the currently depressed buttons
  - a 2-dimensional scroll delta
- an integer currentPointerId
- list of PointerStates called currentPointers
- integers p_currentDown, p_currentOver, p_anyDown, p_anyOver indicating how
  many pointers are down/over

###### 5.2.4.3 Methods important for VR part
- OnHandlePointers()
  - if VR input is active, feeds tracked controllers (VR browser hands) to the
    browser

- FeedVRPointers()
  - if list vrHands is empty
    - fills list by searching VRBrowserHands
    - throws error if no hands are found but VR inout is enabled
    - for each hand:
      - if the hand is not tracked (s. boolean Tracked in VRBrowserHand),
        continues
      - else:
        - creates a struct PointerState out of the given hand and feeds it to the browser

- FeedPointerState(PointerState)
  - feed the given pointer into the handler (browser), i.e.
  - if the pointer is 2-dimensional, calls MapPointerToBrowser() in script PointerUIMesh that
    converts the 2-dimensional coordinate from the screen-space to a browser-space
    coordinate
  - if the pointer is 3-dimensional, calls MapRayToBrowser()  in script PointerUIMesh that
    converts the 3-dimensional ray from the world-space to a browser-space
    coordinate
  - if the given pointer is the current one / not (compares IDs) and if there
    are currently depressed buttons (activeButtons):
    - update counters for pointers: p_currentDown, p_currentOver, p_anyDown, p_anyOver
  - add the given pointer to the list of pointers currentPointers

################################################################################
################################################################################

### 6. Other scripts (in Assets - Scripts) <a name="OtherScripts"></a>

#### 6.1 QuitMuseum

##### 6.1.1 Definition
This script quits the museum if the user clicks the quit button in the left
upper corner. It is assigned to the Quit Button in the editor.

##### 6.1.2 Methods
- quit()
  - If quit button is clicked, a log message about the quit museum is printed
    and the whole museum is quit.

## License

[![CC0](https://licensebuttons.net/p/zero/1.0/88x31.png)](https://creativecommons.org/publicdomain/zero/1.0/)
