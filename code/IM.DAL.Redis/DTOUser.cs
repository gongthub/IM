using IM.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IM.DAL.Redis
{
    public class DTOUser : IDTOUser
    {
        private static string DTOUser_K = Common.RedisConfigHelp.redisConfigHelp.GetRedisKeyByName("DTOUser_K");

        #region 获取所有数据
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        public List<IM.Model.DTOUser> GetModels()
        {
            List<IM.Model.DTOUser> models = Common.RedisHelp.redisHelp.Get<List<Model.DTOUser>>(DTOUser_K);
            if (models != null)
            {
                models.ForEach(delegate(IM.Model.DTOUser user)
                {
                    if (user.user == null)
                    {
                        user.user = new Model.User();
                    }
                });
            }
            models = new List<Model.DTOUser>();
            return models;
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
            List<Model.DTOUser> models = GetModels();
            if (models != null && models.Count > 0)
            {
                Model.DTOUser model = models.Find(m => m.user.UserName == name);
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
        public bool Add(Model.DTOUser user)
        {
            List<Model.DTOUser> models = new List<Model.DTOUser>();
            models = Common.RedisHelp.redisHelp.Get<List<Model.DTOUser>>(DTOUser_K);
            if (models == null)
            {
                models = new List<Model.DTOUser>();
            }
            models.Add(user);
            bool ret = Common.RedisHelp.redisHelp.Set<List<Model.DTOUser>>(DTOUser_K, models);
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
            List<Model.DTOUser> models = new List<Model.DTOUser>();
            models = Common.RedisHelp.redisHelp.Get<List<Model.DTOUser>>(DTOUser_K);
            if (models == null)
            {
                models = new List<Model.DTOUser>();
            }
            else
            {
                Model.DTOUser user = models.Find(m => m.user.UserName == userName);
                models.Remove(user);
            }
            bool ret = Common.RedisHelp.redisHelp.Set<List<Model.DTOUser>>(DTOUser_K, models);
            return ret;
        }
        #endregion
    }
}
