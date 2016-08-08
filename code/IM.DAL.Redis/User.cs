using IM.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IM.DAL.Redis
{
    public class User : IUser
    {
        private static string User_K = Common.RedisConfigHelp.redisConfigHelp.GetRedisKeyByName("User_K");

        #region 获取所有数据
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        public List<IM.Model.User> GetModels()
        {
            return Common.RedisHelp.redisHelp.Get<List<Model.User>>(User_K);
        }
        #endregion

        #region 根据名称判断是否存在 +bool IsExist(string name)
        /// <summary>
        /// 根据名称判断是否存在
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>true:存在 false:不存在</returns>
        public bool IsExist(string name)
        {
            bool ret = false;
            List<Model.User> models = GetModels();
            if (models != null && models.Count > 0)
            {
                Model.User model = models.Find(m => m.UserName == name);
                if (model != null)
                {
                    ret = true;
                }
            }
            return ret;
        }
        #endregion

        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Add(Model.User user)
        {
            List<Model.User> models = new List<Model.User>();
            models = Common.RedisHelp.redisHelp.Get<List<Model.User>>(User_K);
            if (models == null)
            {
                models = new List<Model.User>();
            }
            models.Add(user);
            bool ret = Common.RedisHelp.redisHelp.Set<List<Model.User>>(User_K, models);
            return ret;
        }
        #endregion
         
        #region 移除
        /// <summary>
        /// 移除 
        /// </summary> 
        /// <returns></returns>
        public bool Remove(string userName)
        {
            List<Model.User> models = new List<Model.User>();
            models = Common.RedisHelp.redisHelp.Get<List<Model.User>>(User_K);
            if (models == null)
            {
                models = new List<Model.User>();
            }
            else
            {
                Model.User user = models.Find(m => m.UserName == userName);
                models.Remove(user);
            }
            bool ret = Common.RedisHelp.redisHelp.Set<List<Model.User>>(User_K, models);
            return ret;
        } 
        #endregion
    }
}
