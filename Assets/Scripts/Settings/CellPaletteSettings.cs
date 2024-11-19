using UnityEngine;

namespace Netologia.Necro.Settings
{
    [CreateAssetMenu(fileName = "New CellPalleteSettings", menuName = "Settings/CellPaletteSettings", order = 52)]
    public class CellPaletteSettings : ScriptableObject
    {
        [field: SerializeField, Space(20f)]
        [field: Tooltip("Клетка под выбранным юнитом")]

        public Material SelectCell (get; private set;)
        [field: SerializeField]
        [field: Tooltip("Клетка доступная для передвижения")]

        public Material MoveCell(get; private set;)
        [field: SerializeField]
        [field: Tooltip("Клетка доступная для атаки")]

        public Material AttackCell(get; private set;)
        [field: SerializeField]
        [field: Tooltip("Клетка доступная для атаки и для передвижения")]

        public Material MoveAndAttackCell (get; private set;)
    }
}
