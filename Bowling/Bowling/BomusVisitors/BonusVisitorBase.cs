using System;
using System.Configuration;

namespace Bowling.BomusVisitors
{
    public abstract class BonusVisitorBase
    {
        protected int _bonusThrows;
        protected Frame _frame;
        protected int _pinsCount;
        protected BonusVisitorBase _nextBonus;

        public BonusVisitorBase(Frame frame)
        {
            _frame = frame;
            _pinsCount = GetPinsCountFromConfig();
        }

        public virtual void ManageScore(int knockedPins)
        {
            ChangeFrameState(knockedPins);
            SetNextBonus();
        }

        protected abstract void ChangeFrameState(int knockedPins);
        protected abstract void SetNextBonus();

        internal virtual void AddBonus()
        {
            _bonusThrows--;
            if (_bonusThrows == 0)
            {
                _frame.State = FrameState.Completed;
            }
        }

        private int GetPinsCountFromConfig()
        {
            return string.IsNullOrEmpty(ConfigurationManager.AppSettings["PinsCount"])
                       ? 10
                       : Convert.ToInt32(ConfigurationManager.AppSettings["PinsCount"]);
        }
    }
}
