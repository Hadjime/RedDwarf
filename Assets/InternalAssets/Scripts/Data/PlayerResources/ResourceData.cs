using System;
using System.Collections.Generic;


namespace InternalAssets.Scripts.Data.PlayerResources
{
	[Serializable]
	public class ResourceData
	{
		private Dictionary<ResourceType, int> resources;


		public ResourceData()
		{
			this.resources = new Dictionary<ResourceType, int>();
		}


		public void Add(ResourceType resourceType, int quantity)
		{
			if (resources.ContainsKey(resourceType))
			{
				resources[resourceType] += quantity;
			}
		}
	}
}
