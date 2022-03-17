using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace HomeCareApp.Services.Database
{
    public interface ISQLite
    {
        SQLite.SQLiteConnection GetConnection();
    }
}
