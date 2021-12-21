using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZenFulcrum.EmbeddedBrowser;

// This script checks if the language was changed and reloads the page in the panel.

public class URLLanguageCheck : MonoBehaviour
{
    // a browser, its url and the language in which its content is presented
    public Browser Browser;
    private string currentURL;
    private string currentLanguage;

    void Start()
    {
        // initialize English language, check the url for language changes and set current url to browser's url
        this.currentLanguage = "en";
        this.checkLanguage(this.Browser.Url);
        this.currentURL = this.Browser.Url;
    }

    void Update()
    {
        // if the urls are not the same
        if (this.currentURL != this.Browser.Url)
        {
            // take browser's url as current
            this.currentURL = this.Browser.Url;

            // if language was changed, reload page
            if (this.checkLanguage(this.Browser.Url))
            {
                this.Browser.LoadURL(this.Browser.Url, true);
            }
        }
    }

    // checks if language was changed
    public bool checkLanguage(string url)
    {
        bool hasChanged = false;

        // if url contains specific language suffix 
        if (url.Contains("&lang="))
        {
            // take its index
            int index = url.IndexOf("&lang=");
            // extract the language to which was changed
            string languageCode = url.Substring(index + "&lang=".Length, 2);
            // if current language is not the one in the url, language change has happened
            if (this.currentLanguage != languageCode)
            {
                hasChanged = true;
            }
            // set current language to changed language
            this.currentLanguage = languageCode;
        }
        // if url does not contain specific language suffix 
        else
        {
            string languageCode = "en";
            // if current language is not English, language change has happened
            if (this.currentLanguage != languageCode)
            {
                hasChanged = true;
            }
            // set current language to English
            this.currentLanguage = languageCode;
        }

        // boolean shows if language change has happened
        return hasChanged;
    }
}
