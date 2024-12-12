using System;
using System.Collections.Generic;
using FSM.Scripts;
using UnityEngine;

public class Fsm
{
    private FsmState StateCurrent { get; set; }

    private Dictionary<Type, FsmState> _states = new Dictionary<Type, FsmState>();

    public Animator _animator;  // Теперь доступ к Animator

    public Fsm(Animator animator)  // Принимаем Animator через конструктор
    {
        _animator = animator;
    }
    public void AddState(FsmState state)
    {
        _states.Add(state.GetType(), state);
    }

    public void SetState<T>() where T : FsmState
    {
        var type = typeof(T);

        // Добавляем проверку на null для StateCurrent
        if (StateCurrent != null && StateCurrent.GetType() == type)
        {
            return; // Уже в этом состоянии, ничего не делаем
        }

        if (_states.TryGetValue(type, out var newState))
        {
            StateCurrent?.Exit(); // Вызываем Exit для текущего состояния, если оно не null

            StateCurrent = newState; // Устанавливаем новое состояние

            StateCurrent.Enter(); // Вызываем Enter для нового состояния
        }
        else
        {
            // Логгируем ошибку, если состояние не найдено в словаре
            throw new InvalidOperationException($"State of type {type} not found in FSM.");
        }
    }

    public void Update()
    {
        StateCurrent?.Update();
    }
}
