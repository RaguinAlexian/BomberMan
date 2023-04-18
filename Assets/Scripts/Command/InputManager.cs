using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;


public class InputManager : MonoBehaviour
{
    public PlayerController Player;

    private ICommand UpKeyCommand;
    private ICommand DownKeyCommand;
    private ICommand LeftKeyCommand;
    private ICommand RightKeyCommand;
    private ICommand BombKeyCommand;

    private List<ICommand> _commands; 

    private void Awake()
    {
        UpKeyCommand = new UpCommand(1);
        DownKeyCommand = new DownCommand(1);
        LeftKeyCommand = new LeftCommand(1);
        RightKeyCommand = new RightCommand(1);
        BombKeyCommand = new BombCommand();
    }

    private void Start()
    {
        _commands = new List<ICommand>(); 
    }


    //PARTIE 3
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Z))
        {
            AddUpCommand();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            AddDownCommand();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            AddLeftCommand();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            AddRightCommand();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddBombCommand();
        }


        if (Input.GetKeyDown(KeyCode.O))
        {
            Undo();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            ExecuteAll();
        }
    }

    private void AddUpCommand()
    {
        var command = new UpCommand(1);
        AddCommandToList(command);
    }
    private void AddDownCommand()
    {
        var command = new DownCommand(1);
        AddCommandToList(command);
    }

    private void AddLeftCommand()
    {
        var command = new LeftCommand(1);
        AddCommandToList(command);
    }

    private void AddRightCommand()
    {
        var command = new RightCommand(1);
        AddCommandToList(command);
    }

    private void AddBombCommand()
    {
        var command = new BombCommand();
        AddCommandToList(command);
    }

    private void AddCommandToList(ICommand command)
    {
        _commands.Add(command);
        //command.Execute(Player);
    }

    private void Undo()
    {
        if (_commands.Count == 0) { return; }
        var commandsLastIndex = _commands.Count - 1;
        _commands[commandsLastIndex].Undo(Player);
        _commands.RemoveAt(commandsLastIndex);
    }

    private void ExecuteAll()
    {
        StartCoroutine(ExecuteAllCommands());
    }

    private IEnumerator ExecuteAllCommands()
    {
        foreach (var command in _commands)
        {
            command.Execute(Player);
            yield return new WaitForSeconds(1);
        }
    }
}


