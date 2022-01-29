using System.Collections;
using System.Threading.Tasks;


namespace InternalAssets.Scripts.Tools
{
	
	public static class TaskExtensions
	{
		/// <summary>
		/// Позволяет подождать аснихронный метод в корутине
		/// </summary>
		public static IEnumerator AsIEnumerator(this Task task)
		{
			while (!task.IsCompleted)
			{
				yield return null;
			}

			if (task.IsFaulted)
			{
				if (task.Exception != null)
					throw task.Exception;
			}
		}
	}
}
