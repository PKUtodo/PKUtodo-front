using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;//反射类

namespace TODO
{
    /// <summary>
    /// 数据管理类：实现本地内存数据库管理
    /// 对用户提供添加、删除、修改、查询表的内容，并保持与远端数据库的一致
    /// </summary>
    /// <returns></returns>
    public class DataManager
    {
        //维护的6张表
        public List<StudentClass> all_classes = new List<StudentClass>();//所有的学校课程
        public List<Task> all_class_tasks = new List<Task>();//所有课程中的任务

        public List<StudentList> lists = new List<StudentList>();//所有的用户表单
        public List<StudentClass> person_classes = new List<StudentClass>();//所有个人的课程
        public List<int> person_class_tasks = new List<int>();//所有选课的公有任务，其实只需要存all_class_tasks中的主键ID
        public List<Task> list_tasks= new List<Task>();///所有的私人任务

        # region 添加
        public bool add<T>(string table_name,T item)
        {
            var type = typeof(T);   //反射对象
            var A = type.GetProperties(); //获取对象属性
            //如果是课程
            if (typeof(T) == typeof(StudentClass))
            {
                StudentClass new_class = new StudentClass();
                new_class.class_id=(int)A[0].GetValue(item);
                new_class.name=(string)A[1].GetValue(item);
                new_class.teacher_name= (string)A[2].GetValue(item);
                new_class.score=(int)A[3].GetValue(item);
                new_class.description=(string)A[4].GetValue(item);
                new_class.alltaskIDs=(List<int>)A[5].GetValue(item);

                if (table_name=="all_classes")
                {
                    all_classes.Add(new_class);return true;
                }
                else if(table_name == "person_classes")
                {
                    person_classes.Add(new_class); return true;
                }
                else
                {
                    //出错
                    return false;
                }
            }
            //如果是表单
            else if (typeof(T) == typeof(StudentList))
            {
                return true;
            }
            //如果是任务
            else if (typeof(T) == typeof(Task))
            {
                return true;
            }
            else
            {
                //出错
                return false;
            }
        }
        #endregion
        # region 删除

        #endregion

        # region 修改

        #endregion

        # region 查询

        #endregion

        # region 传输远端

        #endregion

        # region 远端下载

        #endregion
    }
}
