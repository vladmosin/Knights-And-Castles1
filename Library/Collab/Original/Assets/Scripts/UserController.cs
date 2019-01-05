using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class UserController
    {
        private PlayGameState playGameState;
        private BoardStorage boardStorage;
        private TurnType turnType;

        private Dictionary<Army, bool> UserArmiesActive; // my version 

        private Vector2 choosedArmyCoordinates;
        private Dictionary<Vector2, Army> userArmies; // get army by position

        public UserController(TurnType turnType, BoardStorage storage,
                              PlayGameState playGameState, Dictionary<Vector2, Army> userArmies) // my version
        {
            this.turnType = turnType;
            boardStorage = storage;
            this.playGameState = playGameState;
            choosedArmyCoordinates = new Vector2(-1, -1); // my version
            this.userArmies = userArmies; // my version
            foreach (KeyValuePair<Vector2, Army> armyAndPosition in userArmies) // my version
            {
                UserArmiesActive.Add(armyAndPosition.Value, true); // my version
            }
            // for each army we want to know is it active or not
        }

        public void OnButtonClick(BoardButton boardButton)
        {
            if (choosedArmyCoordinates.x != -1)
            {
                if (isNeighbour(boardButton))
                {
                    MoveArmy(userArmies[choosedArmyCoordinates], boardButton);
                }
            }
            else
            {
                Vector2 boardCell = new Vector2(boardButton.boardX, boardButton.boardY);
                if (userArmies.ContainsKey(boardCell))
                {
                    choosedArmyCoordinates = boardCell;
                }
            }
       /*     if (boardButton.boardX == 1)
            {
                FinishTurn();
            }
            */
        }

        public void OnEndTurnButtonClick()
        {
            playGameState.OnFinishTurn(turnType);
        }

        private void MoveArmy(Army removableArmy, BoardButton destinationBoardButton)
        {
            //TODO
        }

        private bool isNeighbour(BoardButton boardButton)
        {
            if (boardButton.boardX == choosedArmyCoordinates.x &&
                Math.Abs(boardButton.boardY - choosedArmyCoordinates.y) == 1)
            {
                return true;
            }
            if (boardButton.boardY == choosedArmyCoordinates.y &&
                Math.Abs(boardButton.boardX - choosedArmyCoordinates.x) == 1)
            {
                return true;
            }
            return false;
        }

        public void Disable()
        {
        }

        public void Enable()
        {
        
        }

        /*public void FinishTurn()
        {
            playGameState.OnFinishTurn(turnType);
        }*/
    }
}