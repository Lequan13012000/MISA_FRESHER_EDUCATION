using Microsoft.Extensions.Options;
using Misa.Fresher.Education.Core.Entities;
using Misa.Fresher.Education.Core.Interfaces.Repository;
using Misa.Fresher.Education.Core.Setting;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Fresher.Education.Infrastructure.Repository
{
    public class AttachmentRepository : BaseRepository<Attachment>, IAttachmentRepository
    {
        public AttachmentRepository(EducationDatabaseSettings educationDatabaseSettings) : base(educationDatabaseSettings)
        {
        }

        public async Task Update(List<Attachment> attachments)
        {
            using (var session = await _mongoClient.StartSessionAsync())
            {
                session.StartTransaction();
                try
                {
                    foreach (Attachment attachment in attachments)
                    {
                        await _baseCollection.ReplaceOneAsync(session, new BsonDocument("_id", attachment.AttachmentId.ToString()), attachment);
                    }
                    await session.CommitTransactionAsync();

                }
                catch (Exception)
                {
                    await session.AbortTransactionAsync();
                    throw;
                }
            }
        }
    }
}
