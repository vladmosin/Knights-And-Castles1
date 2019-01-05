using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class RoundBoardCreator : MonoBehaviour
    {
        public BoardStorage boardStorage;

        public Text testText;
        
        public void FillBoardStorage()
        {
            CheckeredBoard board = boardStorage.board;
            List<BoardStorageItem>[,] storageItems = boardStorage.boardTable;
            for (int col = 1; col <= board.width; col++)
            {
                for (int row = 1; row <= board.height; row++)
                {
                    storageItems[col, row] = new List<BoardStorageItem>();
                    Text essense = Instantiate(testText);
                    essense.gameObject.GetComponent<RectTransform>().SetParent(testText.gameObject.GetComponent<RectTransform>());
                    string text = "(" + col + "," + row + ")";
                    essense.gameObject.GetComponent<Text>().text = text;
                    storageItems[col, row].Add(new BoardStorageItem(
                        board.BoardButtons[row, col].GetComponent<BoardButton>(),
                        essense.gameObject));
                }
            }
            bool a = false;
        }
    }
}
