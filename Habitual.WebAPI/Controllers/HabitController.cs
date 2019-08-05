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
    [RoutePrefix("api/habit")]
    public class HabitController : BaseController
    {
        [HttpPost]
        [Route("create")]
        public HttpResponseMessage CreateHabit(Habit habit)
        {
            MySqlConnection conn = new MySqlConnection(CONNECTION_STRING);
            try
            {
                conn.Open();

                string rtn = "create_habit_procedure";
                MySqlCommand cmd = new MySqlCommand(rtn, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", habit.ID.ToString());
                cmd.Parameters.AddWithValue("@description", habit.Description);
                cmd.Parameters.AddWithValue("@difficulty", habit.Difficulty.ToString());
                cmd.Parameters.AddWithValue("@username", habit.Username);

                cmd.ExecuteNonQuery();
                return base.BuildSuccessResult(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return base.BuildErrorResult(HttpStatusCode.BadRequest, "Error creating habit!");
            }
            finally
            {
                conn.Close();
            }
        }

        [HttpGet]
        [Route("getall/{username}")]
        public HttpResponseMessage GetAllHabits(string username)
        {
            var habitList = new List<Habit>();
            MySqlConnection conn = new MySqlConnection(CONNECTION_STRING);
            try
            {
                conn.Open();

                string rtn = "get_all_habits_procedure";
                MySqlCommand cmd = new MySqlCommand(rtn, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@username", username);

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var habit = new Habit();
                    habit.ID = Guid.Parse(reader.GetString("id"));
                    habit.Description = reader.GetString("description");
                    habit.Difficulty = (Difficulty)Enum.Parse(typeof(Difficulty), reader.GetString("difficulty"));
                    habit.Username = reader.GetString("username");
                    habitList.Add(habit);
                }
                reader.Close();
                return base.BuildSuccessResultList(HttpStatusCode.OK, habitList);
            }
            catch (Exception)
            {
                return base.BuildErrorResult(HttpStatusCode.BadRequest, "Error getting all habits!");
            }
            finally
            {
                conn.Close();
            }
        }

        [HttpGet]
        [Route("getalllogs/{username}/{dateString}")]
        public HttpResponseMessage GetAllHabitLogs(string username, string dateString)
        {
            var habitLogList = new List<HabitLog>();
            MySqlConnection conn = new MySqlConnection(CONNECTION_STRING);
            try
            {
                conn.Open();

                string rtn = "get_all_habit_logs_procedure";
                MySqlCommand cmd = new MySqlCommand(rtn, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@dateRequested", dateString);

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var habitLog = new HabitLog();
                    habitLog.ID = Guid.Parse(reader.GetString("log_id"));
                    habitLog.Timestamp = reader.GetDateTime("time_stamp");
                    habitLog.HabitID = Guid.Parse(reader.GetString("habit_id"));
                    habitLogList.Add(habitLog);
                }
                reader.Close();
                return base.BuildSuccessResultList(HttpStatusCode.OK, habitLogList);
            }
            catch (Exception)
            {
                return base.BuildErrorResult(HttpStatusCode.BadRequest, "Error getting all habits!");
            }
            finally
            {
                conn.Close();
            }
        }

        [HttpPost]
        [Route("log")]
        public HttpResponseMessage LogHabit(HabitLog habitLog)
        {
            MySqlConnection conn = new MySqlConnection(CONNECTION_STRING);
            try
            {
                conn.Open();

                string rtn = "log_habit_procedure";
                MySqlCommand cmd = new MySqlCommand(rtn, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", habitLog.ID.ToString());
                cmd.Parameters.AddWithValue("@habitId", habitLog.HabitID);
                cmd.Parameters.AddWithValue("@habitTimeStamp", habitLog.Timestamp);

                cmd.ExecuteNonQuery();
                return base.BuildSuccessResult(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return base.BuildErrorResult(HttpStatusCode.BadRequest, "Error creating habit!");
            }
            finally
            {
                conn.Close();
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public HttpResponseMessage DeleteHabit(string id)
        {
            MySqlConnection conn = new MySqlConnection(CONNECTION_STRING);
            try
            {
                conn.Open();

                string rtn = "delete_habit_procedure";
                MySqlCommand cmd = new MySqlCommand(rtn, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", id);


                cmd.ExecuteNonQuery();
                return base.BuildSuccessResult(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return base.BuildErrorResult(HttpStatusCode.BadRequest, "Error deleting habit!");
            }
            finally
            {
                conn.Close();
            }
        }
    }
}