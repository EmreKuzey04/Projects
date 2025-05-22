using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.ResponseWrappers
{
    public class PagedResponse<T>:Response<T>where T : class
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;


        public PagedResponse(T data, int totalCount, int pageNumber, int pageSize) : base(data)
        {
            TotalCount= totalCount;
            PageSize=pageSize;
            CurrentPage = pageNumber;
            TotalPages =(int)Math.Ceiling(totalCount / (double)pageSize); //toplam sayfayı yukarı yuvarlama
        }

    }
}
