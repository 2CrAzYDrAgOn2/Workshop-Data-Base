using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace test_DataBase
{
    public partial class AddFormMasters : Form
    {
        private readonly DataBase dataBase = new DataBase();

        public AddFormMasters()
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
                var fullName = textBoxFullName.Text;
                var adress = textBoxAddress.Text;
                var phone = textBoxPhone.Text;
                var dateOfBirth = textBoxDateOfBirth.Text;
                var insuranceNumber = textBoxInsuranceNumber.Text;
                var iNN = textBoxINN.Text;
                var addQuery = $"INSERT INTO Masters (FullName, Address, Phone, DateOfBirth, InsuranceNumber, INN) VALUES ('{fullName}', '{adress}', '{phone}', '{dateOfBirth}', '{insuranceNumber}', '{iNN}')";
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