using System;
using System.IO;

namespace SleepData
{
    class Program
    {
        static void Main(string[] args)
        {
            // ask for input
            Console.WriteLine("Enter 1 to create data file.");
            Console.WriteLine("Enter 2 to parse data.");
            Console.WriteLine("Enter anything else to quit.");
            // input response
            string resp = Console.ReadLine();

            if (resp == "1")
            {
                // create data file

                 // ask a question
                Console.WriteLine("How many weeks of data is needed?");
                // input the response (convert to int)
    
                 // determine start and end date
                DateTime today = DateTime.Now;
                // we want full weeks sunday - saturday
                DateTime dataEndDate = today.AddDays(-(int)today.DayOfWeek);
                // subtract # of weeks from endDate to get startDate
                DateTime dataDate = dataEndDate.AddDays(-(weeks * 7));
                
                // random number generator
                Random rnd = new Random();

                // create file
                StreamWriter sw = new StreamWriter("data.txt");
                // loop for the desired # of weeks
                while (dataDate < dataEndDate)
                {
                    // 7 days in a week
                    int[] hours = new int[7];
                    for (int i = 0; i < hours.Length; i++)
                    {
                        // generate random number of hours slept between 4-12 (inclusive)
                        hours[i] = rnd.Next(4, 13);
                    }
                    // M/d/yyyy,#|#|#|#|#|#|#
                    //Console.WriteLine($"{dataDate:M/d/yy},{string.Join("|", hours)}");
                    sw.WriteLine($"{dataDate:M/d/yyyy},{string.Join("|", hours)}");
                    // add 1 week to date
                    dataDate = dataDate.AddDays(7);
                }
                sw.Close();
            }
            else if (resp == "2")
            {
                StreamReader sr = new StreamReader("data.txt");
                string line;
                do{
                line = sr.ReadLine();
                string[] weekInfo = line.Split(',');
                string dateInfo = weekInfo[0];
                string sleepInfo = weekInfo[1];
                string[] sleepSplit = sleepInfo.Split('|');
                string mondayTime = sleepSplit[0];
                string[] dateSplit = dateInfo.Split('/');
                DateTime dateStuff = new DateTime(Int16.Parse(dateSplit[2]), Int16.Parse(dateSplit[0]), Int16.Parse(dateSplit[1]));
                int total = Int16.Parse(sleepSplit[0]) + Int16.Parse(sleepSplit[1]) + Int16.Parse(sleepSplit[2]) + Int16.Parse(sleepSplit[3]) + Int16.Parse(sleepSplit[4])
                + Int16.Parse(sleepSplit[5]) + Int16.Parse(sleepSplit[6]);
                Console.WriteLine("Week of {0:MMM}, {0:dd}, {0:yyyy}", dateStuff);
                Console.WriteLine(" Su Mo Tu We Th Fr Sa Tot Avg");
                Console.WriteLine(" -- -- -- -- -- -- -- --- ---");
                Console.WriteLine(" {0,2} {1,2} {2,2} {3,2} {4,2} {5,2} {6,2} {7,3} {8,3:F1}", sleepSplit[0], sleepSplit[1], sleepSplit[2], sleepSplit[3], sleepSplit[4], sleepSplit[5],
                sleepSplit[6], total, (double)total/7);
                Console.WriteLine("");

                }while(sr.EndOfStream == false);
                
            }
        }
    }
}