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
    [RoutePrefix("api/routine")]
    public class RoutineController : BaseController
    {
        [HttpPost]
        [Route("create")]
        public HttpResponseMessage CreateRoutine(Routine routine)
        {
            MySqlConnection conn = new MySqlConnection(CONNECTION_STRING);
            try
            {
                conn.Open();

                string rtn = "create_routine_procedure";
                MySqlCommand cmd = new MySqlCommand(rtn, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", routine.ID.ToString());
                cmd.Parameters.AddWithValue("@activeSunday", routine.IsActiveSunday);
                cmd.Parameters.AddWithValue("@activeMonday", routine.IsActiveMonday);
                cmd.Parameters.AddWithValue("@activeTuesday", routine.IsActiveTuesday);
                cmd.Parameters.AddWithValue("@activeWednesday", routine.IsActiveWednesday);
                cmd.Parameters.AddWithValue("@activeThursday", routine.IsActiveThursday);
                cmd.Parameters.AddWithValue("@activeFriday", routine.IsActiveFriday);
                cmd.Parameters.AddWithValue("@activeSaturday", routine.IsActiveSaturday);
                cmd.Parameters.AddWithValue("@description", routine.Description);
                cmd.Parameters.AddWithValue("@difficulty", routine.Difficulty.ToString());
                cmd.Parameters.AddWithValue("@username", routine.Username);

                cmd.ExecuteNonQuery();
                return base.BuildSuccessResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return base.BuildErrorResult(HttpStatusCode.BadRequest, "Error creating routine!");
            }
            finally
            {
                conn.Close();
            }
        }

        [HttpDelete]
        [Route("delete/{routineId}")]
        public HttpResponseMessage DeleteRoutine(string routineId)
        {
            MySqlConnection conn = new MySqlConnection(CONNECTION_STRING);
            try
            {
                conn.Open();

                string rtn = "delete_routine_procedure";
                MySqlCommand cmd = new MySqlCommand(rtn, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", routineId);

                cmd.ExecuteNonQuery();
                return base.BuildSuccessResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return base.BuildErrorResult(HttpStatusCode.BadRequest, "Error deleting routine!");
            }
            finally
            {
                conn.Close();
            }
        }

        [HttpGet]
        [Route("getall/{username}")]
        public HttpResponseMessage GetAllRoutines(string username)
        {
            var routineList = new List<Routine>();
            MySqlConnection conn = new MySqlConnection(CONNECTION_STRING);
            try
            {
                conn.Open();

                string rtn = "get_all_routines_procedure";
                MySqlCommand cmd = new MySqlCommand(rtn, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@username", username);

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var routine = new Routine();
                    routine.ID = Guid.Parse(reader.GetString("id"));
                    routine.Description = reader.GetString("description");
                    routine.Difficulty = (Difficulty)Enum.Parse(typeof(Difficulty), reader.GetString("difficulty"));
                    routine.Username = reader.GetString("username");
                    routine.IsActiveSunday = reader.GetBoolean("is_active_sunday");
                    routine.IsActiveMonday = reader.GetBoolean("is_active_monday");
                    routine.IsActiveTuesday = reader.GetBoolean("is_active_tuesday");
                    routine.IsActiveWednesday = reader.GetBoolean("is_active_wednesday");
                    routine.IsActiveThursday = reader.GetBoolean("is_active_thursday");
                    routine.IsActiveFriday = reader.GetBoolean("is_active_friday");
                    routine.IsActiveSaturday = reader.GetBoolean("is_active_saturday");
                    routineList.Add(routine);
                }
                reader.Close();

                return base.BuildSuccessResultList(HttpStatusCode.OK, routineList);
            }
            catch (Exception ex)
            {
                return base.BuildErrorResult(HttpStatusCode.BadRequest, "Error getting all routines!");
            }
            finally
            {
                conn.Close();
            }
        }

        [HttpGet]
        [Route("getalllogs/{username}/{dateString}")]
        public HttpResponseMessage GetAllRoutineLogs(string username, string dateString)
        {
            var routineLogList = new List<RoutineLog>();
            MySqlConnection conn = new MySqlConnection(CONNECTION_STRING);
            try
            {
                conn.Open();

                string rtn = "get_all_routine_logs_procedure";
                MySqlCommand cmd = new MySqlCommand(rtn, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@dateRequested", dateString);

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var routineLog = new RoutineLog();
                    routineLog.ID = Guid.Parse(reader.GetString("log_id"));
                    routineLog.Timestamp = reader.GetDateTime("time_stamp");
                    routineLog.RoutineID = Guid.Parse(reader.GetString("routine_id"));
                    routineLogList.Add(routineLog);
                }
                reader.Close();

                return base.BuildSuccessResultList(HttpStatusCode.OK, routineLogList);
            }
            catch (Exception ex)
            {
                return base.BuildErrorResult(HttpStatusCode.BadRequest, "Error getting all routines!");
            }
            finally
            {
                conn.Close();
            }
        }

        [HttpPost]
        [Route("log")]
        public HttpResponseMessage LogRoutine(RoutineLog routineLog)
        {
            MySqlConnection conn = new MySqlConnection(CONNECTION_STRING);
            try
            {
                conn.Open();

                string rtn = "log_routine_procedure";
                MySqlCommand cmd = new MySqlCommand(rtn, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", routineLog.ID.ToString());
                cmd.Parameters.AddWithValue("@routineId", routineLog.RoutineID.ToString());
                cmd.Parameters.AddWithValue("@routineTimeStamp", routineLog.Timestamp);


                cmd.ExecuteNonQuery();
                return base.BuildSuccessResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return base.BuildErrorResult(HttpStatusCode.BadRequest, "Error creating routine!");
            }
            finally
            {
                conn.Close();
            }
        }
    }
}

