using System;
using static GlobalEnums;

public interface IPlayerMove
{
    public event Action<MoveType> MovePlayed;
    public void Init();
    public void Deactivate();
}