﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    [ExecuteInEditMode]
    public class CheckeredBoard : MonoBehaviour
    {
        public Canvas ParentCanvas;
        public GameObject PatternButtonGO;
        public GameObject Parent;

        public int width = 5;
        public int height = 5;
        private float spaceBetweenButtons = -2.44f; //buttonWidth/20;

        private float buttonWidth;
        private float buttonHeight;
        private Button patternButton;

        private Button[,] boardButtons;

        public Button[,] BoardButtons
        {
            get
            {
                return boardButtons;
            }
        }

        void Awake()
        {
            DeleteButtons();
            boardButtons = new Button[width + 1, height + 1];
            patternButton = PatternButtonGO.GetComponent<Button>();
            buttonWidth = patternButton.GetComponent<RectTransform>().rect.width;
            buttonHeight = patternButton.GetComponent<RectTransform>().rect.height;
            //Debug.Log(PatternButtonGO.GetComponent<RectTransform>().position);
            CreateButtons();
        }

        public void EnableBoard()
        {
            /*var buttons = FindObjectsOfType(typeof(Button));
            foreach (Button button in buttons.Cast<Button>().Where(button => button.gameObject.name.Contains("Clone")))
            {
                button.gameObject.GetComponent<IBonus>().Enable();
            }*/
        }

        public void DisableBoard()
        {
            /*var buttons = FindObjectsOfType(typeof(Button));
            foreach (Button button in buttons.Cast<Button>().Where(button => button.gameObject.name.Contains("Clone")))
            {
                button.gameObject.GetComponent<IBonus>().Disable();
            }*/
        }

        public void DeleteButtons()
        {
            Debug.Log("Deleting buttons");
            var buttons = FindObjectsOfType(typeof(Button));
            //Debug.Log(buttons.Length);
            foreach (Button button in buttons.Cast<Button>().Where(button => button.gameObject.name.Contains("Clone")))
            {
                DestroyImmediate(button.gameObject);
            }
        }

        void OnApplicationQuit()
        {
            Debug.Log("Quit");
            DeleteButtons();
        }

        public Vector3 GetOffsetFromPattern(int currentColumn, int currentRow)
        {
            return new Vector3((currentColumn - 1) * (buttonWidth + spaceBetweenButtons),
                                                 (currentRow - 1) * (buttonHeight + spaceBetweenButtons));
        }

        //PatternButton размещается на месте левой нижней клетки
        public void CreateButtons()
        {
            Debug.Log("Creating buttons");
            for (int currentRow = 1; currentRow <= height; currentRow++)
            {
                for (int currentColumn = 1; currentColumn <= width; currentColumn++)
                {
                    Vector3 offset = GetOffsetFromPattern(currentColumn, currentRow);
                    Button newButton = Instantiate(patternButton);
                    RectTransform rectTransform = newButton.GetComponent<RectTransform>();

                    //This line seems to be useless (it doesn't change size)
                    rectTransform.rect.size.Set(buttonWidth, buttonHeight);

                    //Debug.Log(PatternButtonGO.GetComponent<RectTransform>().localPosition);
                    rectTransform.position = patternButton.transform.localPosition + offset;
                    rectTransform.SetParent(Parent.transform, false);

                    newButton.gameObject.SetActive(true);
                    newButton = InitButton(newButton, currentColumn, currentRow);

                    boardButtons[currentColumn, currentRow] = newButton;

                }
            }
        }

        private Button InitButton(Button newButton, int x, int y)
        {
            BoardButton boardButton = newButton.GetComponent<BoardButton>();
            boardButton.Initialize(x, y);
            return boardButton.button;
        }
    }
}