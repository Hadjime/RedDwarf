using InternalAssets.Scripts.Services.Input;

namespace InternalAssets.Scripts.Infrastructure
{
    public class Game
    {
        public static IInputService InputServices;
        public Game()
        {
            RegisterInputServices();
        }

        private void RegisterInputServices()
		{
			InputServices = new StandardInputService();
		}
    }
}