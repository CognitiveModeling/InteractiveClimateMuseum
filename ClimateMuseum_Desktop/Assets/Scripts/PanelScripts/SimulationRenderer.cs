using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using ZenFulcrum.EmbeddedBrowser;

// This class renders the simulator from the En-ROADS website in the panels.
// Therefore, in the editor it is assigned to every panel
// (in the tab "Key Dynamics", except for the full-version simulator). 

[RequireComponent(typeof(Browser))]
public class SimulationRenderer : MonoBehaviour
{
    // different types of simulators
    public enum SIMULATOR_TYPE
    {
        TECHCARBONREMOVAL,
        AFFORESTATION,
        METHANE,
        DEFORESTATION,
        CARBONPRICING,
        NEWTECH,
        NUCLEAR,
        RENEWABLES,
        BIOENERGY,
        NATRUALGAS,
        OIL,
        COAL,
        TELECTRIFICATION,
        TEFFICIENCY,
        BIELECTRIFICATION,
        BIEFFICIENCY,
        GECONOMIC,
        GPOPULATION,
        GENERAL
    }

    // an instance of the class Browser
    private Browser browser;

    // specifies the type of simulator (full or downscaled version)
    public SimulationRenderer.SIMULATOR_TYPE Type;


    public void Awake()
    {
        // initializes the browser object
        browser = GetComponent<Browser>();
    }

    public void Start()
    {
        // load En-ROADS simulator into Browser object
        browser.LoadURL("https://en-roads.climateinteractive.org/", true);

        // Javascript (JS) function to deactive all listeners on selected element 
        browser.EvalJS("function deactivateListeners(elem) {" +
            "var old_element = elem;" +
            "var new_element = old_element.cloneNode(true);" +
            "old_element.parentNode.replaceChild(new_element, old_element);" +
            "}");

        // Javascript function to set opacity of selected element to 0.3
        browser.EvalJS("function lowOpacity(elem) {return elem.style.opacity = '0.3';}");
    }

    void Update()
    {
        // coroutine rendering this simulator instance starts only if web page has loaded
        if (browser.IsLoaded)
        {
            StartCoroutine(RenderingCoroutine());
        }
    }

    // adjusts the simulator's appearance in the different panels
    IEnumerator RenderingCoroutine()
    {
        // adjust top toolbar for every simulator instance
        adjustTopToolbar();

        // if this gameObject is the full version of the simulator, do not change design and interactions:
        if (this.Type == SIMULATOR_TYPE.GENERAL)
        {
            yield return 0;
        }

        // if this gameObject is the downscaled version of the simulator, change design and interactions:
        else
        {
            // define items and sections for designing the sliders individually for each panel:
            // Javascript function to save titles of the sub sections (Coal, Oil, ...), needs hard-coded, dynamic title for rendering the graphs
            browser.EvalJS("var items = document.getElementsByClassName('title svelte-1espllb');");
            // Javascript function to save titles of the six panel sections (Energy Supply, Transport, ...)
            browser.EvalJS("var sections = document.getElementsByClassName('section-title');");

            // design the simulator sliders in each panel depending on the simulator type,
            // nonrelevant sliders are deactivated and grayed out with the two JS functions defined in Start()
            switch (this.Type)
            {
                case SIMULATOR_TYPE.TECHCARBONREMOVAL:
                    browser.EvalJS("for (var i = 0; i < items.length; i++) { " +
                        "if (items[i].innerHTML.indexOf('Technological') == -1 && items[i].innerHTML.indexOf('Deforestation') == -1) { " +
                        "lowOpacity(items[i].parentNode.parentNode.parentNode); deactivateListeners(items[i].parentNode.parentNode); }" +
                        "if (items[i].innerHTML.indexOf('Technological') != -1) { " +
                        "items[i].parentNode.parentNode.parentNode.style.border = 'thick solid #32CD32'; }" +
                        "if (items[i].innerHTML.indexOf('Deforestation') != -1) { " +
                        "items[i].parentNode.parentNode.parentNode.style.border = 'thick solid #FFD700'; " +
                        "}}");
                    break;
                case SIMULATOR_TYPE.AFFORESTATION:
                    browser.EvalJS("for (var i = 0; i < items.length; i++) { " +
                        "if (items[i].innerHTML.indexOf('Afforestation') == -1 && items[i].innerHTML.indexOf('Deforestation') == -1) { " +
                        "lowOpacity(items[i].parentNode.parentNode.parentNode); deactivateListeners(items[i].parentNode.parentNode); }" +
                        "if (items[i].innerHTML.indexOf('Afforestation') != -1) { " +
                        "items[i].parentNode.parentNode.parentNode.style.border = 'thick solid #32CD32'; }" +
                        "if (items[i].innerHTML.indexOf('Deforestation') != -1) { " +
                        "items[i].parentNode.parentNode.parentNode.style.border = 'thick solid #FFD700'; " +
                        "}}");
                    break;
                case SIMULATOR_TYPE.METHANE:
                    browser.EvalJS("for (var i = 0; i < items.length; i++) { " +
                        "if (items[i].innerHTML.indexOf('Methane') == -1 && items[i].innerHTML.indexOf('Deforestation') == -1) { " +
                        "items[i].parentNode.parentNode.parentNode.style.opacity = '0.3'; deactivateListeners(items[i].parentNode.parentNode); }" +
                        "if (items[i].innerHTML.indexOf('Methane') != -1) { " +
                        "items[i].parentNode.parentNode.parentNode.style.border = 'thick solid #32CD32'; }" +
                        "if (items[i].innerHTML.indexOf('Deforestation') != -1) { " +
                        "items[i].parentNode.parentNode.parentNode.style.border = 'thick solid #FFD700'; " +
                        "}}");
                    break;
                case SIMULATOR_TYPE.DEFORESTATION:
                    browser.EvalJS("for (var i = 0; i < items.length; i++) { " +
                        "if (items[i].innerHTML.indexOf('Deforestation') == -1 && items[i].innerHTML.indexOf('Methane')" +
                        " == -1 && items[i].innerHTML.indexOf('Methane') == -1 && items[i].innerHTML.indexOf('Bioenergy') " +
                        "== -1 && items[i].innerHTML.indexOf('Technological') == -1 && items[i].innerHTML.indexOf('Afforestation') == -1) { " +
                        "items[i].parentNode.parentNode.parentNode.style.opacity = '0.3'; deactivateListeners(items[i].parentNode.parentNode); }" +
                        "if (items[i].innerHTML.indexOf('Deforestation') != -1) { " +
                        "items[i].parentNode.parentNode.parentNode.style.border = 'thick solid #32CD32'; }" +
                        "if (items[i].innerHTML.indexOf('Methane') != -1) { " +
                        "items[i].parentNode.parentNode.parentNode.style.border = 'thick solid #FFD700'; }" +
                        "if (items[i].innerHTML.indexOf('Bioenergy') != -1) { " +
                        "items[i].parentNode.parentNode.parentNode.style.border = 'thick solid #FFD700'; }" +
                        "if (items[i].innerHTML.indexOf('Technological') != -1) { " +
                        "items[i].parentNode.parentNode.parentNode.parentNode.style.border = 'thick solid #FFD700'; }" +
                        "if (items[i].innerHTML.indexOf('Afforestation') != -1) { " +
                        "items[i].parentNode.parentNode.parentNode.parentNode.style.border = 'thick solid #FFD700'; " +
                        "}}");
                    break;
                case SIMULATOR_TYPE.CARBONPRICING:
                    browser.EvalJS("for (var j = 2; j < 5; j++) {" +
                        "sections[j].parentNode.style.opacity = '0.3'; deactivateListeners(sections[j].parentNode); }" +
                        "for (var i = 0; i < items.length; i++) { " +
                        "if (items[i].innerHTML.indexOf('Price') != -1) { " +
                        "items[i].parentNode.parentNode.parentNode.style.border = 'thick solid #32CD32'; }" +
                        "}" +
                        "sections[0].parentNode.style.border = 'thick solid #FFD700';" +
                        "sections[1].parentNode.style.border = 'thick solid #FFD700';" +
                        "sections[5].parentNode.style.border = 'thick solid #FFD700';");
                    break;
                case SIMULATOR_TYPE.NEWTECH:
                    browser.EvalJS("for (var i = 0; i < items.length; i++) { " +
                        "if (items[i].innerHTML.indexOf('Zero') == -1 && items[i].innerHTML.indexOf('Coal') == -1 && items[i].innerHTML.indexOf('Natural') == -1) { " +
                        "lowOpacity(items[i].parentNode.parentNode.parentNode); deactivateListeners(items[i].parentNode.parentNode); }" +
                        "if (items[i].innerHTML.indexOf('Zero') != -1) { " +
                        "items[i].parentNode.parentNode.parentNode.style.border = 'thick solid #32CD32'; }" +
                        "if (items[i].innerHTML.indexOf('Coal') != -1) { " +
                        "items[i].parentNode.parentNode.parentNode.style.border = 'thick solid #FFD700'; }" +
                        "if (items[i].innerHTML.indexOf('Natural') != -1) { " +
                        "items[i].parentNode.parentNode.parentNode.style.border = 'thick solid #FFD700';" +
                        "}}");
                    break;
                case SIMULATOR_TYPE.NUCLEAR:
                    browser.EvalJS("for (var i = 0; i < items.length; i++) { " +
                        "if (items[i].innerHTML.indexOf('Nuclear') == -1 && items[i].innerHTML.indexOf('Renewables') == -1 && items[i].innerHTML.indexOf('Deforestation') == -1) { " +
                        "lowOpacity(items[i].parentNode.parentNode.parentNode); deactivateListeners(items[i].parentNode.parentNode); }" +
                        "if (items[i].innerHTML.indexOf('Nuclear') != -1) { " +
                        "items[i].parentNode.parentNode.parentNode.style.border = 'thick solid #32CD32'; }" +
                        "if (items[i].innerHTML.indexOf('Renewables') != -1) { " +
                        "items[i].parentNode.parentNode.parentNode.style.border = 'thick solid #FFD700'; }" +
                        "if (items[i].innerHTML.indexOf('Deforestation') != -1) { " +
                        "items[i].parentNode.parentNode.parentNode.style.border = 'thick solid #FFD700'; " +
                        "}}");
                    break;
                case SIMULATOR_TYPE.RENEWABLES:
                    browser.EvalJS("for (var i = 0; i < items.length; i++) { " +
                        "if (items[i].innerHTML.indexOf('Renewables') == -1 && items[i].innerHTML.indexOf('Oil') == -1 && items[i].innerHTML.indexOf('Coal') == -1 && items[i].innerHTML.indexOf('Natural') == -1 && items[i].innerHTML.indexOf('Deforestation') == -1) { " +
                        "lowOpacity(items[i].parentNode.parentNode.parentNode); deactivateListeners(items[i].parentNode.parentNode); }" +
                        "if (items[i].innerHTML.indexOf('Renewables') != -1) { " +
                        "items[i].parentNode.parentNode.parentNode.style.border = 'thick solid #32CD32'; }" +
                        "if (items[i].innerHTML.indexOf('Oil') != -1) { " +
                        "items[i].parentNode.parentNode.parentNode.style.border = 'thick solid #FFD700'; }" +
                        "if (items[i].innerHTML.indexOf('Coal') != -1) { " +
                        "items[i].parentNode.parentNode.parentNode.style.border = 'thick solid #FFD700'; }" +
                        "if (items[i].innerHTML.indexOf('Natural') != -1) { " +
                        "items[i].parentNode.parentNode.parentNode.style.border = 'thick solid #FFD700'; }" +
                        "if (items[i].innerHTML.indexOf('Deforestation') != -1) { " +
                        "items[i].parentNode.parentNode.parentNode.style.border = 'thick solid #FFD700'; " +
                        "}}");
                    break;
                case SIMULATOR_TYPE.BIOENERGY:
                    browser.EvalJS("for (var i = 0; i < items.length; i++) { " +
                        "if (items[i].innerHTML.indexOf('Bioenergy') == -1 && items[i].innerHTML.indexOf('Oil') == -1 && items[i].innerHTML.indexOf('Coal') == -1 && items[i].innerHTML.indexOf('Renewables') == -1 && items[i].innerHTML.indexOf('Deforestation') == -1) { " +
                        "lowOpacity(items[i].parentNode.parentNode.parentNode); deactivateListeners(items[i].parentNode.parentNode); }" +
                        "if (items[i].innerHTML.indexOf('Bioenergy') != -1) { " +
                        "items[i].parentNode.parentNode.parentNode.style.border = 'thick solid #32CD32'; }" +
                        "if (items[i].innerHTML.indexOf('Oil') != -1) { " +
                        "items[i].parentNode.parentNode.parentNode.style.border = 'thick solid #FFD700'; }" +
                        "if (items[i].innerHTML.indexOf('Coal') != -1) { " +
                        "items[i].parentNode.parentNode.parentNode.style.border = 'thick solid #FFD700'; }" +
                        "if (items[i].innerHTML.indexOf('Renewables') != -1) { " +
                        "items[i].parentNode.parentNode.parentNode.style.border = 'thick solid #FFD700'; }" +
                        "if (items[i].innerHTML.indexOf('Deforestation') != -1) { " +
                        "items[i].parentNode.parentNode.parentNode.style.border = 'thick solid #FFD700'; " +
                        "}}");
                    break;
                case SIMULATOR_TYPE.NATRUALGAS:
                    browser.EvalJS("for (var i = 0; i < items.length; i++) { " +
                        "if (items[i].innerHTML.indexOf('Natural') == -1 && items[i].innerHTML.indexOf('Oil') == -1 && items[i].innerHTML.indexOf('Coal') == -1 && items[i].innerHTML.indexOf('Methane') == -1 && items[i].innerHTML.indexOf('Deforestation') == -1) { " +
                        "lowOpacity(items[i].parentNode.parentNode.parentNode); deactivateListeners(items[i].parentNode.parentNode); }" +
                        "if (items[i].innerHTML.indexOf('Natural') != -1) { " +
                        "items[i].parentNode.parentNode.parentNode.style.border = 'thick solid #32CD32'; }" +
                        "if (items[i].innerHTML.indexOf('Oil') != -1) { " +
                        "items[i].parentNode.parentNode.parentNode.style.border = 'thick solid #FFD700'; }" +
                        "if (items[i].innerHTML.indexOf('Coal') != -1) { " +
                        "items[i].parentNode.parentNode.parentNode.style.border = 'thick solid #FFD700'; }" +
                        "if (items[i].innerHTML.indexOf('Methane') != -1) { " +
                        "items[i].parentNode.parentNode.parentNode.style.border = 'thick solid #FFD700'; }" +
                        "if (items[i].innerHTML.indexOf('Deforestation') != -1) { " +
                        "items[i].parentNode.parentNode.parentNode.style.border = 'thick solid #FFD700'; " +
                        "}}");
                    break;
                case SIMULATOR_TYPE.OIL:
                    browser.EvalJS("sections[2].parentNode.style.opacity = '0.3'; deactivateListeners(sections[2].parentNode);" +
                        "for (var i = 0; i < items.length; i++) { " +
                        "if (items[i].innerHTML.indexOf('Oil') == -1 && items[i].innerHTML.indexOf('Coal') == -1 && items[i].innerHTML.indexOf('Natural') == -1 && items[i].innerHTML.indexOf('Deforestation') == -1 && items[i].innerHTML.indexOf('Electrification') == -1) { " +
                        "lowOpacity(items[i].parentNode.parentNode.parentNode); deactivateListeners(items[i].parentNode.parentNode); }" +
                        "if (items[i].innerHTML.indexOf('Oil') != -1) { " +
                        "items[i].parentNode.parentNode.parentNode.style.border = 'thick solid #32CD32'; }" +
                        "if (items[i].innerHTML.indexOf('Coal') != -1) { " +
                        "items[i].parentNode.parentNode.parentNode.style.border = 'thick solid #FFD700'; }" +
                        "if (items[i].innerHTML.indexOf('Natural') != -1) { " +
                        "items[i].parentNode.parentNode.parentNode.style.border = 'thick solid #FFD700'; }" +
                        "if (items[i].innerHTML.indexOf('Deforestation') != -1) { " +
                        "items[i].parentNode.parentNode.parentNode.style.border = 'thick solid #FFD700'; }" +
                        "if (items[i].innerHTML.indexOf('Electrification') != -1) { " +
                        "items[i].parentNode.parentNode.parentNode.style.border = 'thick solid #FFD700'; }}" +
                        "sections[2].parentNode.childNodes[1].childNodes[1].style.border = 'none';");
                    break;
                case SIMULATOR_TYPE.COAL:
                    browser.EvalJS("for (var i = 0; i < items.length; i++) { " +
                        "if (items[i].innerHTML.indexOf('Coal') == -1 && items[i].innerHTML.indexOf('Natural') == -1 && items[i].innerHTML.indexOf('Renewables') == -1 && items[i].innerHTML.indexOf('Efficiency') == -1 && items[i].innerHTML.indexOf('Deforestation') == -1) { " +
                        "lowOpacity(items[i].parentNode.parentNode.parentNode); deactivateListeners(items[i].parentNode.parentNode); }" +
                        "if (items[i].innerHTML.indexOf('Coal') != -1) { " +
                        "items[i].parentNode.parentNode.parentNode.style.border = 'thick solid #32CD32'; }" +
                        "if (items[i].innerHTML.indexOf('Natural') != -1) { " +
                        "items[i].parentNode.parentNode.parentNode.style.border = 'thick solid #FFD700'; }" +
                        "if (items[i].innerHTML.indexOf('Efficiency') != -1) { " +
                        "items[i].parentNode.parentNode.parentNode.style.border = 'thick solid #FFD700'; }" +
                        "if (items[i].innerHTML.indexOf('Renewables') != -1) { " +
                        "items[i].parentNode.parentNode.parentNode.style.border = 'thick solid #FFD700'; }" +
                        "if (items[i].innerHTML.indexOf('Deforestation') != -1) { " +
                        "items[i].parentNode.parentNode.parentNode.style.border = 'thick solid #FFD700'; " +
                        "}}");
                    break;
                case SIMULATOR_TYPE.TELECTRIFICATION:
                    browser.EvalJS("for (var j = 2; j < 4; j++) {" +
                        "sections[j].parentNode.style.opacity = '0.3'; deactivateListeners(sections[j].parentNode); }" +
                        "sections[5].parentNode.style.opacity = '0.3'; deactivateListeners(sections[5].parentNode);" +
                        "sections[1].parentNode.childNodes[1].childNodes[1].style.border = 'thick solid #32CD32';" +
                        "sections[4].parentNode.childNodes[1].childNodes[0].style.border = 'thick solid #FFD700';" +
                        "sections[1].parentNode.childNodes[1].childNodes[0].style.opacity = '0.3'; deactivateListeners(sections[1].parentNode.childNodes[1].childNodes[0]);" +
                        "sections[4].parentNode.childNodes[1].childNodes[1].style.opacity = '0.3'; deactivateListeners(sections[4].parentNode.childNodes[1].childNodes[1]);" +
                        "sections[0].parentNode.style.border = 'thick solid #FFD700';");
                    break;
                case SIMULATOR_TYPE.TEFFICIENCY:
                    browser.EvalJS("for (var i = 2; i < sections.length; i++) {" +
                        "sections[i].parentNode.style.opacity = '0.3'; deactivateListeners(sections[i].parentNode); }" +
                        "sections[1].parentNode.childNodes[1].childNodes[0].style.border = 'thick solid #32CD32';" +
                        "sections[1].parentNode.childNodes[1].childNodes[1].style.opacity = '0.3'; deactivateListeners(sections[1].parentNode.childNodes[1].childNodes[1]);" +
                        "sections[0].parentNode.style.border = 'thick solid #FFD700';");
                    break;
                case SIMULATOR_TYPE.BIELECTRIFICATION:
                    browser.EvalJS("for (var i = 3; i < sections.length; i++) {" +
                        "sections[i].parentNode.style.opacity = '0.3'; deactivateListeners(sections[i].parentNode); }" +
                        "sections[1].parentNode.style.opacity = '0.3'; deactivateListeners(sections[1].parentNode);" +
                        "sections[2].parentNode.childNodes[1].childNodes[1].style.border = 'thick solid #32CD32';" +
                        "sections[2].parentNode.childNodes[1].childNodes[0].style.opacity = '0.3'; deactivateListeners(sections[2].parentNode.childNodes[1].childNodes[0]);" +
                        "sections[0].parentNode.style.border = 'thick solid #FFD700';");
                    break;
                case SIMULATOR_TYPE.BIEFFICIENCY:
                    browser.EvalJS("for (var i = 3; i < sections.length; i++) {" +
                        "sections[i].parentNode.style.opacity = '0.3'; deactivateListeners(sections[i].parentNode); }" +
                        "sections[1].parentNode.style.opacity = '0.3'; deactivateListeners(sections[1].parentNode);" +
                        "sections[2].parentNode.childNodes[1].childNodes[0].style.border = 'thick solid #32CD32';" +
                        "sections[2].parentNode.childNodes[1].childNodes[1].style.opacity = '0.3'; deactivateListeners(sections[2].parentNode.childNodes[1].childNodes[1]);" +
                        "sections[0].parentNode.style.border = 'thick solid #FFD700';");
                    break;
                case SIMULATOR_TYPE.GECONOMIC:
                    browser.EvalJS("for (var i = 4; i < sections.length; i++) {" +
                        "sections[i].parentNode.style.opacity = '0.3'; deactivateListeners(sections[i].parentNode); }" +
                        "sections[1].parentNode.style.opacity = '0.3'; deactivateListeners(sections[1].parentNode);" +
                        "sections[2].parentNode.style.opacity = '0.3'; deactivateListeners(sections[2].parentNode);" +
                        "sections[3].parentNode.childNodes[1].childNodes[1].style.border = 'thick solid #32CD32';" +
                        "sections[3].parentNode.childNodes[1].childNodes[0].style.opacity = '0.3'; deactivateListeners(sections[3].parentNode.childNodes[1].childNodes[0]);" +
                        "sections[0].parentNode.style.border = 'thick solid #FFD700';");
                    break;
                case SIMULATOR_TYPE.GPOPULATION:
                    browser.EvalJS("for (var i = 4; i < sections.length; i++) {" +
                        "sections[i].parentNode.style.opacity = '0.3'; deactivateListeners(sections[i].parentNode); }" +
                        "sections[1].parentNode.style.opacity = '0.3'; deactivateListeners(sections[1].parentNode);" +
                        "sections[2].parentNode.style.opacity = '0.3'; deactivateListeners(sections[2].parentNode);" +
                        "sections[3].parentNode.childNodes[1].childNodes[0].style.border = 'thick solid #32CD32';" +
                        "sections[3].parentNode.childNodes[1].childNodes[1].style.opacity = '0.3'; deactivateListeners(sections[2].parentNode.childNodes[1].childNodes[1]);" +
                        "sections[0].parentNode.style.border = 'thick solid #FFD700';");
                    break;
                default:
                    Debug.LogError("name not found");
                    break;
            }
        }
        yield return 0;
    }

    /**
     * auxiliary methods for the rendering of the simulator instances
     */

    // this method takes an HTML class name className and returns the first element of this class
    public string getFirstByClassName(string className)
    {
        return "document.getElementsByClassName('" + className + "')[0]";
    }

    // this method takes an HTML element name elem and hides this element
    public string hideElement(string elem)
    {
        return elem + ".style.display = 'none';";
    }

    /** 
     * this method takes an HTML element name elem, a position pos and a size px.
     * It adds a padding of size px to the HTML alement elem at defined position 
     * pos (top, bottom, left, right).
     */
    public string addPadding(string elem, string pos, int px)
    {
        string withPadding = elem;
        if (pos == "bottom")
        {
            withPadding += ".style.paddingBottom = " + "'" + px.ToString() + "px';";

        }
        else if (pos == "top")
        {
            withPadding += ".style.paddingTop = " + "'" + px.ToString() + "px';";
        }
        else if (pos == "left")
        {
            withPadding += ".style.paddingLeft = " + "'" + px.ToString() + "px';";
        }
        else if (pos == "right")
        {
            withPadding += ".style.paddingRight = " + "'" + px.ToString() + "px';";
        }
        return withPadding;
    }

    /**
     * this method takes an HTML element name elem and the integers top, right, bottom 
     * and left. It adds each a padding of defined sizes top, right, bottom and left to the 
     * respective position at elem. 
     */

    public string addPadding(string elem, int top, int right, int bottom, int left)
    {
        return elem + ".style.padding = " + "'" + top.ToString() + "px" + right.ToString() + "px" + bottom.ToString() + "px" + left.ToString() + "px';";
    }

    /**
    * this method adjusts the toolbar at the top of the simulator by hiding unnecessary buttons and adding paddings
    */

    public void adjustTopToolbar()
    {
        // hides initial welcome screen
        browser.EvalJS(hideElement(getFirstByClassName("bg")));

        // hides logo in upper left corner (needs hard-coded, dynamic name to hide)
        browser.EvalJS(hideElement(getFirstByClassName("logo svelte-1ytle68")));

        // hides tab "language" in toolbar at the top
        browser.EvalJS(hideElement(getFirstByClassName("top-menu")));

        // hides button "share your scenario" in top right corner
        browser.EvalJS(hideElement(getFirstByClassName("scenario-button")));

        // hides tab "help" in toolbar at the top
        browser.EvalJS(hideElement(getFirstByClassName("top-menu hide-offline")));

        // hides full-screen button in toolbar at the top
        browser.EvalJS(hideElement("document.getElementsByClassName('icon-button')[5]"));

        // hides help button in toolbar at the top
        browser.EvalJS(hideElement("document.getElementsByClassName('icon-button')[7]"));

        // hides logos in right lower corner
        browser.EvalJS(hideElement(getFirstByClassName("logo-container")));

        // adds padding at the bottom of the simulator (below sliders)
        browser.EvalJS(addPadding(getFirstByClassName("bottom-content"), "bottom", 20));
    }
}
