using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchLights : MonoBehaviour
{
	public Texture2D[] darkLightmapDir, darkLightmapColor;
	public Texture2D[] brightLightmapDir, brightLightmapColor;
	
	private LightmapData[] darkLightmap, brightLightmap;
	
        void Start()
	{
		List<LightmapData> dlightmap = new List<LightmapData>();
		
		for(int i = 0; i < darkLightmapDir.Length; i++)
		{
			LightmapData lmdata = new LightmapData();
			
   			lmdata.lightmapDir = darkLightmapDir[i];
   			lmdata.lightmapColor = darkLightmapColor[i];
			
			dlightmap.Add(lmdata);
		}
		
		darkLightmap = dlightmap.ToArray();
		
		List<LightmapData> blightmap = new List<LightmapData>();
		
		for(int i = 0; i < brightLightmapDir.Length; i++)
		{
			LightmapData lmdata = new LightmapData();
			
   			lmdata.lightmapDir = brightLightmapDir[i];
   			lmdata.lightmapColor = brightLightmapColor[i];
			
			blightmap.Add(lmdata);
		}
		
		brightLightmap = blightmap.ToArray();
		
		// Bind Input controlls
		//UserInputMap map = new UserInputMap();
		
		/*map.CurrentScene.SwitchLight1.performed += OnLight1Switched;
		map.CurrentScene.SwitchLight2.performed += OnLight2Switched;
		map.CurrentScene.SwitchLight3.performed += OnLight3Switched;
		
		map.Enable();*/

		LightmapSettings.lightmaps = brightLightmap;
	}
	
	public Light[] lights;

	private void OnLight1Switched(InputAction.CallbackContext ctx)
	{
		// lights[0].enabled = !lights[0].enabled;
		LightmapSettings.lightmaps = darkLightmap;
	}

	private void OnLight2Switched(InputAction.CallbackContext ctx)
	{
		// lights[1].enabled = !lights[1].enabled;
		LightmapSettings.lightmaps = brightLightmap;
	}
	
	private void OnLight3Switched(InputAction.CallbackContext ctx)
	{
		lights[2].enabled = !lights[2].enabled;
	}
}