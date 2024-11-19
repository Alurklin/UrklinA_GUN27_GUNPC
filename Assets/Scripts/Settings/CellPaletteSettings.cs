using UnityEngine;

namespace Netologia.Necro.Settings
{
    [CreateAssetMenu(fileName = "New CellPalleteSettings", menuName = "Settings/CellPaletteSettings", order = 52)]
    public class CellPaletteSettings : ScriptableObject
    {
        [field: SerializeField, Space(20f)]
        [field: Tooltip("������ ��� ��������� ������")]

        public Material SelectCell (get; private set;)
        [field: SerializeField]
        [field: Tooltip("������ ��������� ��� ������������")]

        public Material MoveCell(get; private set;)
        [field: SerializeField]
        [field: Tooltip("������ ��������� ��� �����")]

        public Material AttackCell(get; private set;)
        [field: SerializeField]
        [field: Tooltip("������ ��������� ��� ����� � ��� ������������")]

        public Material MoveAndAttackCell (get; private set;)
    }
}
