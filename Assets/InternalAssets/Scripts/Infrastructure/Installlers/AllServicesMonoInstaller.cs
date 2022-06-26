using Zenject;


namespace InternalAssets.Scripts.Infrastructure.Installlers
{
	public class AllServicesMonoInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			AllServicesInstaller.Install(Container);
		}
	}
}
