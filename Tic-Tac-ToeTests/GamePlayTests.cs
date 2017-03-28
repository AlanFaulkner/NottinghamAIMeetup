using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Tic_Tac_Toe.Tests
{
    [TestClass()]
    public class GamePlayTests
    {
        [TestMethod()]
        public void SetupGame_Test()
        {
            // arrange
            GamePlay Game = new GamePlay();
            List<int> ExpectedGameboard = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            // act
            Game.SetupGame();
            CollectionAssert.AreEqual(ExpectedGameboard, Game.Gameboard, "Failed to initalise game board");
        }

        [TestMethod()]
        public void SwitchPlayersTest()
        {
            GamePlay Game = new GamePlay();
            Game.CurrentPlayer = GamePlay.Player.Player1;

            // act
            Game.SwichPlayers();

            // assert
            Assert.AreEqual(GamePlay.Player.Player2, Game.CurrentPlayer, "Did not switch player");
        }

        // Possible winning states

        [TestMethod()]
        public void EndGame_Senarieo1()
        {
            // arragne
            GamePlay Game = new GamePlay();
            Game.GameboardColumns = 3;
            Game.GameboardRows = 3;
            Game.Gameboard = new List<int> { 1, 1, 1, 0, 0, 0, 0, 0, 0 };

            // assert
            Assert.IsTrue(Game.EndGame(), "Game did not end");
        }

        [TestMethod()]
        public void EndGame_Senarieo2()
        {
            // arragne
            GamePlay Game = new GamePlay();
            Game.GameboardColumns = 3;
            Game.GameboardRows = 3;
            Game.Gameboard = new List<int> { 0, 0, 0, 1, 1, 1, 0, 0, 0 };

            // assert
            Assert.IsTrue(Game.EndGame(), "Game did not end");
        }

        [TestMethod()]
        public void EndGame_Senarieo3()
        {
            // arragne
            GamePlay Game = new GamePlay();
            Game.GameboardColumns = 3;
            Game.GameboardRows = 3;
            Game.Gameboard = new List<int> { 0, 0, 0, 0, 0, 0, 1, 1, 1 };

            // assert
            Assert.IsTrue(Game.EndGame(), "Game did not end");
        }

        [TestMethod()]
        public void EndGame_Senarieo4()
        {
            // arragne
            GamePlay Game = new GamePlay();
            Game.GameboardColumns = 3;
            Game.GameboardRows = 3;
            Game.Gameboard = new List<int> { -1, -1, -1, 0, 0, 0, 0, 0, 0 };

            // assert
            Assert.IsTrue(Game.EndGame(), "Game did not end");
        }

        [TestMethod()]
        public void EndGame_Senarieo5()
        {
            // arragne
            GamePlay Game = new GamePlay();
            Game.GameboardColumns = 3;
            Game.GameboardRows = 3;
            Game.Gameboard = new List<int> { 0, 0, 0, -1, -1, -1, 0, 0, 0 };

            // assert
            Assert.IsTrue(Game.EndGame(), "Game did not end");
        }

        [TestMethod()]
        public void EndGame_Senarieo6()
        {
            // arragne
            GamePlay Game = new GamePlay();
            Game.GameboardColumns = 3;
            Game.GameboardRows = 3;
            Game.Gameboard = new List<int> { 0, 0, 0, 0, 0, 0, -1, -1, -1 };

            // assert
            Assert.IsTrue(Game.EndGame(), "Game did not end");
        }

        [TestMethod()]
        public void EndGame_Senarieo7()
        {
            // arragne
            GamePlay Game = new GamePlay();
            Game.GameboardColumns = 3;
            Game.GameboardRows = 3;
            Game.Gameboard = new List<int> { 1, 0, 0, 1, 0, 0, 1, 0, 0 };

            // assert
            Assert.IsTrue(Game.EndGame(), "Game did not end");
        }

        [TestMethod()]
        public void EndGame_Senarieo8()
        {
            // arragne
            GamePlay Game = new GamePlay();
            Game.GameboardColumns = 3;
            Game.GameboardRows = 3;
            Game.Gameboard = new List<int> { 0, 1, 0, 0, 1, 0, 0, 1, 0 };

            // assert
            Assert.IsTrue(Game.EndGame(), "Game did not end");
        }

        [TestMethod()]
        public void EndGame_Senarieo9()
        {
            // arragne
            GamePlay Game = new GamePlay();
            Game.GameboardColumns = 3;
            Game.GameboardRows = 3;
            Game.Gameboard = new List<int> { 0, 0, 1, 0, 0, 1, 0, 0, 1 };

            // assert
            Assert.IsTrue(Game.EndGame(), "Game did not end");
        }

        [TestMethod()]
        public void EndGame_Senarieo10()
        {
            // arragne
            GamePlay Game = new GamePlay();
            Game.GameboardColumns = 3;
            Game.GameboardRows = 3;
            Game.Gameboard = new List<int> { -1, 0, 0, -1, 0, 0, -1, 0, 0 };

            // assert
            Assert.IsTrue(Game.EndGame(), "Game did not end");
        }

        [TestMethod()]
        public void EndGame_Senarieo11()
        {
            // arragne
            GamePlay Game = new GamePlay();
            Game.GameboardColumns = 3;
            Game.GameboardRows = 3;
            Game.Gameboard = new List<int> { 0, -1, 0, 0, -1, 0, 0, -1, 0 };

            // assert
            Assert.IsTrue(Game.EndGame(), "Game did not end");
        }

        [TestMethod()]
        public void EndGame_Senarieo12()
        {
            // arragne
            GamePlay Game = new GamePlay();
            Game.GameboardColumns = 3;
            Game.GameboardRows = 3;
            Game.Gameboard = new List<int> { 0, 0, -1, 0, 0, -1, 0, 0, -1 };

            // assert
            Assert.IsTrue(Game.EndGame(), "Game did not end");
        }

        [TestMethod()]
        public void EndGame_Senarieo13()
        {
            // arragne
            GamePlay Game = new GamePlay();
            Game.GameboardColumns = 3;
            Game.GameboardRows = 3;
            Game.Gameboard = new List<int> { 1, 0, 0, 0, 1, 0, 0, 0, 1 };

            // assert
            Assert.IsTrue(Game.EndGame(), "Game did not end");
        }

        [TestMethod()]
        public void EndGame_Senarieo14()
        {
            // arragne
            GamePlay Game = new GamePlay();
            Game.GameboardColumns = 3;
            Game.GameboardRows = 3;
            Game.Gameboard = new List<int> { 0, 0, 1, 0, 1, 0, 1, 0, 0 };

            // assert
            Assert.IsTrue(Game.EndGame(), "Game did not end");
        }

        [TestMethod()]
        public void EndGame_Senarieo15()
        {
            // arragne
            GamePlay Game = new GamePlay();
            Game.GameboardColumns = 3;
            Game.GameboardRows = 3;
            Game.Gameboard = new List<int> { -1, 0, 0, 0, -1, 0, 0, 0, -1 };

            // assert
            Assert.IsTrue(Game.EndGame(), "Game did not end");
        }

        [TestMethod()]
        public void EndGame_Senarieo16()
        {
            // arragne
            GamePlay Game = new GamePlay();
            Game.GameboardColumns = 3;
            Game.GameboardRows = 3;
            Game.Gameboard = new List<int> { 0, 0, -1, 0, -1, 0, -1, 0, 0 };

            // assert
            Assert.IsTrue(Game.EndGame(), "Game did not end");
        }

        [TestMethod()]
        public void EndGame_Senarieo17()
        {
            // arragne
            GamePlay Game = new GamePlay();
            Game.GameboardColumns = 3;
            Game.GameboardRows = 3;
            Game.Gameboard = new List<int> { 1, -1, 1,-1, 1, 0, 1, 0, -1 };

            // assert
            Assert.IsTrue(Game.EndGame(), "Game did not end");
        }

        [TestMethod()]
        public void EndGame_GameNotOver_test()
        {
            // arragne
            GamePlay Game = new GamePlay();
            Game.GameboardColumns = 3;
            Game.GameboardRows = 3;
            Game.Gameboard = new List<int> { 1, 0, 1, -1, 0, 1, 1, 1, -1 };

            // assert
            Assert.IsFalse(Game.EndGame(), "Game ended prematurly");
        }


        // Baisc AI Tests

        [TestMethod()]
        public void AIMoveTest_Random()
        {
            // arrange
            GamePlay Game = new GamePlay();
            Game.GameboardRows = 3;
            Game.GameboardColumns = 3;
            Game.Gameboard = new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 0 };
            List<int> ExpectedOutput = new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1 };

            // act
            Game.AIMove(GamePlay.Playertype.Random);

            // accert
            CollectionAssert.AreEqual(ExpectedOutput, Game.Gameboard, "Over wrote existing move");
        }

        [TestMethod()]
        public void AIEasy_Test_BlockRow()
        {
            // arrange
            GamePlay game = new GamePlay();
            game.Gameboard = new List<int> { 1, 1, 0, 0, 0, 0, 0, 0, 0 };
            List<int> result = new List<int> { 1, 1, 1, 0, 0, 0, 0, 0, 0 };

            // act
            game.AIMove(GamePlay.Playertype.Easy);

            CollectionAssert.AreEqual(result, game.Gameboard, "Made incorrect move");
        }

        [TestMethod()]
        public void AIEasy_Test_BlockColumn()
        {
            // arrange
            GamePlay game = new GamePlay();
            game.Gameboard = new List<int> { 1, 0, 0, 1, 0, 0, 0, 0, 0 };
            List<int> result = new List<int> { 1, 0, 0, 1, 0, 0, 1, 0, 0 };

            // act
            game.AIMove(GamePlay.Playertype.Easy);

            CollectionAssert.AreEqual(result, game.Gameboard, "Made incorrect move");
        }

        [TestMethod()]
        public void AIEasy_Test_BlockDiagnol()
        {
            // arrange
            GamePlay game = new GamePlay();
            game.Gameboard = new List<int> { 1, 0, 0, 0, 1, 0, 0, 0, 0 };
            List<int> result = new List<int> { 1, 0, 0, 0, 1, 0, 0, 0, 1 };

            // act
            game.AIMove(GamePlay.Playertype.Easy);

            CollectionAssert.AreEqual(result, game.Gameboard, "Made incorrect move");
        }

        [TestMethod()]
        public void AIHard_Test_OpenMoveP1()
        {
            // arrange
            GamePlay game = new GamePlay();
            game.Gameboard = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            List<int> result = new List<int> { 1, 0, 0, 0, 0, 0, 0, 0, 0 };

            // act
            game.AIMove(GamePlay.Playertype.Hard);

            CollectionAssert.AreEqual(result, game.Gameboard, "Made incorrect move");
        }

        [TestMethod()]
        public void AIHard_Test_OpenMoveP2()
        {
            // arrange
            GamePlay game = new GamePlay();
            game.CurrentPlayer = GamePlay.Player.Player2;
            game.Gameboard = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            List<int> result = new List<int> { 0, 0, 0, 0, -1, 0, 0, 0, 0 };

            // act
            game.AIMove(GamePlay.Playertype.Hard);

            CollectionAssert.AreEqual(result, game.Gameboard, "Made incorrect move");
        }

        [TestMethod()]
        public void AIeasy_Test_OpenMove()
        {
            // arrange
            GamePlay game = new GamePlay();
            game.CurrentPlayer = GamePlay.Player.Player2;
            game.Gameboard = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            List<int> result = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            // act
            game.AIMove(GamePlay.Playertype.Easy);

            CollectionAssert.AreNotEqual(result, game.Gameboard, "did not make move");
        }

        [TestMethod()]
        public void AIeasy_Test_choosewinningoverblocking()
        {
            // arrange
            GamePlay game = new GamePlay();
            game.CurrentPlayer = GamePlay.Player.Player2;
            game.Gameboard = new List<int> { 1, 1, 0, -1, -1, 0, 0, 0, 0 };
            List<int> result = new List<int> { 1, 1, 0, -1, -1, -1, 0, 0, 0 };

            // act
            game.AIMove(GamePlay.Playertype.Easy);

            CollectionAssert.AreEqual(result, game.Gameboard, "choose to block over wining");
        }

        
        }
    }
