using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NoName.Data;
using NoName.Data.DbData;

namespace NoName.Pages
{
    public class CRUDIndexModel : PageModel
    {
        private readonly DataContext _context;
        private readonly DataDbManager manager;
        [BindProperty]
        public TableDataJob TableDataJob { get; set; }
        public IList<TablePost> TablePost { get; set; }
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
            //Create Job
            _context.Job.Add(TableDataJob);
            _context.SaveChanges();
            //Job������ �⺻ �Խ��� ����
            //�ְ�=1,�ǽð� �α��2, �ְ� �α�� 3, �����Խ��� 4, ��аԽ��� 5, �����Խ��� 6 ����/ȫ��/����/���
            var defaultBoard = new string[] { "HOT�Խ���", "�ǽð� �α��", "�ְ� �α��", "�����Խ���", "��аԽ���", "�����Խ���", "��аԽ���" };
            for (var i = 0; i < 6; i++)
            {
                //Create Default 6 Boards
                var board = new TableBoard
                {
                    //PK�������� �ڵ��Է� BoardId = i+1,
                    JobCode = TableDataJob.JobCode,
                    BoardName = defaultBoard[i]
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
