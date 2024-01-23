using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FroguesFramework
{
    public class AbilitiesPanelHealthBar : BaseHealthBar
    {
        [SerializeField] private GameObject blockIcon;
        [SerializeField] private GameObject armorIcon;
        [SerializeField] private GameObject spikesIcon;

        [SerializeField] private TextMeshProUGUI healthTextField;
        [SerializeField] private TextMeshProUGUI blockTextField;
        [SerializeField] private TextMeshProUGUI armorTextField;
        [SerializeField] private TextMeshProUGUI spikesTextField;

        [SerializeField] private AbilityDescriptionTag blockMechanicDescription;
        [SerializeField] private AbilityDescriptionTag armorMechanicDescription;
        [SerializeField] private AbilityDescriptionTag spikesMechanicDescription;

        protected override void Redraw()
        {
            base.Redraw();

            blockIcon.SetActive(health.TemporaryBlock != 0);
            armorIcon.SetActive(health.PermanentBlock != 0);
            spikesIcon.SetActive(stats.Spikes != 0);

            healthTextField.text = (health.CurrentHp).ToString();
            blockTextField.text = (health.TemporaryBlock).ToString();
            armorTextField.text = (health.PermanentBlock).ToString();
            spikesTextField.text = (stats.Spikes).ToString();

            resizableParents.ForEach(resizableParent => LayoutRebuilder.ForceRebuildLayoutImmediate(resizableParent));
        }

        public void ShowHealthHint()
        {
            EntryPoint.Instance.AbilityHint.Init("��������", GenerateHealthStatsString(), "", transform, new Vector2(0.5f, 0), Vector2.up * 36);
            EntryPoint.Instance.AbilityHint.EnableContent(true, true);
        }

        public void ShowBlockHint()
        {
            EntryPoint.Instance.AbilityHint.Init("����", blockMechanicDescription.DescriptionText, "", blockIcon.transform, new Vector2(0.5f, 0), Vector2.up * 36);
            EntryPoint.Instance.AbilityHint.EnableContent(true, true);
        }

        public void ShowArmorHint()
        {
            EntryPoint.Instance.AbilityHint.Init("�����", armorMechanicDescription.DescriptionText, "", armorIcon.transform, new Vector2(0.5f, 0), Vector2.up * 36);
            EntryPoint.Instance.AbilityHint.EnableContent(true, true);
        }

        public void ShowSpikesHint()
        {
            EntryPoint.Instance.AbilityHint.Init("����", spikesMechanicDescription.DescriptionText, "", spikesIcon.transform, new Vector2(0.5f, 0), Vector2.up * 36);
            EntryPoint.Instance.AbilityHint.EnableContent(true, true);
        }

        public void HideHint()
        {
            EntryPoint.Instance.AbilityHint.EnableContent(false);
        }

        private string GenerateHealthStatsString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"������� �����: {health.CurrentHp}")
                .AppendLine($"������������ �����: {health.MaxHp}");

            return sb.ToString();
        }
    }
}