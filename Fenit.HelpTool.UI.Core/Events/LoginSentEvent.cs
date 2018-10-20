using Prism.Events;

namespace Fenit.HelpTool.UI.Core.Events
{
    public class LoggedInEvent : PubSubEvent<bool>
    {
    }

    public class LoggedOutEvent : PubSubEvent<bool>
    {
    }
}