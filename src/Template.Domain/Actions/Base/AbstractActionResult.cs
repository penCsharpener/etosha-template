using Template.Domain.Models;

namespace Template.Domain.Actions.Base
{
    public abstract class AbstractActionResult
    {
        public AbstractActionResult(AbstractAction action) { }
    }

    public abstract class AbstractActionResult<TAction> : AbstractActionResult where TAction : AbstractAction
    {
        public AbstractActionResult(TAction action) : base(action)
        {
            Action = action;
        }

        public TAction Action { get; }
    }
}
