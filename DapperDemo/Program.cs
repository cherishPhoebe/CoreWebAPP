using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DapperDemo
{
    class Program
    {
        // 数据库连接字符串
        private const string connectionStr = @"Data Source=127.0.0.1;User ID=sa;Password=sa;Initial Catalog=DapperDemo;Pooling=true;Max Pool Size=100;";

        static void Main(string[] args)
        {
            Console.WriteLine("Start Test : ");
            do
            {
                var key = Console.ReadLine();
                var actionArr = key.Split('_');
                var action = actionArr[0];
                switch (action.ToUpper())
                {
                    case "C":
                        test_insert();
                        break;
                    case "MC":
                        test_mult_insert();
                        break;
                    case "R":
                        test_select_one(Convert.ToInt32(actionArr[1]));
                        break;
                    case "GETALL":
                        test_select_all();
                        break;
                    case "U":
                        test_update(Convert.ToInt32(actionArr[1]), actionArr[2], actionArr[3]);
                        break;
                    case "D":
                        break;
                    case "EXIT":
                        Environment.Exit(0);
                        break;
                    case "LINQ":
                        test_linq_forXml();
                        break;
                    default:
                        Console.WriteLine("请输入合法的指令!");
                        break;
                }
            } while (true);

        }

        /// <summary>
        /// 插入数据
        /// </summary>
        static void test_insert()
        {
            var content = new Content
            {
                title = "标题1",
                content = "内容1"
            };

            using (var conn = new SqlConnection(connectionStr))
            {
                string sql_insert = @"Insert into content (title,[content],status,add_time,modify_time)
values (@title,@content,@status,@add_time,@modify_time)";
                var result = conn.Execute(sql_insert, content);

                Console.WriteLine($"test_insert:插入了{result}条数据!");
            }
        }

        static void test_mult_insert()
        {
            List<Content> contents = new List<Content>() {
                new Content{
                    title="批量插入标题1",
                    content="批量插入内容1"
                },new Content{
                    title="批量插入标题2",
                    content="批量插入内容2"
                }
            };
            using (var conn = new SqlConnection(connectionStr)) {
                string sql_mult_insert = @"Insert into content (title,[content],status,add_time,modify_time)
values (@title,@content,@status,@add_time,@modify_time)";

                var result = conn.Execute(sql_mult_insert, contents);

                Console.WriteLine($"test_mult_insert:批量插入数据{result}条!");
            }
        }


        static void test_update(int id, string title, string content)
        {
            var updateContent = new Content
            {
                id = id,
                title = title,
                content = content,
                modify_time = DateTime.Now
            };

            using (var conn = new SqlConnection(connectionStr)) {
                var sql_update = "update content set title=@title,content=@content,modify_time=@modify_time where id = @id";
                var result = conn.Execute(sql_update, updateContent);
                Console.WriteLine($"test_update:更新了id为{updateContent.id}的记录{result}条!");
            }
        }


        static void test_select_one(int id) {
            using (var conn = new SqlConnection(connectionStr)) {
                string sql_select = @"select * from content where id = @id";
                var result = conn.QueryFirstOrDefault<Content>(sql_select, new { id = id });
                Console.WriteLine("id:{0};title:{1};content:{2},addtime:{3}",result.id.ToString(), result.title, result.content, result.add_time.ToString("yyyy-MM-dd HH:mm:ss"));                
            }
        }

        static void test_select_all()
        {
            using (var conn = new SqlConnection(connectionStr))
            {
                string sql_select = @"select * from content";
                var result = conn.Query<Content>(sql_select);
                foreach (var item in result)
                {
                    Console.WriteLine("id:{0};title:{1};content:{2},addtime:{3}", item.id.ToString(), item.title, item.content, item.add_time.ToString("yyyy-MM-dd HH:mm:ss"));
                }
            }

        }


        static void test_linq_forXml() {
            var jobList = new List<JobEntity>();
            jobList.Add(new JobEntity { Id = 1, Job = "ZY", Name = "lZY" });
            jobList.Add(new JobEntity { Id = 1, Job = "ZY1", Name = "lZY1" });
            jobList.Add(new JobEntity { Id = 2, Job = "ZY2", Name = "lZY2" });
            jobList.Add(new JobEntity { Id = 2, Job = "ZY22", Name = "lZY22" });
            jobList.Add(new JobEntity { Id = 2, Job = "ZY222", Name = "lZY222" });
            jobList.Add(new JobEntity { Id = 3, Job = "ZY3", Name = "lZY3" });
            jobList.Add(new JobEntity { Id = 3, Job = "ZY33", Name = "lZY33" });
            jobList.Add(new JobEntity { Id = 3, Job = "ZY333", Name = "lZY333" });

            var result = jobList.GroupBy(e => e.Id).ToList()
                        .Select(eg => new
                        {
                            Id = eg.Key,
                            Job = string.Join(",",eg.Select(i => i.Job)),
                            Name = string.Join(",", eg.Select(i => i.Name)),
                            Total = eg.Count()
                        }).ToList();

            foreach (var item in result)
            {
                Console.WriteLine("id:{0};job:{1};name:{2};Total:{3}", item.Id.ToString(), item.Job, item.Name,item.Total);
            }

        }


    }


    public class JobEntity {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Job { get; set; }

        public string Total { get; set; }
    }

}
