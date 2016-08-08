using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IM.BLL
{
   public class DTOUser
    {
        #region 属性
        /// <summary>
        /// 任务组接口
        /// </summary>
       private static IDAL.IDTOUser iDTOUser = null;

        #endregion

        #region 构造函数

        static DTOUser()
        {
            iDTOUser = DAL.DataAccessFactory.DataAccessFactory.CreateIDTOUser();
        }

        #endregion

        #region 获取数据 +static List<Model.DTOUser> GetModels()
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public static List<Model.DTOUser> GetModels()
        {
            return iDTOUser.GetModels();
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public static List<Model.DTOUser> GetModels(Predicate<Model.DTOUser> pre)
        {
            List<Model.DTOUser> models = GetModels();
            if (models != null && models.Count > 0)
            {
                models = models.FindAll(pre);
            }
            return models;
        }
        #endregion

        #region 获取数据条数 +static int GetCount()
        /// <summary>
        /// 获取数据条数
        /// </summary>
        /// <returns></returns>
        public static int GetCount()
        { 
            return GetModels().Count;
        }  
        #endregion

        #region 根据名称判断是否存在 +static bool IsExist(string name)
        /// <summary>
        /// 根据名称判断是否存在
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>true:存在 false:不存在</returns>
        public static bool IsExist(string name)
        {
            return iDTOUser.IsExist(name);
        }
        #endregion

        #region 添加 +static bool Add(Model.DTOUser user)
        /// <summary>
        /// 添加
        /// </summary> 
        public static bool Add(Model.DTOUser user)
        {
            return iDTOUser.Add(user);
        }
        #endregion

        #region 移除 +static bool Remove(string userName)
        /// <summary>
        /// 移除
        /// </summary> 
        public static bool Remove(string userName)
        {
            return iDTOUser.Remove(userName);
        }
        #endregion
    }
}
