using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NoName.Data.DbData;

namespace NoName.Pages
{
    public class Pagination<T> : List<T>
    {
        /****
         * CurrentPage = 1~10 -> StartPage = 1 / CurrentPage = 10~19 -> StartPage = 11
        ****/
        //CurrentBoard
        //Not Used
        public int BoardId { get; set; }
        //Current Page => Defualt Index page = 0
        public int CurrentPage { get; private set; } = 1;
        //Number of Posting
        public int PostingCount { get; private set; }
        //Posting Count on a Page
        public int PageSize { get; private set; }
        //Paging Count on a View
        public int PageBarSize { get; private set; } = 10;
        //Start Page Number 1,11,21,31,41,51...
        public int StartPage => CurrentPage - CurrentPage % 10 + 1 - (CurrentPage % 10 ==0 ? 10 : 0);
        //Total 'Page' Count => Ceiling(count = 25 / pageSize = 10) = Ceiling(2.5) = 3
        public int TotalPages => (int)Math.Ceiling(decimal.Divide(PostingCount, PageSize));
        //CurrentPage=35 => PreviousPage = 31(CurrentPage = 30)
        public int Previous => StartPage - 1;
        //CurrentPage=35 => NextPage = 41(CurrentPage = 40)
        public int Next => StartPage + PageBarSize;

        public bool HasPreviousPage => StartPage > 1;
        public bool HasNextPage => StartPage * PageBarSize < TotalPages;
        public bool IsCurrentPage(int current)
        {
            return CurrentPage == current;
        }
        public Pagination(List<T> items, int postingCount, int currentPage, int pageSize)
        {
            PostingCount = postingCount;
            //indexPage에서 currentPage가 0을 반환함으로 강제변경.
            CurrentPage = currentPage == 0 ? 1 : currentPage;
            PageSize = pageSize;

            this.AddRange(items);
        }

        internal static Task CreateAsync(IList<TablePost> tablePost, int v)
        {
            throw new NotImplementedException();
        }

        //static method => instance member 사용불가
        //IQueryable<T> => 데이터 형식이 지정되지 않은 특정 데이터 소스에 대한 쿼리를 실행하는 기능을 제공합니다.
        public static async Task<Pagination<T>> CreateAsync(IQueryable<T> source, int currentPage, int pageSize = 10)
        {
            var postingCount = await source.CountAsync();
            //CreateList. length is pageSize.
            var items = await source.Skip(currentPage * pageSize).Take(pageSize).ToListAsync();
            return new Pagination<T>(items, postingCount, currentPage, pageSize);
        }
    }
}
