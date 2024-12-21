using UnityEngine;

namespace Netologia.Necro.Settings
{
    [CreateAssetMenu(fileName = "New CellPaletteSettings", menuName = "Settings/CellPaletteSettings", order = 52)]

    public class CellPalleteSettings : ScriptableObject
    {
        [field: SerializeField, Space(20f)]
        [field: Tooltip("������ ��� ��������� ������")]
        public Material SelectCell { get; private set; }

        [field: SerializeField, Space(20f)]
        [field: Tooltip("������ ��� ��������� ������")]
        public Material MoveCell { get; private set; }

        [field: SerializeField, Space(20f)]
        [field: Tooltip("������ ��� ��������� ������")]
        public Material AttackCell { get; private set; }

        [field: SerializeField, Space(20f)]
        [field: Tooltip("������ ��� ��������� ������")]
        public Material MoveAndAttackCell { get; private set; }
    }
}


