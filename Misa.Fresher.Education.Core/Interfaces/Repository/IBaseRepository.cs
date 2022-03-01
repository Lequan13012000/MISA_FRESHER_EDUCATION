using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Fresher.Education.Core.Interfaces.Repository
{

        /// <summary>
        /// Interface các service phục vụ cho Infrastructure
        /// Created by LEQUAN (17/2/2021)
        /// </summary>
        public interface IBaseRepository<MISAEntity>
        {
            /// <summary>
            /// Lấy toàn bộ dữ liẹu vật thể
            /// </summary>
            /// <returns>List đối tượng</returns>
            /// Created by LEQUAN (10/1/2021)
            public Task<List<MISAEntity>>  GetAll();
            /// <summary>
            /// Lấy dữ liệu theo Id
            /// </summary>
            /// <param name="entityId"></param>
            /// <returns>Đối tượng</returns>
            /// Created by LEQUAN (10/1/2021)
            public Task<MISAEntity>  GetById(Guid entityId);
            /// <summary>
            /// Thêm mới
            /// </summary>
            /// <param name="entity">Thông tin thực thể</param>
            /// <returns>số lượng nhân viên thêm mới thành công</returns>
            /// Created by LEQUAN (10/1/2021)
            public Task<MISAEntity>  Insert(MISAEntity entity);
            /// <summary>
            /// Sửa
            /// </summary>
            /// <param name="entityId">Id thực thể</param>
            /// <param name="entity">Thông tin thực thể</param>
            /// <returns>số lượng nhân viên sửa thành công</returns>
            /// Created by LEQUAN (10/1/2021)
            public Task<MISAEntity> Update(MISAEntity entity, Guid entityId);
            /// <summary>
            /// Xóa
            /// </summary>
            /// <param name="entityId">Id thực thể</param>
            /// <returns>số lượng nhân xóa thành công</returns>
            /// Created by LEQUAN (10/1/2021)
            public Task Delete(Guid entityId);
        }

}
