using System;
using WpfBarStock.Model;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Windows;
using System.Linq.Expressions;
using System.Reflection;
using System.Collections;

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
            catch (Exception ex)
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

        /// <summary>
        /// Calculates sold Amount for every article.
        /// </summary>
        /// <param name="articles"></param>
        /// <returns>List of articles with calculated sold amount</returns>
        public void CalculateSoldArticles(List<vwArticle> articles)
        {
            for (int i = 0; i < articles.Count; i++)
            {
                if (articles[i].ProcuredAmount == null)
                {
                    articles[i].ProcuredAmount = 0;
                }

                // calculation depends on calculation method
                if (articles[i].CalculationMethodID == 1)
                {
                    articles[i].AmountSold = articles[i].Amount - articles[i].NewAmount + articles[i].ProcuredAmount;
                }
                else if (articles[i].CalculationMethodID == 3)
                {
                    articles[i].AmountSold = articles[i].NewAmount - articles[i].Amount;
                }

                if (articles[i].AmountSold < 0)
                {
                    MessageBox.Show("Broj prodatih " + articles[i].ArticleName + " ne moze biti manja od 0.");
                    break;
                }
            }
        }

        public void CalculatePriceSold(List<vwArticle> articles)
        {
            for (int i = 0; i < articles.Count; i++)
            {
                articles[i].PriceSold = Convert.ToInt32(articles[i].AmountSold * articles[i].Price);
            }
        }

        /// <summary>
        /// Calculates summed PriceSold for all articles in the list.
        /// </summary>
        /// <param name="articles"></param>
        /// <returns></returns>
        public int CalculateBar(List<vwArticle> articles)
        {
            int sum = 0;
            for (int i = 0; i < articles.Count; i++)
            {
                sum += Convert.ToInt32(articles[i].PriceSold);
            }

            return sum;
        }

        public int CalculateCash(int cashbox, int kitchen, int card, int salary, int owner, int newspaper, int plus, int minus, int bar, List<Check> checks)
        {
            //sum all check's amounts
            int checksSum = 0;
            for (int i = 0; i < checks.Count; i++)
            {
                checksSum += checks[i].Amount;
            }

            int cash = cashbox + kitchen - card - salary - owner - newspaper + plus - minus + bar - checksSum;
            return cash;
        }

        public int CalculateChecks(List<int> checks)
        {
            int sum = 0;
            for (int i = 0; i < checks.Count; i++)
            {
                sum += checks[i];
            }
            return sum;
        }

        /// <summary>
        /// Updates articles amount in DB with data from the list.
        /// </summary>
        /// <param name="articles"></param>
        public void UpdateAmount(List<vwArticle> articles)
        {
            using (BarStockEntities context = new BarStockEntities())
            {
                for (int i = 0; i < articles.Count; i++)
                {
                    if (articles[i].NewAmount != null)
                    {
                        int id = articles[i].ArticleID;
                        tblArticle article = (from a in context.tblArticles where a.ArticleID == id select a).First();
                        article.Amount = (decimal)articles[i].NewAmount;
                    }
                }
                context.SaveChanges();
            }
        }

        public bool YesNoWarning(string message)
        {
            MessageBoxResult dialogResult = MessageBox.Show(message, "Upozorenje", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        ///  Writes a code to make a table from the in an html file with the fileName.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="fileName"></param>
        public void MakeHtmlTableFromList<T>(List<T> list, string fileName)
        {
            PropertyInfo[] myPropertyInfo;
            // Get the properties of list elements.
            myPropertyInfo = Type.GetType(list[0].GetType().ToString()).GetProperties();

            using (StreamWriter sw = new StreamWriter(@"..\..\" + fileName + ".html"))
            {
                sw.WriteLine("<table>");

                // Write properties names.
                sw.WriteLine("<tr>");
                for (int i = 0; i < myPropertyInfo.Length; i++)
                {
                    sw.WriteLine("<th>{0}</th>", myPropertyInfo[i].Name);
                }
                sw.WriteLine("</tr>");

                // Write values
                foreach (T item in list)
                {
                    sw.WriteLine("<tr>");
                    for (int i = 0; i < myPropertyInfo.Length; i++)
                    {
                        sw.WriteLine("<td>{0}</td>", myPropertyInfo[i].GetValue(item));
                    }
                    sw.WriteLine("</tr>");
                }

                sw.WriteLine("</table>");
            }
        }

        /// <summary>
        /// Writes a code to make a table from the list in file opened by the StreamWriter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="sw"></param>
        public void MakeHtmlTableFromList<T>(List<T> list, StreamWriter sw)
        {
            PropertyInfo[] myPropertyInfo;
            // Get the properties of list elements.
            myPropertyInfo = Type.GetType(list[0].GetType().ToString()).GetProperties();

            sw.WriteLine("<table>");

            // Write properties names.
            sw.WriteLine("<tr>");
            for (int i = 0; i < myPropertyInfo.Length; i++)
            {
                sw.WriteLine("<th>{0}</th>", myPropertyInfo[i].Name);
            }
            sw.WriteLine("</tr>");

            // Write values
            foreach (T item in list)
            {
                sw.WriteLine("<tr>");
                for (int i = 0; i < myPropertyInfo.Length; i++)
                {
                    sw.WriteLine("<td>{0}</td>", myPropertyInfo[i].GetValue(item));
                }
                sw.WriteLine("</tr>");
            }

            sw.WriteLine("</table>");
        }
    }
}
