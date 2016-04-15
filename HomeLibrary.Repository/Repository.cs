//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using HomeLibrary.Common;
//using HomeLibrary.Common.Entity;

//namespace HomeLibrary.Repository
//{
//    public class Repository<T> : IRepository<T> where T : Entity
//    {
//        Entity book = new Lendee();
//        

//        private List<T> GetEntities()
//        {
//            var type = typeof (T);

//            if (type == typeof (Book))
//                return books as List<T>;
//            if (type == typeof (Lendee))
//                return lendees as List<T>;
//            if (type == typeof (Lending))
//                return lendings as List<T>;

//            throw new Exception(string.Format("Type {0} is not supported", type.Name));
//        }

//        public Repository()
//        {
//            Seed();
//        }

//        public T GetById(int id)
//        {
//            return GetEntities().FirstOrDefault(x => x.Id == id);
//        }

//        public T[] GetAll()
//        {
//            return GetEntities().ToArray();
//        }

//        public T Create(T entity)
//        {
//            var entities = GetEntities();
//            var id = entities.Max(x => x.Id);
//            entity.Id = id;
//            entities.Add(entity);

//            return entity;
//        }

//        public T Update(int id, T entity)
//        {
//            throw new NotImplementedException();
//        }

//        public void Delete(int id)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
