using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bowling.BomusVisitors;

namespace Bowling
{
    internal enum FrameState
    {
        Current, 
        WaitingForBonus, 
        Completed
    }

    public class Frame
    {
        private readonly int _maxThrows;

        public int FrameScore { get; private set; }
        public int CurrentThrowNumber { get; private set; } = 1;

        internal FrameState State { get; set; } = FrameState.Current;
        internal BonusVisitorBase BonusVisitor;

        public Frame()
        {
            _maxThrows = string.IsNullOrEmpty(ConfigurationManager.AppSettings["MaxThrows"]) ? 2 : Convert.ToInt32(ConfigurationManager.AppSettings["MaxThrows"]);
            BonusVisitor = new StrikeVisitor(this);//can be decoupled and initialized from config or DI container
        }

        internal void UpdateScore(int knockedPins)
        {
            FrameScore += knockedPins;

            BonusVisitor.ManageScore(knockedPins);
            if(State == FrameState.Current)
            {
                IncrementThrowNumberOrCompleteTheFrame();
            }
        }

        internal void AddBonus(int knockedPins)
        {
            FrameScore += knockedPins;
            BonusVisitor.AddBonus();
        }

        internal void IncrementThrowNumberOrCompleteTheFrame()
        {
            if(CurrentThrowNumber >= _maxThrows)
            {
                State = FrameState.Completed;
                return;
            }

            CurrentThrowNumber++;
        }
    }
}
