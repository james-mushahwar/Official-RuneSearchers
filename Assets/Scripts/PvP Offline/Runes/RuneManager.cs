using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneManager : MonoBehaviour
{
    [Header("Rune placeholder")]
    [SerializeField]
    private GameObject runePlaceholder;

    [Header("Rune class lists")]
    [SerializeField]
    private List<RuneAppearance> runesAppearancesList;
    [SerializeField]
    private List<RuneClass> runesClassesList;

    [Header("Rune settings")]
    [SerializeField]
    private RuneSettings runeSettings;

    public RuneSettings RuneSettings => runeSettings;

    public GameObject InstantiateRune()
    {
        GameObject newRune = Instantiate(runePlaceholder);
        InplayRune newInPlayRune = newRune.GetComponent<InplayRune>();

        RuneAppearance randomRuneAppearance = runesAppearancesList[Random.Range(0, runesAppearancesList.Count)];
        RuneClass randomRuneClass = runesClassesList[Random.Range(0, runesClassesList.Count)];
        RuneType runeType = new RuneType(randomRuneClass, randomRuneAppearance);

        newInPlayRune.SetUpRuneType(runeType);

        Vector2Int runeStats = GetRuneStats(randomRuneAppearance.RuneSize.Size);
        newInPlayRune.SetupRuneStats(runeStats.x, runeStats.y);

        newRune.GetComponent<SpriteRenderer>().sprite = randomRuneAppearance.RuneInPlaySprite;
        Color newColor = ColourRuneSprite(newInPlayRune);
        newRune.GetComponent<SpriteRenderer>().color = new Color(newColor.r, newColor.g, newColor.b);

        return newRune;
    }

    private Color ColourRuneSprite(InplayRune newInplayRune)
    {
        RuneClass runeClass = newInplayRune.RuneType.RuneClass;

        switch (runeClass)
        {
            case RuneClass.Red:
                return runeSettings.RedRuneColour;
            case RuneClass.Yellow:
                return runeSettings.YellowRuneColour;
            case RuneClass.Blue:
            default:
                return runeSettings.BlueRuneColour;
        }
    }

    private Vector2Int GetRuneStats(RuneSizeClass size)
    {
        switch (size)
        {
            case RuneSizeClass.Large:
                return runeSettings.LargeRuneStats;
            case RuneSizeClass.Medium:
                return runeSettings.MediumRuneStats;
            default:
                return runeSettings.SmallRuneStats;
        }
    }
}
