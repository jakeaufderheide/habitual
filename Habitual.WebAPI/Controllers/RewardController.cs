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
    [RoutePrefix("api/reward")]
    public class RewardController : BaseController
    {
        [HttpPost]
        [Route("create")]
        public HttpResponseMessage CreateReward(Reward reward)
        {
            MySqlConnection conn = new MySqlConnection(CONNECTION_STRING);
            try
            {
                conn.Open();

                string rtn = "create_reward_procedure";
                MySqlCommand cmd = new MySqlCommand(rtn, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", reward.ID.ToString());
                cmd.Parameters.AddWithValue("@description", reward.Description);
                cmd.Parameters.AddWithValue("@username", reward.Username);
                cmd.Parameters.AddWithValue("@cost", reward.Cost);

                cmd.ExecuteNonQuery();
                return base.BuildSuccessResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
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
        public HttpResponseMessage GetAllRewards(string username)
        {
            var rewardList = new List<Reward>();
            MySqlConnection conn = new MySqlConnection(CONNECTION_STRING);
            try
            {
                conn.Open();

                string rtn = "get_all_rewards_procedure";
                MySqlCommand cmd = new MySqlCommand(rtn, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@username", username);

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var reward = new Reward();
                    reward.ID = Guid.Parse(reader.GetString("id"));
                    reward.Description = reader.GetString("description");
                    reward.Username = reader.GetString("username");
                    reward.Cost = reader.GetInt32("cost");
                    rewardList.Add(reward);
                }
                reader.Close();
                return base.BuildSuccessResultList(HttpStatusCode.OK, rewardList);
            }
            catch (Exception ex)
            {
                return base.BuildErrorResult(HttpStatusCode.BadRequest, "Error getting all rewards!");
            }
            finally
            {
                conn.Close();
            }
        }

        [HttpGet]
        [Route("delete/{id}")]
        public HttpResponseMessage DeleteReward(string id)
        {
            MySqlConnection conn = new MySqlConnection(CONNECTION_STRING);
            try
            {
                conn.Open();

                string rtn = "delete_reward_procedure";
                MySqlCommand cmd = new MySqlCommand(rtn, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", id);


                cmd.ExecuteNonQuery();
                return base.BuildSuccessResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return base.BuildErrorResult(HttpStatusCode.BadRequest, "Error deleting reward!");
            }
            finally
            {
                conn.Close();
            }
        }

        [HttpPost]
        [Route("buy")]
        public HttpResponseMessage BuyReward(Reward reward)
        {
            MySqlConnection conn = new MySqlConnection(CONNECTION_STRING);
            try
            {
                conn.Open();

                string rtn = "buy_reward_procedure";
                MySqlCommand cmd = new MySqlCommand(rtn, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@username", reward.Username);
                cmd.Parameters.AddWithValue("@cost", reward.Cost);
                cmd.Parameters.AddWithValue("@result", 0);
                cmd.Parameters["@result"].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                var result = Convert.ToBoolean(cmd.Parameters["@result"].Value);
                return base.BuildSuccessResult(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return base.BuildErrorResult(HttpStatusCode.BadRequest, "Error buying reward!");
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
