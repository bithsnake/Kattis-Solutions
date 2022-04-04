using System;
using System.Linq;
using System.Collections.Generic;

/*
             At the start of class, I like to call roll. I like to go through my list of students in alphabetical order.
            Where possible, I like to call students by their first names. Of course, if two students have the same first name,
            I have to also give the last name so they know who I’m calling. Write a program to help me out. Given a class roll,
            it is going to tell how I should call the names.

            Input
            Input consists of up to 200 names, one per line, terminated by the end of file. Each line contains a first
            and a last name for a particular person. First and last names use 1 to 20 letters (a–z), always starting with
            an uppercase letters first followed by only lowercase letters. No two people will have exactly the same first
            and last names.

            Output
            Print the list of names, one per line, sorted by last name. If two or more people have the same last name,
            order these people by first name. Where the first name is unambiguous, just list the first name. If two people
            have the same first name, also list their last names to resolve the ambiguity.

            Sample Input 1
            ---------------
            Will Smith
            Agent Smith
            Peter Pan
            Micky Mouse
            Minnie Mouse
            Peter Gunn

            Sample Output 1
            ---------------
            Peter Gunn
            Micky
            Minnie
            Peter Pan
            Agent
            Will
 */
namespace KattisProblem
{
    internal class RollCall
    {
        public static void Main(string[] args)
        {
            List<Student> students = new List<Student>();
            List<Student> __students = null;
            int count = 0;
            string line;
            while ((line = Console.ReadLine()) != null)
            {
                if (line == "")
                {
                    break;
                }
                count++;
                line.Trim();
                string fName = line.Split(' ')[0].Trim();
                string lName = line.Split(' ')[1].Trim();
                var _student = new Student(fName, lName);
                students.Add(_student);
            }

            if(line == "" || line == null || line == " ")
            {
                if (students.Count > 0)
                {
                    __students = students.OrderBy(x => x.LastName).ToList();
                    //__students.Sort(1, students.Count -1, Comparer<Student>.Create( (x,y) => x.LastName.CompareTo(y.LastName)));

                    var lastNameDuplicates = __students.GroupBy(x => x.LastName).Where(y => y.Count() >= 2);
                    lastNameDuplicates.OrderBy(v => v.Key);
                    var existTwoOrMoreDuplicates = lastNameDuplicates.Any();
                    for (int x = 0; x < __students.Count; x++)
                    {
                        for (int y = 0; y < __students.Count; y++)
                        {
                            if (y != x)
                            {
                                var fNameA = __students[x].FirstName;
                                var lNameA = __students[x].LastName;
                                var lNameB = __students[y].LastName;
                                var fNameB = __students[y].FirstName;
                                if (lNameA != "" && lNameB != "" && lNameA == lNameB && existTwoOrMoreDuplicates)
                                {
                                    if(lastNameDuplicates.Count() <= 2)
                                    {
                                        var list = __students.FindAll(j => j.FirstName == __students[x].FirstName || j.FirstName == __students[y].FirstName);
                                        var sortedList = list.OrderBy(c => c.FirstName).ToList();
                                        var tempA = sortedList[0].FirstName;
                                        var tempB = sortedList[1].FirstName;
                                        __students[x].FirstName = tempA;
                                        __students[y].FirstName = tempB;
                                    }
                                    else
                                    {
                                        __students.Sort(1, students.Count - 1, Comparer<Student>.Create((q, w) => q.FirstName.CompareTo(w.FirstName)));
                                    }
                                    if (__students[x].FirstName != __students[y].FirstName)
                                    {
                                        __students[x].LastName = "";
                                        __students[y].LastName = "";
                                    }                                
                                }
                            }
                        }
                    }

                    foreach (var student in __students)
                    {
                        Console.WriteLine($"{student.FirstName} {student.LastName}");
                    }
                }

            }

        }

        /// <summary>
        /// Student class that has the properties FirstName, LastName
        /// </summary>
        public class Student
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public Student(string _fname, string _lname)
            {
                this.FirstName = _fname;
                this.LastName = _lname;
            }
        }
    }
}