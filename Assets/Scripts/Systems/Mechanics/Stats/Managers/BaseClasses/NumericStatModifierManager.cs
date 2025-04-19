using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NumericStatModifierManager : StatModifierManager
{
    [Header("Lists - Runtime Filled")]
    [SerializeField] protected List<NumericStatModifier> numericStatModifiers;

    public List<NumericStatModifier> NumericStatModifiers => numericStatModifiers;


    #region In-Line Methods
    public override bool HasStatModifiers() => numericStatModifiers.Count > 0;
    public override int GetStatModifiersQuantity() => numericStatModifiers.Count;

    protected override StatValueType GetStatValueType() => StatValueType.Numeric;

    #endregion

    #region Add/Remove Stat Modifiers
    public override void AddStatModifiers(string originGUID, IHasEmbeddedStats embeddedStatsHolder)
    {
        if (originGUID == "")
        {
            if (debug) Debug.Log("GUID is empty. StatModifiers will not be added");
            return;
        }

        int statsAdded = 0;

        foreach (NumericEmbeddedStat numericEmbeddedStat in embeddedStatsHolder.GetNumericEmbeddedStats())
        {
            if (AddNumericStatModifier(originGUID, numericEmbeddedStat)) statsAdded++;
        }

        if (statsAdded > 0) UpdateStats();
    }

    protected bool AddNumericStatModifier(string originGUID, NumericEmbeddedStat numericEmbeddedStat)
    {
        if (numericEmbeddedStat == null)
        {
            if (debug) Debug.Log("NumericEmbeddedStat is null. StatModifier will not be added");
            return false;
        }

        if (numericEmbeddedStat.GetStatValueType() != GetStatValueType()) return false; //If not numeric, return false

        NumericStatModifier numericStatModifier = new NumericStatModifier {originGUID = originGUID, numericStatType = numericEmbeddedStat.numericStatType, numericStatModificationType = numericEmbeddedStat.numericStatModificationType, value = numericEmbeddedStat.value};

        numericStatModifiers.Add(numericStatModifier);

        return true;
    }

    public override void RemoveStatModifiersByGUID(string originGUID)
    {
        if (originGUID == "")
        {
            if (debug) Debug.Log("GUID is empty. StatModifiers will not be removed");
            return;
        }

        int removedStats = numericStatModifiers.RemoveAll(statModifier => statModifier.originGUID == originGUID);

        if (removedStats > 0) UpdateStats();
    }
    #endregion

    public void SetStatList(List<NumericStatModifier> setterList) => numericStatModifiers.AddRange(setterList);
}