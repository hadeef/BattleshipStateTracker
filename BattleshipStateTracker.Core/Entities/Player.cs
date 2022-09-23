﻿namespace BattleshipStateTracker.Core
{
    public class Player : IPlayer
    {
        public Player()
        {
            Board = new Board();
            Board.PlaceShip(new Destroyer());
        }

        public IBoard Board { get; }
    }
}
