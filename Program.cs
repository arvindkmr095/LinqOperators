using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LinqOperators
{
    public class Program
    {
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

        #region Filtering Operators
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

        #region Sorting Operators
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

        #region Sets Operators
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

        #region Quantifiers Operators

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

        #region Projection Operators

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

        #region Partitioning Operators

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

        #region Joining Operators

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


        #endregion

        #region Grouping Operators
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

        #region Generaion Operators
        /// <summary>
        /// Four type of Generation operators are Linq
        /// 1. DefaultIfEmpty()
        /// 2. Empty()
        /// 3. Range()
        /// 4. Repeat()
        /// </summary>
        public static void Generation()
        {
            // DefaultIfEmpty(): it is extension method that is found in System.Linq.Enumerable  
            IList<string> strList = new List<string>();
            var default1 = strList.DefaultIfEmpty();  // Return single string defaul value: null
            var default2 = strList.DefaultIfEmpty("None"); // Return single string default value : None

            // Range(): It is not extension method. it is static method and found in System.Linq.Enumerable
            //  Generates a sequence of integral numbers within a specified range.       
            var range1 = Enumerable.Range(5, 5); // Returns IEnumerable<Int> : {5,6,7,8,9}

            // Empty(): It is generic static method and found in System.Linq.Enumerable.
            Enumerable.Empty<string>();    // Returns IEnumerable<string> empty object;

            // Repeat()   
            String[] arrString = new string[5] { "A", "B", "C", "D", "E" };
            var repeatObject = Enumerable.Repeat<string[]>(arrString, 5);
        }

        #endregion

        #region Equality Operator
        /// <summary>
        /// SequenceEqual operator is only one Equality operator in LINQ.
        /// That is used to compare elements order in two sequences.
        /// </summary>
        public static void Equality()
        {
            int[] arr1 = { 1, 2, 3 };
            int[] arr2 = { 2, 3, 1 };

            Console.WriteLine(arr1.SequenceEqual(arr2));  // Return False.
        }
        #endregion

        #region Elements Operators
        /// <summary>
        /// Four types of Elements operators in Linq
        /// 1. ElementAt and ElementAtOrDefault: Returns the element at specified index.
        /// 2. First and FirstOrDefault : Returns first element of collection.
        /// 3. Last and LastOrDefault : Resturns Last element of collection.
        /// 4. Single and SingleOrDefault : return single element of collection.
        /// </summary>
        public static void Elements()
        {
            string[] empty = { };
            string[] notEmpty = { "One", "Two", "Three" };

            empty.ElementAt(1);                 // Exception: IndexOutOfRangeException
            empty.ElementAtOrDefault(1);        // Return null value of default string 
            notEmpty.ElementAt(1);              // Return "One"
            notEmpty.ElementAtOrDefault(1);     // Return "One"

            empty.First();                      // Exception: InvalidOperationException (Sequence contain no elemenet)
            empty.FirstOrDefault();             // Return null
            notEmpty.First();                   // Return "One"
            notEmpty.FirstOrDefault();          // Return "One"

            empty.Single();                     // Exception: InvalidOperationException (Sequence contain no elemenet)
            empty.SingleOrDefault();            // Return :null
            notEmpty.Single();                  // Exception: InvalidOperationException (Sequence contain more than one elemenet)
            notEmpty.SingleOrDefault();         // Exception: InvalidOperationException (Sequence contain more than one elemenet)


        }
        #endregion

        #region Conversion Operators
        public static void Conversion()
        {
            var cityList = new string[] { "Delhi", "Mumbai", "Calcutta", "Chennai" };

            // AsEnumerable
            var objCityAsEnumerable = cityList.AsEnumerable<string>();

            // AsQueryable
            var objCityAsQueryable = cityList.AsQueryable<string>();

            // ToList
            var objCityList = cityList.ToList();

            // ToDictionary
            var objCityDictionary = cityList.ToDictionary(c => c);

            // ToLookup
            var objCityListLookup = cityList.ToLookup(c => c);

            // ToArray
            var objCityArray = cityList.ToArray();

            //OfType
            var objCitylistOfStringType = cityList.OfType<string>();

            // Cast
            var objCityListCast = cityList.Cast<string>();

        }
        #endregion

        #region Concatentation Operators 
        /// <summary>
        /// Two types of concatenation operators
        /// 1. Concat   : return all with duplicacy
        /// 2. Union   : return all without duplicacy
        /// </summary>
        public static void Concatenation()
        {
            var cityList1 = new string[] { "Delhi", "Mumbai", "Calcutta", "Chennai" };
            var cityList2 = new string[] { "Delhi", "Lucknow", "Patna", "Chennai" };
            var concatCity = cityList1.Concat(cityList2); // Return all city string array into single string array with duplicacy

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
            // Generation();
            // Equality();
            // Elements();
            // Conversion();
            Console.ReadKey();
        }
    }

}
