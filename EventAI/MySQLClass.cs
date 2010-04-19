﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using EventAI.Properties;

namespace EventAI
{
    class MySQLClass
    {
        public DataTable AI { get { return TextTable; } } 
        private DataTable TextTable;
        private MySqlDataAdapter da;

        public MySQLClass(String param)
        {
            TextTable = new DataTable();

            try
            {
                var connstr = String.Format("Server=localhost;Port=3306;Uid={0};Pwd={1};Connection Timeout=10",
                    Settings.Default.user,
                    Settings.Default.pass);

                MySqlConnection conn = new MySqlConnection(connstr);
                conn.Open();

                da = new MySqlDataAdapter("SELECT * FROM mangos.creature_ai_scripts WHERE creature_id = '"+param+"'", conn);
                da.Fill(TextTable);

                conn.Close();
            }
            catch
            {
                MessageBox.Show("Can't connect to database!");
            }
        }
        public CreatureInfo Creature(string entry)
        {
            CreatureInfo cr;
            try
            {
                var connstr = String.Format("Server=localhost;Port=3306;Uid={0};Pwd={1};Connection Timeout=10",
                    Settings.Default.user,
                    Settings.Default.pass);

                MySqlConnection conn = new MySqlConnection(connstr);
                conn.Open();
                var query = String.Format("SELECT t.entry, t.name, t.description, l.name_loc8, l.description_loc8"+ 
                " FROM mangos.creatute_template t LEFT JOIN locales_creature l ON l.entry=t.entry WHERE t.entry = '{0}'", entry);
                da = new MySqlDataAdapter(query, conn);

                conn.Close();
                return cr;
            }
            catch
            {
                MessageBox.Show("Can't connect to database!");
                return cr;
            }
        }
    }
}