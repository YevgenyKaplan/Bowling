using System;
using Bowling;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void InitializeGame()
        {
            Game game = new Game();

            Assert.IsNotNull(game.GetFrame(9));
            Assert.ThrowsException<Exception>(() => game.GetFrame(10));
            Assert.AreEqual(game.GameScore, 0);
            Assert.AreSame(game.CurrentFrame, game.GetFrame(0));
            Assert.AreEqual(game.CurrentFrame.FrameScore, 0);
            Assert.AreEqual(game.CurrentFrame.CurrentThrowNumber, 1);
        }

        [TestMethod]
        public void Strike()
        {
            Game game = new Game();
            game.UpdateScore(10);

            Assert.AreEqual(game.GameScore, 10);
            Assert.AreEqual(game.GetFrame(0).FrameScore, 10);
            Assert.AreEqual(game.CurrentFrame.FrameScore, 0);
            Assert.AreSame(game.CurrentFrame, game.GetFrame(1));

            game.UpdateScore(10);

            Assert.AreEqual(game.GameScore, 30);
            Assert.AreEqual(game.GetFrame(0).FrameScore, 20);
            Assert.AreEqual(game.GetFrame(1).FrameScore, 10);
            Assert.AreEqual(game.CurrentFrame.FrameScore, 0);
            Assert.AreSame(game.CurrentFrame, game.GetFrame(2));

            game.UpdateScore(2);

            Assert.AreEqual(game.GameScore, 36);
            Assert.AreEqual(game.GetFrame(0).FrameScore, 22);
            Assert.AreEqual(game.GetFrame(1).FrameScore, 12);
            Assert.AreEqual(game.CurrentFrame.FrameScore, 2);
            Assert.AreSame(game.CurrentFrame, game.GetFrame(2));

            game.UpdateScore(4);

            Assert.AreEqual(game.GameScore, 44);
            Assert.AreEqual(game.GetFrame(0).FrameScore, 22);
            Assert.AreEqual(game.GetFrame(1).FrameScore, 16);
            Assert.AreEqual(game.GetFrame(2).FrameScore, 6);
            Assert.AreEqual(game.CurrentFrame.FrameScore, 0);
            Assert.AreSame(game.CurrentFrame, game.GetFrame(3));
        }

        [TestMethod]
        public void Spare()
        {
            Game game = new Game();
            game.UpdateScore(8);

            Assert.AreEqual(game.GameScore, 8);
            Assert.AreEqual(game.CurrentFrame.FrameScore, 8);
            Assert.AreSame(game.CurrentFrame, game.GetFrame(0));

            game.UpdateScore(2);

            Assert.AreEqual(game.GameScore, 10);
            Assert.AreEqual(game.GetFrame(0).FrameScore, 10);
            Assert.AreEqual(game.CurrentFrame.FrameScore, 0);
            Assert.AreSame(game.CurrentFrame, game.GetFrame(1));

            game.UpdateScore(5);

            Assert.AreEqual(game.GameScore, 20);
            Assert.AreEqual(game.GetFrame(0).FrameScore, 15);
            Assert.AreEqual(game.CurrentFrame.FrameScore, 5);
            Assert.AreSame(game.CurrentFrame, game.GetFrame(1));

            game.UpdateScore(4);

            Assert.AreEqual(game.GameScore, 24);
            Assert.AreEqual(game.GetFrame(0).FrameScore, 15);
            Assert.AreEqual(game.GetFrame(1).FrameScore, 9);
            Assert.AreEqual(game.CurrentFrame.FrameScore, 0);
            Assert.AreSame(game.CurrentFrame, game.GetFrame(2));
        }
    }
}
