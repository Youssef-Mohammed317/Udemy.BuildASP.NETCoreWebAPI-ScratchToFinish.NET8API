using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NZWalks.Application.DTOs.Common.Filtering;
using NZWalks.Application.DTOs.Common.Paginate;
using NZWalks.Application.DTOs.Common.Sorting;

namespace NZWalks.Application.DTOs.WalkDTOs
{
    public class GetAllRequestDto
    {
        public List<FilterParam?>? filterParams { get; set; } = new();
        public List<SortParam?>? sortParams { get; set; }
        public PaginateParam? paginateParam { get; set; }
    }
}
