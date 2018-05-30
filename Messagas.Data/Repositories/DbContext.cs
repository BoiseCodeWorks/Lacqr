using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Messages.Data.Repositories
{
    internal class DbContext
    {
        protected readonly IDbConnection _db;

        public DbContext(IDbConnection db)
        {
            _db = db;
        }
    }
}
