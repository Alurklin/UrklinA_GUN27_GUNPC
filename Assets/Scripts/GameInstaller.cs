using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Netologia.Necro.Settings;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    private Controls _controls;

    [SerializeField]
    private CellManager cellManager;
    [SerializeField]
    private SceneController _controller;

    [SerializeField, Space(15f)]
    private CellPaletteSettings _cellPaletteSettings;

    public override void InstallBindings()
    {
        _controls = new Controls();
        _controls.Game.Enable();
        Container.BindInstance(_controls.Game).AsSingle;

        Container.BindInstance(_cellManager).AsSingle;
        Container.BindInstance(_controller).AsSingle;

        Container.BindInstance(_cellPaletteSettings).AsSingle;

        //for test
        _cellManager.OnCellClicked += CellManagerOnOncellClicked;
    }

    private void CellManagerOnOncellClicked(Cell obj)
    {
        obj.SetSelect(_cellPaletteSettings.SelectCell);
    }

    private void OnDestroy()
    {
        _controls.Dispose();
    }
}
