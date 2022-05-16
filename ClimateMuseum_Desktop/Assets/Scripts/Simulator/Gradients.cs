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
        colorKeyTree1[0].color = new Color(0.364f, 0.925f, 0.160f, 1f);
        colorKeyTree1[0].time = 0.0f;
        colorKeyTree1[1].color = new Color(0.172f, 0.560f, 0.078f, 1f);
        colorKeyTree1[1].time = 0.4f;
        colorKeyTree1[2].color = new Color(0.7169f, 0.4171f, 0.186f, 1f);
        colorKeyTree1[2].time = 1.0f;

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
        colorKeyTree2[0].color = new Color(0.231f, 0.639f, 0.082f, 1f);
        colorKeyTree2[0].time = 0.0f;
        colorKeyTree2[1].color = new Color(0.074f, 0.219f, 0.023f, 1f);
        colorKeyTree2[1].time = 0.45f;
        colorKeyTree2[2].color = new Color(0.580f, 0.258f, 0.011f, 1f);
        colorKeyTree2[2].time = 1.0f;

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
        colorKeyTree3[0].color = new Color(0.027f, 0.380f, 0.015f, 1f);
        colorKeyTree3[0].time = 0.0f;
        colorKeyTree3[1].color = new Color(0.0921f, 0.4339f, 0.123f, 1f);
        colorKeyTree3[1].time = 0.5f;
        colorKeyTree3[2].color = new Color(0.2078f, 0.1615f, 0.0823f, 1f);
        colorKeyTree3[2].time = 0.68f;

        // Populate the alpha keys at relative time 0 and 1  (0 and 100%)
        alphaKeyTree3 = new GradientAlphaKey[2];
        alphaKeyTree3[0].alpha = 1.0f;
        alphaKeyTree3[0].time = 0.0f;
        alphaKeyTree3[1].alpha = 1.0f;
        alphaKeyTree3[1].time = 1.0f;

        gradientTree3.SetKeys(colorKeyTree3, alphaKeyTree3);

        // ------------------ Ground ------------------ 
        // gradient from bright green over dark green to yellow to brown
        gradientGround = new Gradient();

        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        colorKeyGround = new GradientColorKey[4];
        colorKeyGround[0].color = new Color(0.054f, 1f, 0f, 1f);
        colorKeyGround[0].time = 0.1f;
        colorKeyGround[1].color = new Color(0.2f, 0.603f, 0f, 1f);
        colorKeyGround[1].time = 0.41f;
        colorKeyGround[2].color = new Color(0.396f, 0.298f, 0f, 1f);
        colorKeyGround[2].time = 0.69f;
        colorKeyGround[3].color = new Color(1f, 0.905f, 0f, 1f);
        colorKeyGround[3].time = 0.88f;

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
        colorKeyMountain[0].color = new Color(0.294f, 0.188f, 0f, 1f);
        colorKeyMountain[0].time = 0.15f;
        colorKeyMountain[1].color = new Color(0.294f, 0.188f, 0f, 1f);
        colorKeyMountain[1].time = 0.55f;
        colorKeyMountain[2].color = new Color(1f, 0.619f, 0.117f, 1f);
        colorKeyMountain[2].time = 0.95f;

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

        colorKeyOcean = new GradientColorKey[4];
        colorKeyOcean[0].color = new Color(0f, 0.760f, 1f, 1f);
        colorKeyOcean[0].time = 0.13f;
        colorKeyOcean[1].color = new Color(0.145f, 0.203f, 0.552f, 1f);
        colorKeyOcean[1].time = 0.42f;
        colorKeyOcean[2].color = new Color(0f, 0.478f, 0.231f, 1f);
        colorKeyOcean[2].time = 0.77f;
        colorKeyOcean[3].color = new Color(0.007f, 0.564f, 0f, 1f);
        colorKeyOcean[3].time = 1f;

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
        colorKeyClouds[0].time = 0.21f;
        colorKeyClouds[1].color = new Color(0.305f, 0.305f, 0.305f, 1f);
        colorKeyClouds[1].time = 1f;

        // Populate the alpha keys at relative time 0 and 1  (0 and 100%)
        alphaKeyClouds = new GradientAlphaKey[2];
        alphaKeyClouds[0].alpha = 1.0f;
        alphaKeyClouds[0].time = 0.0f;
        alphaKeyClouds[1].alpha = 1.0f;
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
        alphaKeySmog[0].alpha = 1.0f;
        alphaKeySmog[0].time = 0.0f;
        alphaKeySmog[1].alpha = 1.0f;
        alphaKeySmog[1].time = 1.0f;

        gradientSmog.SetKeys(colorKeySmog, alphaKeySmog);

        // ------------------ SKYBOX ------------------ 
        // gradient from light blue green to dark gray
        gradientSkybox = new Gradient();

        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        colorKeySkybox = new GradientColorKey[2];
        colorKeySkybox[0].color = new Color(0.501f, 0.937f, 1f, 1f);
        colorKeySkybox[0].time = 0.15f;
        colorKeySkybox[1].color = new Color(0.137f, 0.243f, 0.270f, 1f);
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
