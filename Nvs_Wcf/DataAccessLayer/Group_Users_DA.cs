using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Nvs_Wcf
{
    public class Group_Users_DA
    {
        public DataSet GetGroup_ByUser(decimal p_user_id)
        {
            try
            {
               return OracleHelper.ExecuteDataset(CommonData.GConnectionString, CommandType.StoredProcedure, "pkg_user_friends.proc_getByUser",
                     new OracleParameter("p_user_id", OracleDbType.Decimal, p_user_id, ParameterDirection.Input),
                     new OracleParameter("p_cursor", OracleDbType.RefCursor, ParameterDirection.Output));
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new DataSet();
            }
        }

        public bool Delete_Friend(decimal p_user_id, decimal p_user_friend_id)
        {
            try
            {
                OracleHelper.ExecuteNonQuery(CommonData.GConnectionString, CommandType.StoredProcedure, "pkg_group_user.proc_group_user_delete",
                     new OracleParameter("p_user_id", OracleDbType.Decimal, p_user_id, ParameterDirection.Input),
                     new OracleParameter("p_user_friend_id", OracleDbType.Decimal, p_user_friend_id, ParameterDirection.Input));
                return true;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return false;
            }
        }

        public decimal Group_User_Insert(decimal p_group_id, decimal p_user_id, DateTime p_joindate)
        {
            try
            {
                OracleHelper.ExecuteNonQuery(CommonData.GConnectionString, CommandType.StoredProcedure, "pkg_group_user.proc_group_user_insert",
                new OracleParameter("p_group_id", OracleDbType.Decimal, p_group_id, ParameterDirection.Input),
                new OracleParameter("p_user_id", OracleDbType.Decimal, p_user_id, ParameterDirection.Input),
                new OracleParameter("p_joindate", OracleDbType.Date, p_joindate, ParameterDirection.Input)); return 0;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return -1;
            }
        }
    }
}
