using Microsoft.AspNetCore.Mvc;

namespace Uı.Models
{
    public class ResultCommentViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Content { get; set; }
        public string UserName { get; set; }
        public string UserTitle { get; set; }
    }
}
