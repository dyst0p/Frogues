using System.Collections.Generic;
using UnityEngine;

namespace FroguesFramework
{
    public class IncreaseHpAndBloodAndAddStatEffects : PassiveAbility, IAbleToApplyStrenghtModificator, IAbleToApplyIntelligenceModificator, IAbleToApplyDexterityModificator, IAbleToApplyDefenceModificator, IAbleToApplySpikesModificator
        , IAbleToModifyMaxHP, IAbleToModifyMaxBloodPoints
    {
        [SerializeField] private int additionalHp;
        [SerializeField] private int additionalMaxBlood;
        [SerializeField] private List<StatEffect> effects;

        public override void Init(Unit unit)
        {
            base.Init(unit);
            _owner.Health.IncreaseMaxHp(additionalHp);

            if(additionalHp > 0)
                _owner.Health.TakeHealing(additionalHp);

            _owner.BloodPoints.IncreaseLimit(additionalMaxBlood);
            effects.ForEach(effect => _owner.Stats.AddStatEffect(effect));
        }

        public override void UnInit()
        {
            _owner.Health.IncreaseMaxHp(-additionalHp);
            _owner.BloodPoints.IncreaseLimit(-additionalMaxBlood);
            effects.ForEach(effect => _owner.Stats.RemoveStatEffect(effect));
            base.UnInit();
        }

        public int GetModificatorForMaxHP() => additionalHp;

        public int GetModificatorForMaxBloodPoints() => additionalMaxBlood;

        #region IAbleToApplyDefenceModificator
        public int GetDefenceModificatorValue() => Extensions.GetModificatorValue(effects, StatEffectTypes.defence);

        public int GetdeltaOfDefenceValueForEachTurn() => Extensions.GetDeltaValueOfModificatorForEachTurn(effects, StatEffectTypes.defence);

        public int GetTimeToEndOfDefenceEffect() => Extensions.GetTimeToEndOfEffect(effects, StatEffectTypes.defence);

        public bool GetDefenceEffectIsConstantly() => Extensions.GetEffectIsConstantly(effects, StatEffectTypes.defence);
        #endregion

        #region IAbleToApplyStrenghtModificator
        public int GetStrenghtModificatorValue() => Extensions.GetModificatorValue(effects, StatEffectTypes.strenght);

        public int GetDeltaOfStrenghtValueForEachTurn() => Extensions.GetDeltaValueOfModificatorForEachTurn(effects, StatEffectTypes.strenght);

        public int GetTimeToEndOfStrenghtEffect() => Extensions.GetTimeToEndOfEffect(effects, StatEffectTypes.strenght);

        public bool GetStrenghtEffectIsConstantly() => Extensions.GetEffectIsConstantly(effects, StatEffectTypes.strenght);
        #endregion

        #region IAbleToApplyIntelligenceModificator
        public int GetIntelligenceModificatorValue() => Extensions.GetModificatorValue(effects, StatEffectTypes.intelegence);

        public int GetDeltaOfIntelligenceValueForEachTurn() => Extensions.GetDeltaValueOfModificatorForEachTurn(effects, StatEffectTypes.intelegence);

        public int GetTimeToEndOfIntelligenceEffect() => Extensions.GetTimeToEndOfEffect(effects, StatEffectTypes.intelegence);

        public bool GetIntelligenceEffectIsConstantly() => Extensions.GetEffectIsConstantly(effects, StatEffectTypes.intelegence);
        #endregion

        #region IAbleToApplyDexterityModificator
        public int GetDexterityModificatorValue() => Extensions.GetModificatorValue(effects, StatEffectTypes.dexterity);

        public int GetDeltaOfDexterityValueForEachTurn() => Extensions.GetDeltaValueOfModificatorForEachTurn(effects, StatEffectTypes.dexterity);

        public int GetTimeToEndOfDexterityEffect() => Extensions.GetTimeToEndOfEffect(effects, StatEffectTypes.dexterity);

        public bool GetDexterityEffectIsConstantly() => Extensions.GetEffectIsConstantly(effects, StatEffectTypes.dexterity);
        #endregion

        #region IAbleToApplySpikesModificator
        public int GetSpikesModificatorValue() => Extensions.GetModificatorValue(effects, StatEffectTypes.spikes);

        public int GetdeltaOfSpikesValueForEachTurn() => Extensions.GetDeltaValueOfModificatorForEachTurn(effects, StatEffectTypes.spikes);

        public int GetTimeToEndOfSpikesEffect() => Extensions.GetTimeToEndOfEffect(effects, StatEffectTypes.spikes);

        public bool GetSpikesEffectIsConstantly() => Extensions.GetEffectIsConstantly(effects, StatEffectTypes.spikes);
        #endregion
    }
}   