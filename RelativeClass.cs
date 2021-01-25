using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODO
{
    public class StudentList
    {
        public string name;
        public int list_id;
        public List<int> taskIDs=new List<int>();//包含的任务索引
    }
    public class Task
    {
        public string name;
        public int task_id;//全局唯一索引
        public DateTime start_time;
        public DateTime due_time;
        public string description;
        public int parent_id;//父辈的class或者list的id
        //public string parent_type;//父辈是class还是list,之所以用它不好，因为和Form的耦合性太强

        public double position_x=0;
        public double position_y=0;
    }
    public class StudentClass
    {
        public int class_id;//课程编号
        public string name;
        public string teacher_name;
        public int score;//学分
        public string description;
        public List<int> alltaskIDs = new List<int>();//包含的任务索引
    }
}
