using KhApiHandler.Modules;

namespace KhApiHandler
{
    public class KhClient
    {
        public ICharacterModule Characters { get; }

        public KhClient()
        {
            Characters = new CharacterModule();
        }
    }
}