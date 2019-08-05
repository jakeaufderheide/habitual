using System;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Habitual.Core.Entities;
using MySql.Data.MySqlClient;

namespace Habitual.WebAPI.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : BaseController
    {
        [HttpPost]
        [Route("create")]
        public HttpResponseMessage CreateUser(User user)
        {
            MySqlConnection conn = new MySqlConnection(CONNECTION_STRING);
            try
            {
                conn.Open();

                string rtn = "create_user_procedure";
                MySqlCommand cmd = new MySqlCommand(rtn, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@username", user.Username);
                cmd.Parameters.AddWithValue("@pw", user.Password);

                cmd.ExecuteNonQuery();
                return base.BuildSuccessResult(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return base.BuildErrorResult(HttpStatusCode.BadRequest, "Error creating user!");
            }
            finally
            {
                conn.Close();
            }
        }

        [HttpPost]
        [Route("get")]
        public HttpResponseMessage GetUser(User user)
        {
            MySqlConnection conn = new MySqlConnection(CONNECTION_STRING);
            try
            {
                conn.Open();

                string rtn = "get_user_procedure";
                MySqlCommand cmd = new MySqlCommand(rtn, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@username", user.Username);
                cmd.Parameters.AddWithValue("@pw", user.Password);

                var reader = cmd.ExecuteReader();
                var updatedUser = new User();
                while (reader.Read())
                {
                    updatedUser.Username = reader.GetString("username");
                    updatedUser.Password = reader.GetString("password");
                    updatedUser.Points = reader.GetInt32("points");
                }
                reader.Close();

                return base.BuildSuccessResult(HttpStatusCode.OK, updatedUser);
            }
            catch (Exception)
            {
                return base.BuildErrorResult(HttpStatusCode.BadRequest, "Error creating user!");
            }
            finally
            {
                conn.Close();
            }
        }

        [HttpGet]
        [Route("points/{username}/{points}")]
        public HttpResponseMessage IncrementPoints(string username, string points)
        {
            MySqlConnection conn = new MySqlConnection(CONNECTION_STRING);
            try
            {
                conn.Open();

                string rtn = "increment_points_procedure";
                MySqlCommand cmd = new MySqlCommand(rtn, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@points", points);

                var reader = cmd.ExecuteNonQuery();

                return base.BuildSuccessResult(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return base.BuildErrorResult(HttpStatusCode.BadRequest, "Error incrementing points!");
            }
            finally
            {
                conn.Close();
            }
        }

    }
}
