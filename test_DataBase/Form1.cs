using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace test_DataBase
{
    internal enum RowState
    {
        Existed,
        New,
        Modified,
        ModifiedNew,
        Deleted
    }

    public partial class Form1 : Form
    {
        private readonly DataBase dataBase = new DataBase();
        private int selectedRow;

        public Form1()
        {
            try
            {
                InitializeComponent();
                StartPosition = FormStartPosition.CenterScreen;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// CreateColumns вызывается при создании колонок
        /// </summary>
        private void CreateColumns()
        {
            try
            {
                dataGridViewMaterials.Columns.Add("MaterialID", "Номер");
                dataGridViewMaterials.Columns.Add("MaterialName", "Название материала");
                dataGridViewMaterials.Columns.Add("IsNew", String.Empty);
                dataGridViewMasters.Columns.Add("MasterID", "Номер");
                dataGridViewMasters.Columns.Add("FullName", "ФИО");
                dataGridViewMasters.Columns.Add("Address", "Адрес");
                dataGridViewMasters.Columns.Add("Phone", "Телефон");
                dataGridViewMasters.Columns.Add("DateOfBirth", "Дата рождения");
                dataGridViewMasters.Columns.Add("InsuranceNumber", "Номер страхового свидетельства");
                dataGridViewMasters.Columns.Add("INN", "ИНН");
                dataGridViewMasters.Columns.Add("IsNew", String.Empty);
                dataGridViewProducts.Columns.Add("ProductID", "Номер");
                dataGridViewProducts.Columns.Add("ModelNumber", "Номер модели");
                dataGridViewProducts.Columns.Add("ModelName", "Название модели");
                dataGridViewProducts.Columns.Add("ProductType", "Тип изделия");
                dataGridViewProducts.Columns.Add("Size", "Размеры");
                dataGridViewProducts.Columns.Add("Description", "Описание");
                dataGridViewProducts.Columns.Add("IsNew", String.Empty);
                dataGridViewOrders.Columns.Add("OrderID", "Номер");
                dataGridViewOrders.Columns.Add("CustomerName", "Заказчик");
                dataGridViewOrders.Columns.Add("OrderDate", "Дата заказа");
                dataGridViewOrders.Columns.Add("ExecutionDate", "Дата исполнения");
                dataGridViewOrders.Columns.Add("Price", "Стоимость");
                dataGridViewOrders.Columns.Add("SpecialInstructions", "Особые указания");
                dataGridViewOrders.Columns.Add("MasterID", "Номер мастера");
                dataGridViewOrders.Columns.Add("MaterialID", "Номер материала");
                dataGridViewOrders.Columns.Add("IsNew", String.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// CreateColumns вызывается при очистке полей
        /// </summary>
        private void ClearFields()
        {
            try
            {
                textBoxMaterialID.Text = "";
                textBoxMaterialName.Text = "";
                textBoxMasterID.Text = "";
                textBoxFullName.Text = "";
                textBoxAddress.Text = "";
                textBoxPhone.Text = "";
                textBoxDateOfBirth.Text = "";
                textBoxInsuranceNumber.Text = "";
                textBoxINN.Text = "";
                textBoxProductID.Text = "";
                textBoxModelNumber.Text = "";
                textBoxModelName.Text = "";
                textBoxProductType.Text = "";
                textBoxSize.Text = "";
                textBoxDescription.Text = "";
                textBoxOrderID.Text = "";
                textBoxCustomerName.Text = "";
                textBoxOrderDate.Text = "";
                textBoxExecutionDate.Text = "";
                textBoxPrice.Text = "";
                textBoxSpecialInstructions.Text = "";
                textBoxMasterIDOrders.Text = "";
                textBoxMaterialIDOrders.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ReadSingleRow вызывается при чтении строк
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <param name="iDataRecord"></param>
        private void ReadSingleRow(DataGridView dataGridView, IDataRecord iDataRecord)
        {
            try
            {
                switch (dataGridView.Name)
                {
                    case "dataGridViewMaterials":
                        dataGridView.Rows.Add(iDataRecord.GetInt32(0), iDataRecord.GetString(1), RowState.Modified);
                        break;

                    case "dataGridViewMasters":
                        dataGridView.Rows.Add(iDataRecord.GetInt32(0), iDataRecord.GetString(1), iDataRecord.GetString(2), iDataRecord.GetString(3), iDataRecord.GetDateTime(4), iDataRecord.GetString(5), iDataRecord.GetString(6), RowState.Modified);
                        break;

                    case "dataGridViewProducts":
                        dataGridView.Rows.Add(iDataRecord.GetInt32(0), iDataRecord.GetString(1), iDataRecord.GetString(2), iDataRecord.GetString(3), iDataRecord.GetString(4), iDataRecord.GetString(5), RowState.Modified);
                        break;

                    case "dataGridViewOrders":
                        dataGridView.Rows.Add(iDataRecord.GetInt32(0), iDataRecord.GetString(1), iDataRecord.GetDateTime(2), iDataRecord.GetDateTime(3), iDataRecord.GetInt32(4), iDataRecord.GetString(5), iDataRecord.GetInt32(6), iDataRecord.GetInt32(7), RowState.Modified);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// RefreshDataGrid вызывается при обновлении dataGridView
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <param name="tableName"></param>
        private void RefreshDataGrid(DataGridView dataGridView, string tableName)
        {
            try
            {
                dataGridView.Rows.Clear();
                string queryString = $"select * from {tableName}";
                SqlCommand sqlCommand = new SqlCommand(queryString, dataBase.GetConnection());
                dataBase.OpenConnection();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    ReadSingleRow(dataGridView, sqlDataReader);
                }
                sqlDataReader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Form1_Load вызывается при загрузке формы "Form1"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                CreateColumns();
                RefreshDataGrid(dataGridViewMaterials, "Materials");
                RefreshDataGrid(dataGridViewMasters, "Masters");
                RefreshDataGrid(dataGridViewProducts, "Products");
                RefreshDataGrid(dataGridViewOrders, "Orders");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// DataGridView_CellClick вызывается при нажатии на ячейку в DataGridView
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <param name="selectedRow"></param>
        private void DataGridView_CellClick(DataGridView dataGridView, int selectedRow)
        {
            try
            {
                DataGridViewRow dataGridViewRow = dataGridView.Rows[selectedRow];
                switch (dataGridView.Name)
                {
                    case "dataGridViewMaterials":
                        textBoxMaterialID.Text = dataGridViewRow.Cells[0].Value.ToString();
                        textBoxMaterialName.Text = dataGridViewRow.Cells[1].Value.ToString();
                        break;

                    case "dataGridViewMasters":
                        textBoxMasterID.Text = dataGridViewRow.Cells[0].Value.ToString();
                        textBoxFullName.Text = dataGridViewRow.Cells[1].Value.ToString();
                        textBoxAddress.Text = dataGridViewRow.Cells[2].Value.ToString();
                        textBoxPhone.Text = dataGridViewRow.Cells[3].Value.ToString();
                        textBoxDateOfBirth.Text = dataGridViewRow.Cells[4].Value.ToString();
                        textBoxInsuranceNumber.Text = dataGridViewRow.Cells[5].Value.ToString();
                        textBoxINN.Text = dataGridViewRow.Cells[6].Value.ToString();
                        break;

                    case "dataGridViewProducts":
                        textBoxProductID.Text = dataGridViewRow.Cells[0].Value.ToString();
                        textBoxModelNumber.Text = dataGridViewRow.Cells[1].Value.ToString();
                        textBoxModelName.Text = dataGridViewRow.Cells[2].Value.ToString();
                        textBoxProductType.Text = dataGridViewRow.Cells[3].Value.ToString();
                        textBoxSize.Text = dataGridViewRow.Cells[4].Value.ToString();
                        textBoxDescription.Text = dataGridViewRow.Cells[5].Value.ToString();
                        break;

                    case "dataGridViewOrders":
                        textBoxOrderID.Text = dataGridViewRow.Cells[0].Value.ToString();
                        textBoxCustomerName.Text = dataGridViewRow.Cells[1].Value.ToString();
                        textBoxOrderDate.Text = dataGridViewRow.Cells[2].Value.ToString();
                        textBoxExecutionDate.Text = dataGridViewRow.Cells[3].Value.ToString();
                        textBoxPrice.Text = dataGridViewRow.Cells[4].Value.ToString();
                        textBoxSpecialInstructions.Text = dataGridViewRow.Cells[5].Value.ToString();
                        textBoxMasterIDOrders.Text = dataGridViewRow.Cells[6].Value.ToString();
                        textBoxMaterialIDOrders.Text = dataGridViewRow.Cells[7].Value.ToString();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Search вызывается при поиске данных в DataGridView
        /// </summary>
        /// <param name="dataGridView"></param>
        private void Search(DataGridView dataGridView)
        {
            try
            {
                dataGridView.Rows.Clear();
                switch (dataGridView.Name)
                {
                    case "dataGridViewMaterials":
                        string searchStringDormitories = $"select * from Materials where concat (MaterialID ,MaterialName) like '%" + textBoxSearchMaterials.Text + "%'";
                        SqlCommand sqlCommandDormitories = new SqlCommand(searchStringDormitories, dataBase.GetConnection());
                        dataBase.OpenConnection();
                        SqlDataReader sqlDataReaderDormitories = sqlCommandDormitories.ExecuteReader();
                        while (sqlDataReaderDormitories.Read())
                        {
                            ReadSingleRow(dataGridView, sqlDataReaderDormitories);
                        }
                        sqlDataReaderDormitories.Close();
                        break;

                    case "dataGridViewMasters":
                        string searchStringHousingOrders = $"select * from Masters where concat (MaterID, FullName, Address, Phone, DateOfBirth, InsuranceNumber, INN) like '%" + textBoxSearchMasters.Text + "%'";
                        SqlCommand sqlCommandHousingOrders = new SqlCommand(searchStringHousingOrders, dataBase.GetConnection());
                        dataBase.OpenConnection();
                        SqlDataReader sqlDataReaderHousingOrders = sqlCommandHousingOrders.ExecuteReader();
                        while (sqlDataReaderHousingOrders.Read())
                        {
                            ReadSingleRow(dataGridView, sqlDataReaderHousingOrders);
                        }
                        sqlDataReaderHousingOrders.Close();
                        break;

                    case "dataGridViewProducts":
                        string searchStringRooms = $"select * from Products where concat (ProductID, ModelNumber, ModelName, ProductType, Size, Description) like '%" + textBoxSearchProducts.Text + "%'";
                        SqlCommand sqlCommandRooms = new SqlCommand(searchStringRooms, dataBase.GetConnection());
                        dataBase.OpenConnection();
                        SqlDataReader sqlDataReaderRooms = sqlCommandRooms.ExecuteReader();
                        while (sqlDataReaderRooms.Read())
                        {
                            ReadSingleRow(dataGridView, sqlDataReaderRooms);
                        }
                        sqlDataReaderRooms.Close();
                        break;

                    case "dataGridViewOrders":
                        string searchStringStudents = $"select * from Orders where concat (OrdersID ,CustomerName, OrderDate, ExecutionDate, Price, SpecialInstructions, MasterID, MaterialID) like '%" + textBoxSearchOrders.Text + "%'";
                        SqlCommand sqlCommandStudents = new SqlCommand(searchStringStudents, dataBase.GetConnection());
                        dataBase.OpenConnection();
                        SqlDataReader sqlDataReaderStudents = sqlCommandStudents.ExecuteReader();
                        while (sqlDataReaderStudents.Read())
                        {
                            ReadSingleRow(dataGridView, sqlDataReaderStudents);
                        }
                        sqlDataReaderStudents.Close();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// DeleteRow вызывается при удалении строки
        /// </summary>
        /// <param name="dataGridView"></param>
        private void DeleteRow(DataGridView dataGridView)
        {
            try
            {
                int index = dataGridView.CurrentCell.RowIndex;
                dataGridView.Rows[index].Visible = false;
                switch (dataGridView.Name)
                {
                    case "dataGridViewMaterials":
                        if (dataGridView.Rows[index].Cells[0].Value.ToString() == string.Empty)
                        {
                            dataGridView.Rows[index].Cells[2].Value = RowState.Deleted;
                            return;
                        }
                        dataGridView.Rows[index].Cells[2].Value = RowState.Deleted;
                        break;

                    case "dataGridViewMasters":
                        if (dataGridView.Rows[index].Cells[0].Value.ToString() == string.Empty)
                        {
                            dataGridView.Rows[index].Cells[7].Value = RowState.Deleted;
                            return;
                        }
                        dataGridView.Rows[index].Cells[7].Value = RowState.Deleted;
                        break;

                    case "dataGridViewProducts":
                        if (dataGridView.Rows[index].Cells[0].Value.ToString() == string.Empty)
                        {
                            dataGridView.Rows[index].Cells[6].Value = RowState.Deleted;
                            return;
                        }
                        dataGridView.Rows[index].Cells[6].Value = RowState.Deleted;
                        break;

                    case "dataGridViewOrders":
                        if (dataGridView.Rows[index].Cells[0].Value.ToString() == string.Empty)
                        {
                            dataGridView.Rows[index].Cells[8].Value = RowState.Deleted;
                            return;
                        }
                        dataGridView.Rows[index].Cells[8].Value = RowState.Deleted;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// UpdateBase вызывается при обновлении базы данных
        /// </summary>
        /// <param name="dataGridView"></param>
        private void UpdateBase(DataGridView dataGridView)
        {
            try
            {
                dataBase.OpenConnection();
                for (int index = 0; index < dataGridView.Rows.Count; index++)
                {
                    switch (dataGridView.Name)
                    {
                        case "dataGridViewMaterials":
                            var rowStateMaterials = (RowState)dataGridView.Rows[index].Cells[2].Value;
                            if (rowStateMaterials == RowState.Existed)
                            {
                                continue;
                            }
                            if (rowStateMaterials == RowState.Deleted)
                            {
                                var materialID = Convert.ToInt32(dataGridView.Rows[index].Cells[0].Value);
                                var deleteQuery = $"delete from Materials where MaterialID = {materialID}";
                                var sqlCommand = new SqlCommand(deleteQuery, dataBase.GetConnection());
                                sqlCommand.ExecuteNonQuery();
                            }
                            if (rowStateMaterials == RowState.Modified)
                            {
                                var materialID = dataGridView.Rows[index].Cells[0].Value.ToString();
                                var materialName = dataGridView.Rows[index].Cells[1].Value.ToString();
                                var changeQuery = $"update Materials set MaterialName = '{materialName}' where MaterialID = '{materialID}'";
                                var sqlCommand = new SqlCommand(changeQuery, dataBase.GetConnection());
                                sqlCommand.ExecuteNonQuery();
                            }
                            break;

                        case "dataGridViewMasters":
                            var rowStateMasters = (RowState)dataGridView.Rows[index].Cells[7].Value;
                            if (rowStateMasters == RowState.Existed)
                            {
                                continue;
                            }
                            if (rowStateMasters == RowState.Deleted)
                            {
                                var masterID = Convert.ToInt32(dataGridView.Rows[index].Cells[0].Value);
                                var deleteQuery = $"delete from Masters where MasterID = {masterID}";
                                var sqlCommand = new SqlCommand(deleteQuery, dataBase.GetConnection());
                                sqlCommand.ExecuteNonQuery();
                            }
                            if (rowStateMasters == RowState.Modified)
                            {
                                var masterID = dataGridView.Rows[index].Cells[0].Value.ToString();
                                var fullName = dataGridView.Rows[index].Cells[1].Value.ToString();
                                var adress = dataGridView.Rows[index].Cells[2].Value.ToString();
                                var phone = dataGridView.Rows[index].Cells[3].Value.ToString();
                                var dateOfBirth = dataGridView.Rows[index].Cells[4].Value.ToString();
                                var insuranceNumber = dataGridView.Rows[index].Cells[5].Value.ToString();
                                var iNN = dataGridView.Rows[index].Cells[6].Value.ToString();
                                var changeQuery = $"update Masters set FullName = '{fullName}', Address = '{adress}', Phone = '{phone}', DateOfBirth = '{dateOfBirth}', InsuranceNumber = '{insuranceNumber}', INN = '{iNN}' where MasterID = '{masterID}'";
                                var sqlCommand = new SqlCommand(changeQuery, dataBase.GetConnection());
                                sqlCommand.ExecuteNonQuery();
                            }
                            break;

                        case "dataGridViewProducts":
                            var rowStateProducts = (RowState)dataGridView.Rows[index].Cells[6].Value;
                            if (rowStateProducts == RowState.Existed)
                            {
                                continue;
                            }
                            if (rowStateProducts == RowState.Deleted)
                            {
                                var productID = Convert.ToInt32(dataGridView.Rows[index].Cells[0].Value);
                                var deleteQuery = $"delete from Products where ProductID = {productID}";
                                var sqlCommand = new SqlCommand(deleteQuery, dataBase.GetConnection());
                                sqlCommand.ExecuteNonQuery();
                            }
                            if (rowStateProducts == RowState.Modified)
                            {
                                var productID = dataGridView.Rows[index].Cells[0].Value.ToString();
                                var modelNumber = dataGridView.Rows[index].Cells[1].Value.ToString();
                                var modelName = dataGridView.Rows[index].Cells[2].Value.ToString();
                                var productType = dataGridView.Rows[index].Cells[3].Value.ToString();
                                var size = dataGridView.Rows[index].Cells[4].Value.ToString();
                                var description = dataGridView.Rows[index].Cells[5].Value.ToString();
                                var changeQuery = $"update Products set ModelNumber = '{modelNumber}', ModelName = '{modelName}', ProductType = '{productType}', Size = '{size}', Description = '{description}' where ProductID = '{productID}'";
                                var sqlCommand = new SqlCommand(changeQuery, dataBase.GetConnection());
                                sqlCommand.ExecuteNonQuery();
                            }
                            break;

                        case "dataGridViewOrders":
                            var rowStateOrders = (RowState)dataGridView.Rows[index].Cells[8].Value;
                            if (rowStateOrders == RowState.Existed)
                            {
                                continue;
                            }
                            if (rowStateOrders == RowState.Deleted)
                            {
                                var orderID = Convert.ToInt32(dataGridView.Rows[index].Cells[0].Value);
                                var deleteQuery = $"delete from Orders where OrderID = {orderID}";
                                var sqlCommand = new SqlCommand(deleteQuery, dataBase.GetConnection());
                                sqlCommand.ExecuteNonQuery();
                            }
                            if (rowStateOrders == RowState.Modified)
                            {
                                var orderID = dataGridView.Rows[index].Cells[0].Value.ToString();
                                var customerName = dataGridView.Rows[index].Cells[1].Value.ToString();
                                var orderDate = dataGridView.Rows[index].Cells[2].Value.ToString();
                                var executionDate = dataGridView.Rows[index].Cells[3].Value.ToString();
                                var price = dataGridView.Rows[index].Cells[4].Value.ToString();
                                var specialInstructions = dataGridView.Rows[index].Cells[5].Value.ToString();
                                var masterID = dataGridView.Rows[index].Cells[6].Value.ToString();
                                var materialID = dataGridView.Rows[index].Cells[7].Value.ToString();
                                var changeQuery = $"update Orders set CustomerName = '{customerName}', OrderDate = '{orderDate}', ExecutionDate = '{executionDate}', Price = '{price}', SpecialInstructions = '{specialInstructions}', MasterID = '{masterID}', MaterialID = '{materialID}' where OrderID = '{orderID}'";
                                var sqlCommand = new SqlCommand(changeQuery, dataBase.GetConnection());
                                sqlCommand.ExecuteNonQuery();
                            }
                            break;
                    }
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

        /// <summary>
        /// Change вызывается при изменении данных в базе данных
        /// </summary>
        /// <param name="dataGridView"></param>
        private void Change(DataGridView dataGridView)
        {
            try
            {
                var selectedRowIndex = dataGridView.CurrentCell.RowIndex;
                switch (dataGridView.Name)
                {
                    case "dataGridViewMaterials":
                        var materialID = textBoxMaterialID.Text;
                        var materialName = textBoxMaterialName.Text;
                        dataGridView.Rows[selectedRowIndex].SetValues(materialID, materialName);
                        dataGridView.Rows[selectedRowIndex].Cells[2].Value = RowState.Modified;
                        break;

                    case "dataGridViewMasters":
                        var masterID = textBoxMasterID.Text;
                        var fullName = textBoxFullName.Text;
                        var adress = textBoxAddress.Text;
                        var phone = textBoxPhone.Text;
                        var dateOfBirth = textBoxDateOfBirth.Text;
                        var insuranceNumber = textBoxInsuranceNumber.Text;
                        var iNN = textBoxINN.Text;
                        dataGridView.Rows[selectedRowIndex].SetValues(masterID, fullName, adress, phone, dateOfBirth, insuranceNumber, iNN);
                        dataGridView.Rows[selectedRowIndex].Cells[7].Value = RowState.Modified;
                        break;

                    case "dataGridViewProducts":
                        var productID = textBoxProductID.Text;
                        var modelNumber = textBoxModelNumber.Text;
                        var modelName = textBoxModelName.Text;
                        var productType = textBoxProductType.Text;
                        var size = textBoxSize.Text;
                        var description = textBoxDescription.Text;
                        dataGridView.Rows[selectedRowIndex].SetValues(productID, modelNumber, modelName, productType, size, description);
                        dataGridView.Rows[selectedRowIndex].Cells[6].Value = RowState.Modified;
                        break;

                    case "dataGridViewOrders":
                        var orderID = textBoxOrderID.Text;
                        var customerName = textBoxCustomerName.Text;
                        var orderDate = textBoxOrderDate.Text;
                        var executionDate = textBoxExecutionDate.Text;
                        var price = textBoxPrice.Text;
                        var specialInstructions = textBoxSpecialInstructions.Text;
                        var masterIDOrders = textBoxMasterIDOrders.Text;
                        var materialIDOrders = textBoxMaterialIDOrders.Text;
                        dataGridView.Rows[selectedRowIndex].SetValues(orderID, customerName, orderDate, executionDate, price, specialInstructions, masterIDOrders, materialIDOrders);
                        dataGridView.Rows[selectedRowIndex].Cells[8].Value = RowState.Modified;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ButtonRefresh_Click вызывается при нажатии на кнопку обновления
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                RefreshDataGrid(dataGridViewMaterials, "Materials");
                RefreshDataGrid(dataGridViewMasters, "Masters");
                RefreshDataGrid(dataGridViewProducts, "Products");
                RefreshDataGrid(dataGridViewOrders, "Orders");
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ButtonClear_Click вызывается при нажатии на кнопку "Изменить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClear_Click(object sender, EventArgs e)
        {
            try
            {
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// TextBoxSearchMaterials_TextChanged вызывается при изменении текста
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxSearchMaterials_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Search(dataGridViewMaterials);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// DataGridViewMaterials_CellClick вызывается при нажатии на ячейку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridViewMaterials_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                selectedRow = e.RowIndex;
                if (e.RowIndex >= 0)
                {
                    DataGridView_CellClick(dataGridViewMaterials, selectedRow);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ButtonNewMaterials_Click вызывается при нажатии на кнопку "Новая запись"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonNewMaterials_Click(object sender, EventArgs e)
        {
            try
            {
                AddFormMaterials addForm = new AddFormMaterials();
                addForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ButtonDeleteMaterials_Click вызывается при нажатии на кнопку "Удалить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonDeleteMaterials_Click(object sender, EventArgs e)
        {
            try
            {
                DeleteRow(dataGridViewMaterials);
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ButtonChangeMaterials_Click вызывается при нажатии на кнопку "Изменить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonChangeMaterials_Click(object sender, EventArgs e)
        {
            try
            {
                Change(dataGridViewMaterials);
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ButtonSaveMaterials_Click вызывается при нажатии на кнопку "Сохранить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSaveMaterials_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateBase(dataGridViewMaterials);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// TextBoxSearchMasters_TextChanged вызывается при изменении текста
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxSearchMasters_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Search(dataGridViewMasters);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// DataGridViewMasters_CellClick вызывается при нажатии на ячейку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridViewMasters_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                selectedRow = e.RowIndex;
                if (e.RowIndex >= 0)
                {
                    DataGridView_CellClick(dataGridViewMasters, selectedRow);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ButtonNewMasters_Click вызывается при нажатии на кнопку "Новая запись"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonNewMasters_Click(object sender, EventArgs e)
        {
            try
            {
                AddFormMasters addForm = new AddFormMasters();
                addForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ButtonDeleteMasters_Click вызывается при нажатии на кнопку "Удалить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonDeleteMasters_Click(object sender, EventArgs e)
        {
            try
            {
                DeleteRow(dataGridViewMasters);
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ButtonChangeMasters_Click вызывается при нажатии на кнопку "Изменить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonChangeMasters_Click(object sender, EventArgs e)
        {
            try
            {
                Change(dataGridViewMasters);
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ButtonSaveMasters_Click вызывается при нажатии на кнопку "Сохранить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSaveMasters_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateBase(dataGridViewMasters);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// TextBoxSearchProducts_TextChanged вызывается при изменении текста
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxSearchProducts_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Search(dataGridViewProducts);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// DataGridViewProducts_CellClick вызывается при нажатии на ячейку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridViewProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                selectedRow = e.RowIndex;
                if (e.RowIndex >= 0)
                {
                    DataGridView_CellClick(dataGridViewProducts, selectedRow);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ButtonNewProducts_Click вызывается при нажатии на кнопку "Новая запись"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonNewProducts_Click(object sender, EventArgs e)
        {
            try
            {
                AddFormProducts addForm = new AddFormProducts();
                addForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ButtonDeleteProducts_Click вызывается при нажатии на кнопку "Удалить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonDeleteProducts_Click(object sender, EventArgs e)
        {
            try
            {
                DeleteRow(dataGridViewProducts);
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ButtonChangeProducts_Click вызывается при нажатии на кнопку "Изменить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonChangeProducts_Click(object sender, EventArgs e)
        {
            try
            {
                Change(dataGridViewProducts);
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ButtonSaveProducts_Click вызывается при нажатии на кнопку "Сохранить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSaveProducts_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateBase(dataGridViewProducts);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// TextBoxSearchOrders_TextChanged вызывается при изменении текста
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxSearchOrders_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Search(dataGridViewOrders);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// DataGridViewOrders_CellClick вызывается при нажатии на ячейку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridViewOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                selectedRow = e.RowIndex;
                if (e.RowIndex >= 0)
                {
                    DataGridView_CellClick(dataGridViewOrders, selectedRow);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ButtonNewOrders_Click вызывается при нажатии на кнопку "Новая запись"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonNewOrders_Click(object sender, EventArgs e)
        {
            try
            {
                AddFormOrders addForm = new AddFormOrders();
                addForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ButtonDeleteOrders_Click вызывается при нажатии на кнопку "Удалить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonDeleteOrders_Click(object sender, EventArgs e)
        {
            try
            {
                DeleteRow(dataGridViewOrders);
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ButtonChangeOrders_Click вызывается при нажатии на кнопку "Изменить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonChangeOrders_Click(object sender, EventArgs e)
        {
            try
            {
                Change(dataGridViewOrders);
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ButtonSaveOrders_Click вызывается при нажатии на кнопку "Сохранить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSaveOrders_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateBase(dataGridViewOrders);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}