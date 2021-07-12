using System;

namespace CAXMLDocumentation
{
    class Program
    {
        static void Main(string[] args)
        { 
            do
            {
                Console.Write("First Name: ");
                var fname = Console.ReadLine();

                Console.Write(" Last Name: ");
                var lname = Console.ReadLine();

                Console.Write(" Hire Date: ");
                DateTime? hireDate = null;
                if(DateTime.TryParse(Console.ReadLine(), out DateTime hDate))
                {
                    hireDate = hDate;
                }
                var empId = Generator.GenerateId(fname, lname, hireDate);
                var randomPassword = Generator.GenerateRandomPassword(8);

                Console.WriteLine($"{{\n Id: {empId},\n FName: {fname},\n LName: {lname},\n hire date: {hireDate.Value.ToShortDateString()}, \n Password: {randomPassword}\n}}");

            } while (1 == 1);
             
        }
    }

    /// <include file="Generator.xml" path='docs/members[@name="generator"]/Generator/*'/>
    public class Generator
    {
      
        public static int LastIdSequence { get; private set; } = 1;



        /// <include file="Generator.xml" path='docs/members[@name="generator"]/GenerateId/*'/>
        public static string GenerateId(string fname, string lname, DateTime? hireDate)
        {
            // II YY MM DD 01

            if (fname is null)
                throw new InvalidOperationException($" {nameof(fname)} can not be null");

            if (lname is null)
                throw new InvalidOperationException($" {nameof(lname)} can not be null");

            if (hireDate is null)
            {
                hireDate = DateTime.Now;
            }
            else
            {
                if(hireDate.Value.Date < DateTime.Now.Date) // yyyy-MM-dd hh:mm:ss 
                    throw new InvalidOperationException($" {nameof(hireDate)} can not be in the past"); 
            }

            var yy = hireDate.Value.ToString("yy");
            var mm = hireDate.Value.ToString("MM");
            var dd = hireDate.Value.ToString("dd");

            var code = $"{lname.ToUpper()[0]}{fname.ToUpper()[0]} {yy} {mm} {dd} {(LastIdSequence++).ToString().PadLeft(2, '0')}";

            return code;
        }

        public static string GenerateRandomPassword(int length)
        {
            const string ValidScope = "abcdefghijklmnopqrstuvwxyzABCDEFGHIGKLMNOPQRSTUVWXYZ0123456789";
            var result = "";
            Random rnd = new Random();

            while(0 < length--)
            {
                result += (ValidScope[rnd.Next(ValidScope.Length)]);
            }
            return result;
        }
    }
}
