using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NoName.Data.DbData
{
     /*
      * 쪽지
      */
    public class TableMessage
    {

        [Key]
        public int MessageNumber { get; set; }
        public string Content { get; set; }

        /*
         * 보낸이/받는이 아이디는 DB에 저장되지만, 쪽지 자체에서 확인은 불가함? (익명)
         */
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }

        /*
         * 보낸시간, 읽었는지 여부 - 안읽은 메시지만 해당 ReceiverId 유저에 뿌려준다.
         */
        public DateTime CreateTime { get; set; }
        public bool IsChekced { get; set; }
    }
}
