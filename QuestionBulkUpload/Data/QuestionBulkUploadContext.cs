using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuestionBulkUpload.Models;

namespace QuestionBulkUpload.Data
{
    public class QuestionBulkUploadContext : DbContext
    {
        public QuestionBulkUploadContext (DbContextOptions<QuestionBulkUploadContext> options)
            : base(options)
        {
        }

        public DbSet<QuestionBulkUpload.Models.QuestionData> Questions { get; set; } = default!;
    }
}
