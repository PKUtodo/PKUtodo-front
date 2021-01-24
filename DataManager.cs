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
        public List<int> person_classes = new List<int>();//所有个人的课程,其实只需要存all_class中的主键ID
        public List<int> person_class_tasks = new List<int>();//所有选课的公有任务，其实只需要存all_class_tasks中的主键ID
        public List<Task> list_tasks= new List<Task>();///所有的私人任务

        # region 添加
        //public bool add<T>(string table_name, T item)
        //{
        //    var type = typeof(T);   //反射对象
        //    var A = type.GetProperties(); //获取对象属性
        //    //如果是课程
        //    if (typeof(T) == typeof(StudentClass))
        //    {
        //        StudentClass new_class = new StudentClass();
        //        new_class.class_id = (int)A[0].GetValue(item);
        //        new_class.name = (string)A[1].GetValue(item);
        //        new_class.teacher_name = (string)A[2].GetValue(item);
        //        new_class.score = (int)A[3].GetValue(item);
        //        new_class.description = (string)A[4].GetValue(item);
        //        new_class.alltaskIDs = (List<int>)A[5].GetValue(item);

        //        if (table_name == "all_classes")
        //        {
        //            all_classes.Add(new_class); return true;
        //        }
        //        else if (table_name == "person_classes")
        //        {
        //            person_classes.Add(new_class); return true;
        //        }
        //        else
        //        {
        //            //出错
        //            return false;
        //        }
        //    }
        //    //如果是表单
        //    else if (typeof(T) == typeof(StudentList))
        //    {
        //        return true;
        //    }
        //    //如果是任务
        //    else if (typeof(T) == typeof(Task))
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        //出错
        //        return false;
        //    }
        //}
        public bool add(string table_name,StudentClass item)
        {
            StudentClass new_class = new StudentClass();
            new_class.class_id = item.class_id;
            new_class.name = item.name;
            new_class.teacher_name = item.teacher_name;
            new_class.score = item.score;
            new_class.description = item.description;
            new_class.alltaskIDs = item.alltaskIDs;
            if (table_name == "all_classes")
            {
                all_classes.Add(new_class); 
                return true;
            }
            else if(table_name == "person_classes")
            {
                int index = get_class_index(item.class_id);
                if (index >= 0) { person_classes.Add(index); return true; }
                else { return false; }
            }
            else
            {
                return false;
            }
        }

        public bool add(string table_name, StudentList item)
        {
            StudentList new_list= new StudentList();
            new_list.name = item.name;
            new_list.list_id = item.list_id;
            new_list.taskIDs = item.taskIDs;//有没有可能是传递指针，造成危险？
            if (table_name == "lists")
            {
                lists.Add(item); return true;
            }
            else
            {
                return false;
            }
        }

        public bool add(string table_name, Task item)
        {
            Task new_task = new Task();
            new_task.task_id = item.task_id;
            new_task.start_time = item.start_time;
            new_task.parent_id = item.parent_id;
            new_task.name = item.name;
            new_task.description = item.description;
            new_task.due_time = item.due_time;
            if(table_name== "all_class_tasks")
            {
                all_class_tasks.Add(item);
                //@warning:
                //服务器需要发信号给所有选了这个课的人让他们更新（这显然是管理员功能）

                return true;
            }
            else if (table_name == "person_class_tasks")
            {
                //这个item必须是all_class_tasks中已经有的，否则没有办法布置
                int index = get_all_class_task_index(item.task_id);
                if (index >= 0)
                { person_class_tasks.Add(index); return true; }
                else { return false; }
            }
            else if (table_name== "list_tasks")
            {
                all_class_tasks.Add(item); return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
        
        # region 删除
        public bool delete(string table_name, StudentClass item)
        {
            int index = 0;
            if (table_name=="all_classes")
            {
                index = get_class_index(item.class_id);
                if (index >= 0)
                { 
                    //删除课程所有作业
                    for(int i=0;i<item.alltaskIDs.Count;i++)
                    {
                        int temp = get_all_class_task_index(item.alltaskIDs[i]);
                        delete("all_classes", temp);
                    }
                    //@warning:
                    //删除所有选修这门课的人的person_classes
                    //这一点目前还没有办法实现，可能在处理管理员的时候出现一些问题

                    delete(table_name, index); 
                    return true; }
                else { return false; }
            }
            else if (table_name == "person_classes")
            {
                index = get_person_class_index(item.class_id);
                if (index >= 0)
                {
                    //删除课程所有作业
                    for (int i = 0; i < item.alltaskIDs.Count; i++)
                    {
                        int temp = get_all_class_task_index(item.alltaskIDs[i]);
                        delete("all_classes", temp);
                    }
                    delete(table_name, index); 
                    return true; }
                else { return false; }
            }
            else
            {
                return false;
            }
        }
        public bool delete(string table_name, StudentList item)
        {
            int index = 0;
            if (table_name == "lists")
            {
                index = get_list_index(item.list_id);
                if (index >= 0)
                {
                    //删除清单中的所有任务
                    for (int i = 0; i < item.taskIDs.Count; i++)
                    {
                        int temp = get_list_task_index(item.taskIDs[i]);
                        delete("list_tasks", temp);
                    }
                    //删除清单
                    delete(table_name, index);
                    return true; }
                else { return false; }
            }
            else
            {
                return false;
            }
        }
        public bool delete(string table_name, Task item)
        {
            int index = 0;
            if (table_name == "all_class_tasks")
            {
                index = get_all_class_task_index(item.task_id);
                if (index >= 0)
                { 
                    //@warning：
                    //还需要删除所有用户的该任务，这里没写

                    delete(table_name, index);
                    return true; 
                }
                else { return false; }
            }
            else if (table_name == "person_tasks")
            {
                index = get_person_class_task_index(item.task_id);
                if (index >= 0)
                { delete(table_name, index); return true; }
                else { return false; }
            }
            else if (table_name == "list_tasks")
            {
                index = get_list_task_index(item.task_id);
                if (index >= 0)
                { delete(table_name, index); return true; }
                else { return false; }
            }
            else
            {
                return false;
            }
        }
        public void delete(string table_name, int index)//这是索引而不是ID，要小心
        {
            //删除对应表格对应索引处的东西，不进行检查，因为别的函数检查过了
            if(table_name== "all_classes") { all_classes.RemoveAt(index); }
            else if (table_name == "all_class_tasks") { all_class_tasks.RemoveAt(index); }
            else if (table_name == "lists") { lists.RemoveAt(index); }
            else if (table_name == "person_classes") { person_classes.RemoveAt(index); }
            else if (table_name == "person_class_tasks") { person_class_tasks.RemoveAt(index); }
            else if (table_name == "list_tasks") { list_tasks.RemoveAt(index); }
        }
        #endregion

        #region 修改
        //暂时先不动
        #endregion

        #region 查询
        public int get_list_task_index(int task_id)
        {
            //通过task_id找到在list_task中的索引
            for (int i = 0; i < list_tasks.Count; i++)
            {
                if (list_tasks[i].task_id == task_id)
                {
                    return i;
                }
            }
            //如果没有找到，返回task_id
            return -1;
        }
        public int get_person_class_task_index(int task_id)
        {
            //通过task_id找到在person_class_tasks中的索引
            for (int i = 0; i < person_class_tasks.Count; i++)
            {
                if (person_class_tasks[i] == task_id)
                {
                    return i;
                }
            }
            //如果没有找到，返回task_id
            return -1;
        }
        public int get_list_index(int list_id)
        {
            //通过list_id找到在lists中的索引
            for (int i = 0; i <lists.Count; i++)
            {
                if (lists[i].list_id == list_id)
                {
                    return i;
                }
            }
            //如果没有找到，返回list_id
            return -1;
        }
        public int get_person_class_index(int class_id)
        {
            //通过class_id找到在person_classes中的索引
            for (int i = 0; i < person_classes.Count; i++)
            {
                if (person_classes[i]== class_id)
                {
                    return i;
                }
            }
            //如果没有找到，返回class_id
            return -1;
        }
        public int get_all_class_task_index(int task_id)
        {
            //通过task_id找到在all_class_tasks中的索引
            for (int i = 0; i < all_class_tasks.Count; i++)
            {
                if (all_class_tasks[i].task_id == task_id)
                {
                    return i;
                }
            }
            //如果没有找到，task_id
            return -1;
        }
        public int get_class_index(int class_id)
        {
            if((class_id>-1)&&(class_id<all_classes.Count))
            {
                return class_id;//一一对应
            }
            return -1;
        }

        #endregion

        # region 传输远端

        #endregion

        # region 远端下载

        #endregion
    }
}

//想要删除和增加课程，实际上需要教学网级别的管理员
//想要删除和增加课程作业，实际上需要课程级别的管理员
//本项目只能实现课程级别的管理员，手工充当教学网管理员