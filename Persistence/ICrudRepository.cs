using app.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace app.persistence
{
    public interface ICrudRepository<ID, T> where T : HasID<ID>
    {
        void Save(T entity);
        void Delete(ID id);
        T FindOne(ID id);
        void Update(ID id, T e);
        IEnumerable<T> FindAll();
    }

}