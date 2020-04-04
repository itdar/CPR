using NoName.BackendClass.PostSelection;
using NoName.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoName.Services
{
    public class PostSelectionService
    {
        private readonly DataDbManager manager;
        //현지시간(한국)
        public DateTime localDate { get => DateTime.Now; }
        //협정 세계시(Coordinated Universal Time)
        public DateTime utcDate { get => DateTime.UtcNow; }
        public string[] cultureNames = { "euc-kr", "en-US" };

        public List<Qualification> Selection { get; set; }
        public HotPostSelection HotSelection { get; set; }
        public RealTimePostSelection RealTimeSelection { get; set; }
        public WeeklyPostSelection WeeklySelection { get; set; }

        public PostSelectionService()
        {
            manager = DataDbManager.GetInstance();

            Selection.Add(new HotPostSelection());
            Selection.Add(new RealTimePostSelection());
            Selection.Add(new WeeklyPostSelection());
        }

        public async Task Update()
        {
            foreach (var selection in Selection)
            {
                /*if(selection.TheTime == this.TheTime)
                {
                    var hotPost = new TableHotPost{
                        BoardId=a,
                        PostNumber=b,
                        Title=c,
                        UserId=d,
                        SelectionTime=d
                        }
                    manager.HotPost.Add(hotPost);
                }*/
            }
        }
    }
}
