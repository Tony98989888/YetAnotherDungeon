using deVoid.UIFramework;
using deVoid.Utils;
using UI.HomeScreen;

namespace UI.StartupScene
{
    public class TitleScreenUIController : AWindowController
    {
        public void NewGame()
        {
            Signals.Get<NewGameSignal>().Dispatch();
        }

        public void LoadGame()
        {
            Signals.Get<ShowUserSelectionUISignal>().Dispatch();
        }
    }

    public class NewGameSignal : ASignal
    {
    }
}