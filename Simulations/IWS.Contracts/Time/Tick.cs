﻿namespace IWS.Contracts.Time
{
	public class Tick
	{
		public static Tick Empty = new Tick(string.Empty);

		public Tick()
		{
			Id = Guid.NewGuid().ToString();
		}

		public Tick(string id)
		{
			Id = id;
		}

		public string Id { get; set; }
	}
}