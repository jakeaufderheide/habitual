using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Habitual.Core.Entities;
using MySql.Data.MySqlClient;

namespace Habitual.WebAPI.Controllers
{
    [RoutePrefix("api/todo")]
    public class todoController : BaseController
    {
        [HttpPost]
        [Route("create")]
        public HttpResponseMessage CreateTodo(Todo todo)
        {
            MySqlConnection conn = new MySqlConnection(CONNECTION_STRING);
            try
            {
                conn.Open();

                string rtn = "create_todo_procedure";
                MySqlCommand cmd = new MySqlCommand(rtn, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", todo.ID.ToString());
                cmd.Parameters.AddWithValue("@description", todo.Description);
                cmd.Parameters.AddWithValue("@difficulty", todo.Difficulty.ToString());
                cmd.Parameters.AddWithValue("@username", todo.Username);
                cmd.Parameters.AddWithValue("@isDone", false);

                cmd.ExecuteNonQuery();
                return base.BuildSuccessResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return base.BuildErrorResult(HttpStatusCode.BadRequest, "Error creating todo!");
            }
            finally
            {
                conn.Close();
            }
        }

        [HttpDelete]
        [Route("delete/{todoId}")]
        public HttpResponseMessage DeleteTodo(string todoId)
        {
            MySqlConnection conn = new MySqlConnection(CONNECTION_STRING);
            try
            {
                conn.Open();

                string rtn = "delete_todo_procedure";
                MySqlCommand cmd = new MySqlCommand(rtn, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", todoId);

                cmd.ExecuteNonQuery();
                return base.BuildSuccessResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return base.BuildErrorResult(HttpStatusCode.BadRequest, "Error deleting todo!");
            }
            finally
            {
                conn.Close();
            }
        }

        [HttpGet]
        [Route("getall/{username}")]
        public HttpResponseMessage GetAllTodos(string username)
        {
            var todoList = new List<Todo>();
            MySqlConnection conn = new MySqlConnection(CONNECTION_STRING);
            try
            {
                conn.Open();

                string rtn = "get_all_todos_procedure";
                MySqlCommand cmd = new MySqlCommand(rtn, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@username", username);

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var todo = new Todo();
                    todo.ID = Guid.Parse(reader.GetString("id"));
                    todo.Description = reader.GetString("description");
                    todo.Difficulty = (Difficulty)Enum.Parse(typeof(Difficulty), reader.GetString("difficulty"));
                    todo.Username = reader.GetString("username");
                    todo.IsDone = reader.GetBoolean("is_done");
                    todoList.Add(todo);
                }
                reader.Close();
                
                return base.BuildSuccessResultList(HttpStatusCode.OK, todoList);
            }
            catch (Exception ex)
            {
                return base.BuildErrorResult(HttpStatusCode.BadRequest, "Error getting all todos!");
            }
            finally
            {
                conn.Close();
            }
        }

        [HttpPost]
        [Route("log")]
        public HttpResponseMessage LogTodo(Todo todo)
        {
            MySqlConnection conn = new MySqlConnection(CONNECTION_STRING);
            try
            {
                conn.Open();

                string rtn = "log_todo_procedure";
                MySqlCommand cmd = new MySqlCommand(rtn, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", todo.ID.ToString());
                cmd.Parameters.AddWithValue("@done", todo.IsDone);
               
                cmd.ExecuteNonQuery();
                return base.BuildSuccessResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return base.BuildErrorResult(HttpStatusCode.BadRequest, "Error toggling todo!");
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
