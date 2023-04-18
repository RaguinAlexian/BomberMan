using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using DG.Tweening; 


public class LeftCommand : ICommand
{
    private float _moveDistance;

    private Vector3 _previousPosition;

    public LeftCommand(float _distance)
    {
        _moveDistance = _distance;
    }

    public void Execute(PlayerController _player)
    {
        _previousPosition = _player.transform.position;

        var nextPosition = _previousPosition + _player.transform.right * -1 * _moveDistance;
        //_player.transform.DOMove(nextPosition, 0.5f); 


    }

    public void Undo(PlayerController _player)
    {
        var nextPosition = _previousPosition;
        //_player.transform.DOMove(nextPosition, 0.5f);
    }
}
