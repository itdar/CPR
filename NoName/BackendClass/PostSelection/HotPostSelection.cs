﻿using NoName.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoName.BackendClass.PostSelection
{
    public class HotPostSelection : Qualification
    {
        private readonly DataDbManager manager;
        /*
         * 주기(Cycle) : 24hour / 7days /...
         * 조건(qualification) : ViewCount, LikeCount, ...
         */
        //주기
        private int CycleTime = 10;
        //기준시간
        public DateTime CriterionTime { get; set; }
        public HotPostSelection()
        {

        }
        public static int TheTime()
        {
            new HotPostSelection();
            return 0;
        }
    }
}
