using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace WebAPI_2.Models.Repository
{
    public class ToDoRepo
    {
        private readonly ToDoContext _db;
        public ToDoRepo(ToDoContext context)
        {
            _db = context;
        }

        public List<ToDo> GetAllToDos()
        {
            try
            {
                return _db.ToDos.ToList();
            } catch(Exception ex)
            {
                Console.WriteLine("###Error GATD: ", ex.Message);
                return null!;
            }
        }

        public ToDo GetOneToDo(int id) 
        {
            try
            {
                var toDo = _db.ToDos
                    .Where(item => item.Id == id)
                    .FirstOrDefault();

                return toDo;

            } catch(Exception ex)
            {
                Console.WriteLine("###Error GOTD: ", ex.Message);
                return null!;
            }
        }

        public ToDo AddNewToDo(ToDo newToDo)
        {
            try
            {
                var idExist = _db.ToDos
                                .Where(item => item.Id == newToDo.Id)
                                .AsNoTracking()
                                .FirstOrDefault();
                
                if (idExist != null)
                {
                    newToDo.Id = -1;
                    return newToDo;
                }

                var addTodo = new ToDo(newToDo.Priority, newToDo.Description, newToDo.IsComplete);
                
                _db.Add(addTodo);
                _db.SaveChanges();

                return addTodo;

            } catch(Exception ex)
            {
                Console.WriteLine("###Error ANTD: ", ex.Message);
                return null!;
            }
        }

        public ToDo UpdateToDo(ToDo updateToDo)
        {
            try
            {
                var toDo = _db.ToDos
                                .Where(item => item.Id == updateToDo.Id)
                                .AsNoTracking()
                                .FirstOrDefault();

                if (toDo == null)
                {
                    var temp = new ToDo(0, "", false);
                    temp.Id = -1;
                    return temp;
                }

                _db.Update(updateToDo);
                _db.SaveChanges();

                return updateToDo;

            }
            catch (Exception ex)
            {
                Console.WriteLine("###Error UTD: ", ex.Message);
                return null!;
            }
        }

        public ToDo DeleteOneToDo(int id)
        {
            try
            {
                var toDo = _db.ToDos
                                .Where(item => item.Id == id)
                                .FirstOrDefault();

                if (toDo == null)
                {
                    var temp = new ToDo(0, "", false);
                    temp.Id = -1;
                    return temp;
                }

                _db.Remove(toDo);
                _db.SaveChanges();

                return toDo;

            }
            catch (Exception ex)
            {
                Console.WriteLine("###Error DOTD: ", ex.Message);
                return null!;
            }
        }
    }
}
