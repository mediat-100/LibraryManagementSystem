using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Domain.Enums;

namespace LMS.Application.Dtos.Requests
{
    public class UpdateBookRequestDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Isbn { get; set; }
        public DateTime PublishedDate { get; set; }
        public RecordStatus RecordStatus { get; set; }
    }
}
