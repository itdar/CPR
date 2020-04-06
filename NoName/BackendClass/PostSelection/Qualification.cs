using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoName.BackendClass.PostSelection
{
    public abstract class Qualification
    {
        //조회수
        public int ViewCount { get; set; }
        //좋아요
        public int LikeCount { get; set; }
    }
}
