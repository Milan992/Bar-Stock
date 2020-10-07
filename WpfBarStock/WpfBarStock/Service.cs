using System;
using WpfBarStock.Model;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace WpfBarStock
{
    class Service
    {
        /// <summary>
        /// Checks if employee with username and password exists in database BarStock.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
        public bool IsEmployee(string username, string password, out tblEmployee employee)
        {
            try
            {
                using (BarStockEntities context = new BarStockEntities())
                {
                    employee = (from e in context.tblEmployees where e.UserName == username && e.Pass == password select e).First();
                    return true; 
                }
            }
            catch
            {
                employee = null;
                return false;
            }
        }

        /// <summary>
        /// Checks if username and password match credentials from txt file AdminCredentials.
        /// username has to match first row from the file.
        /// password has to match second row from the file.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool IsAdmin(string username, string password)
        {
            try
            {
                using (StreamReader sr = new StreamReader(@"..\..\AdminCredentials.txt"))
                {
                    List<string> credentials = new List<string>();
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        credentials.Add(line);
                    }
                    if (credentials[0] == username && credentials[1] == username)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Retrives all articles from database and adds them to a list.
        /// </summary>
        /// <returns></returns>
        public List<vwArticle> GetAllArticles()
        {
            List<vwArticle> articles;
            try
            {
                using (BarStockEntities context = new BarStockEntities())
                {
                    articles = (from a in context.vwArticles select a).ToList();
                }
            }
            catch 
            {
                articles = null;
            }
            return articles;
        }
    }
}
