using deVoid.UIFramework;
using deVoid.Utils;

namespace UI.StartupScene
{
    public class TitleScreenUIController : AWindowController
    {
        public void NewGame()
        {
            Signals.Get<NewGameSignal>().Dispatch();
        }
    }

    public class NewGameSignal : ASignal
    {
    }
}