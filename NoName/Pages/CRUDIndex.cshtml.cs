using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NoName.Data;
using NoName.Data.DbData;
using NoName.Enumerator;


//���� ����Ʈ : https://docs.microsoft.com/ko-kr/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/enumeration-classes-over-enum-types


namespace NoName.Pages
{
    public class CRUDIndexModel : PageModel
    {
        private readonly DataContext _context;
        private readonly DataDbManager manager;
        [BindProperty]
        public TableDataJob TableDataJob { get; set; }
        public IList<TablePost> TablePost { get; set; }
        public IEnumerable<BoardType> TableBoard { get; set; }
        public CRUDIndexModel(DataContext context)
        {
            manager = DataDbManager.GetInstance();
            _context = context;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //Create a TableJob
            _context.Job.Add(TableDataJob);
            _context.SaveChanges();
            /***
             * Job������ �⺻ �Խ��� ����
             * 1. HOT�Խ���
             * 2. �ǽð� �α��
             * 3. �ְ� �α��
             * 4. �����Խ���
             * 5. ��аԽ���
             * 6. �����Խ���
             * 7. ȫ���Խ���
             * 4444. ��������
             * */
            var TableBoard = Enumerator.Enumerator.GetAll<BoardType>();
            int length = TableBoard.Count();
            for (var i = 0; i < length; i++)
            {
                var board = new TableBoard
                {
                    BoardId = TableBoard.ElementAt(i).Id,
                    JobCode = TableDataJob.JobCode,
                    BoardName = TableBoard.ElementAt(i).Name
                };
                _context.Board.Add(board);
                _context.SaveChanges();
                //Create 100 Posts
                for (int j = 1; j <= 10; j++)
                {
                    _context.Post.Add(new TablePost
                    {
                        //UserId �ӽ�
                        UserId = "����" + j.ToString(),
                        CategoryNumber = 1,
                        Title = j.ToString(),
                        Content = (j + j).ToString(),
                        ViewCount = 0,
                        LikeCount = 0,
                        DislikeCount = 0,
                        HasNewComment = false,
                        CreateTime = DateTime.Now,
                        InitialContent = "",
                        LastModifiedTime = DateTime.MinValue,
                        IsDeleted = false,
                        DeletedTime = DateTime.MinValue,
                        BoardId = board.BoardId
                    });
                }
            }
            
            await _context.SaveChangesAsync();

            return RedirectToPage("./CRUD/TablePostCRUD/Index");
        }
    }
}
