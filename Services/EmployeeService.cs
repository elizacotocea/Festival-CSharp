using app.Model;
using app.persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.services
{
    public class EmployeeService
    {
        private EmployeeRepository employeeRepository;
        private int maxId = 0;

        private int getMaxId()
        {
            IEnumerable<Employee> all = employeeRepository.FindAll();
            foreach (Employee a in all)
            {
                if (a.Id > maxId)
                    maxId = a.Id;
            }
            return maxId + 1;

        }
        public EmployeeService(EmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public void Save(string username,string password)
        {
            employeeRepository.Save(new Employee() { Id = getMaxId(), Username = username, Password=password });
            maxId += 1;
        }

        public void Delete(int Id)
        {
            employeeRepository.Delete(Id);
        }

        public void Update(int Id, string username,string password)
        {
            employeeRepository.Update(Id, new Employee() { Id = Id, Username = username, Password = password });
        }

        public Employee FindOne(int ID)
        {
            return employeeRepository.FindOne(ID);
        }

        public IEnumerable<Employee> FindAll()
        {
            return employeeRepository.FindAll();
        }

        public bool checkAccount(string username, string passwd)
        {
            var all = employeeRepository.FindAll();
            foreach (Employee e in all){
                if (e.Username == username && e.Password == passwd)
                    return true;
            }
            return false;
        }
    }
}
