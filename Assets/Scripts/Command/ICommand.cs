using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand
{
    void Execute(PlayerController _player);

    void Undo(PlayerController _player); 
}
