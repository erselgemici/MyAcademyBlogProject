using Blogy.Business.DTOs.BlogDtos;
using Blogy.Entity.Entities;

namespace Blogy.WebUI.Areas.Admin.Models
{
    public class DashboardViewModel
    {
        public int BlogCount { get; set; }
        public int CategoryCount { get; set; }
        public int TagCount { get; set; }
        public int CommentCount { get; set; }
        public int UserCount { get; set; }
        public int MessageCount { get; set; }

        public string LastBlogTitle { get; set; }
        public string LastCommentContent { get; set; }
        public string LastUserEmail { get; set; }

        public List<string> ChartCategoryNames { get; set; }
        public List<int> ChartCategoryCounts { get; set; }

        public List<string> ChartWriterNames { get; set; }
        public List<int> ChartWriterCounts { get; set; }

        public List<ResultBlogDto> LastFiveBlogs { get; set; }
    }
}
