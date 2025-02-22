using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace FroguesFramework
{
    [RequireComponent(typeof(BaseAbility))]
    public class AbilityDataForButton : MonoBehaviour
    {
        [SerializeField] private Material material;
        [SerializeField] private string abilityName;
        [SerializeField] private List<AbilityDescriptionTag> shortDataTags;
        [SerializeField] private List<AbilityDescriptionTag> descriptionTags;
        private BaseAbility ability;
        private Dictionary<string, Func<string>> dataByKeyWords = new Dictionary<string, Func<string>>();

        public Material Material => material;
        public string AbilityName => abilityName;
        public string ShortData => GetShortData();
        public string Description => GetDescription();

        private void Awake()
        {
            ability = GetComponent<BaseAbility>();

            dataByKeyWords.Add("{value}", () => (ability as IAbleToReturnSingleValue).GetValue().ToString());
            dataByKeyWords.Add("{value 2}", () => (ability as IAbleToReturnSecondSingleValue).GetSecondValue().ToString());

            dataByKeyWords.Add("{max blood points mod}", () => (ability as IAbleToModifyMaxBloodPoints).GetModificatorForMaxBloodPoints().ToString());
            dataByKeyWords.Add("{max hp mod}", () => (ability as IAbleToModifyMaxHP).GetModificatorForMaxHP().ToString());

            dataByKeyWords.Add("{range}", () => (ability as IAbleToReturnRange).ReturnRange().ToString());
            dataByKeyWords.Add("{alternative range}", () => (ability as IAbleToHaveAlternativeRange).GetAlternativeRange().ToString());

            dataByKeyWords.Add("{cooldown after use}", () => (ability as IAbleToHaveCooldown).GetCooldownAfterUse().ToString());
            dataByKeyWords.Add("{cooldown after start}", () => (ability as IAbleToHaveCooldown).GetCooldownAfterStart().ToString());
            dataByKeyWords.Add("{max charges}", () => (ability as IAbleToHaveCooldown).GetMaxCharges().ToString());

            dataByKeyWords.Add("{action points cost}", () => (ability as IAbleToCost).GetActionPointsCost().ToString());
            dataByKeyWords.Add("{blood points cost}", () => (ability as IAbleToCost).GetBloodPointsCost().ToString());
            dataByKeyWords.Add("{health points cost}", () => (ability as IAbleToCost).GetHealthCost().ToString());

            dataByKeyWords.Add("{default damage value}", () => (ability as IAbleToDealDamage).GetDefaultDamage().ToString());
            dataByKeyWords.Add("{calculated damage value}", () => IntToStringByCompareValues((ability as IAbleToDealDamage).CalculateDamage(), (ability as IAbleToDealDamage).GetDefaultDamage()));
            dataByKeyWords.Add("{damage type}", () => (ability as IAbleToDealDamage).GetDamageType().ToString());

            dataByKeyWords.Add("{alternative default damage value}", () => (ability as IAbleToDealAlternativeDamage).GetDefaultAlternativeDamage().ToString());
            dataByKeyWords.Add("{calculated alternative damage value}", () => IntToStringByCompareValues((ability as IAbleToDealAlternativeDamage).CalculateAlternativeDamage(), (ability as IAbleToDealAlternativeDamage).GetDefaultAlternativeDamage()));
            dataByKeyWords.Add("{alternative damage type}", () => (ability as IAbleToDealAlternativeDamage).GetAlternativeDamageType().ToString());

            dataByKeyWords.Add("{current default damage value}", () => (ability as IAbleToReturnCurrentDamage).GetDefaultCurrentDamage().ToString());
            dataByKeyWords.Add("{calculated current damage value}", () => IntToStringByCompareValues((ability as IAbleToReturnCurrentDamage).GetCalculatedCurrentDamage(), (ability as IAbleToReturnCurrentDamage).GetDefaultCurrentDamage()));

            dataByKeyWords.Add("{effect value}", () => (ability as IAbleToApplyAnyModificator).GetModificatorValue().ToString());
            dataByKeyWords.Add("{effect delta}", () => (ability as IAbleToApplyAnyModificator).GetDeltaValueForEachTurn().ToString());
            dataByKeyWords.Add("{effect time}", () => (ability as IAbleToApplyAnyModificator).GetTimeToEndOfEffect().ToString());
            dataByKeyWords.Add("{effect constantly}", () => (ability as IAbleToApplyAnyModificator).GetEffectIsConstantly().ToString());

            dataByKeyWords.Add("{defence effect value}", () => (ability as IAbleToApplyDefenceModificator).GetDefenceModificatorValue().ToString());
            dataByKeyWords.Add("{defence effect delta}", () => (ability as IAbleToApplyDefenceModificator).GetdeltaOfDefenceValueForEachTurn().ToString());
            dataByKeyWords.Add("{defence effect time}", () => (ability as IAbleToApplyDefenceModificator).GetTimeToEndOfDefenceEffect().ToString());
            dataByKeyWords.Add("{defence effect constantly}", () => (ability as IAbleToApplyDefenceModificator).GetDefenceEffectIsConstantly().ToString());

            dataByKeyWords.Add("{strenght effect value}", () => (ability as IAbleToApplyStrenghtModificator).GetStrenghtModificatorValue().ToString());
            dataByKeyWords.Add("{strenght effect delta}", () => (ability as IAbleToApplyStrenghtModificator).GetDeltaOfStrenghtValueForEachTurn().ToString());
            dataByKeyWords.Add("{strenght effect time}", () => (ability as IAbleToApplyStrenghtModificator).GetTimeToEndOfStrenghtEffect().ToString());
            dataByKeyWords.Add("{strenght effect constantly}", () => (ability as IAbleToApplyStrenghtModificator).GetStrenghtEffectIsConstantly().ToString());

            dataByKeyWords.Add("{intelligence effect value}", () => (ability as IAbleToApplyIntelligenceModificator).GetIntelligenceModificatorValue().ToString());
            dataByKeyWords.Add("{intelligence effect delta}", () => (ability as IAbleToApplyIntelligenceModificator).GetDeltaOfIntelligenceValueForEachTurn().ToString());
            dataByKeyWords.Add("{intelligence effect time}", () => (ability as IAbleToApplyIntelligenceModificator).GetTimeToEndOfIntelligenceEffect().ToString());
            dataByKeyWords.Add("{intelligence effect constantly}", () => (ability as IAbleToApplyIntelligenceModificator).GetIntelligenceEffectIsConstantly().ToString());

            dataByKeyWords.Add("{dexterity effect value}", () => (ability as IAbleToApplyDexterityModificator).GetDexterityModificatorValue().ToString());
            dataByKeyWords.Add("{dexterity effect delta}", () => (ability as IAbleToApplyDexterityModificator).GetDeltaOfDexterityValueForEachTurn().ToString());
            dataByKeyWords.Add("{dexterity effect time}", () => (ability as IAbleToApplyDexterityModificator).GetTimeToEndOfDexterityEffect().ToString());
            dataByKeyWords.Add("{dexterity effect constantly}", () => (ability as IAbleToApplyDexterityModificator).GetDexterityEffectIsConstantly().ToString());

            dataByKeyWords.Add("{spikes effect value}", () => (ability as IAbleToApplySpikesModificator).GetSpikesModificatorValue().ToString());
            dataByKeyWords.Add("{spikes effect delta}", () => (ability as IAbleToApplySpikesModificator).GetdeltaOfSpikesValueForEachTurn().ToString());
            dataByKeyWords.Add("{spikes effect time}", () => (ability as IAbleToApplySpikesModificator).GetTimeToEndOfSpikesEffect().ToString());
            dataByKeyWords.Add("{spikes effect constantly}", () => (ability as IAbleToApplySpikesModificator).GetSpikesEffectIsConstantly().ToString());

            dataByKeyWords.Add("{immobilized effect time}", () => (ability as IAbleToApplyImmobilizedModificator).GetTimeToEndOfImmpobilizedEffect().ToString());

            dataByKeyWords.Add("{default block value}", () => (ability as IAbleToApplyBlock).GetDefaultBlockValue().ToString());
            dataByKeyWords.Add("{calculated block value}", () => IntToStringByCompareValues((ability as IAbleToApplyBlock).CalculateBlock(), (ability as IAbleToApplyBlock).GetDefaultBlockValue()));

            dataByKeyWords.Add("{default armor value}", () => (ability as IAbleToApplyArmor).GetDefaultArmorValue().ToString());
            dataByKeyWords.Add("{calculated armor value}", () => IntToStringByCompareValues((ability as IAbleToApplyArmor).CalculateArmor(), (ability as IAbleToApplyArmor).GetDefaultArmorValue()));

            dataByKeyWords.Add("{delta value}", () => (ability as IAbleToHaveDelta).GetDeltaValue().ToString());
            dataByKeyWords.Add("{step value}", () => (ability as IAbleToHaveDelta).GetStepValue().ToString());

            dataByKeyWords.Add("{alternative delta value}", () => (ability as IAbleToHaveAlternativeDelta).GetAlternativeDeltaValue().ToString());
            dataByKeyWords.Add("{alternative step value}", () => (ability as IAbleToHaveAlternativeDelta).GetAlternativeStepValue().ToString());

            dataByKeyWords.Add("{count}", () => (ability as IAbleToHaveCount).GetCount().ToString());

            dataByKeyWords.Add("{action point regeneration penalty}", () => (ability as IAbleToApplyActionPointsRegenerationPenalty).GetActionPointsRegenerationPenaltyValue().ToString());
        }

        private string GetShortData()
        {
            return GenerateDescription(shortDataTags, true);
        }

        private string GetDescription()
        {
            return GenerateDescription(descriptionTags, false);
        }

        private string GenerateDescription(List<AbilityDescriptionTag> tags, bool thereAreNewLinesBetweenTags)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var tag in tags)
            {
                string tagText = tag.DescriptionText;
                bool ignoreTag = false;

                foreach (var dataByKeyWord in dataByKeyWords)
                {
                    if (tagText.Contains(dataByKeyWord.Key))
                    {
                        string textToReplaceTag = dataByKeyWord.Value.Invoke();

                        for(int i = 0; i < tag.BlackListTags.Count; i++)
                        {
                            if (tag.BlackListTags[i] == dataByKeyWord.Key && tag.BlackListValues[i] == textToReplaceTag)
                            {
                                ignoreTag = true;
                            }
                        }

                        tagText = tagText.Replace(dataByKeyWord.Key, textToReplaceTag);
                    }
                }

                if (ignoreTag)
                    continue;

                stringBuilder.Append(tagText);

                if (thereAreNewLinesBetweenTags)
                    stringBuilder.Append("\n");
                else 
                    stringBuilder.Append(" ");
            }

            return stringBuilder.ToString();
        }

        private string IntToStringByCompareValues(int comparableValue, int targetValueToCompare)
        {
            if(comparableValue == targetValueToCompare)
                return comparableValue.ToString();

            string color = comparableValue > targetValueToCompare ? "#96c620" : "#e05454";
            return $"<color={color}>{comparableValue}</color>";
        }
    }
}