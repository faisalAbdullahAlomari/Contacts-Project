using System;

namespace Core
{
    public static class clsInput
    {

        public static string ReadString(string Message = "")
        {
            Console.Write(Message);
            return Console.ReadLine();
        }

        public static int ReadInteger(string Message)
        {
            string Input = ReadString(Message);            
            while (!IsInteger(Input))
            {
                Console.Write("Enter A Valid Number: ");
                Input = ReadString("");
            }
            return Convert.ToInt32(Input);
        }

        public static bool IsInteger(string Input)
        {
            try
            {
                Convert.ToInt32(Input);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
