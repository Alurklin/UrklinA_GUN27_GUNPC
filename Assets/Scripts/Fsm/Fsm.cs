using System;
using System.Collections.Generic;
using FSM.Scripts;
using UnityEngine;

public class Fsm
{
    private FsmState StateCurrent { get; set; }

    private Dictionary<Type, FsmState> _states = new Dictionary<Type, FsmState>();

    public Animator _animator;  // ������ ������ � Animator

    public Fsm(Animator animator)  // ��������� Animator ����� �����������
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

        // ��������� �������� �� null ��� StateCurrent
        if (StateCurrent != null && StateCurrent.GetType() == type)
        {
            return; // ��� � ���� ���������, ������ �� ������
        }

        if (_states.TryGetValue(type, out var newState))
        {
            StateCurrent?.Exit(); // �������� Exit ��� �������� ���������, ���� ��� �� null

            StateCurrent = newState; // ������������� ����� ���������

            StateCurrent.Enter(); // �������� Enter ��� ������ ���������
        }
        else
        {
            // ��������� ������, ���� ��������� �� ������� � �������
            throw new InvalidOperationException($"State of type {type} not found in FSM.");
        }
    }

    // ����� ��� ��������� ��������� �� ����
    public T GetState<T>() where T : FsmState
    {
        // ���������, ���������� �� ��������� � ������� �� ���� T
        if (_states.TryGetValue(typeof(T), out var state))
        {
            return (T)state; // ����������� � ���� T � ����������
        }
        else
        {
            // ���� ��������� �� �������, ����������� ����������
            throw new InvalidOperationException($"State of type {typeof(T)} not found in FSM.");
        }
    }

    public void Update()
    {
        StateCurrent?.Update();
    }
}
