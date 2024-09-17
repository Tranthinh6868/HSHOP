using System.Security.Cryptography;
using System.Text;

namespace WebApp;
public static class Helper{
    public static string RandomString(int len){
        string pattern = "4f6544564654546465sdgsagdfg5asfgafsdg6564";
        char[] arr = new char[len];
        Random random = new Random();
        for (int i=0; i< len ; i++){
            arr[i] = pattern[random.Next(pattern.Length)];
        }
        return string.Join(string.Empty, arr); // string.Empty =""
    }
    public static string HmacSha512(string plaintext, string key){
        using HMACSHA512 hMACSHA = new  HMACSHA512(Encoding.ASCII.GetBytes(key));
        return Convert.ToHexString(hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(plaintext)));
    }
} 