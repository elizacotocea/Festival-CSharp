using Dapper;
using app.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using persistence;
using System.Data.SQLite;

namespace app.persistence
{
    public class EmployeeRepository:ICrudRepository<int,Employee>
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private int maxId = 0;
        String connectionString = ConfigurationManager.ConnectionStrings["FestivalBD"].ToString();
        private int getMaxId()
        {
            IEnumerable<Employee> all = FindAll();
            foreach (Employee a in all)
            {
                if (a.Id > maxId)
                    maxId = a.Id;
            }
            return maxId + 1;
        }

        
        public Employee FindByUserAndPasswd(string username, string passwd)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                log.InfoFormat("Find Employee with username={0} and password={1}", username,passwd);
                return conn.QueryFirstOrDefault<Employee>("Select * FROM employee WHERE username=@username and password=@password", new { username=username, password=passwd});
            }
        }
        public void Delete(int Id)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                log.InfoFormat("Delete one Employee id={0}", Id);
                conn.Execute("DELETE FROM employee WHERE id=@id", new { id = Id });
                log.InfoFormat("Deleted the Employee id={0} succesfully", Id);
            }
        }

        public IEnumerable<Employee> FindAll()
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                log.Info("Selecting everything from employee table");
                return conn.Query<Employee>("SELECT * FROM Employee");
            }

        }

        public Employee FindOne(int Id)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                log.InfoFormat("Selecting Employee with id={0}", Id);
                return conn.QueryFirstOrDefault<Employee>("SELECT * FROM employee where id=@id", new { id = Id });
            }
        }

        public void Save(Employee elem)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                log.InfoFormat("Save element with id={0}", elem.Id);
                conn.Execute("INSERT INTO employee(id,username,password) VALUES (@Id,@Username,@Password)", new { id = getMaxId(), name = elem.Username,password=elem.Password});
                log.InfoFormat("Saved element with id={0} succesfully", elem.Id);
                maxId += 1;
            }
        }

        public void Update(int Id, Employee elem)
        {
            using (var conn = DbUtils.getConnection())
            {
                log.InfoFormat("Update element with id={0}", elem.Id);
                conn.Execute("update employee set username=@username, password=@password where id=@id", new { id = Id, name = elem.Username, password=elem.Password });
                log.InfoFormat("Updated element with id={0} succesfully", elem.Id);
            }
        }
    }
}
