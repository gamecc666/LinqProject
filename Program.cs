using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

//全程MSDN：https://docs.microsoft.com/zh-cn/dotnet/csharp/programming-guide/concepts/linq/walkthrough-writing-queries-linq
namespace LinqProject
{
    public class PStudent
    {
        #region  Linq入门-->模块八-->数据类
        public string First { get; set; }
        public string Last { get; set; }
        public int ID { get; set; }
        public List<int> Scores;        
        #endregion
    }
    class Program
    {
        #region 模块八-->初始化数据源
        static List<PStudent> pstudents = new List<PStudent>
            {
                new PStudent {First="Svetlana", Last="Omelchenko", ID=111, Scores= new List<int> {97, 92, 81, 60}},
                new PStudent {First="Claire", Last="O'Donnell", ID=112, Scores= new List<int> {75, 84, 91, 39}},
                new PStudent {First="Sven", Last="Mortensen", ID=113, Scores= new List<int> {88, 94, 65, 91}},
                new PStudent {First="Cesar", Last="Garcia", ID=114, Scores= new List<int> {97, 89, 85, 82}},
                new PStudent {First="Debra", Last="Garcia", ID=115, Scores= new List<int> {35, 72, 91, 70}},
                new PStudent {First="Fadi", Last="Fakhouri", ID=116, Scores= new List<int> {99, 86, 90, 94}},
                new PStudent {First="Hanying", Last="Feng", ID=117, Scores= new List<int> {93, 92, 80, 87}},
                new PStudent {First="Hugo", Last="Garcia", ID=118, Scores= new List<int> {92, 90, 83, 78}},
                new PStudent {First="Lance", Last="Tucker", ID=119, Scores= new List<int> {68, 79, 88, 92}},
                new PStudent {First="Terry", Last="Adams", ID=120, Scores= new List<int> {99, 82, 81, 79}},
                new PStudent {First="Eugene", Last="Zabokritski", ID=121, Scores= new List<int> {96, 85, 91, 60}},
                new PStudent {First="Michael", Last="Tucker", ID=122, Scores= new List<int> {94, 92, 91, 91}}
            };
        #endregion
        static void Main(string[] args)
        {
            //LINQ入门
            #region 模块一
            Console.WriteLine("*********************************************GameCC666-Linq学习**********************************************");
            int[] numbers = new int[7] { 1, 2, 3, 4, 5, 6, 7 };

            //直接将查询结果转换成List(),也可以将ToList()换为ToArray()
            List<int> numquerylist =
                (from element in numbers
                 where (element % 2 == 0)
                 select element).ToList();
            Console.WriteLine("直接转换成List<int>类型时候,获得的数据个数：{0}\n", numquerylist.Count);

            var numquery =
                from num in numbers
                where (num % 2 == 0)
                select num;

            foreach (int num in numquery)
            {
                Console.WriteLine("得到的值：{0}", num);
            }
            #endregion
            #region 模块二
            Console.WriteLine("\n-----------------------------------------------------------------------\n");
            List<Student> students = new List<Student>()
            {
                new Student{
                    First="Svetlana",
                    Last="Omel",
                    ID=1111,
                    Street="123 main street",
                    City="Seattle",
                    Scores=new List<int>{97,92,81,60},
                },
                new Student{
                    First="Claire",
                    Last="Donnell",
                    ID=112,
                    Street="124 main Street",
                    Scores=new List<int>{ 75,84,91,39,}
                },
                new Student{
                    First="Sven",
                    Last="Morten",
                    ID=113,
                    Street="125 Main street",
                    City="Lake city",
                    Scores=new List<int>{ 88,94,65,91,}
                },
            };
            List<Teacher> teachers = new List<Teacher>()
            {
                new Teacher{First="Ann",Last="Beebe",ID=945,City="Seattle"},
                new Teacher{First="Alex", Last="Robinson", ID=956, City="Redmond"},
                new Teacher{First="Michiyo", Last="Sato", ID=972, City="Tacoma"}
            };
            var resquery = (
                from student in students
                where student.City == "Seattle"
                select student.Last)
                .Concat(
                    from teacher in teachers
                    where teacher.City == "Seattle"
                    select teacher.Last
                );
            Console.WriteLine("住在同一地区的老师和同学:::");
            foreach (var persion in resquery)
            {
                Console.WriteLine(persion);
            }
            #endregion
            #region 模块三
            Console.WriteLine("\n----------------------将内存对象转换为XML-----------------------------\n");
            //KeyNote：
            //       a:let子句用于在查询中添加一个新的局部变量，使其在后面的查询中可见，类似sql中为as关键字为字段起别名;可参考web:https://www.cnblogs.com/wolf-sun/p/4270911.html
            //       b:$ 字符串内插，参考web:https://docs.microsoft.com/zh-cn/dotnet/csharp/language-reference/tokens/interpolated
            List<Student> _students = new List<Student>() {
                new Student{First="Svetlana",Last="Omelchenko",ID=111,Scores=new List<int>{ 97,92,81,60} },
                new Student{First="Claire", Last="O’Donnell", ID=112, Scores = new List<int>{75, 84, 91, 39} },
                new Student{First="Sven", Last="Mortensen", ID=113, Scores = new List<int>{88, 94, 65, 91}},
            };
            var _studentToXML = new XElement("Root",
                from _student in _students
                let _scores = string.Join(",", _student.Scores)
                select new XElement("student",
                    new XElement("First", _student.First),
                    new XElement("Last", _student.Last),
                    new XElement("_scores", _scores)
                    )
                );
            Console.WriteLine("生成的XML文件\n" + _studentToXML);
            #endregion
            #region 模块四
            Console.WriteLine("\n------------------------对源元素进行操作------------------------------\n");
            double[] radii = { 1, 2, 3 };
            IEnumerable<string> query =
                from rad in radii
                select $"Area={rad * rad * Math.PI:F2}";
            foreach (string s in query)
            {
                Console.WriteLine("不同半径对应的面积是：{0}\n", s);
            }
            #endregion
            #region 模块五
            Console.WriteLine("\n------------------------不转换源数据的查询----------------------------\n");
            List<string> _names = new List<string> { "Jon", "Rick", "Maggie", "Mary" };
            IEnumerable<string> _namequery = from name in _names
                                             where name[0] == 'M'
                                             select name;
            foreach (string str in _namequery)
            {
                Console.WriteLine("输出源数据中符合条件的元素：{0}\n", str);
            }
            #endregion
            #region 模块六
            Console.WriteLine("\n-----------转换源数据的查询-直接见代码中的注释代码理解就行------------\n");
            /*
             *----------------------简单的转换数据源的查询--------------------
             * Table<Customer> Customers=db.GetTable<Customers>();
             * IQuery<string> custNameQuery=
             *                       from cust in Customers
             *                       where cust.City=='Landon'
             *                       select cust.Name;
             *foreach(string  str in custNameQuery)                       
             * {
             *      Console.WriteLine(str);
             * }
             *     目的：查询将一个 Customer 对象序列用作输入，并只选择结果中的 Name 属性
             * 例子分析：             
             *          1：数据源的类型参数决定范围变量的类型。
             *          2：select 语句返回 Name 属性，而非完整的 Customer 对象。 因为 Name 是一个字符串，所以 custNameQuery 的类型参数是 string，而非 Customer。
             *          3：因为 custNameQuery 是一个字符串序列，所以 foreach 循环的迭代变量也必须是 string。
             *----------------------复杂一点的的转换数据源的查询--------------------          
             * Table<Customer> Customers=db.GetTable<Customers>();
             *var namePhoneQuery=
             *              from cust in Customers
             *              where cust.City=='Landon'
             *              select new { name=cust.Name,
             *                     phone=cust.Phone  
             *              };
             *foreach(var item in namePhoneQuery)                       
             * {
             *      Console.WriteLine(item);
             * }
             *     目的：select 语句返回只捕获原始 Customer 对象的两个成员的匿名类型。
             * 例子分析：
             *         1：数据源的类型参数始终为查询中范围变量的类型。
             *         2：因为 select 语句生成匿名类型，所以必须使用 var 隐式类型化查询变量。
             *         3：因为查询变量的类型是隐式的，所以 foreach 循环中的迭代变量也必须是隐式的。
             */
            #endregion
            #region 模块七
            Console.WriteLine("\n-----------------------标准查询运算符扩展方法-------------------------\n");
            int[] _numbers = { 5, 10, 8, 3, 6, 12 };
            //Query syntax:
            IEnumerable<int> _numQuery1 =
                from num in _numbers
                where num % 2 == 0
                orderby num
                select num;
            //Method syntax:
            IEnumerable<int> _numQuery2 = _numbers.Where(num => num % 2 == 0).OrderBy(n => n);
            Console.WriteLine("基于查询语法的查询结果");
            foreach (int i in _numQuery1)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("基于方法语法的查询结果");
            foreach (int i in _numQuery2)
            {
                Console.Write(i + " ");
            }
            #endregion
            #region 模块八
            Console.WriteLine("\n\n-----------------------用C#编写查询（Linq）演练-------------------------\n");
            //目的创建简单查询；得到第一次测试中分数高于90分的学生列表            
            IEnumerable<PStudent> _studentQuery =
                from student in pstudents
                where student.Scores[0] > 90
                select student;
            Console.WriteLine("查询分数高于90的人:");
            foreach (PStudent student in _studentQuery)
            {
                Console.WriteLine("{0},{1}", student.Last, student.First);
            }
            IEnumerable<PStudent> _studentQuery2 =
                from student in pstudents
                where student.Scores[0] > 90 && student.Scores[3] < 80
                select student;
            Console.WriteLine("\n查询第一个分数大于90，最后一个小于80的人:");
            foreach (PStudent student in _studentQuery2)
            {
                Console.WriteLine("{0},{1}", student.Last, student.First);
            }
            //修改查询（对结果进行排序）
            IEnumerable<PStudent> _studentQuery3 =
                from student in pstudents
                where student.Scores[0] > 90 && student.Scores[3] < 80 orderby student.Last ascending
                select student;
            Console.WriteLine("\n查询第一个分数大于90，最后一个小于80的人并按照名字顺序打印出来:");
            foreach (PStudent student in _studentQuery3)
            {
                Console.WriteLine("{0},{1}", student.Last, student.First);
            }
            IEnumerable<PStudent> _studentQuery4 =
                from student in pstudents
                where student.Scores[0] > 90 orderby student.Scores[0] descending
                select student;
            Console.WriteLine("\n查询分数高于90的人并按照由高到低的顺序打印出来:");
            foreach (PStudent student in _studentQuery4)
            {
                Console.WriteLine("{0},{1} ----> {2}", student.Last, student.First, student.Scores[0]);
            }
            //对结果进行分组
            var _studentQuery5 =
                from student in pstudents
                group student by student.Last[0];
            Console.WriteLine("\n根据查询的结果的首字符作为相应的键来进行分类：");
            foreach (var studentGroup in _studentQuery5)
            {
                Console.WriteLine("Key=>{0}", studentGroup.Key);
                foreach (PStudent student in studentGroup)
                {
                    Console.WriteLine("   data:{0}.{1}", student.Last, student.First);
                }
            }
            Console.WriteLine("\n根据查询的结果的首字符作为相应的键来进行分类(使用对变量隐式类型化的方法)：");
            foreach (var studentGroup in _studentQuery5)
            {
                Console.WriteLine("Key=>{0}", studentGroup.Key);
                foreach (var student in studentGroup)
                {
                    Console.WriteLine("   data:{0}.{1}", student.Last, student.First);
                }
            }
            //按照键值对组进行排序
            var _studentQuery6 =
                from student in pstudents
                group student by student.Last[0] into studentGroup
                orderby studentGroup.Key
                select studentGroup;
            Console.WriteLine("\n将查找的结果的-键-按照字母的顺序进行排序：");
            foreach (var groupOfStudents in _studentQuery6)
            {
                Console.WriteLine("Key=>{0}", groupOfStudents.Key);
                foreach (var student in groupOfStudents)
                {
                    Console.WriteLine("    data:{0}.{1}", student.Last, student.First);
                }
            }
            //使用let引入标识符
            /*
             * KeyNote：
             *       1:使用-let-关键字来引入查询表达式中任何表达式结果的标识符
             */
            var _studentQuery7 =
               from student in pstudents
               let totalScore = student.Scores[0] + student.Scores[1] + student.Scores[2] + student.Scores[3]
               where totalScore / 4 < student.Scores[0]
               select "平均分：" + totalScore / 4 + "|第一次成绩：" + student.Scores[0] + "|学生姓名：" + student.Last + "-" + student.First;
            Console.WriteLine("\n使用let标识符选择平均分<第一次的成绩：");
            foreach (string s in _studentQuery7)
            {
                Console.WriteLine("     {0}", s);
            }
            #endregion

            //标准查询运算符概述
            #region
            string _sentence = "the quick brown fox jumps over the lazy dog";
            string[] _words = _sentence.Split(' ');
            //使用查询语法查询
            var _query = from word in _words
                         group word.ToUpper() by word.Length into gr
                         orderby gr.Key
                         select new { Length = gr.Key, Words = gr };
            //使用方法语法查询
            var _query2 = _words.GroupBy(w => w.Length, w => w.ToUpper()).
                Select(g => new { Length = g.Key, Words = g }).
                OrderBy(o => o.Length);
            Console.WriteLine("\n将英文句子拆成字符串数组然后使用linq进行查询;");
            foreach (var obj in _query)
            {
                Console.WriteLine("word of length {0}:", obj.Length);
                foreach (string word in obj.Words)
                {
                    Console.WriteLine("   {0}", word);
                }
            }
            #endregion

            //LINQ to Objects
            #region 模块一
            /*
             * KeyNote：
             *        1：在对一句话使用-split-方法时存在性能开销，如果只是统计字符串的字数，可以考虑使用-Matches-,-IndexOf-方法
             *        2：StringSplitOptions.RemoveEmptyEntries => 根据拆分之后的字符串数组，移除其中为空的元素
             *        3：String.ToLowerInvariant()=> 返回时使用固定区域性的大小写规则,该固定区域性表示不区分区域性的区域性,它与英语语言关联，不与任何国家/地区关联。 详细信息请见：https://docs.microsoft.com/zh-cn/previous-versions/dotnet/netframework-4.0/4c5zdc6a(v=vs.100)
             */
            Console.WriteLine("\n\n-------------------对某个词在字符串中出现的次数进行计数----------------------");
            string _text = @"Historically, the world of data and the world of objects" +
                          @" have not been well integrated. Programmers work in C# or Visual Basic" +
                          @" and also in SQL or XQuery. On the one side are concepts such as classes," +
                          @" objects, fields, inheritance, and .NET Framework APIs. On the other side" +
                          @" are tables, columns, rows, nodes, and separate languages for dealing with" +
                          @" them. Data types often require translation between the two worlds; there are" +
                          @" different standard functions. Because the object world has no notion of query, a" +
                          @" query can only be represented as a string without compile-time type checking or" +
                          @" IntelliSense support in the IDE. Transferring data from SQL tables or XML trees to" +
                          @" objects in memory is often tedious and error-prone.";
            string _searchTerem = "data";
            Console.WriteLine("输出单词出现的次数：");
            string[] _source = _text.Split(new char[] { '.', '?', '!', ' ', ';', ':', ',' }, StringSplitOptions.RemoveEmptyEntries);
            var _matchQuery = from word in _source
                              where word.ToLowerInvariant() == _searchTerem.ToLowerInvariant()
                              select word;
            int _wordCount = _matchQuery.Count();
            Console.WriteLine("单词‘data’出现的次数：{0};", _wordCount);
            #endregion
            #region 模块二 
            /*
             * KeyNote：
             *        1：String.Intersect(xxxx)=> 通过使用默认的相等比较器对值进行比较生成两个序列的交集
             */
            Console.WriteLine("\n\n-----------------------查询包含一组指定词语的句子----------------------------");
            string[] _sentences = _text.Split(new char[] { '.', '?', '!' });
            string[] _wordsToMatch = { "Historically", "data", "integrated" };
            var _sentenceQuery = from sentence in _sentences
                                 let w = sentence.Split(new char[] { '.', '?', '!', ' ', ';', ':', ',' }, StringSplitOptions.RemoveEmptyEntries)
                                 where w.Distinct().Intersect(_wordsToMatch).Count() == _wordsToMatch.Count()
                                 select sentence;
            Console.WriteLine("查询结果：");
            foreach(string str in _sentenceQuery)
            {
                Console.WriteLine(str);
            }
            #endregion
            #region 模块三 
            /*
             * KeyNote:
             *       1：Char.IsDigit（）=>判断该字符是不是数字
             *       2：String.TakeWhile(xxxx)=>TakeWhile()从集合的第一个元素开始，使用参数进行计算,如果返回true提取并继续判断下一个元素。如果返回false则停止判断，返回集合中被提取的元素。
             */
            Console.WriteLine("\n\n------------------------------查询字符串中的字符----------------------------n");
            string _str = "ABCDE99F-J74-12-89A";
            IEnumerable<char> _stringQuery =
                from ch in _str
                where Char.IsDigit(ch)
                select ch;
            Console.Write("字符串中包含的数字：");
            foreach (char c in _stringQuery)
                Console.Write(c+" ");
            int _count = _stringQuery.Count();
            Console.WriteLine("\n总共出现的次数：{0};",_count);
            Console.Write("满足条件的字符串序列为(截取到第一个‘—’位置的字符串)：");
            IEnumerable<char> _stringQuery2 = _str.TakeWhile(c => c != '-');
            foreach (char c in _stringQuery2)
                Console.Write(c+" ");
            #endregion
            #region 模块四 
            /*
             * KeyNote:
             *       1：Regex => 参考web：https://docs.microsoft.com/zh-cn/dotnet/api/system.text.regularexpressions.regex.-ctor?view=netframework-4.8
             *       2：Match => 参考web：https://docs.microsoft.com/zh-cn/dotnet/api/system.text.regularexpressions.match?view=netframework-4.8
             *       3：Directory.GetFiles => 参考web：https://docs.microsoft.com/zh-cn/dotnet/api/system.io.directory.getfiles?view=netframework-4.8
             *       4：正则表达式英语 => regular expression
             */
            Console.WriteLine("\n\n----------------------------将Linq查询与正则表达式合并-----------------------n");
            string _startFolder = @"C:\Program Files (x86)\Microsoft Visual Studio 14.0\";
            IEnumerable<FileInfo> fileList = GetFiles(_startFolder);
            Regex seaarchTern = new Regex(@"Visual (Basic|C#|C\+\+|Studio)");
            var queryMatchingFiles =
                from file in fileList
                where file.Extension == ".html"
                let fileText = File.ReadAllText(file.FullName)
                let matches = seaarchTern.Matches(fileText)
                select new
                {
                    name = file.FullName,
                    matchedValues = from Match match in matches
                                    select match.Value
                };
            Console.WriteLine("The term \"{0}\" was found in ;", seaarchTern.ToString());
            foreach(var v in queryMatchingFiles)
            {
                string s = v.name.Substring(_startFolder.Length - 1);
                Console.WriteLine(s);
                foreach(var v2 in v.matchedValues)
                {
                    Console.WriteLine("-||-" + v2);
                }
            }
            #endregion
            #region 模块五
            /*
             * KeyNote:
             *       1:
             *       2:
             */
            Console.WriteLine("\n\n----------------------------查找两个列表之间的差集-----------------------n");
            string[] names1 = File.ReadAllLines(@"../依赖文件/names1.txt");
            string[] names2 = File.ReadAllLines(@"../依赖文件/names2.txt");
            IEnumerable<string> differenceQuery = names1.Except(names2);
            Console.WriteLine("(取差集)The following lines are in names1.txt but not names2.txt");
            foreach(string s in differenceQuery)
            {
                Console.WriteLine(s);
            }
            #endregion
            #region 模块六
            /*
             * KeyNote:
             *       1:
             *       2:
             */
            Console.WriteLine("\n\n---------------------按任意词或字段对文本数据进行排序或筛选---------------n");
            #endregion
            #region 模块七
            /*
             * KeyNote:
             *       1:
             *       2:
             */
            Console.WriteLine("\n\n-------------------------重新排列带分隔符的文件的字段---------------------n");
            #endregion
            #region 模块八
            /*
             * KeyNote:
             *       1:IEnumerable.Concat/Union/Intersect => 见网址：https://docs.microsoft.com/zh-cn/dotnet/api/system.linq.enumerable.intersect?view=netframework-4.8 
             */
            Console.WriteLine("\n\n---------------------------组合和比较字符串集合---------------------------n");
            string[] fileA = File.ReadAllLines(@"../依赖文件/names1.txt");
            string[] fileB = File.ReadAllLines(@"../依赖文件/names2.txt");
            IEnumerable<string> concatQuery =
                fileA.Concat(fileB).OrderBy(s => s);
            OutputQueryResults(concatQuery, "(文本链接并排序)Simple concatenate and sort. Duplicates are preserved:");
            IEnumerable<string> uniqueNamesQuery =
                fileA.Union(fileB).OrderBy(s => s);
            OutputQueryResults(uniqueNamesQuery, "（取并集）Union removes duplicate names:");
            IEnumerable<string> commonNamesQuery =
                fileA.Intersect(fileB);
            OutputQueryResults(commonNamesQuery, "（取交集）Merge based on intersect:");
            string nameMatch = "Garcia";

            IEnumerable<String> tempQuery1 =
                from name in fileA
                let n = name.Split(',')
                where n[0] == nameMatch
                select name;

            IEnumerable<string> tempQuery2 =
                from name2 in fileB
                let n2 = name2.Split(',')
                where n2[0] == nameMatch
                select name2;

            IEnumerable<string> nameMatchQuery =
                tempQuery1.Concat(tempQuery2).OrderBy(s => s);
            OutputQueryResults(nameMatchQuery, $"（取名字中第一个单词相同为 Garcia）Concat based on partial name match \"{nameMatch}\":");

            #endregion
            #region 模块九
            /*
             * KeyNote:
             *       1:
             *       2:
             */
            Console.WriteLine("\n\n--------------------------从多个数据源填充对象集合-------------------------n");
            #endregion
            #region 模块十
            /*
             * KeyNote:
             *       1:
             *       2:
             */
            Console.WriteLine("\n\n-----------------------使用组将一个文件拆分成多个文件----------------------n");
            #endregion
            #region 模块十一
            /*
             * KeyNote:
             *       1:
             *       2:
             */
            Console.WriteLine("\n\n--------------------------联接不同文件中的内容-----------------------------n");
            #endregion
            #region 模块十二
            /*
             * KeyNote:
             *       1:
             *       2:
             */
            Console.WriteLine("\n\n--------------------------在CSV文本文件中计算列值--------------------------n");
            #endregion






            Console.WriteLine("\n\n*********************************gamecc666测试完毕！！！***************************************");
            Console.ReadKey();
        }






        //模块调用方法
        #region Linq To Object-->模块四       
        static IEnumerable<FileInfo> GetFiles(string path)
        {
            if(!Directory.Exists(path))
            {
                throw new DirectoryNotFoundException();
            }
            string[] fileNames = null;
            List<FileInfo> _files = new List<FileInfo>();
            fileNames = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
            foreach(string name in fileNames)
            {
                _files.Add(new FileInfo(name));
            }
            return _files;
        }
        #endregion
        #region Linq To Objects-->模块八
        /*
         * KeyNote：
         *       1：  Environment.NewLine => 是专门为当前平台和实现 .NET Framework 而自定义的常量;
         */
        static void OutputQueryResults(IEnumerable<string> query, string message)
        {
            Console.WriteLine(Environment.NewLine + message);
            foreach (string item in query)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("{0} total names in list", query.Count());
        }
        #endregion
    }
}
