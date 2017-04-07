
using NaviCommon;
using ObjInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Text;
using ZetaCompressionLibrary;

namespace Nvs_Wcf
{

    public partial interface NvsContractService
    {
        [OperationContract()]
        List<User_Friends_Info> Get_Friends_ByUser(string p_user_name);

        [OperationContract()]
        bool Delete_Friend(decimal p_user_id, decimal p_user_friend_id);

        [OperationContract()]
        bool Insert_Friend(decimal p_user_id, decimal p_user_friend_id, string p_user_friend_name);
    }


    public partial class NvsService : NvsContractService
    {

        public List<User_Friends_Info> Get_Friends_ByUser(string p_user_name)
        {
            try
            {
                if (DBMemory.c_dic_User_Interface.ContainsKey(p_user_name))
                {
                    return DBMemory.c_dic_User_Interface[p_user_name].Get_Friends();
                }
                else return new List<User_Friends_Info>();
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new List<User_Friends_Info>();
            }
        }

        public bool Delete_Friend(decimal p_user_id, decimal p_user_friend_id)
        {
            try
            {
                User_Friends_DA _User_Friends_DA = new User_Friends_DA();
                return _User_Friends_DA.Delete_Friend(p_user_id, p_user_friend_id);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return false;
            }
        }

        public bool Insert_Friend(decimal p_user_id, decimal p_user_friend_id, string p_user_friend_name)
        {
            try
            {
                User_Friends_DA _User_Friends_DA = new User_Friends_DA();
                return _User_Friends_DA.Insert_Friend(p_user_id, p_user_friend_id, p_user_friend_name);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return false;
            }
        }
    }
}
