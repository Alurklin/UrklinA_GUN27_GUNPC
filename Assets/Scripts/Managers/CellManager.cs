using System;
using System.Collections.Generic;
using Netologia.Necro.Units;
using UnityEngine;

namespace Netologia.Necro.Managers
{
    public class CellManager : MonoBehaviour
    {
        private Dictionary<CellNeighbour, Cell> _neighbours;
        private Cell[] _cells;

        public event Action<Cell> OnCellClicked;

        private void Awake()
        {
            _cells = FindObjectOfType<Cell>();
            _neighbours = new Dictionary<CellNeighbour, Cell>(_cells.Length * 8);
            var positions = Array.ConvertAll(_cells, t => t.transform.position);
            var distance = 0f;
            for (int i = 0, iMax = _cells.Length; i < iMax; i++)
            {
                _cells[i].OnPointerClickEvent += OnCellClicked;
#if UNITY_EDITOR
                _cells[i].OnPointerClickEvent += DebugOnPointerClick;
#endif
                for (int j = 0, jMax = _cells.Length; j < jMax; j++)
                {
                    if (i == j) continue;
                    var source = positions[i];
                    var destination = positions[j];

                    var forward = destination.z.CompareTo(source.z);
                    var right = destination.x.CompareTo(source.x);
                    var type = (forward, right) switch
                    {
                        (1, 1) => NeighbourType.ForwardRight,
                        (1, 0) => NeighbourType.Forward,
                        (1, -1) => NeighbourType.ForwardLeft,
                        (0, 1) => NeighbourType.Right,
                        (0, -1) => NeighbourType.Left,
                        (-1, 1) => NeighbourType.BackwardRight,
                        (-1, 0) => NeighbourType.Backward,
                        (-1, -1) => NeighbourType.BackwardLeft,
                        _ => default
                    };
                    var key = new CellNeighbour(type, _cells[i]);
                    var check = _neighbours.TryGetValue(key,out var cell)
                        ? Vector3.Distance(source, cell.transform.position)
                }
            }
        }
    }
}