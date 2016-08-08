using IM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IM.IDAL
{
    public interface IDTOUser
    {
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        List<DTOUser> GetModels();

        /// <summary>
        /// 根据名称判断是否存在
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>true:存在 false:不存在</returns>
        bool IsExist(string name);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        bool Add(DTOUser user);

        /// <summary>
        /// 移除
        /// </summary> 
        /// <returns></returns>
        bool Remove(string userName);
    }
}
