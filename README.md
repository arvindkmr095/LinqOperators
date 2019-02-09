public class Program
    {
        #region Filtering
        /// <summary>
        /// Two types of Filter operators are available
        /// 1. Where operator
        /// 2. OfType operator
        /// </summary>
        public static void Filtering()
        {
            // OfType Operator
            object[] things = { "Arvind", 7, "Chapra" };
            var thingsQuery = things.OfType<string>();

            // Where Operator
            var thingsQuery1 = things.Where(t => t.ToString() != "something");

            thingsQuery.ToList().ForEach(o => { Console.WriteLine(o); });
            Console.WriteLine("");
            thingsQuery1.ToList().ForEach(o => { Console.WriteLine(o); });
        }

        #endregion

        #region Sorting
        /// <summary>
        /// There are three types of Sorting operators
        /// 1. OrderBy and OrderByDescending
        /// 2. ThenBy and ThenByDescending
        /// 3. Reverse
        /// </summary>
        public static void Sorting()
        {
            var query = Process.GetProcesses()
                      .OrderBy(p => p.ProcessName)
                      .ThenBy(p => p.PeakWorkingSet64)
                      .ToList();

            query.ForEach(p => { Console.WriteLine("{0} : {1}", p.ProcessName, p.PagedMemorySize64); });

        }
        #endregion

        #region Sets
        /// <summary>
        /// Four types of set operators are available
        /// 1. Distinct : Remove duplicates items from sequence
        /// 2. Except : Returns the difference of two sequences
        /// 3. Intersect: Returns the common items of two sequences
        /// 4. Union: Returns the unique elements from both sequences
        /// </summary>
        public static void Sets()
        {
            int[] twos = { 2, 4, 6, 8, 10, 12, 14, 16, 18 };
            int[] threes = { 3, 6, 9, 12, 15, 18 };

            // Intersection 
            var intersectionQuery = twos.Intersect(threes);
            Console.WriteLine("Intersected results:");
            intersectionQuery.ToList().ForEach(i =>
            {

                Console.Write(",{0}", i);
            });
            Console.WriteLine();
            // Union
            var unionQuery = twos.Union(threes);
            Console.WriteLine("union results:");
            unionQuery.ToList().ForEach(i =>
            {

                Console.Write(",{0}", i);
            });
            Console.WriteLine();

            // Except
            var exceptQuery = twos.Except(threes);
            Console.WriteLine("Except results:");
            exceptQuery.ToList().ForEach(i =>
            {

                Console.Write(",{0}", i);
            });
        }
        #endregion

        #region Quantifiers

        /// <summary>
        /// There are three types of quantifiers in Linq.
        /// 1. All  : tests all elements satisfy the conditions
        /// 2. Any : tests any elements satisfy the conditions
        /// 3. Contains: tests if the sequence contains the specific elements 
        /// </summary>
        public static void Quanitifiers()
        {
            int[] twos = { 2, 4, 6, 8, 10, 12 };

            // All Operator
            var areAllEvenNumbers = twos.All(n => n % 2 == 0);

            // Any Operator
            var anyMultipleOfThree = twos.Any(n => n % 3 == 0);

            // Contains Operator
            var hasFive = twos.Contains(7);

        }

        #endregion

        #region Projection

        /// <summary>
        /// There are two types of projection operators
        /// 1. Select
        /// 2. SelectMany
        /// </summary>
        public static void Projection()
        {
            string[] famouusQuotes =
                {
                 "Advertising is legalized lying",
                 "Advertising is greatest art form of the twentieth century"
                };

            var querySelect = famouusQuotes.Select(s => s.Split(" "));    // Returns two arrays, each array contains array of strings
            var querySelectWith = famouusQuotes.SelectMany(s => s.Split(" ")); // Returns one array of strings

        }
        #endregion

        #region Partitioning

        /// <summary>
        /// There are two types of partitioning operators
        /// 1. Skip, SkipLast and SkipWhile
        /// 2. Take and TakeWhile
        /// </summary>
        public static void Partitioning()
        {
            int[] numbers = { 1, 11, 22, 111, 44, 55, 66, 77, 12, 33, 5, 6, 7, 9, 0, 1, 2, 3, 4, 5, 6, 7, 87, 895, 45, 56, 7, 7, 77, 8, 8, 8 };
            var query1 = numbers.Skip(5).Take(10).ToList();
            var query2 = numbers.Skip(1).ToList();
            var query3 = query2.SkipWhile(n => n != 111).ToList();
            var query4 = query3.Take(10).ToList();
            var query5 = query4.TakeWhile(n => n != 5).ToList();
            var query6 = query5.SkipLast(1).TakeLast(1);

        }
        #endregion

        #region Joining

        /// <summary>
        /// Two types of joining available in Linq
        /// 1. Join: it is inner join
        /// 2. GroupJoin: it is outer join
        /// </summary>
        public static void Joining()
        {
            var employees = new List<Employee>()
            {
              new Employee(){ Id=1,EmployeeName="Ramesh Kumar",DepartmentId=1},
              new Employee(){ Id=2,EmployeeName="Amit Singh",DepartmentId=2},
              new Employee(){ Id=3,EmployeeName="Rajan Kumar",DepartmentId=2},
            };

            var departments = new List<Depatrtment>()
            {
              new Depatrtment(){ Id=1,DepartmentName="Account"},
              new Depatrtment(){Id=2,DepartmentName="HR"},
              new Depatrtment(){Id=3,DepartmentName="Operations & Admin"}
            };

            // Inner Join
            var query = departments.Join(employees,
                                            d => d.Id,
                                            e => e.DepartmentId,
                                            (d, e) => new
                                            {
                                                DeptName = d.DepartmentName,
                                                EmpName = e.EmployeeName
                                            })
                                            .ToList();

            // Group Join
            var query1 = departments.GroupJoin(employees,
                                            d => d.Id,
                                            e => e.DepartmentId,
                                            (d, e) => new
                                            {
                                                DeptName = d.DepartmentName,
                                                Employee = e
                                            })
                                            .ToList();


        }

        internal class Employee
        {
            public int Id { get; set; }
            public string EmployeeName { get; set; }
            public int DepartmentId { get; set; }
        }

        internal class Depatrtment
        {
            public int Id { get; set; }
            public string DepartmentName { get; set; }
        }
        #endregion


        #region Grouping
        /// <summary>
        /// Two type of grouping operators are available in Linq
        /// 1. GroupBy
        /// 2. ToLookup
        /// </summary>
        public static void Grouping()
        {
            var employees = new List<Employee>()
            {
              new Employee(){ Id=1,EmployeeName="Ramesh Kumar",DepartmentId=1},
              new Employee(){ Id=2,EmployeeName="Amit Singh",DepartmentId=2},
              new Employee(){ Id=3,EmployeeName="Rajan Kumar",DepartmentId=2},
            };

            // GroupBy
            var queryGroupBy = employees
                            .GroupBy(e => e.DepartmentId)
                            .Select(seq => new { DeptId = seq.Key, Employees = seq });

            // ToLookup operators
            var queryToLookup = employees
                            .ToLookup(e => e.DepartmentId)
                            .Select(seq => new { DeptId = seq.Key, Employees = seq });
        }
        #endregion



        static void Main(string[] args)
        {
            // Filtering();
            // Sorting();
            // Sets();
            // Quanitifiers();
            // Projection();
            // Partitioning();
            // Joining();
            // Grouping();
            Console.ReadKey();
        }
    }
