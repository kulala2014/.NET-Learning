using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Thread
{
   public static class Linq_Learning
    {
        static List<Student> students = new List<Student>
        {
            new Student {First="Svetlana", Last="Omelchenko", ID=111, Scores= new List<int> {97, 92, 81, 60}},
            new Student {First="Claire", Last="O'Donnell", ID=112, Scores= new List<int> {75, 84, 91, 39}},
            new Student {First="Sven", Last="Mortensen", ID=113, Scores= new List<int> {88, 94, 65, 91}},
            new Student {First="Cesar", Last="Garcia", ID=114, Scores= new List<int> {97, 89, 85, 82}},
            new Student {First="Debra", Last="Garcia", ID=115, Scores= new List<int> {35, 72, 91, 70}},
            new Student {First="Fadi", Last="Fakhouri", ID=116, Scores= new List<int> {99, 86, 90, 94}},
            new Student {First="Hanying", Last="Feng", ID=117, Scores= new List<int> {93, 92, 80, 87}},
            new Student {First="Hugo", Last="Garcia", ID=118, Scores= new List<int> {92, 90, 83, 78}},
            new Student {First="Lance", Last="Tucker", ID=119, Scores= new List<int> {68, 79, 88, 92}},
            new Student {First="Terry", Last="Adams", ID=120, Scores= new List<int> {99, 82, 81, 79}},
            new Student {First="Eugene", Last="Zabokritski", ID=121, Scores= new List<int> {96, 85, 91, 60}},
            new Student {First="Michael", Last="Tucker", ID=122, Scores= new List<int> {94, 92, 91, 91}},
            new Student {First="Clyde", Last="Gao", ID=123, Scores= new List<int> {100, 90, 99, 87}}
        };

        static List<Category> categories = new List<Category>()
        {
            new Category {Name="Beverages", ID=001},
            new Category {Name="Condiments", ID=002},
            new Category {Name="Vegetables", ID=003},
            new Category {Name="Grains", ID=004},
            new Category {Name="Fruit", ID=005}
        };

        // Specify the second data source.
        static List<Product> products = new List<Product>()
        {
            new Product {Name="Cola",  CategoryID=001},
            new Product {Name="Tea",  CategoryID=001},
            new Product {Name="Mustard", CategoryID=002},
            new Product {Name="Pickles", CategoryID=002},
            new Product {Name="Carrots", CategoryID=003},
            new Product {Name="Bok Choy", CategoryID=003},
            new Product {Name="Peaches", CategoryID=005},
            new Product {Name="Melons", CategoryID=005},
        };


        public static void Show()
        {
            //简单查询，查询第一个科目大于90分的学生
            var studentsQuery1 = 
                from student in students
                where student.Scores[0] > 0 && student.Scores[3] < 80
                orderby student.Scores[0] descending
                select student;
            foreach (var student in studentsQuery1)
            {
                Console.WriteLine($"Student:{student.First}-{student.Last}, First class score:{student.Scores[0]}, Fouth class score: {student.Scores[3]}");
            }

            //分组+排序+过滤
            var studentsQuery2 =
            from student in students
            group student by student.Last into studentGroup
            where studentGroup.Count() > 2
            orderby studentGroup.Key
            select studentGroup;

            foreach (var studentGroup in studentsQuery2)
            {
                Console.WriteLine(studentGroup.Key);
                foreach (var student in studentGroup)
                {
                    Console.WriteLine($"{student.First}-{student.Last}");
                }
            }

            //let变量和匿名类
            var STQuery =
                from student in students
                let score = string.Join(",", student.Scores)
                let totalScore = student.Scores[0] + student.Scores[1] + student.Scores[0] + student.Scores[1]
                where student.Scores[0] > 80
                orderby totalScore descending
                select new { Name = $"{student.First}-{student.Last}", Score = score, TotalScore = totalScore };
            foreach (var student in STQuery)
            {
                Console.WriteLine($"Student:{student.Name}'s score info: {student.Score}, total score is {student.TotalScore}");
            }

            //average
            var STQuery1 =
            from student in students
            let score = string.Join(",", student.Scores)
            let totalScore = student.Scores[0] + student.Scores[1] + student.Scores[0] + student.Scores[1]
            where student.Scores[0] > 80
            orderby totalScore descending
            select totalScore;

            Console.WriteLine($"Average{STQuery1.Average()}");

            //Join

            InnerJoin();
            GroupJoin();
            GroupInnerJoin();
            GroupJoin3();
            LeftOuterJoin();
            LeftOuterJoin2();
        }

        static void InnerJoin()
        {
            var query =
                from product in products
                join categorie in categories on product.CategoryID equals categorie.ID
                select new { Categorie = product.CategoryID, Product = product.Name};

            foreach (var item in query)
            {
                Console.WriteLine($"{item.Categorie,-10}-{item.Product}");
            }
        }

        static void GroupJoin()
        {
            var query =
                from category in categories
                join product in products on category.ID equals product.CategoryID into prodGroup
                select prodGroup;

            int totalItems = 0;

            foreach (var prodGrouping in query)
            {
                Console.WriteLine("Group: ");
                foreach (var item in prodGrouping)
                {
                    totalItems++;
                    Console.WriteLine($"   {item.Name,-10}{item.CategoryID}");
                }
            }
            Console.WriteLine("Unshaped GroupJoin: {0} items in {1} unnamed groups", totalItems, query.Count());
            Console.WriteLine(System.Environment.NewLine);
        }

        static void GroupInnerJoin()
        {
            var query =
                from category in categories
                orderby category.ID
                join product in products on category.ID equals product.CategoryID into prodGroup
                select new
                {
                    Category = category.Name,
                    Products = from prod in prodGroup
                               orderby prod.Name
                               select prod
                };

            int totalItems = 0;

            foreach (var prodGroup in query)
            {
                Console.WriteLine("GroupInnerJoin: ");
                foreach (var pruductItem in prodGroup.Products)
                {
                    totalItems++;
                    Console.WriteLine($"   {pruductItem.Name,-10}{pruductItem.CategoryID}");
                }
            }
            Console.WriteLine("Unshaped GroupJoin: {0} items in {1} unnamed groups", totalItems, query.Count());
            Console.WriteLine(System.Environment.NewLine);
        }

        static void GroupJoin3()
        {
            var query =
                from category in categories
                join product in products on category.ID equals product.CategoryID into prodGroup
                from prod in prodGroup
                orderby prod.CategoryID
                select new { Category = prod.CategoryID, ProductName = prod.Name};

            int totalItems = 0;

            Console.WriteLine("GroupJoin3:");
            foreach (var item in query)
            {
                totalItems++;
                Console.WriteLine("   {0}:{1}", item.ProductName, item.Category);
            }

            Console.WriteLine("GroupJoin3: {0} items in 1 group", totalItems);
            Console.WriteLine(System.Environment.NewLine);
        }

        static void LeftOuterJoin()
        {
            var query =
                from category in categories
                join prod in products on category.ID equals prod.CategoryID into prodGroup
                select prodGroup.DefaultIfEmpty(new Product() { Name = "Nothing!", CategoryID = category.ID});
            // Store the count of total items (for demonstration only).
            int totalItems = 0;

            Console.WriteLine("Left Outer Join:");

            // A nested foreach statement  is required to access group items
            foreach (var prodGrouping in query)
            {
                Console.WriteLine("Group:");
                foreach (var item in prodGrouping)
                {
                    totalItems++;
                    Console.WriteLine("  {0,-10}{1}", item.Name, item.CategoryID);
                }
            }
            Console.WriteLine("LeftOuterJoin: {0} items in {1} groups", totalItems, query.Count());
            Console.WriteLine(System.Environment.NewLine);
        }

        static void LeftOuterJoin2()
        {
            var query =
                from category in categories
                join prod in products on category.ID equals prod.CategoryID into prodGroup
                from item in prodGroup.DefaultIfEmpty()
                select new { Name = "Nothing!", CategoryID = category.ID };

            Console.WriteLine("LeftOuterJoin2: {0} items in 1 group", query.Count());
            // Store the count of total items
            int totalItems = 0;

            Console.WriteLine("Left Outer Join 2:");

            // Groups have been flattened.
            foreach (var item in query)
            {
                totalItems++;
                Console.WriteLine("{0,-10}{1}", item.Name, item.CategoryID);
            }
            Console.WriteLine("LeftOuterJoin2: {0} items in 1 group", totalItems);
        }
    }

    public class Student
    {
        public string First { get; set; }
        public string Last { get; set; }
        public int ID { get; set; }
        public List<int> Scores;
    }

    class Product
    {
        public string Name { get; set; }
        public int CategoryID { get; set; }
    }

    class Category
    {
        public string Name { get; set; }
        public int ID { get; set; }
    }
}
