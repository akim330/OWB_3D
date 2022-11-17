using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Random = UnityEngine.Random;

public class PaletteManager: MonoBehaviour, IGameManager
{
    private Color[][] palette;
    private string[][] paletteNames;
    //private Color[] colors;

    public Dictionary<string, Color> colors;

    private Dictionary<(int, int), List<string>> workableColors;

    private Dictionary<int, Color[]> skinColors;

    public ManagerStatus status { get; private set; }

    public void Startup()
    { 
        colors = new Dictionary<string, Color>()
        {
            {"dark_gray", new Color(99, 102, 99) }, // #636663: storm dust (dark gray) (120, 3, 40)
            {"gray", new Color(135, 133, 124) }, // #87857c: natural gray (49, 8, 52)
            {"light_gray", new Color(188, 173, 159) }, // #bcad9f: heathered gray (light gray) (28, 15, 73)

            {"light_orange", new Color(242, 184, 136) }, // #f2b888: peach (light orange) (27, 43, 94)
            {"orange", new Color(235, 150, 97) }, // #eb9661: dark salmon (orange) (23, 58, 92)
            {"dark_orange", new Color(181, 89, 69) }, // #b55945: brown rust (dark orange) (10, 61, 70)

            {"brown", new Color(115, 76, 68) }, // #734c44: coffee (brown) (10, 40, 45)
            {"dark_brown", new Color(61, 51, 51) }, // #3d3333: charcoal (dark brown) (0, 16, 23)

            {"dark_maroon", new Color(89, 62, 71) }, // #593e47: purple taupe (dark maroon) (339, 30, 34)
            {"maroon", new Color(122, 88, 89) }, // #7a5859: puce (maroon) (358, 27, 47)
         
            {"dark_sand", new Color(165, 120, 85) }, // #a57855: french beige (dark sand) (26, 48, 64)
            {"sand", new Color(222, 159, 71) }, // #de9f47: indian yellow (sand) (34, 68, 87)
            {"light_sand", new Color(253, 209, 121) }, // #fdd179: golden sand (light sand) (40, 52, 99)
            {"pale_sand", new Color(254, 225, 184) }, // #fee1b8: cream (pale sand) (35, 27, 99)

            {"green1", new Color(212, 198, 146) }, // #d4c692: green1 (thistle green) (47, 31, 83)
            {"green2", new Color(166, 176, 79) }, // #a6b04f: green2 (light olive green) (66, 55, 59)
            {"green3", new Color(129, 148, 71) }, // #819447: green3 (moss green) (74, 52, 58)
            {"green4", new Color(68, 112, 45) }, // #44702d: green4 (forest green) (99, 59, 43)
            {"green5", new Color(47, 77, 47) },  // #2f4d2f: green5 (pine green) (120, 38, 30)

            {"blue_green1", new Color(202, 230, 217) },  // #cae6d9: blue green 1 (jagged ice) (152, 12, 90)
            {"blue_green2", new Color(164, 197, 175) }, // #a4c5af: blue green 2 (spring rain) (139, 16, 77)
            {"blue_green3", new Color(137, 164, 119) }, // #89a477: blue green 3 (chelsea cucumber) (96, 27, 64)
            {"blue_green4", new Color(84, 103, 86) }, // #546756: blue gray (carbon gray) (126, 18, 40)


            {"white0", new Color(241, 246, 240) }, // #f1f6f0: white0 (white smoke) (110, 2, 96)
            {"white1", new Color(213, 214, 219) }, // #d5d6db: white1 (iron) (230, 2, 85)

            {"blue0", new Color(187, 195, 208) }, // #bbc3d0: blue0 (metallic silver) (217, 10, 81)
            {"blue1", new Color(150, 169, 193) }, // #96a9c1: blue1 (gull grey) (213, 22, 75)
            {"blue2", new Color(108, 129, 161) }, // #6c81a1: blue2 (steel) (216, 32, 63)
            {"blue3", new Color(64, 82, 115) }, // #405273: blue3 (blue), (218, 44, 45)
            {"blue4", new Color(48, 56, 67) }, // #303843: blue4 (dark slate gray) (214, 28, 26)
            {"blue5", new Color(20, 35, 58) } // ##14233a: blue5 (dark blue) (216, 65, 22)
        };

        foreach (KeyValuePair<string, Color> entry in colors.ToList())
        {
            Color scaledColor = entry.Value / 255f;
            scaledColor.a = 1;

            colors[entry.Key] = scaledColor;
        }


        paletteNames = new string[][]
        {
            // Row 0
            new string[]{"blue3", "maroon", "dark_orange", "orange", "light_sand", "pale_sand"},
            // Row 1
            new string[]{"dark_brown", "brown", "dark_orange", "orange", "light_orange", "pale_sand"},
            // Row 2
            new string[]{"dark_brown", "brown", "dark_orange", "sand", "light_sand", "pale_sand"},
            // Row 3
            new string[]{"blue5", "green5", "green4", "green3", "green2", "green1"},
            // Row 4
            new string[]{"blue4", "blue_green4", "blue_green3", "green1", "pale_sand"},
            // Row 5
            new string[]{"blue5", "blue4", "blue_green4", "blue_green3", "blue_green2", "blue_green1", "white0"},
            // Row 6
            new string[]{"blue5", "blue4", "blue3", "blue2", "blue1", "blue0", "white1"},
            // Row 7
            new string[]{"dark_brown", "dark_maroon", "maroon", "gray", "light_gray"},
            // Row 8
            new string[]{"blue5", "blue4", "gray", "light_gray", "white1", "white0"},
            // Row 9
            new string[]{"dark_brown", "dark_maroon", "maroon", "dark_sand", "sand", "light_sand", "pale_sand"},

        };

        palette = new Color[paletteNames.Length][];

        for (var i = 0; i < palette.Length; i++)
        {
            palette[i] = new Color[paletteNames[i].Length];
            for (var j = 0; j < paletteNames[i].Length; j++)
            {
                palette[i][j] = colors[paletteNames[i][j]];
            }

        }

        workableColors = new Dictionary<(int, int), List<string>>();

        for (var i = 0; i <= 5; i++)
        {
            for (var j = 0; j <= 5; j++)
            {
                List<string> workable = new List<string>();

                foreach(KeyValuePair<string, Color> entry in colors)
                {
                    if (GetShades(entry.Key, i, j, print_error : false) != null)
                    {
                        workable.Add(entry.Key);

                    }
                }
                workableColors.Add((i, j), workable);
            }
        }

        //foreach (KeyValuePair<(int, int), List<string>> kvp in workableColors)
        //{
        //    Debug.Log($"Key = {kvp.Key}, Value = {kvp.Value.ToString()}");
        //}

        skinColors = new Dictionary<int, Color[]>();

        skinColors.Add(0, NamesToColors(new string[] { "dark_sand", "light_orange", "pale_sand" }));
        skinColors.Add(1, NamesToColors(new string[] { "brown", "dark_sand", "sand" }));
        skinColors.Add(2, NamesToColors(new string[] { "maroon", "orange", "light_orange" }));
        skinColors.Add(3, NamesToColors(new string[] { "dark_sand", "light_sand", "pale_sand" }));
        skinColors.Add(4, NamesToColors(new string[] { "dark_sand", "sand", "light_sand" }));
        skinColors.Add(5, NamesToColors(new string[] { "blue1", "blue0", "white1" })); // Ghost

    }

    public Color[] GetRandomSkinPalette()
    {
        return skinColors[Random.Range(0, 4)];
    }

    private Color[] NamesToColors(string[] names)
    {
        return names.Select(name => colors[name]).ToArray();
    }

    public Color[] GetShades(string baseColorName, int nDarker, int nLighter, bool print_error = true)
    {
        //if (!workableColors[(nDarker, nLighter)].Contains(baseColorName))
        //{
        //    Debug.Log($"ERROR: No sequences found with {nDarker} darker colors and {nLighter} lighter colors than {baseColorName}");
        //    return null;
        //}

        List<Color[]> sequences = new List<Color[]>();

        for (int i = 0; i < paletteNames.Length; i++)
        {
            int idxFound = Array.IndexOf(paletteNames[i], baseColorName);

            if (idxFound >= nDarker && idxFound <= paletteNames[i].Length - 1 - nLighter)
            {
                sequences.Add(palette[i][(idxFound - nDarker) .. (idxFound + nLighter + 1)]);
            }
        }

        if (sequences.Count == 0)
        {
            if (print_error)
            {
                Debug.Log($"ERROR: No sequences found with {nDarker} darker colors and {nLighter} lighter colors than {baseColorName}");
            }
            return null;
        }
        else
        {
            return sequences[Random.Range(0, sequences.Count)];

        }

    }

    //public string GetRandomColorName()
    //{
    //    var rnd = new SysRandom();
    //    var randomEntry = colors.ElementAt(rnd.Next(0, colors.Count));
    //    return randomEntry.Key;
    //}

    public Color[] GetRandomShades(int nDarker, int nLighter)
    {
        List<string> workable = workableColors[(nDarker, nLighter)]; 

        if (workable.Count == 0)
        {
            Debug.LogError($"ERROR: No colors have {nDarker} darker and {nLighter} lighter");
            return null;
        }
        else
        {
            return GetShades(workable[Random.Range(0, workable.Count)], nDarker, nLighter);
        }
    }

}
