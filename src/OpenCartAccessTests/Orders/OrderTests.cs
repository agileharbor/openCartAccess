﻿using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using LINQtoCSV;
using NUnit.Framework;
using OpenCartAccess;
using OpenCartAccess.Models.Configuration;

namespace OpenCartAccessTests.Orders
{
	[ TestFixture ]
	internal class OrderTests
	{
		private readonly IOpenCartFactory OpenCartFactory = new OpenCartFactory();
		private OpenCartConfig Config;

		[ SetUp ]
		public void Init()
		{
			const string credentialsFilePath = @"..\..\Files\OpenCartCredentials.csv";

			var cc = new CsvContext();
			var testConfig = cc.Read< TestConfig >( credentialsFilePath, new CsvFileDescription { FirstLineHasColumnNames = true } ).FirstOrDefault();

			if( testConfig != null )
				this.Config = new OpenCartConfig( testConfig.ApiKey, testConfig.ShopUrl );
		}

		[ Test ]
		public void GetOrders()
		{
			var service = this.OpenCartFactory.CreateOrdersService( this.Config );
			var orders = service.GetOrders( DateTime.UtcNow.AddDays( -14 ), DateTime.UtcNow );
			//var orders = service.GetOrders( new DateTime( 2014, 6, 24, 10, 7, 35 ), new DateTime( 2014, 7, 8, 3, 4, 57 ) );

			orders.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public async Task GetOrdersAsync()
		{
			var service = this.OpenCartFactory.CreateOrdersService( this.Config );
			var orders = await service.GetOrdersAsync( DateTime.UtcNow.AddDays( -14 ), DateTime.UtcNow );
			//var orders = await service.GetOrdersAsync(new DateTime(2014, 6, 24, 10, 7, 35), new DateTime(2014, 7, 8, 3, 4, 57));

			orders.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public void GetOrdersByDateTimeRange()
		{
			var service = this.OpenCartFactory.CreateOrdersService( this.Config );
			var orders = service.GetOrders( DateTime.UtcNow.AddMinutes( -5 ), DateTime.UtcNow );

			orders.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public async Task GetOrdersByDateTimeRangeAsync()
		{
			var service = this.OpenCartFactory.CreateOrdersService( this.Config );
			var orders = await service.GetOrdersAsync( DateTime.UtcNow.AddMinutes( -5 ), DateTime.UtcNow );

			orders.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public void GetDateTimeOffset()
		{
			var service = this.OpenCartFactory.CreateOrdersService( this.Config );
			var offset = service.GetDateTimeOffset();

			offset.Should().NotBeNull();
		}

		[ Test ]
		public async Task GetDateTimeOffsetAsync()
		{
			var service = this.OpenCartFactory.CreateOrdersService( this.Config );
			var offset = await service.GetDateTimeOffsetAsync();

			offset.Should().NotBeNull();
		}
	}
}