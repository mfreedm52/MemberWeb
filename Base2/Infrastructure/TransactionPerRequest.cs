﻿using System.Data.Entity;
using System.Web;
using Base2.Data;
using Base2.Infrastructure.Tasks;

namespace Base2.Infrastructure
{
	public class TransactionPerRequest :
		IRunOnEachRequest, IRunOnError, IRunAfterEachRequest
	{
		private readonly ApplicationDbContext _dbContext;
		private readonly HttpContextBase _httpContext;

		public TransactionPerRequest(ApplicationDbContext dbContext,
			HttpContextBase httpContext)
		{
			_dbContext = dbContext;
			_httpContext = httpContext;
		}

		void IRunOnEachRequest.Execute()
		{
			_httpContext.Items["_Transaction"] =
				_dbContext.Database.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
		}

		void IRunOnError.Execute()
		{
			_httpContext.Items["_Error"] = true;
		}

		void IRunAfterEachRequest.Execute()
		{
			var transaction = (DbContextTransaction) _httpContext.Items["_Transaction"];

			if (_httpContext.Items["_Error"] != null)
			{
				transaction.Rollback();
			}
			else
			{
				transaction.Commit();				
			}
		}
	}
}