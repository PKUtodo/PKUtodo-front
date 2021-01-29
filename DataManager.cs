using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;//反射类
using Newtonsoft.Json.Linq;
using System.Net.Mail;
using System.Diagnostics;

namespace TODO
{
    /// <summary>
    /// 数据管理类：实现本地内存数据库管理
    /// 对用户提供添加、删除、修改、查询表的内容，并保持与远端数据库的一致
    /// </summary>
    /// <returns></returns>
    public class DataManager
    {
        //维护的5张表
        public List<StudentClass> all_classes = new List<StudentClass>();//所有的学校课程
        public List<Task> class_tasks = new List<Task>();//个人课程中的任务，不需要存所有任务

        public List<StudentList> lists = new List<StudentList>();//所有的用户表单
        public List<int> person_classes = new List<int>();//所有个人的课程,其实只需要存all_class中的主键ID
        public List<Task> list_tasks= new List<Task>();///所有的私人任务

        public JObject receiver;//返回json接收器

        public UserData myuser_;
        # region 添加


        //加入课程
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
                //这段代码不会用到，仅仅为了扩展
                return true;
            }

            else if(table_name == "person_classes")
            {
                int index = get_all_class_index(item.class_id);
                if (index >= 0) {
                    string req=JSONHelper.CreateJson("join", myuser_.email,myuser_.user_id,myuser_.password,item.class_id);
                    try
                    {
                        receiver = HTTP.HttpPost(req);
                        if(receiver.Value<int>("success")==1)
                        {
                            Debug.Assert(receiver["data"].Value<int>("list_id") == item.class_id);
                            JArray tasks = receiver["data"].Value<JArray>("task_list");
                            for(int i=0;i<tasks.Count;i++)
                            {
                                all_classes[index].alltaskIDs.Add(tasks[i].Value<int>("id"));
                                Task new_class_task = new Task();
                                new_class_task.task_id = tasks[i].Value<int>("id");
                                new_class_task.name= tasks[i].Value<string>("name");
                                new_class_task.start_time = tasks[i].Value<DateTime>("create_date");
                                new_class_task.due_time= tasks[i].Value<DateTime>("due_date");
                                new_class_task.position_x= tasks[i].Value<double>("pos_x");
                                new_class_task.position_x = tasks[i].Value<double>("pos_y");
                                new_class_task.description= tasks[i].Value<string>("content");
                                new_class_task.is_finished = tasks[i].Value<bool>("is_finished");
                                new_class_task.parent_id = item.class_id;
                                class_tasks.Add(new_class_task);
                            }
                            person_classes.Add(item.class_id);
                            all_classes[index].isSelected = true;
                            return true;
                        }
                        else { return false; }
                    }
                    catch(Exception e)
                    {
                        return false;
                    }
                    
                }
                else {
                    Debug.Assert(false);
                    return false; 
                }
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
            //存在list id不匹配的问题
            new_list.list_id = item.list_id;
            new_list.taskIDs = item.taskIDs;//有没有可能是传递指针，造成危险？
            if (table_name == "lists")
            {
                string req=JSONHelper.CreateJson("add_list",myuser_.email,myuser_.user_id,myuser_.password,item.name);
                try{
                    receiver=HTTP.HttpPost(req);
                    if(receiver.Value<int>("success")==1)
                    {
                        item.list_id=receiver["data"].Value<int>("list_id");
                        lists.Add(item); return true;
                    }
                    else {return false;}
                }
                catch(Exception e)
                {
                    return false;
                }

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
            //事实上不会使用
            if(table_name== "all_class_tasks")
            {
                Debug.Assert(false);
                string req=JSONHelper.CreateJson("add_task",myuser_.email,myuser_.user_id,
                    myuser_.password,item.parent_id,item.name,item.start_time,item.due_time,
                    item.description,item.position_x,item.position_y);
                try{
                    receiver=HTTP.HttpPost(req);
                    if(receiver.Value<int>("success")==1)
                    {
                        item.task_id= receiver["data"].Value<int>("task_id"); ;
                        //all_class_tasks.Add(item);
                        return true;
                    }
                    else {return false;}
                }
                catch(Exception e)
                {
                    return false;
                }

                //@warning:
                //服务器需要发信号给所有选了这个课的人让他们更新（这显然是管理员功能）
                
            }
            else if (table_name == "person_class_tasks")
            {
                string req=JSONHelper.CreateJson("add_task",myuser_.email, myuser_.user_id,
                    myuser_.password,item.parent_id,item.name,item.start_time,item.due_time,
                    item.description,item.position_x,item.position_y);
                try{
                    receiver=HTTP.HttpPost(req);
                    if(receiver.Value<int>("success")==1)
                    {
                        item.task_id= receiver["data"].Value<int>("task_id");
                        //将task加入task列表中
                        class_tasks.Add(item);
                        //将task注册到class中
                        int class_index=get_all_class_index(item.parent_id);
                        all_classes[class_index].alltaskIDs.Add(item.task_id);
                        return true;
                    }
                    else {return false;}
                }
                catch(Exception e)
                {
                    return false;
                }
            }
            else if (table_name== "list_tasks")
            {
                string req=JSONHelper.CreateJson("add_task",myuser_.email,myuser_.user_id,
                    myuser_.password,item.parent_id,item.name,item.start_time,item.due_time,
                    item.description,item.position_x,item.position_y);
                try{
                    receiver=HTTP.HttpPost(req);
                    if(receiver.Value<int>("success")==1)
                    {
                        item.task_id=receiver["data"].Value<int>("task_id");
                        //将task加入task列表中
                        class_tasks.Add(item);
                        //将task注册到class中
                        int list_index=get_list_index(item.parent_id);
                        lists[list_index].taskIDs.Add(item.task_id);
                        //将task注册到所有私人事件表
                        list_tasks.Add(item);
                        return true;
                    }
                    else {return false;}
                }
                catch(Exception e)
                {
                    return false;
                }
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
            //该功能不存在
            if (table_name=="all_classes")
            {
                Debug.Assert(false);
                index = get_person_class_index(item.class_id);
                if (index >= 0)
                {
                    Debug.Assert(false);
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
                string req=JSONHelper.CreateJsonDelList("quit_class",myuser_.email,myuser_.user_id,myuser_.password,item.class_id);
                try{
                    receiver=HTTP.HttpPost(req);
                    if(receiver.Value<int>("success")==1)
                    {
                        int all_class_index = get_all_class_index(item.class_id);
                        index=get_person_class_index(item.class_id);
                        all_classes[all_class_index].isSelected = false;
                        Debug.Assert(index>=0);
                        //删除课程所有作业
                        for (int i = 0; i < item.alltaskIDs.Count; i++)
                        {
                            int temp_index = get_class_task_index(item.alltaskIDs[i]);
                            class_tasks.RemoveAt(temp_index);
                            item.alltaskIDs.RemoveAt(i);
                        }
                        person_classes.RemoveAt(index);
                        
                        return true;
                        
                    }
                    else {return false;}
                }
                catch(Exception e)
                {
                    return false;
                }

            }
            Debug.Assert(false);
            return false;
        }
        public bool delete(string table_name, StudentList item)
        {
            int index = 0;
            if (table_name == "lists")
            {
                string req = JSONHelper.CreateJsonDelList("del_list", myuser_.email, myuser_.user_id, myuser_.password, item.list_id);
                try
                {
                    receiver = HTTP.HttpPost(req);
                    if (receiver.Value<int>("success") == 1)
                    {
                        index = get_list_index(item.list_id);
                        Debug.Assert(index >= 0);
                        //删除清单中的所有任务
                        for (int i = 0; i < item.taskIDs.Count; i++)
                        {
                            int temp = get_list_task_index(item.taskIDs[i]);
                            list_tasks.RemoveAt(temp);
                        }
                        //删除清单
                        lists.RemoveAt(index);
                        return true;
                    }
                    else { return false; }
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            else
            {
                Debug.Assert(false);
                return false;
            }
        }
        public bool delete(string table_name, Task item)
        {
            int index = 0;
            //该功能不存在
            if (table_name == "all_class_tasks")
            {
                Debug.Assert(false);
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
            //管理员权限
            else if (table_name == "person_tasks")
            {
                string req = JSONHelper.CreateJsonDelTask("del_assignment", myuser_.email, myuser_.user_id, myuser_.password, item.task_id);
                try
                {
                    receiver = HTTP.HttpPost(req);
                    if (receiver.Value<int>("success") == 1)
                    {
                        index = get_all_class_index(item.parent_id);
                        Debug.Assert(index >= 0);
                        //删除StudentClass中task记录
                        for(int i=0;i<all_classes[index].alltaskIDs.Count;i++)
                        {
                            if (all_classes[index].alltaskIDs[i] == item.task_id)
                            {
                                all_classes[index].alltaskIDs.RemoveAt(i);
                                break;
                            }
                        }
                        //删除class_tasks中的对应task
                        int task_index = get_class_task_index(item.task_id);
                        class_tasks.RemoveAt(task_index);
                        return true;
                    }
                    else { return false; }
                }
                catch (Exception e)
                {
                    return false;
                }

            }
            else if (table_name == "list_tasks")
            {
                string req = JSONHelper.CreateJsonDelTask("del_task", myuser_.email, myuser_.user_id, myuser_.password, item.task_id);
                try
                {
                    receiver = HTTP.HttpPost(req);
                    if (receiver.Value<int>("success") == 1)
                    {
                        index = get_list_index(item.parent_id);
                        Debug.Assert(index >= 0);
                        //删除StudentClass中task记录
                        for (int i = 0; i < lists[index].taskIDs.Count; i++)
                        {
                            if (lists[index].taskIDs[i] == item.task_id)
                            {
                                lists[index].taskIDs.RemoveAt(i);
                                break;
                            }
                        }
                        //删除class_tasks中的对应task
                        int task_index = get_list_task_index(item.task_id);
                        list_tasks.RemoveAt(task_index);
                        return true;
                    }
                    else { return false; }
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            else
            {
                Debug.Assert(false);
                return false;
            }
        }
        public void delete(string table_name, int index)//这是索引而不是ID，要小心
        {
            //删除对应表格对应索引处的东西，不进行检查，因为别的函数检查过了
            if(table_name== "all_classes") { all_classes.RemoveAt(index); }
            else if (table_name == "all_class_tasks") { class_tasks.RemoveAt(index); }
            else if (table_name == "lists") { lists.RemoveAt(index); }
            else if (table_name == "person_classes") { person_classes.RemoveAt(index); }
            else if (table_name == "person_class_tasks") { class_tasks.RemoveAt(index); }
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
        public int get_class_task_index(int task_id)
        {
            //通过task_id找到在class_tasks中的索引
            for (int i = 0; i < class_tasks.Count; i++)
            {
                if (class_tasks[i].task_id == task_id)
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
        public int get_all_class_index(int class_id)
        {
            //通过class_id找到在person_classes中的索引
            for (int i = 0; i < all_classes.Count; i++)
            {
                if (all_classes[i].class_id== class_id)
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
            for (int i = 0; i <class_tasks.Count; i++)
            {
                if (class_tasks[i].task_id == task_id)
                {
                    return i;
                }
            }
            //如果没有找到，task_id
            return -1;
        }
        public int get_person_class_index(int class_id)
        {
            //通过class_id找到在person_classes中的索引
            for (int i = 0; i < person_classes.Count; i++)
            {
                if (person_classes[i] == class_id)
                {
                    return i;
                }
            }
            //如果没有找到，返回class_id
            return -1;
        }
        public int correct_class_index(int class_id)
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
        //下载所有用户数据
        public bool update_all()
        {
            string req=JSONHelper.CreateJson("refresh", myuser_.email, myuser_.user_id, myuser_.password);
            try
            {
                receiver = HTTP.HttpPost(req);
                if(receiver.Value<int>("success")==1)
                {
                    all_classes.Clear();
                    class_tasks.Clear();
                    list_tasks.Clear();
                    lists.Clear();
                    person_classes.Clear();
                    JArray classes = receiver["data"].Value<JArray>("class");
                    for (int i = 0; i < classes.Count; i++)
                    {
                        Debug.Assert(classes[i].Value<bool>("is_public") == true);
                        StudentClass new_class = new StudentClass();
                        new_class.class_id = classes[i].Value<int>("list_id");
                        new_class.name = classes[i].Value<string>("list_name");
                        //new_class.description = classes[i].Value<string>("content");
                        new_class.admin_id = classes[i].Value<int>("admin_id");
                        all_classes.Add(new_class);

                    }
                    JArray new_lists = receiver["data"].Value<JArray>("list");
                    for (int i = 0; i < new_lists.Count; i++)
                    {
                        if(new_lists[i].Value<bool>("is_public")==true)
                        {
                            //TODO：检查是否满足外码条件
                            person_classes.Add(new_lists[i].Value<int>("list_id"));

                        }
                        else
                        {
                            StudentList new_list = new StudentList();
                            new_list.list_id = new_lists[i].Value<int>("list_id");
                            new_list.name = new_lists[i].Value<string>("list_name");
                            lists.Add(new_list);
                        }
                    }
                    JArray tasks = receiver["data"].Value<JArray>("task");
                    for (int i = 0; i < tasks.Count; i++)
                    {
                        Task new_task = new Task();
                        new_task.task_id = tasks[i].Value<int>("task_id");
                        new_task.name = tasks[i].Value<string>("task_name");
                        new_task.start_time = tasks[i].Value<DateTime>("create_date");
                        new_task.due_time = tasks[i].Value<DateTime>("due_date");
                        //new_task.position_x = tasks[i]["position_x"]==null?0:tasks[i].Value<double>("position_x");
                        JToken test = tasks[i]["position_x"];
                        if (test.HasValues==false)
                        {
                            new_task.position_x = 0;
                        }
                        else
                        {
                            new_task.position_x = tasks[i].Value<double>("position_x");
                        }
                        test= tasks[i]["position_y"];
                        if (test.HasValues == false)
                        {
                            new_task.position_y = 0;
                        }
                        else
                        {
                            new_task.position_y = tasks[i].Value<double>("position_y");
                        }
                        new_task.description = tasks[i].Value<string>("content");
                        new_task.is_finished = tasks[i].Value<bool>("is_finished");
                        new_task.parent_id = tasks[i].Value<int>("list_id");

                        bool find = false;
                        for(int j=0;j<all_classes.Count;j++)
                        {
                            if(all_classes[j].class_id==new_task.parent_id)
                            {
                                Debug.Assert(find == false);
                                all_classes[j].alltaskIDs.Add(new_task.task_id);
                                class_tasks.Add(new_task);
                                find = true;
                                break;
                            }
                        }
                        for(int j=0;j<lists.Count;j++)
                        {
                            if(lists[j].list_id==new_task.parent_id)
                            {
                                Debug.Assert(find == false);
                                lists[j].taskIDs.Add(new_task.task_id);
                                list_tasks.Add(new_task);
                                find = true;
                                break;
                            }
                        }
                        Debug.Assert(find == true);
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public bool update(string table_name)
        {
            /*更新某个表*/
            //如果更新的是lists，同时要更新list_tasks
            //如果更新的是person_classes,同时要更新class_tasks
            //如果更新list_tasks，lists也要更新（因为taskID会变）
            //如果更新class_tasks，也要更新person_classes和all_classes
            return false;

        }
        #endregion
    }
}

//想要删除和增加课程，实际上需要教学网级别的管理员
//想要删除和增加课程作业，实际上需要课程级别的管理员
//本项目只能实现课程级别的管理员，手工充当教学网管理员