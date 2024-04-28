using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Common
{
    public interface IConnectionUtility
    {
        SqlConnection GetConfigDbConneciton();
    }
    public class ConnectionUtility : IConnectionUtility
    {
        private readonly Configs configs;
        public ConnectionUtility(IOptions<Configs> options)
        {
            this.configs = options.Value;
        }
        public SqlConnection GetConfigDbConneciton()
        {
            return new SqlConnection(configs.DBConnection);
        }


    }
}
