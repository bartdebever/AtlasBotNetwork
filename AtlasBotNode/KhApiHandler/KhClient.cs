using KhApiHandler.Modules;

namespace KhApiHandler
{
    public class KhClient
    {
        public KhClient()
        {
            Characters = new CharacterModule();
        }

        public ICharacterModule Characters { get; }
    }
}