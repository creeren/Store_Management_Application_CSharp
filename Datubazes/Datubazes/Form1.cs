using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Datubazes
{
    
    public partial class Form1 : Form
    {
        bool stores_grid = false;
        bool chocolate_grid = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SelectStores();
            SelectChocolate();
        }

        /// <summary>
        /// Select from Stores Table.
        /// </summary>
        private void SelectStores()
        {
            try
            {
                SqlConnection SqlCon = new SqlConnection(
                @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\krist\source\repos\Datubazes\Datubazes\ChStores.mdf;Integrated Security=True");
                string sql = "SELECT * FROM Stores";
                SqlDataAdapter dataadapter = new SqlDataAdapter(sql, SqlCon);
                DataSet ds = new DataSet();
                SqlCon.Open();
                dataadapter.Fill(ds);
                SqlCon.Close();
                BindingSource bs;
                bs = new BindingSource();
                bs.DataSource = ds.Tables[0].DefaultView;
                storesDataGridView.DataSource = bs;
                label8.Text = "Count of the records in the Store: " + bs.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Select from Chocolate table.
        /// </summary>
        private void SelectChocolate()
        {
            try { 
                SqlConnection SqlCon = new SqlConnection(
                @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\krist\source\repos\Datubazes\Datubazes\ChStores.mdf;Integrated Security=True");
                
                string sql = "SELECT * FROM Chocolate";                         
                SqlDataAdapter dataadapter = new SqlDataAdapter(sql, SqlCon);   
                DataSet ds = new DataSet();                                              
                SqlCon.Open();                                                           
                dataadapter.Fill(ds);                                                    
                SqlCon.Close();                                                          
                BindingSource bs;                                                        
                bs = new BindingSource();                                               
                bs.DataSource = ds.Tables[0].DefaultView;                                                                  
                chocolateDataGridView.DataSource = bs;
                label9.Text = "Count of the records in the Chocolate: " + bs.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        
        /// <summary>
        /// Combobox refresh.
        /// </summary>
        private void ComboboxRefresh()
        {

            SqlConnection SqlCon1 = new SqlConnection(
            @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\krist\source\repos\Datubazes\Datubazes\ChStores.mdf;Integrated Security=True");

            string sql = "Select ID_Store from Stores";
            SqlDataAdapter dataadapter = new SqlDataAdapter(sql, SqlCon1);
            DataSet ds = new DataSet();

            SqlCon1.Open();
            dataadapter.Fill(ds);
            SqlCon1.Close();

            BindingSource bs;
            bs = new BindingSource();
            bs.DataSource = ds.Tables[0].DefaultView;

            comboBox1.DataSource = bs;
        }

        /// <summary>
        /// Insert into Table new record.
        /// </summary>
        private void Insert_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked) {


                string name = textBox2.Text;
                string addr = textBox5.Text;
                string tel = textBox6.Text;

                // Checks if fields is filled
                if (name == "" || addr == "" || tel == "") 
                {
                    MessageBox.Show("Nav aizpilditi lauki");
                }
                else {    
                    SqlConnection SqlCon = new SqlConnection(
                    @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\krist\source\repos\Datubazes\Datubazes\ChStores.mdf;Integrated Security=True");
                    string SqlStrFillStores = "INSERT INTO Stores" +
                   "(Name, Address, Telefon)" +
                   "values('" + name + "','" + addr + "','" + tel + "'); ";

                    SqlCommand SqlCom = new SqlCommand();

                    try
                    {
                        SqlCon.Open();
                        SqlCom.Connection = SqlCon;
                        SqlCom.CommandText = SqlStrFillStores;
                        SqlCom.ExecuteNonQuery();

                        MessageBox.Show("Inserted");
                        SqlCon.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                    SelectStores();
                    textBox2.Clear();
                    textBox5.Clear();
                    textBox6.Clear();
                    ComboboxRefresh();
                }
            }
            if (checkBox2.Checked)
            {
                try
                {
                    string brand = textBox1.Text;
                    int idStore = int.Parse(comboBox1.Text);
                    int qty = int.Parse(textBox3.Text);
                    float price = float.Parse(textBox4.Text);

                    SqlConnection SqlCon = new SqlConnection(
                    @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\krist\source\repos\Datubazes\Datubazes\ChStores.mdf;Integrated Security=True");

                    string SqlStrFillChocolate = "insert into Chocolate" +
                    "(Brand, ID_Store, Quantity, Price)" +
                    "values('" + brand + "','" + idStore + "','" + qty + "','" + price +"'); ";
              
                    SqlCommand SqlCom = new SqlCommand(); 
                    try
                    {
                        SqlCon.Open();
                        SqlCom.Connection = SqlCon;
                        SqlCom.CommandText = SqlStrFillChocolate;
                        SqlCom.ExecuteNonQuery();

                        MessageBox.Show("Inserted");
                        SqlCon.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    SelectChocolate();
                    textBox1.Clear();
                    comboBox1.Refresh();
                    textBox3.Clear();
                    textBox4.Clear();
                }
                catch (Exception)
                { MessageBox.Show("Nav aizpilditi lauki"); }
            }       
            if (checkBox1.Checked==false && checkBox2.Checked==false) { MessageBox.Show("Please check the box before insert!"); }
        }
       
        /// <summary>
        /// Create tables.
        /// </summary>
        private void CreateTables()
        {

            string connectionString = @"Data source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\krist\source\repos\Datubazes\Datubazes\ChStores.mdf;Integrated Security=True";
            SqlConnection SqlCon = new SqlConnection(connectionString);

            // Text string - SQL command
            const string SqlStrCreateStores = "create table Stores(" +
            "ID_Store int identity primary key, " + 
            "Name nvarchar(20), " + 
            "Address nvarchar(30), " +
            "Telefon nvarchar(20)" +
            ");";

            const string SqlStrCreateChocolate = "create table Chocolate(" +
            "ID_Chocolate int identity primary key, " +  
            "Brand nvarchar(20), " + 
            "ID_Store int, " +                  
            "Quantity int, " +
            "Price float, " +
            
            "CONSTRAINT FK_Stores_Chocolate FOREIGN KEY (ID_Store) " +
            "REFERENCES Stores (ID_Store)" +
            ");";

            SqlCommand SqlCom = new SqlCommand(); 
            try
            {
                SqlCon.Open();
                SqlCom.Connection = SqlCon;
                SqlCom.CommandText = SqlStrCreateStores;
                SqlCom.ExecuteNonQuery();
            
                SqlCom.CommandText = SqlStrCreateChocolate;
                SqlCom.ExecuteNonQuery();

                MessageBox.Show("Stores & Chocolate Created");
                SqlCon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }         
        }

        /// <summary>
        /// Fill tables.
        /// </summary>
        private void FillTables()
        {

            SqlConnection SqlCon = new SqlConnection(
            @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\krist\source\repos\Datubazes\Datubazes\ChStores.mdf;Integrated Security=True");

            const string SqlStrFillStores = "insert into Stores" +
            "(Name, Address, Telefon)" +
            "values('Chocolate world', 'Middlestone street 35', '33985885263'); " +
            "insert into Stores(Name, Address, Telefon)" +
            "values('LAC candy world', 'Kingscross 24', '33552668211'); " +
            "insert into Stores(Name, Address, Telefon)" +
            "values('Lindstore', 'Landyhill street 3', '33785526422'); ";

            const string SqlStrFillChocolate = "insert into Chocolate" +
            "(Brand, ID_Store, Quantity, Price)" +
            "values('Lindt', 3, 60, 2.70); " +   
            "insert into Chocolate(Brand, ID_Store, Quantity, Price)" +
            "values('Toblerone', 1, 30, 5.70); " +
            "insert into Chocolate(Brand, ID_Store, Quantity, Price)" +
            "values('Cadbury', 2, 35, 1.53); " +
            "insert into Chocolate(Brand, ID_Store, Quantity, Price)" +
            "values('Ferrero Rosher', 1, 50, 5.20); ";


            SqlCommand SqlCom = new SqlCommand();
            try
            {
                SqlCon.Open();
                SqlCom.Connection = SqlCon;
               
                SqlCom.CommandText = SqlStrFillStores;
                SqlCom.ExecuteNonQuery();
              
                SqlCom.CommandText = SqlStrFillChocolate;
                SqlCom.ExecuteNonQuery();
                MessageBox.Show("Stores & Chocolate Filled");
                SqlCon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            SelectStores();
            SelectChocolate();
            ComboboxRefresh();
        }

        /// <summary>
        /// Create tables click.
        /// </summary>
        private void Create_Click(object sender, EventArgs e)
        {
            CreateTables();
        }

        /// <summary>
        /// Fill tables click.
        /// </summary>
        private void Fill_Click(object sender, EventArgs e)
        {
            FillTables();
        }

        /// <summary>
        /// Delete tables click.
        /// </summary>
        private void Delete_table_Click(object sender, EventArgs e)
        {
            SqlConnection SqlCon = new SqlConnection(
           @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\krist\source\repos\Datubazes\Datubazes\ChStores.mdf;Integrated Security=True");

            const string SqlStrDropConstraint = "ALTER TABLE Chocolate DROP CONSTRAINT  FK_Stores_Chocolate;"; //delete link between tables
            const string SqlStrDropChocolate = "DROP TABLE Chocolate;"; // delete Chocolate table first
            const string SqlStrDropStores = "DROP TABLE Stores;"; // delete Stores table 

            SqlCommand SqlCom = new SqlCommand();
            try
            {
                SqlCon.Open();
                SqlCom.Connection = SqlCon;
                SqlCom.CommandText = SqlStrDropConstraint;
                SqlCom.ExecuteNonQuery();
                SqlCom.CommandText = SqlStrDropChocolate;
                SqlCom.ExecuteNonQuery();
                SqlCom.CommandText = SqlStrDropStores;
                SqlCom.ExecuteNonQuery();
                MessageBox.Show("Stores & Chocolate Deleted");
                          
                SqlCon.Close();
                this.storesDataGridView.DataSource = null;
                this.storesDataGridView.Rows.Clear();
                this.chocolateDataGridView.DataSource = null;
                this.chocolateDataGridView.Rows.Clear();
                if (storesDataGridView.DataSource == null && chocolateDataGridView.DataSource == null) 
                {
                    label8.Text = "Count of the records in the Store: " + "0";
                    label9.Text = "Count of the records in the Chocolate: " + "0";
                }          
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }      
        }

        /// <summary>
        /// Checks if Store data grid view was clicked.
        /// </summary>
        private void StoresDataGridView_MouseClick(object sender, MouseEventArgs e)
        {
            stores_grid = true;
        }

        /// <summary>
        /// Checks if Chocolate data grid view was clicked.
        /// </summary>
        private void ChocolateDataGridView_MouseClick(object sender, MouseEventArgs e)
        {
            chocolate_grid = true;
        }

        /// <summary>
        /// Edit click.
        /// </summary>
        private void Edit_Click(object sender, EventArgs e)
        {
            //If stores grid selected
            if (stores_grid == true)
            {
                try 
                {  // To mark the entire record and not just one cell
                    textBox2.Text = storesDataGridView.SelectedRows[0].Cells[1].Value.ToString();
                    textBox5.Text = storesDataGridView.SelectedRows[0].Cells[2].Value.ToString();
                    textBox6.Text = storesDataGridView.SelectedRows[0].Cells[3].Value.ToString();
                }
                catch (Exception) 
                {
                    MessageBox.Show("Please select the row from table!");
                }
            }
            //If chocolate grid selected
            if (chocolate_grid == true) 
            {
                try { 
                textBox1.Text = chocolateDataGridView.SelectedRows[0].Cells[1].Value.ToString();
                comboBox1.Text = chocolateDataGridView.SelectedRows[0].Cells[2].Value.ToString();
                textBox3.Text = chocolateDataGridView.SelectedRows[0].Cells[3].Value.ToString();
                textBox4.Text = chocolateDataGridView.SelectedRows[0].Cells[4].Value.ToString();
                }
                catch (Exception)
                {
                    MessageBox.Show("Please select the row from table!");
                }
            }
        }

        /// <summary>
        /// Save record into table after editing.
        /// </summary>
        private void Save_Click(object sender, EventArgs e)
        {
            if (stores_grid == true) 
            {
                string name = textBox2.Text;
                string addr = textBox5.Text;
                string tel = textBox6.Text;
                SqlConnection SqlCon = new SqlConnection(
                @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\krist\source\repos\Datubazes\Datubazes\ChStores.mdf;Integrated Security=True");
                SqlCommand SqlCom = new SqlCommand("UPDATE Stores SET  Name = '" + name + "', Address = '" + addr + "', Telefon = '" + tel + "'WHERE ID_Store= " + storesDataGridView.SelectedRows[0].Cells[0].Value.ToString()); //  SQL komanda

                try
                {
                    SqlCon.Open();
                    SqlCom.Connection = SqlCon;
                    SqlCom.Parameters.AddWithValue("Name", name);
                    SqlCom.Parameters.AddWithValue("Address", addr);
                    SqlCom.Parameters.AddWithValue("Telefon", tel);
                    SqlCom.ExecuteNonQuery();
                    MessageBox.Show("Store table Updated");
                    SqlCon.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                SelectStores();
                stores_grid = false;
                textBox2.Clear();
                textBox5.Clear();
                textBox6.Clear();

            }
            if (chocolate_grid == true) 
            {
                string brand = textBox1.Text;
                int idStore = int.Parse(comboBox1.Text);
                int qty = int.Parse(textBox3.Text);
                float price = float.Parse(textBox4.Text);

                SqlConnection SqlCon = new SqlConnection(
                @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\krist\source\repos\Datubazes\Datubazes\ChStores.mdf;Integrated Security=True");

                SqlCommand SqlCom = new SqlCommand("UPDATE Chocolate SET  Brand = '" + brand + "', ID_Store = '" + idStore + "', Quantity = '" + qty + "', Price = '" + price + "'WHERE ID_Chocolate= " + chocolateDataGridView.SelectedRows[0].Cells[0].Value.ToString()); //  SQL komanda

                try
                {
                    SqlCon.Open();
                    SqlCom.Connection = SqlCon;
                    SqlCom.Parameters.AddWithValue("Brand", brand);
                    SqlCom.Parameters.AddWithValue("ID_Store", idStore);
                    SqlCom.Parameters.AddWithValue("Quantity", qty);
                    SqlCom.Parameters.AddWithValue("Price", price);
                    SqlCom.ExecuteNonQuery();
                    MessageBox.Show("Chocolate table Updated");
                    SqlCon.Close();
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                SelectChocolate();
                chocolate_grid = false;
                textBox1.Clear();
                comboBox1.ResetText();
                textBox3.Clear();
                textBox4.Clear();
            }
            
         }

         /// <summary>
         /// Delete record from table.
         /// </summary>
         private void Delete_Click(object sender, EventArgs e)
         {

            SqlConnection SqlCon = new SqlConnection(
            @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\krist\source\repos\Datubazes\Datubazes\ChStores.mdf;Integrated Security=True");

            if (MessageBox.Show("Are you sure you wanto delete this record?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                
                if (stores_grid == true) { 
                    try
                    {
                        const string SqlStrDropConstraint = "ALTER TABLE Chocolate DROP CONSTRAINT FK_Stores_Chocolate;"; 
                        string SqlStrDeleteChocolate = "DELETE FROM Chocolate WHERE ID_Store= " + storesDataGridView.SelectedRows[0].Cells[0].Value.ToString() + ""; // sākumā dzēš no Chocolate tabulas
                        string SqlStrDeleteStores = "DELETE FROM Stores WHERE ID_Store= " + storesDataGridView.SelectedRows[0].Cells[0].Value.ToString() + ""; // tad dzēš no Stores tabulas

                        SqlCommand SqlCom = new SqlCommand();
                        try
                        {
                            SqlCon.Open();
                            SqlCom.Connection = SqlCon;
                            SqlCom.CommandText = SqlStrDropConstraint;
                            SqlCom.ExecuteNonQuery();
                            SqlCom.CommandText = SqlStrDeleteChocolate;
                            SqlCom.ExecuteNonQuery();
                            SqlCom.CommandText = SqlStrDeleteStores;
                            SqlCom.ExecuteNonQuery();
                            MessageBox.Show("Removed");
                            SqlCon.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    } 
                    catch (Exception)
                    {
                        MessageBox.Show("Please select the row from table!");
                    }

                    SelectStores();
                    SelectChocolate();
                    stores_grid = false;

                    const string SqlStrChangeChocolate = "ALTER TABLE Chocolate ADD CONSTRAINT FK_Stores_Chocolate FOREIGN KEY (ID_Store) REFERENCES Stores (ID_Store) ON DELETE CASCADE;";
                    ComboboxRefresh();
 
                    SqlCommand SqlCom1 = new SqlCommand();
                    try
                    {
                        SqlCon.Open();
                        SqlCom1.Connection = SqlCon;
                        SqlCom1.CommandText = SqlStrChangeChocolate;
                        SqlCom1.ExecuteNonQuery();
                        SqlCon.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                if (chocolate_grid == true) 
                {                  
                    string SqlStrDeleteChocolate = "DELETE FROM Chocolate WHERE ID_Store= " + chocolateDataGridView.SelectedRows[0].Cells[0].Value.ToString() + "";
                    
                    SqlCommand SqlCom = new SqlCommand();
                    try
                    {
                        SqlCon.Open();
                        SqlCom.Connection = SqlCon;        
                        SqlCom.CommandText = SqlStrDeleteChocolate;
                        SqlCom.ExecuteNonQuery();
                      
                        MessageBox.Show("Removed from Chocolate");
                        SqlCon.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    
                    SelectChocolate();
                    chocolate_grid = false;
                    ComboboxRefresh();
                }           
            }
         }

        /// <summary>
        /// Display table.
        /// </summary>
        private void Display_table_Click(object sender, EventArgs e)
        {
            SelectStores();
            SelectChocolate();
            ComboboxRefresh();
        }

        /// <summary>
        /// Search records into Stores table.
        /// </summary>
        private void SearchStores_TextChanged(object sender, EventArgs e)
        {
            SqlConnection SqlCon = new SqlConnection(
            @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\krist\source\repos\Datubazes\Datubazes\ChStores.mdf;Integrated Security=True");
            string SqlStrSearchStore = "SELECT * FROM Stores WHERE Name LIKE '%" + textBox7.Text +"%'";

            SqlCommand SqlCom = new SqlCommand(); 

            try
            {
                SqlCon.Open();
                SqlCom.Connection = SqlCon;
                SqlCom.CommandText = SqlStrSearchStore;
                SqlCom.ExecuteNonQuery();
                SqlCon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            SqlDataAdapter dataadapter = new SqlDataAdapter(SqlStrSearchStore, SqlCon);   
            DataSet ds = new DataSet();                                              

            SqlCon.Open();                                                          
            dataadapter.Fill(ds);                                                   
            SqlCon.Close();                                                          

            BindingSource bs;                                                        
            bs = new BindingSource();                                                
            bs.DataSource = ds.Tables[0].DefaultView;                                
            storesDataGridView.DataSource = bs;           
        }

        /// <summary>
        /// Search records in Chocolate table.
        /// </summary>
        private void SearchChocolate_TextChanged(object sender, EventArgs e)
        {
            SqlConnection SqlCon = new SqlConnection(
           @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\krist\source\repos\Datubazes\Datubazes\ChStores.mdf;Integrated Security=True");
            string SqlStrSearchChocolate = "SELECT * FROM Chocolate WHERE Brand LIKE '%" + textBox8.Text + "%'";

            SqlCommand SqlCom = new SqlCommand(); 

            try
            {
                SqlCon.Open();
                SqlCom.Connection = SqlCon;
                SqlCom.CommandText = SqlStrSearchChocolate;
                SqlCom.ExecuteNonQuery();

                SqlCon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            SqlDataAdapter dataadapter = new SqlDataAdapter(SqlStrSearchChocolate, SqlCon);   
            DataSet ds = new DataSet();                                              

            SqlCon.Open();                                                           
            dataadapter.Fill(ds);                                                    
            SqlCon.Close();                                                          

            BindingSource bs;                                                        
            bs = new BindingSource();                                               
            bs.DataSource = ds.Tables[0].DefaultView;                                                                 

            chocolateDataGridView.DataSource = bs;        
        }
    }
}
