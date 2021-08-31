using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InplayUnit : MonoBehaviour
{
    [SerializeField]
    private Unit unit;
    [SerializeField]
    private TextMeshProUGUI attackText;
    [SerializeField]
    private TextMeshProUGUI defenseText;

    [Header("In-battle stats")]
    [SerializeField]
    private UnitStats currentUnitStats;
    [SerializeField]
    private UnitState unitState = new UnitState(); 
    [SerializeField]
    private HeldRunes heldRunes = new HeldRunes();

    public UnitState UnitState => unitState;
    public Unit Unit
    {
        get => unit;
        set => unit = value;
    }
    public UnitStats CurrentUnitStats => currentUnitStats;
    public HeldRunes HeldRunes
    {
        get => heldRunes;
        set => heldRunes = value;
    }

    private void Start()
    {
        currentUnitStats = (UnitStats)ScriptableObject.CreateInstance("UnitStats");
        currentUnitStats.CopyStats(unit.UnitStats);
        currentUnitStats.UnitAbility = currentUnitStats.GenerateUnitClassAbility();
        gameObject.GetComponent<SpriteRenderer>().sprite = unit.UnitAppearance.UnitSprite;
    }

    private void Update()
    {
        attackText.text = currentUnitStats.AttackPoints.ToString();
        defenseText.text = currentUnitStats.HealthPoints.ToString();
    }
}
