using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NoName.Controllers
{
    public class Note : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "내용")]
            public string Content { get; set; }
            public string SenderID { get; set; }
            public string ReceiverID { get; set; }
            public DateTime SendingTime { get; set; }
        }
    }
}
