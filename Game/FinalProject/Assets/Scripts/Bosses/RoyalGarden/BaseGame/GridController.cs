using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

namespace FinalProject.Assets.Scripts.Bosses.RoyalGarden
{
    [RequireComponent(typeof(Grid))]
    public class GridController : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        private string playerSymbol;
        private string aiSymbol;
        [SerializeField] private AiGridBehaviour aiGridBehaviour;
        public Grid grid;
        [SerializeField] private bool useAi;



        public Action AiTurn;
        public Action<Square> PlayedTurn;

        void Awake()
        {
            playerSymbol = gameManager.playerSymbol;
            aiSymbol = gameManager.aiSymbol;

            grid = GetComponent<Grid>();
        }

        void Start()
        {
            foreach (var square in grid.squares)
            {
                square.Clicked += OnSquareClick;
            }
        }


        public void SelectSquare(Square square, string symbol)
        {
            if (!grid.HasWinner) square.SetSymbol(symbol);

            gameManager.CheckWinner(grid);


            PlayedTurn?.Invoke(square);
            NextTurn();
            /*else// if (grid.SquaresAvailable())
            {
                NextTurn();
            }*/
        }

        public void OnSquareClick(Square square)
        {
            // Checks if it's Player's turn
            if (gameManager.currentTurn == playerSymbol && !square.occupied)
            {
                SelectSquare(square, playerSymbol);
            }
        }

        public void NextTurn()
        {
            gameManager.currentTurn = gameManager.currentTurn == playerSymbol? aiSymbol : playerSymbol;

            if (gameManager.currentTurn == aiSymbol)
            {
                AiTurn?.Invoke();
            }

        }
    }
}