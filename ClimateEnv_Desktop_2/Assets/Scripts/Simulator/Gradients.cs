using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Creates and stores the gradients
public class Gradients : MonoBehaviour
{
    // Every object/group of objects that will change its color depending   
    // on the simulation, bases the color on its individual color gradient

    public Gradient gradientTree1;
    public GradientColorKey[] colorKeyTree1;
    public GradientAlphaKey[] alphaKeyTree1;

    public Gradient gradientTree2;
    public GradientColorKey[] colorKeyTree2;
    public GradientAlphaKey[] alphaKeyTree2;

    public Gradient gradientTree3;
    public GradientColorKey[] colorKeyTree3;
    public GradientAlphaKey[] alphaKeyTree3;

    public Gradient gradientTree4;
    public GradientColorKey[] colorKeyTree4;
    public GradientAlphaKey[] alphaKeyTree4;

    public Gradient gradientTree5;
    public GradientColorKey[] colorKeyTree5;
    public GradientAlphaKey[] alphaKeyTree5;

    public Gradient gradientTree6;
    public GradientColorKey[] colorKeyTree6;
    public GradientAlphaKey[] alphaKeyTree6;

    public Gradient gradientTree7;
    public GradientColorKey[] colorKeyTree7;
    public GradientAlphaKey[] alphaKeyTree7;

    public Gradient gradientTree0;
    public GradientColorKey[] colorKeyTree0;
    public GradientAlphaKey[] alphaKeyTree0;

    public Gradient gradientOcean;
    public GradientColorKey[] colorKeyOcean;
    public GradientAlphaKey[] alphaKeyOcean;

    public Gradient gradientClouds;
    public GradientColorKey[] colorKeyClouds;
    public GradientAlphaKey[] alphaKeyClouds;

    public Gradient gradientSmog;
    public GradientColorKey[] colorKeySmog;
    public GradientAlphaKey[] alphaKeySmog;

    public Gradient gradientSkybox;
    public GradientColorKey[] colorKeySkybox;
    public GradientAlphaKey[] alphaKeySkybox;

    public Gradient gradientGround;
    public GradientColorKey[] colorKeyGround;
    public GradientAlphaKey[] alphaKeyGround;

    public Gradient gradientMountain;
    public GradientColorKey[] colorKeyMountain;
    public GradientAlphaKey[] alphaKeyMountain;

    // Start is called before the first frame update
    void Start()
    {

        // ------------------ TREE 1 ------------------ 
        // gradient from bright green to brown
        gradientTree1 = new Gradient();

        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        colorKeyTree1 = new GradientColorKey[3];
        colorKeyTree1[0].color = new Color(0.46666667f, 0.72549020f, 0.37254902f, 1f);
        colorKeyTree1[0].time = 0.0f;
        colorKeyTree1[1].color = new Color(0.35686275f, 0.67058824f, 0.23529412f, 1f);
        colorKeyTree1[1].time = 0.33f;
        colorKeyTree1[2].color = new Color(0.42352941f, 0.42352941f, 0.23137255f, 1f);
        colorKeyTree1[2].time = 0.65f;

        // Populate the alpha keys at relative time 0 and 1  (0 and 100%)
        alphaKeyTree1 = new GradientAlphaKey[2];
        alphaKeyTree1[0].alpha = 1.0f;
        alphaKeyTree1[0].time = 0.0f;
        alphaKeyTree1[1].alpha = 1.0f;
        alphaKeyTree1[1].time = 1.0f;

        gradientTree1.SetKeys(colorKeyTree1, alphaKeyTree1);


        // ------------------ TREE 2 ------------------ 
        // gradient from green to brown
        gradientTree2 = new Gradient();

        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        colorKeyTree2 = new GradientColorKey[3];
        colorKeyTree2[0].color = new Color(0.45490196f, 0.75294118f, 0.27450980f, 1f);
        colorKeyTree2[0].time = 0.0f;
        colorKeyTree2[1].color = new Color(0.37254902f, 0.48235294f, 0.02745098f, 1f);
        colorKeyTree2[1].time = 0.35f;
        colorKeyTree2[2].color = new Color(0.27450980f, 0.22352941f, 0.04705882f, 1f);
        colorKeyTree2[2].time = 0.9f;

        // Populate the alpha keys at relative time 0 and 1  (0 and 100%)
        alphaKeyTree2 = new GradientAlphaKey[2];
        alphaKeyTree2[0].alpha = 1.0f;
        alphaKeyTree2[0].time = 0.0f;
        alphaKeyTree2[1].alpha = 1.0f;
        alphaKeyTree2[1].time = 1.0f;

        gradientTree2.SetKeys(colorKeyTree2, alphaKeyTree2);


        // ------------------ TREE 3 ------------------ 
        // gradient from bright green to brown
        gradientTree3 = new Gradient();

        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        colorKeyTree3 = new GradientColorKey[3];
        colorKeyTree3[0].color = new Color(0.05555556f, 0.63137255f, 0.38431373f, 1f);
        colorKeyTree3[0].time = 0.0f;
        colorKeyTree3[1].color = new Color(0.06666667f, 0.46274510f, 0.29019608f, 1f);
        colorKeyTree3[1].time = 0.5f;
        colorKeyTree3[2].color = new Color(0.01960784f, 0.30274510f, 0.05098039f, 1f);
        colorKeyTree3[2].time = 0.9f;

        // Populate the alpha keys at relative time 0 and 1  (0 and 100%)
        alphaKeyTree3 = new GradientAlphaKey[2];
        alphaKeyTree3[0].alpha = 1.0f;
        alphaKeyTree3[0].time = 0.0f;
        alphaKeyTree3[1].alpha = 1.0f;
        alphaKeyTree3[1].time = 1.0f;

        gradientTree3.SetKeys(colorKeyTree3, alphaKeyTree3);

        // ------------------ TREE 4 ------------------ 
        // gradient from green to brown
        gradientTree4 = new Gradient();

        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        colorKeyTree4 = new GradientColorKey[3];
        colorKeyTree4[0].color = new Color(0.26797386f, 0.50980392f, 0.33333333f, 1f);
        colorKeyTree4[0].time = 0.0f;
        colorKeyTree4[1].color = new Color(0.12941176f, 0.41568627f, 0.29803922f, 1f);
        colorKeyTree4[1].time = 0.5f;
        colorKeyTree4[2].color = new Color(0.32156863f, 0.29803922f, 0.03137255f, 1f);
        colorKeyTree4[2].time = 0.9f;

        // Populate the alpha keys at relative time 0 and 1  (0 and 100%)
        alphaKeyTree4 = new GradientAlphaKey[2];
        alphaKeyTree4[0].alpha = 1.0f;
        alphaKeyTree4[0].time = 0.0f;
        alphaKeyTree4[1].alpha = 1.0f;
        alphaKeyTree4[1].time = 1.0f;

        gradientTree4.SetKeys(colorKeyTree4, alphaKeyTree4);

        // ------------------ TREE 5 ------------------ 
        // gradient from green to brown
        gradientTree5 = new Gradient();

        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        colorKeyTree5 = new GradientColorKey[3];
        colorKeyTree5[0].color = new Color(0.18039216f, 0.61176471f, 0.34117647f, 1f);
        colorKeyTree5[0].time = 0.0f;
        colorKeyTree5[1].color = new Color(0.10196078f, 0.44313725f, 0.23137255f, 1f);
        colorKeyTree5[1].time = 0.5f;
        colorKeyTree5[2].color = new Color(0.05490196f, 0.27843137f, 0.13725490f, 1f);
        colorKeyTree5[2].time = 0.9f;

        // Populate the alpha keys at relative time 0 and 1  (0 and 100%)
        alphaKeyTree5 = new GradientAlphaKey[2];
        alphaKeyTree5[0].alpha = 1.0f;
        alphaKeyTree5[0].time = 0.0f;
        alphaKeyTree5[1].alpha = 1.0f;
        alphaKeyTree5[1].time = 1.0f;

        gradientTree5.SetKeys(colorKeyTree5, alphaKeyTree5);

        // ------------------ TREE 6 ------------------ 
        // gradient from green to brown
        gradientTree6 = new Gradient();

        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        colorKeyTree6 = new GradientColorKey[3];
        colorKeyTree6[0].color = new Color(0.00000000f, 0.61176471f, 0.00000000f, 1f);
        colorKeyTree6[0].time = 0.0f;
        colorKeyTree6[1].color = new Color(0.00000000f, 0.43529412f, 0.07450980f, 1f);
        colorKeyTree6[1].time = 0.5f;
        colorKeyTree6[2].color = new Color(0.00000000f, 0.29411765f, 0.03921569f, 1f);
        colorKeyTree6[2].time = 0.9f;

        // Populate the alpha keys at relative time 0 and 1  (0 and 100%)
        alphaKeyTree6 = new GradientAlphaKey[2];
        alphaKeyTree6[0].alpha = 1.0f;
        alphaKeyTree6[0].time = 0.0f;
        alphaKeyTree6[1].alpha = 1.0f;
        alphaKeyTree6[1].time = 1.0f;

        gradientTree6.SetKeys(colorKeyTree6, alphaKeyTree6);

        // ------------------ TREE 7 ------------------ 
        // gradient from green to brown
        gradientTree7 = new Gradient();

        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        colorKeyTree7 = new GradientColorKey[3];
        colorKeyTree7[0].color = new Color(0.41176471f, 0.76470588f, 0.28627451f, 1f);
        colorKeyTree7[0].time = 0.0f;
        colorKeyTree7[1].color = new Color(0.28627451f, 0.65098039f, 0.14901961f, 1f);
        colorKeyTree7[1].time = 0.5f;
        colorKeyTree7[2].color = new Color(0.12549020f, 0.36862745f, 0.03529412f, 1f);
        colorKeyTree7[2].time = 0.9f;

        // Populate the alpha keys at relative time 0 and 1  (0 and 100%)
        alphaKeyTree7 = new GradientAlphaKey[2];
        alphaKeyTree7[0].alpha = 1.0f;
        alphaKeyTree7[0].time = 0.0f;
        alphaKeyTree7[1].alpha = 1.0f;
        alphaKeyTree7[1].time = 1.0f;

        gradientTree7.SetKeys(colorKeyTree7, alphaKeyTree7);

        // ------------------ TREE 0 ------------------ 
        // gradient from green to brown
        gradientTree0 = new Gradient();

        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        colorKeyTree0 = new GradientColorKey[3];
        colorKeyTree0[0].color = new Color(0.11437908f, 0.50787402f, 0.09921260f, 1f);
        colorKeyTree0[0].time = 0.0f;
        colorKeyTree0[1].color = new Color(0.04803922f, 0.37500000f, 0.07843137f, 1f);
        colorKeyTree0[1].time = 0.23f;
        colorKeyTree0[2].color = new Color(0.20784314f, 0.16078431f, 0.08235294f, 1f);
        colorKeyTree0[2].time = 0.68f;

        // Populate the alpha keys at relative time 0 and 1  (0 and 100%)
        alphaKeyTree0 = new GradientAlphaKey[2];
        alphaKeyTree0[0].alpha = 1.0f;
        alphaKeyTree0[0].time = 0.0f;
        alphaKeyTree0[1].alpha = 1.0f;
        alphaKeyTree0[1].time = 1.0f;

        gradientTree0.SetKeys(colorKeyTree0, alphaKeyTree0);

        // ------------------ Ground ------------------ 
        // gradient from bright green over dark green to yellow to brown
        gradientGround = new Gradient();

        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        colorKeyGround = new GradientColorKey[3];
        colorKeyGround[0].color = new Color(0.24313725f, 0.69019608f, 0.21176471f, 1f);
        colorKeyGround[0].time = 0.25f;
        colorKeyGround[1].color = new Color(0.41960784f, 0.42352941f, 0.12941176f, 1f);
        colorKeyGround[1].time = 0.60f;
        colorKeyGround[2].color = new Color(0.79215686f, 0.72549020f, 0.32549020f, 1f);
        colorKeyGround[2].time = 0.95f;

        // Populate the alpha keys at relative time 0 and 1  (0 and 100%)
        alphaKeyGround = new GradientAlphaKey[2];
        alphaKeyGround[0].alpha = 1.0f;
        alphaKeyGround[0].time = 0.0f;
        alphaKeyGround[1].alpha = 1.0f;
        alphaKeyGround[1].time = 1.0f;

        gradientGround.SetKeys(colorKeyGround, alphaKeyGround);

        // ------------------ Mountain ------------------ 
        // gradient from dark brown to orange
        gradientMountain = new Gradient();

        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        colorKeyMountain = new GradientColorKey[3];
        colorKeyMountain[0].color = new Color(0.29411765f, 0.18431373f, 0.00000000f, 1f);
        colorKeyMountain[0].time = 0.00f;
        colorKeyMountain[1].color = new Color(0.47843137f, 0.31764706f, 0.03921569f, 1f);
        colorKeyMountain[1].time = 0.5f;
        colorKeyMountain[2].color = new Color(0.78431373f, 0.56078431f, 0.27058824f, 1f);
        colorKeyMountain[2].time = 0.90f;

        // Populate the alpha keys at relative time 0 and 1  (0 and 100%)
        alphaKeyMountain = new GradientAlphaKey[2];
        alphaKeyMountain[0].alpha = 1.0f;
        alphaKeyMountain[0].time = 0.0f;
        alphaKeyMountain[1].alpha = 1.0f;
        alphaKeyMountain[1].time = 1.0f;

        gradientMountain.SetKeys(colorKeyMountain, alphaKeyMountain);

        // ------------------ OCEAN ------------------ 
        // gradient from green to blue
        gradientOcean = new Gradient();

        colorKeyOcean = new GradientColorKey[3];
        colorKeyOcean[0].color = new Color(0.00000000f, 0.76078431f, 1.00000000f, 1f);
        colorKeyOcean[0].time = 0f;
        colorKeyOcean[1].color = new Color(0.14509804f, 0.20392157f, 0.55294118f, 1f);
        colorKeyOcean[1].time = 0.35f;
        colorKeyOcean[2].color = new Color(0.00784314f, 0.52941176f, 0.33333333f, 1f);
        colorKeyOcean[2].time = 1f;

        // Populate the alpha keys at relative time 0 and 1  (0 and 100%)
        alphaKeyOcean = new GradientAlphaKey[2];
        alphaKeyOcean[0].alpha = 1.0f;
        alphaKeyOcean[0].time = 0.0f;
        alphaKeyOcean[1].alpha = 1.0f;
        alphaKeyOcean[1].time = 1.0f;

        gradientOcean.SetKeys(colorKeyOcean, alphaKeyOcean);


        // ------------------ CLOUDS ------------------ 
        // gradient from bright white to dark grey
        gradientClouds = new Gradient();

        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        colorKeyClouds = new GradientColorKey[2];
        colorKeyClouds[0].color = new Color(1f, 1f, 1f, 1f);
        colorKeyClouds[0].time = 0.0f;
        colorKeyClouds[1].color = new Color(0.245283f, 0.245283f, 0.245283f, 1f);
        colorKeyClouds[1].time = 0.7f;

        // Populate the alpha keys at relative time 0 and 1  (0 and 100%)
        alphaKeyClouds = new GradientAlphaKey[2];
        alphaKeyClouds[0].alpha = 0.9f;
        alphaKeyClouds[0].time = 0.0f;
        alphaKeyClouds[1].alpha = 0.9f;
        alphaKeyClouds[1].time = 1.0f;

        gradientClouds.SetKeys(colorKeyClouds, alphaKeyClouds);

        // ------------------ SMOG ------------------ 
        // gradient from bright white to light grey
        gradientSmog = new Gradient();

        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        colorKeySmog = new GradientColorKey[2];
        colorKeySmog[0].color = new Color(1f, 1f, 1f, 1f);
        colorKeySmog[0].time = 0.21f;
        colorKeySmog[1].color = new Color(0.639f, 0.639f, 0.639f, 1f);
        colorKeySmog[1].time = 1f;

        // Populate the alpha keys at relative time 0 and 1  (0 and 100%)
        alphaKeySmog = new GradientAlphaKey[2];
        alphaKeySmog[0].alpha = 0.0f;
        alphaKeySmog[0].time = 0.0f;
        alphaKeySmog[1].alpha = 0.9f;
        alphaKeySmog[1].time = 1.0f;

        gradientSmog.SetKeys(colorKeySmog, alphaKeySmog);

        // ------------------ SKYBOX ------------------ 
        // gradient from light blue green to dark gray
        gradientSkybox = new Gradient();

        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        colorKeySkybox = new GradientColorKey[2];
        colorKeySkybox[0].color = new Color(0f, 0.6698113f, 0.6501371f, 1f);
        colorKeySkybox[0].time = 0.15f;
        colorKeySkybox[1].color = new Color(0.501f, 0.937f, 1f, 1f);
        colorKeySkybox[1].time = 1f;

        // Populate the alpha keys at relative time 0 and 1  (0 and 100%)
        alphaKeySkybox = new GradientAlphaKey[2];
        alphaKeySkybox[0].alpha = 1.0f;
        alphaKeySkybox[0].time = 0.0f;
        alphaKeySkybox[1].alpha = 1.0f;
        alphaKeySkybox[1].time = 1.0f;

        gradientSkybox.SetKeys(colorKeySkybox, alphaKeySkybox);
    }

}
