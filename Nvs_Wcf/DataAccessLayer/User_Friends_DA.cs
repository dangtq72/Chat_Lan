using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Nvs_Wcf
{
    public class User_Friends_DA
    {
        public DataSet GetFriend_ByUser(decimal p_user_id)
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
                OracleHelper.ExecuteNonQuery(CommonData.GConnectionString, CommandType.StoredProcedure, "pkg_user_friends.proc_delete",
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

        public bool Insert_Friend(decimal p_user_id, decimal p_user_friend_id,string p_user_friend_name)
        {
            try
            {
                OracleHelper.ExecuteNonQuery(CommonData.GConnectionString, CommandType.StoredProcedure, "pkg_user_friends.Proc_User_Friends_Insert",
                     new OracleParameter("p_user_id", OracleDbType.Decimal, p_user_id, ParameterDirection.Input),
                     new OracleParameter("p_user_friend_id", OracleDbType.Decimal, p_user_friend_id, ParameterDirection.Input),
                     new OracleParameter("p_user_friend_name", OracleDbType.Varchar2, p_user_friend_name, ParameterDirection.Input));
                return true;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return false;
            }
        }
    }
}
