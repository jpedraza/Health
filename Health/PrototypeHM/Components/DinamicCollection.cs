using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PrototypeHM.Components
{
    public partial class DinamicCollection : UserControl
    {
        public DinamicCollection()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Подгоняем ширину таблицы по месту испольования
        /// </summary>
        public int tableWidth
        {
            set
            {
                this.dgvMetaData.Width = value;
                this.dgvMetaData.Left = 10;
            }
        }

        /// <summary>
        /// Вешаем обработчик нажатия кнопки "Добавить элемент коллекции"
        /// </summary>
        public Action<object, EventArgs> AddButtonClick { set { button1.Click += new EventHandler(value); } }

        /// <summary>
        /// Вешаем обработчие нажатия конпки "Удалить элемент коллекции"
        /// </summary>
        public Action<object, EventArgs> DeleteButtonClick { set { button2.Click += new EventHandler(value); } }

        public void deleteRow(int index) {
            this.dgvMetaData.Rows.RemoveAt(index);
        }

        /// <summary>
        /// Необходим для наполнения коллекции данными
        /// </summary>
        public object LoadData
        {
            get { return this.dgvMetaData.DataSource; }
            set {
                this.dgvMetaData.DataSource = value;
            }
        }
        
    }
}
