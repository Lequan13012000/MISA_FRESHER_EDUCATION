using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Fresher.Education.Core.Interfaces.Service
{
    public interface IBaseService<MISAEntity>
    {
        /// <summary>
        /// Thêm mới
        /// </summary>
        /// <param name="entity">Thông tin thực thể</param>
        /// <returns>Service Result</returns>
        public int  Insert(MISAEntity entity);
        /// <summary>
        /// Sửa
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityId"></param>
        /// <returns>Service Result</returns>
        public int Update(MISAEntity entity, Guid entityId);
        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns>Service Result</returns>
        public int Delete(Guid entityId);
    }
}
