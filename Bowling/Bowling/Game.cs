using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Bowling
{
    public class Game
    {
        private readonly IReadOnlyList<Frame> _frames;
        private int _currentFrameIndex = 0;

        public int GameScore => _frames.Sum(f => f.FrameScore);
        public Frame CurrentFrame => _frames[_currentFrameIndex];

        public Game()
        {
            _frames = CreateFrames();
        }

        public void UpdateScore(int knockedPins)
        {
            if(GameIsFinished())
            {
                return;
            }

            AddBonuses(knockedPins);

            CurrentFrame.UpdateScore(knockedPins);

            if(CurrentFrame.State != FrameState.Current)
            {
                IncrementFrameIndex();
            }
        }

        internal void IncrementFrameIndex()
        {
            if (_currentFrameIndex >= _frames.Count)
            {
                //log/exception
                return;
            }

            _currentFrameIndex++;
        }

        private void AddBonuses(int knockedPins)
        {
            foreach(Frame frame in _frames.Where(f => f.State == FrameState.WaitingForBonus))
            {
                frame.AddBonus(knockedPins);
            }
        }

        private bool GameIsFinished()
        {
            return _currentFrameIndex == _frames.Count - 1 && CurrentFrame.State == FrameState.Completed;
        }

        public Frame GetFrame(int frameIndex)
        {
            if(frameIndex >= _frames.Count)
            {
                //log
                throw new Exception("No such frame");
            }
            return _frames[frameIndex];
        }

        private List<Frame> CreateFrames()
        {
            int framesCount = GetFramesCountFromConfig();
            List<Frame> frames = FillFramesByFramesCount(framesCount);

            return frames;
        }

        private List<Frame> FillFramesByFramesCount(int framesNumber)
        {
            List<Frame> frames = new List<Frame>();
            for(int i = 0; i < framesNumber; i++)
            {
                frames.Add(new Frame());
            }
            return frames;
        }

        private int GetFramesCountFromConfig()
        {
            int framesNumber = string.IsNullOrEmpty(ConfigurationManager.AppSettings["FramesNumber"])
                                   ? 10
                                   : Convert.ToInt32(ConfigurationManager.AppSettings["FramesNumber"]);
            return framesNumber;
        }
    }
}
