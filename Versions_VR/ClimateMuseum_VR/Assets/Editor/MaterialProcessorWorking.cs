﻿
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;


public class MaterialProcessorWorking : AssetPostprocessor
{
	[MenuItem("Assets/Import Images")]
    private static void ImportImages()
    {
        DirectoryInfo dir = new DirectoryInfo("Assets/Images");
        FileInfo[] info = dir.GetFiles("*.jpg");

        foreach (FileInfo finfo in info)
        {
            var createdTexture = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Images/" + finfo.Name);
            string name = finfo.Name.Split(new string[] {".jpg"}, StringSplitOptions.None)[0] + ".mat";
            
            Material material = new Material(Shader.Find("Specular"));
      		AssetDatabase.CreateAsset(material, "Assets/Materials/" + name);

            var createdMaterial = AssetDatabase.LoadAssetAtPath<Material>("Assets/Materials/" + name);
            createdMaterial.mainTexture = createdTexture;
       
        }

        info = dir.GetFiles("*.png");

        foreach (FileInfo finfo in info)
        {
            var createdTexture = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Images/" + finfo.Name);
            string name = finfo.Name.Split(new string[] {".png"}, StringSplitOptions.None)[0] + ".mat";

            Material material = new Material(Shader.Find("Specular"));
      		AssetDatabase.CreateAsset(material, "Assets/Materials/" + name);

            var createdMaterial = AssetDatabase.LoadAssetAtPath<Material>("Assets/Materials/" + name);
            createdMaterial.mainTexture = createdTexture;
        }
    }

    public void OnPreprocessTexture ()
	{
		TextureImporter textureImporter = (TextureImporter)assetImporter;
		// keep scale of Image as it is (not power of two)
        textureImporter.npotScale = TextureImporterNPOTScale.None;
        textureImporter.textureType = TextureImporterType.Sprite;
        // is this sensible?
        textureImporter.mipmapEnabled = false;

        // To Do: Bilder kopfüber drehen bei Import oder Skalierung auf y- Achse negativ?
	}


    public void OnPostprocessTexture (Texture2D texture)
    {
    	bool isInImagesDirectory = assetPath.Contains("/Images/");

    	if (isInImagesDirectory)
    	{
    		string currentTexture = Path.GetFileName(assetPath).Split('.')[0];
            var newMat = new Material(Shader.Find("Unlit/Texture"));

            string createdTextureName = assetPath;
    		string createdMaterialName = "Assets/Materials/" + currentTexture + ".mat";


 			if (AssetDatabase.LoadAssetAtPath<Material>(createdMaterialName))
            {
                //material already exists, don't overwrite
                createdTextureName = null;
                createdMaterialName = null;
            }
            else
            {
                AssetDatabase.CreateAsset(newMat, createdMaterialName);
            }
    	}
    }

}
