namespace Template.Domain.Models
{
    public abstract class AbstractAction
    {
        protected AbstractAction(ActionCallContext context)
        {
            Context = context;
        }

        public string Name => GetType().Name;

        public ActionCallContext Context { get; }
    }

    public abstract class AbstractAction<TResult> : AbstractAction
    {
        protected AbstractAction(ActionCallContext context) : base(context) { }
    }
}
