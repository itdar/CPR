using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NoName.Data.DbData
{
    /*
     * 직업별 연봉
     * >> 성별, 나이, 연차 등에 따라 나누려면
     * >> User 정보와 연관이 필요함
     */
    public class TableSalary
    {
        [Key]
        public int SalaryNumber { get; set; }
        public int Average { get; set; }

        /*
         * 남/여 연봉평균, 계산해서 넣을지 뺄 때 계산하고 없앨지
         */
        public int MaleAverage { get; set; }
        public int FemaleAverage { get; set; }

        public int Minimum { get; set; }
        public int Maximum { get; set; }

        public int JobCode { get; set; }
    }
}
