using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IM.BLL
{
    public class User
    {
        #region 属性
        /// <summary>
        /// 任务组接口
        /// </summary>
        private static IDAL.IUser iUser = null;

        #endregion

        #region 构造函数

        static User()
        {
            iUser = DAL.DataAccessFactory.DataAccessFactory.CreateIUser();
        }

        #endregion

        #region 获取数据 +static List<Model.JobGroup> GetModels()
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public static List<Model.User> GetModels()
        {
            return iUser.GetModels();
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public static List<Model.User> GetModels(Predicate<Model.User> pre)
        {
            List<Model.User> models = GetModels();
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
            return iUser.IsExist(name);
        }
        #endregion

        #region 添加 +static bool Add(Model.User user)
        /// <summary>
        /// 添加
        /// </summary> 
        public static bool Add(Model.User user)
        {
            return iUser.Add(user);
        }
        #endregion

        #region 移除 +static bool Remove(string userName)
        /// <summary>
        /// 移除
        /// </summary> 
        public static bool Remove(string userName)
        {
            return iUser.Remove(userName);
        }
        #endregion
    }
}
