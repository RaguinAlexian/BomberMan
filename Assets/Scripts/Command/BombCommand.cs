using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCommand : ICommand
{
    public void Execute(PlayerController _player)
    {
        _player.Bomb();
    }

    public void Undo(PlayerController _player)
    {
        _player.Bomb();
    }
}
