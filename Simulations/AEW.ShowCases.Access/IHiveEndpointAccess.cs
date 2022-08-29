﻿using AEW.Contracts.ShowCases;

namespace AEW.ShowCases.Access
{
	public interface IHiveEndpointAccess
	{
		public Task<IEnumerable<HiveDetails>> GetEndpointsAsync();
	}
}