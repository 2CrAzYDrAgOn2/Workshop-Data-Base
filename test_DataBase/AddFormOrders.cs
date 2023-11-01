using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace test_DataBase
{
    public partial class AddFormOrders : Form
    {
        private readonly DataBase dataBase = new DataBase();

        public AddFormOrders()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        /// <summary>
        /// ButtonSave_Click вызывается при нажатии на кнопку "Сохранить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                dataBase.OpenConnection();
                var customerName = textBoxCustomerName.Text;
                var orderDate = textBoxOrderDate.Text;
                var executionDate = textBoxExecutionDate.Text;
                var specialInstructions = textBoxSpecialInstructions.Text;
                if (int.TryParse(textBoxPrice.Text, out int price) && int.TryParse(textBoxMasterIDOrders.Text, out int masterIDOrders) && int.TryParse(textBoxMaterialIDOrders.Text, out int materialIDOrders))
                {
                    var addQuery = $"INSERT INTO Orders (CustomerName, OrderDate, ExecutionDate, Price, SpecialInstructions, MasterID, MaterialID) VALUES('{customerName}', '{orderDate}', '{executionDate}', '{price}', '{specialInstructions}', '{masterIDOrders}', '{materialIDOrders}')";
                    var sqlCommand = new SqlCommand(addQuery, dataBase.GetConnection());
                    sqlCommand.ExecuteNonQuery();
                    MessageBox.Show("Запись успешно создана!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Цена должна иметь числовой формат!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                dataBase.CloseConnection();
            }
        }
    }
}