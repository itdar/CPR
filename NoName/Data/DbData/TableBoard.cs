﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NoName.Data.DbData
{
    /*
     * 게시판
     */
    public class TableBoard
    {
        private ICollection<TablePost> posts;

        /*
         * 전체웹에서의 게시판 고유번호?
         * 직업홈에서의 게시판 코드?
         * 게시판 한글이름
         * 게시판 연결된 JobCode (FK) >> Job 에서 다수 관계
         */
        [Key]
        public int BoardNumber { get; set; }
        public int BoardCode { get; set; }
        public string BoardName { get; set; }
        public ICollection<TablePost> Posts
        {
            get => posts;
            set => posts = value;
        }
    }
}