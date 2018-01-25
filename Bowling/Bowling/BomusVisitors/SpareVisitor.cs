using System;
using System.Configuration;

namespace Bowling.BomusVisitors
{
    public class SpareVisitor : BonusVisitorBase
    {
        public SpareVisitor(Frame frame) : base(frame)
        {
            _bonusThrows = string.IsNullOrEmpty(ConfigurationManager.AppSettings["SpareBonusThrows"]) ? 1 : Convert.ToInt32(ConfigurationManager.AppSettings["SpareBonusThrows"]);
        }

        protected override void ChangeFrameState(int knockedPins)
        {
            _frame.State = _frame.FrameScore == _pinsCount ? FrameState.WaitingForBonus : FrameState.Completed;
        }

        protected override void SetNextBonus()
        {}
    }
}   
