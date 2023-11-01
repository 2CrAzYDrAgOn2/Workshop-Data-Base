using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace test_DataBase
{
    public partial class AddFormProducts : Form
    {
        private readonly DataBase dataBase = new DataBase();

        public AddFormProducts()
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
                var modelNumber = textBoxModelNumber.Text;
                var modelName = textBoxModelName.Text;
                var productType = textBoxProductType.Text;
                var size = textBoxSize.Text;
                var description = textBoxDescription.Text;
                var addQuery = $"INSERT INTO Products (ModelNumber, ModelName, ProductType, Size, Description) VALUES('{modelNumber}', '{modelName}', '{productType}', '{size}', '{description}')";
                var sqlCommand = new SqlCommand(addQuery, dataBase.GetConnection());
                sqlCommand.ExecuteNonQuery();
                MessageBox.Show("Запись успешно создана!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
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