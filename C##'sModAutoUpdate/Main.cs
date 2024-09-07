using System;
using System.Reflection;
using HarmonyLib;
using Overlayer;
using Overlayer.Controllers;
using KeyViewer;
using KeyViewer.Controllers;
using UnityEngine;
using UnityModManagerNet;
using static UnityModManagerNet.UnityModManager;

public class Main
{
    private static Harmony harmony;
    public static bool Start(ModEntry modEntry)
    {
        modEntry.OnToggle = (entry, value) =>
        {
            if (value)
            {
                harmony = new Harmony(modEntry.Info.Id);
                harmony.PatchAll(Assembly.GetExecutingAssembly());
            }
            else
            {
                harmony?.UnpatchAll(modEntry.Info.Id);
            }
            return true;
        };

        return true;
    }

    public static UnityModManager.ModEntry modEntry;

    [HarmonyPatch(typeof(Overlayer.Controllers.GUIController), "Draw")]
    public static class OverlayerAddText
    {
        public static void Prefix()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("<b><size=30>Check Last Version</size></b>", GUILayout.ExpandWidth(false));
            GUILayout.Space(5); 

            if (GUILayout.Button("Go", GUILayout.Width(100), GUILayout.Height(40) ,GUILayout.ExpandWidth(false)))
            {
                Application.OpenURL("https://c3nb.mod-g.cc/overlayer/");
            }

            GUILayout.EndHorizontal();
            GUILayout.Space(10);
        }
    }

    [HarmonyPatch(typeof(KeyViewer.Controllers.GUIController), "Draw")]
    public static class KeyViewerAddText
    {
        public static void Prefix()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("<b><size=30>Check Last Version</size></b>", GUILayout.ExpandWidth(false));
            GUILayout.Space(5);

            if (GUILayout.Button("Go", GUILayout.Width(100), GUILayout.Height(40), GUILayout.ExpandWidth(false)))
            {
                Application.OpenURL("https://c3nb.mod-g.cc/keyviewer/");
            }

            GUILayout.EndHorizontal();
            GUILayout.Space(10);
        }
    }
}
