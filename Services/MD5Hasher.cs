
namespace B2BWebService.Services;

using B2BWebService.ResponseRequestModels;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

public class MD5Hasher
{
    public static AppInfo GetAppInfo()
    {
        string token1 = "13e2db34034a3f91498a3ff5c3e12543";
        string token2 = "91fe007f7b6fc1f33cfaa0e7276c7e54";
        string id1 = "0003"; // ID для рабочего токена
        string id2 = "0004"; // ID для тестового токена
        string selectedToken = "", selectedID = "";

        var choice = 2;

        if (choice == 1)
        {
            selectedToken = token1;
            selectedID = id1;
        }
        else if (choice == 2)
        {
            selectedToken = token2;
            selectedID = id2;
        }

        var currentDate = DateTime.UtcNow;
        int year = currentDate.Year;
        int month = currentDate.Month;
        int day = currentDate.Day;
        var currentDayString = $"{year}{month}{day}";

        string tokenWithDate = selectedToken + currentDayString;

        string md5Hash = ComputeMD5Hash(tokenWithDate);

        var appInfo = new AppInfo
        {
            AppID = selectedID,
            TokenHashWS = md5Hash
        };
        return appInfo;
    }

    static string ComputeMD5Hash(string input)
    {
        using (MD5 md5 = MD5.Create())
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}


