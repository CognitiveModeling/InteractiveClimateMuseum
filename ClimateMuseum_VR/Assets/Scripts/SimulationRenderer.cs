using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using ZenFulcrum.EmbeddedBrowser;


[RequireComponent(typeof(Browser))]
public class SimulationRenderer : MonoBehaviour
{
	private Browser browser;
	public void Awake()
	{
		// load En-ROADS simulator into Browser object
		browser = GetComponent<Browser>();
	}

    public void Start()
    {
        browser.LoadURL("https://en-roads.climateinteractive.org/", true);

        // Javascript function to deactive all listerners on selected element 
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
	IEnumerator RenderingCoroutine()
	{
        // adjust top toolbar for every simulator instance
        adjustTopToolbar();

		// if this gameObject is the full version of the simulator, do not change anything else
		if (this.gameObject.transform.parent.parent.parent.name.Equals("Simulator"))
		{
			yield return 0;
		}
		// if this gameObject is a downscaled version, make more changes dependent on the panel name 
		else

		{
			string panelName = this.gameObject.transform.parent.parent.parent.parent.parent.parent.parent.parent.name;
			browser.EvalJS("var items = document.getElementsByClassName('title svelte-1espllb');");
			browser.EvalJS("var sections = document.getElementsByClassName('section-title svelte-18tz0gg');");

			switch (panelName)
			{
				case "Technological Carbon Removal":
                    browser.EvalJS("for (var i = 0; i < items.length; i++) { " +
                        "if (items[i].innerHTML.indexOf('Technological') == -1 && items[i].innerHTML.indexOf('Deforestation') == -1) { " +
                        "lowOpacity(items[i].parentNode.parentNode.parentNode); deactivateListeners(items[i].parentNode.parentNode); }" +
                        "if (items[i].innerHTML.indexOf('Technological') != -1) { " +
                        "items[i].parentNode.parentNode.parentNode.style.border = 'thick solid #32CD32'; }" +
                        "if (items[i].innerHTML.indexOf('Deforestation') != -1) { " +
                        "items[i].parentNode.parentNode.parentNode.style.border = 'thick solid #FFD700'; " +
                        "}}");
                    break;
				case "Afforestation":
                    browser.EvalJS("for (var i = 0; i < items.length; i++) { " +
                        "if (items[i].innerHTML.indexOf('Afforestation') == -1 && items[i].innerHTML.indexOf('Deforestation') == -1) { " +
                        "lowOpacity(items[i].parentNode.parentNode.parentNode); deactivateListeners(items[i].parentNode.parentNode); }" +
                        "if (items[i].innerHTML.indexOf('Afforestation') != -1) { " +
                        "items[i].parentNode.parentNode.parentNode.style.border = 'thick solid #32CD32'; }" +
                        "if (items[i].innerHTML.indexOf('Deforestation') != -1) { " +
                        "items[i].parentNode.parentNode.parentNode.style.border = 'thick solid #FFD700'; " +
                        "}}");
                    break;
				case "Methane & Co":
					browser.EvalJS("for (var i = 0; i < items.length; i++) { " +
						"if (items[i].innerHTML.indexOf('Methane') == -1 && items[i].innerHTML.indexOf('Deforestation') == -1) { " +
						"items[i].parentNode.parentNode.parentNode.style.opacity = '0.3'; deactivateListeners(items[i].parentNode.parentNode); }" +
						"if (items[i].innerHTML.indexOf('Methane') != -1) { " +
						"items[i].parentNode.parentNode.parentNode.style.border = 'thick solid #32CD32'; }" +
						"if (items[i].innerHTML.indexOf('Deforestation') != -1) { " +
						"items[i].parentNode.parentNode.parentNode.style.border = 'thick solid #FFD700'; " +
						"}}");
					break;
				case "Deforestation":
					browser.EvalJS("for (var i = 0; i < items.length; i++) { " +
						"if (items[i].innerHTML.indexOf('Deforestation') == -1 && items[i].innerHTML.indexOf('Methane') == -1 && items[i].innerHTML.indexOf('Methane') == -1 && items[i].innerHTML.indexOf('Bioenergy') == -1 && items[i].innerHTML.indexOf('Technological') == -1 && items[i].innerHTML.indexOf('Afforestation') == -1) { " +
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
				case "Carbon Pricing":
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
				case "New Technologies":
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
				case "Nuclear":
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
				case "Renewables":
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
				case "Bioenergy":
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
				case "Natural Gas":
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
				case "Oil":
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
				case "Coal":
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
				case "T Electrification":
					browser.EvalJS("for (var j = 2; j < 4; j++) {" +
						"sections[j].parentNode.style.opacity = '0.3'; deactivateListeners(sections[j].parentNode); }" +
						"sections[5].parentNode.style.opacity = '0.3'; deactivateListeners(sections[5].parentNode);" +
						"sections[1].parentNode.childNodes[1].childNodes[1].style.border = 'thick solid #32CD32';" +
						"sections[4].parentNode.childNodes[1].childNodes[0].style.border = 'thick solid #FFD700';" +
						"sections[1].parentNode.childNodes[1].childNodes[0].style.opacity = '0.3'; deactivateListeners(sections[1].parentNode.childNodes[1].childNodes[0]);" +
						"sections[4].parentNode.childNodes[1].childNodes[1].style.opacity = '0.3'; deactivateListeners(sections[4].parentNode.childNodes[1].childNodes[1]);" +
						"sections[0].parentNode.style.border = 'thick solid #FFD700';");
					break;
				case "T Efficiency":
					browser.EvalJS("for (var i = 2; i < sections.length; i++) {" +
						"sections[i].parentNode.style.opacity = '0.3'; deactivateListeners(sections[i].parentNode); }" +
						"sections[1].parentNode.childNodes[1].childNodes[0].style.border = 'thick solid #32CD32';" +
						"sections[1].parentNode.childNodes[1].childNodes[1].style.opacity = '0.3'; deactivateListeners(sections[1].parentNode.childNodes[1].childNodes[1]);" +
						"sections[0].parentNode.style.border = 'thick solid #FFD700';");
					break;
				case "BI Electrification":
					browser.EvalJS("for (var i = 3; i < sections.length; i++) {" +
						"sections[i].parentNode.style.opacity = '0.3'; deactivateListeners(sections[i].parentNode); }" +
						"sections[1].parentNode.style.opacity = '0.3'; deactivateListeners(sections[1].parentNode);" +
						"sections[2].parentNode.childNodes[1].childNodes[1].style.border = 'thick solid #32CD32';" +
						"sections[2].parentNode.childNodes[1].childNodes[0].style.opacity = '0.3'; deactivateListeners(sections[2].parentNode.childNodes[1].childNodes[0]);" +
						"sections[0].parentNode.style.border = 'thick solid #FFD700';");
					break;
				case "BI Efficiency":
					browser.EvalJS("for (var i = 3; i < sections.length; i++) {" +
						"sections[i].parentNode.style.opacity = '0.3'; deactivateListeners(sections[i].parentNode); }" +
						"sections[1].parentNode.style.opacity = '0.3'; deactivateListeners(sections[1].parentNode);" +
						"sections[2].parentNode.childNodes[1].childNodes[0].style.border = 'thick solid #32CD32';" +
						"sections[2].parentNode.childNodes[1].childNodes[1].style.opacity = '0.3'; deactivateListeners(sections[2].parentNode.childNodes[1].childNodes[1]);" +
						"sections[0].parentNode.style.border = 'thick solid #FFD700';");
					break;
				case "G Economic":
					browser.EvalJS("for (var i = 4; i < sections.length; i++) {" +
						"sections[i].parentNode.style.opacity = '0.3'; deactivateListeners(sections[i].parentNode); }" +
						"sections[1].parentNode.style.opacity = '0.3'; deactivateListeners(sections[1].parentNode);" +
						"sections[2].parentNode.style.opacity = '0.3'; deactivateListeners(sections[2].parentNode);" +
						"sections[3].parentNode.childNodes[1].childNodes[1].style.border = 'thick solid #32CD32';" +
						"sections[3].parentNode.childNodes[1].childNodes[0].style.opacity = '0.3'; deactivateListeners(sections[3].parentNode.childNodes[1].childNodes[0]);" +
						"sections[0].parentNode.style.border = 'thick solid #FFD700';");
					break;
				case "G Population":
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
	public string getFirstByClassName (string className)
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

    public void adjustTopToolbar()
    {
        browser.EvalJS(hideElement(getFirstByClassName("bg svelte-fnsfcv")));
        browser.EvalJS(hideElement(getFirstByClassName("logo svelte-1ogce1i")));
        browser.EvalJS(hideElement(getFirstByClassName("button-gray share-button svelte-1ogce1i")));
        browser.EvalJS(hideElement(getFirstByClassName("top-menu hide-offline svelte-gypx8b")));
        browser.EvalJS(hideElement("document.getElementsByClassName('icon-button svelte-1ogce1i')[5]"));
        browser.EvalJS(hideElement("document.getElementsByClassName('icon-button svelte-1ogce1i')[7]"));
        browser.EvalJS(hideElement(getFirstByClassName("logo-container svelte-18tz0gg")));
        browser.EvalJS(addPadding(getFirstByClassName("bottom-content svelte-1f9pc7r"), "bottom", 20));
    }
}
