﻿using OpenCartAccess.Models.Configuration;

namespace OpenCartAccess
{
	public interface IOpenCartFactory
	{
		IOpenCartOrdersService CreateOrdersService( OpenCartConfig config );
		IOpenCartProductsService CreateProductsService( OpenCartConfig config );

		IOpenCartChecksumService CreateChecksumService( OpenCartConfig config );
	}

	public class OpenCartFactory : IOpenCartFactory
	{
		public IOpenCartOrdersService CreateOrdersService( OpenCartConfig config )
		{
			return new OpenCartOrdersService( config );
		}

		public IOpenCartProductsService CreateProductsService( OpenCartConfig config )
		{
			return new OpenCartProductsService( config );
		}

		public IOpenCartChecksumService CreateChecksumService( OpenCartConfig config )
		{
			return new OpenCartChecksumService( config );
		}
	}
}