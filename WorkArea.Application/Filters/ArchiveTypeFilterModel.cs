using WorkArea.Domain.Entities;

namespace WorkArea.Application.Filters
{
    public class ArchiveTypeFilterModel : FilterModelBase
    {
        public string Term { get; set; }
        public int UserId { get; set; }

        public ArchiveTypeFilterModel(DataTableParameters dataTableParameters)
            : base(dataTableParameters)
        {
            if (dataTableParameters.Search?.Value?.Length > 0)
                Term = dataTableParameters.Search.Value;
            if(dataTableParameters.UserId > 0)
                UserId = dataTableParameters.UserId;
        }
    }
    public static partial class FilterExtensions
    {
        public static IQueryable<ArchiveType> AddSearchFilters(this IQueryable<ArchiveType> input, ArchiveTypeFilterModel filter)
        {
            input = input.Where(x=>x.UserId==filter.UserId);
            
            if (filter != null)
            {
                if (filter.Term?.Length > 0)
                {
                    input = input.Where(x => x.Name.Contains(filter.Term));
                }
            }

            return input;
        }
    }
}
