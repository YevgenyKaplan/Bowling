using System;
using System.Configuration;

namespace Bowling.BomusVisitors
{
    public class StrikeVisitor : BonusVisitorBase
    {
        public StrikeVisitor(Frame frame) : base(frame)
        {
            _nextBonus = new SpareVisitor(frame);//can be decoupled and initialized from config or DI container
            _bonusThrows = string.IsNullOrEmpty(ConfigurationManager.AppSettings["StrikeBonusThrows"]) ? 2 : Convert.ToInt32(ConfigurationManager.AppSettings["StrikeBonusThrows"]);
        }

        protected override void ChangeFrameState(int knockedPins)
        {
            if(knockedPins == _pinsCount)
            {
                _frame.State = FrameState.WaitingForBonus;
            }
        }

        protected override void SetNextBonus()
        {
            if(_bonusThrows > 0 && _frame.State == FrameState.WaitingForBonus)
            {
                return;
            }
            _frame.BonusVisitor = _nextBonus;
        }
    }
}
