using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Tic_Tac_Toe
{
    public partial class userInterface : Form
    {
        private GamePlay Game = new GamePlay();
        private List<Button> GameBoard = new List<Button> { };

        public userInterface()
        {
            InitializeComponent();
            InitGameBoard();
            Player1.DataSource = Enum.GetNames(typeof(GamePlay.Playertype));
            Player2.DataSource = Enum.GetNames(typeof(GamePlay.Playertype));
        }

        private void InitGameBoard()
        {
            int Horizantal = 30;
            int Vertical = 50;

            for (int i = 0; i < Game.GameboardRows; i++)
            {
                for (int j = 0; j < Game.GameboardColumns; j++)
                {
                    Button Square = new Button();
                    Square.Size = new Size((228 / Game.GameboardRows), (228 / Game.GameboardColumns));
                    Square.Location = new Point(Horizantal, Vertical);
                    Square.FlatStyle = FlatStyle.Flat;
                    Square.FlatAppearance.BorderColor = Color.Black;
                    Square.FlatAppearance.BorderSize = 1;
                    Square.BackColor = Color.LightGray;
                    Square.Tag = Game.GameboardRows * i + j;
                    Square.Click += new System.EventHandler(this.SelectedSquare);
                    Square.Enabled = false;
                    GameBoard.Add(Square);
                    Controls.Add(Square);
                    Horizantal += ((228 / Game.GameboardRows) + 1);
                }
                Horizantal = 30;
                Vertical += ((228 / Game.GameboardColumns) + 1);
            }
        }

        private void SelectedSquare(object sender, EventArgs e)
        {
            var Square = (Button)sender;
            Game.Gameboard[Convert.ToInt32(Square.Tag)] = (int)Game.CurrentPlayer;
            UpdateGameBoard();
            if (Game.EndGame()) { Endgame(); }
            else
            {
                Game.SwichPlayers();
                if (Game.CurrentPlayer == GamePlay.Player.Player1 && Game.Player1Type != GamePlay.Playertype.Human) { AIMove(); }
                else if (Game.CurrentPlayer == GamePlay.Player.Player2 && Game.Player2Type != GamePlay.Playertype.Human) { AIMove(); }
            }
        }
        
        private void playbutton_Click(object sender, EventArgs e)
        {
            playbutton.Enabled = false;
            Player1.Enabled = false;
            Player2.Enabled = false;

            Game.SetupGame();
            UpdateGameBoard();
            
            Game.Player1Type = (GamePlay.Playertype)Enum.Parse(typeof(GamePlay.Playertype), Player1.SelectedValue.ToString());
            Game.Player2Type = (GamePlay.Playertype)Enum.Parse(typeof(GamePlay.Playertype), Player2.SelectedValue.ToString());

            if (Game.CurrentPlayer == GamePlay.Player.Player1 && Game.Player1Type != GamePlay.Playertype.Human) { AIMove(); }
            else if (Game.CurrentPlayer == GamePlay.Player.Player2 && Game.Player2Type != GamePlay.Playertype.Human) { AIMove(); }
        }

        private void AIMove()
        {
            backgroundWorker_AIMove.RunWorkerAsync();
        }

        private void backgroundWorker_AIMove_DoWork(object sender, DoWorkEventArgs e)
        {
            if (Game.CurrentPlayer == GamePlay.Player.Player1) { Game.AIMove(Game.Player1Type); }
            else if (Game.CurrentPlayer == GamePlay.Player.Player2) { Game.AIMove(Game.Player2Type); }
        }

        private void backgroundWorker_AIMove_Complete(object sender, RunWorkerCompletedEventArgs e)
        {
            UpdateGameBoard();
            if (Game.EndGame()) { Endgame(); }
            else
            {
                Game.SwichPlayers();
                if (Game.CurrentPlayer == GamePlay.Player.Player1 && Game.Player1Type != GamePlay.Playertype.Human) { AIMove(); }
                else if (Game.CurrentPlayer == GamePlay.Player.Player2 && Game.Player2Type != GamePlay.Playertype.Human) { AIMove(); }
            }
        }

        private void Endgame()
        {
            MessageBox.Show("Game Over!");
            for (int square = 0; square < GameBoard.Count; square++) { GameBoard[square].Enabled = false; }
            playbutton.Enabled = true;
            Player1.Enabled = true;
            Player2.Enabled = true;
        }

        private void UpdateGameBoard()
        {
            for (int i = 0; i < Game.Gameboard.Count(); i++)
            {
                if (Game.Gameboard[i] == (int)GamePlay.Player.Player1)
                {
                    GameBoard[i].BackgroundImage = Tic_Tac_Toe.Properties.Resources.O;
                    GameBoard[i].BackgroundImageLayout = ImageLayout.Stretch;
                    GameBoard[i].Enabled = false;
                }
                else if (Game.Gameboard[i] == (int)GamePlay.Player.Player2)
                {
                    GameBoard[i].BackgroundImage = Tic_Tac_Toe.Properties.Resources.X;
                    GameBoard[i].BackgroundImageLayout = ImageLayout.Stretch;
                    GameBoard[i].Enabled = false;
                }
                else
                {
                    GameBoard[i].BackgroundImage = null;
                    GameBoard[i].Enabled = true;
                }
            }
        }
    }
}