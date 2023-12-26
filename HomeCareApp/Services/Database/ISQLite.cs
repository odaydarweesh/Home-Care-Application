using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace HomeCareApp.Services.Database
{
    public interface ISQLite
    {
        Task<SQLiteConnection> GetConnection();
    }
}
