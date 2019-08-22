using System;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Common;

namespace PostgresCom
{
    public partial class first : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ConnectionBtn_Click(object sender, EventArgs e)
        {
            // Реализовано не по принципам SOLID для простоты в сжатые сроки - что бы дать пробную версию. Такое было задание
            string connString = string.Format("Server={0};Username={1};Database={2};Port={3};Password={4};SSLMode=Prefer",
                   "localhost",
                   LoginTxt.Text,
                   LoginTxt.Text,
                   "5432",
                   PassTxt.Text
                );
            using (var conn = new NpgsqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    ConnectionStatus.Text = "Подключение успешно";
                    conn.Close();
                }
                catch (NpgsqlException ex)
                {
                    if (ex.Message.IndexOf("28P01") > -1)
                    {
                        ConnectionStatus.Text = "Неверное имя пользователя или пароль.";
                    }
                    else
                    {
                        ConnectionStatus.Text = "Ошибка подключения: "+ex.ErrorCode.ToString() + " сообщение: " + ex.Message;
                    }
                }
            }
        }
    }
}