using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using PrototypeHM.DB;
using PrototypeHM.DB.DI;
using PrototypeHM.Forms.EntitysForm.ParameterForms.ValueTypes;
using PrototypeHM.Parameter;
using PrototypeHM.Properties;

namespace PrototypeHM.Forms.EntitysForm.ParameterForms.Other
{
    /// <summary>
    /// Методы расширения для форм работающих с БД.
    /// </summary>
    public static class ExtensionMethods
    {
        public static void SwitchOnNoticePanel<TData>(this TData form) where TData:DIForm, ICommonFormsFunctions
        {
           
            var panel = new Panel
                            {
                                Height = 30,
                                Dock = DockStyle.Bottom,
                                Width = form.Width,
                                //BackColor = Color.Beige,
                                Name = "NoticeInfoPanel",
                                BorderStyle = BorderStyle.Fixed3D,
                                Font = new Font("TimesNewRoman", 10f, FontStyle.Bold),
                                
                                
                            };
            var label = new Label
                            {
                                //Text = "Успешно подгружено",
                                Font = new Font("TimesNewRoman", 10f, FontStyle.Bold),
                                Name = "Label",
                                Left = 10,
                                AutoSize = true
                            };
            panel.Controls.Add(label);
            form.Height += panel.Height;
            form.Controls.Add(panel);
        }

        public static void GetPositiveNotice<TData>(this TData form) where TData : DIForm, ICommonFormsFunctions
        {
            var label = form.Controls["NoticeInfoPanel"].Controls["Label"] as Label;
            if (label != null){ label.ForeColor = Color.Teal;
                label.Text = Resources.ExtensionMethods_GetPositiveNotice_Выполнено_успешно_;
            }

            TimeGo(label);
        }

        private static void TimeGo(Control label)
        {
            var timer = new Timer {Interval = 250};
            timer.Start();
            timer.Tick += delegate(object sender, EventArgs args)
                              {
                                  var t = sender as Timer;
                                  if (t == null) return;
                                  t.Interval += 20;
                                  if (t.Interval < 350)
                                  {
                                       if (label != null) label.Visible = !label.Visible;
                                  }
                                  if (t.Interval > 350){
                                      
                                      if (label != null) label.Visible = true;
                                      if(t.Interval >= 500)
                                      {
                                          if (label != null) label.Visible = false;
                                          t.Interval = 250;
                                          t.Stop();
                                      }
                                  }
                              };
        }

        public static void GetPositiveNotice<TData>(this TData form, string message) where TData:DIForm, ICommonFormsFunctions
        {
            var label = form.Controls["NoticeInfoPanel"].Controls["Label"] as Label;
            if (label != null)
            {
                label.ForeColor = Color.Teal;

                if (message != null) label.Text = message;
            }
            TimeGo(label);
        }

        public static void GetNegativeNotice<TData>(this TData form) where TData:DIForm, ICommonFormsFunctions
        {
            var label = form.Controls["NoticeInfoPanel"].Controls["Label"] as Label;
            if (label != null)
            {
                label.ForeColor = Color.Red;
                label.Text = Resources.ExtensionMethods_GetNegativeNotice_Не_удалось_выполнить_;
                TimeGo(label);
            }
        }

        public static void GetNegativeNotice<T>(this T form, string message) where T:DIForm,ICommonFormsFunctions
        {
            var label = form.Controls["NoticeInfoPanel"].Controls["Label"] as Label;
            if (label != null)
            {
                label.ForeColor = Color.Red;

                if (message != null) label.Text = message;
                TimeGo(label);
            }
        }

        /// <summary>
        /// Данный метод выполняет стадартные действия по загрузке списка данных.
        /// Те действия, которые конкретны для каждой формы, передайте через делегатом через параметр
        /// 
        /// </summary>
        /// <typeparam name="TData">Тип данных отрисовываемого объекта</typeparam>
        /// <param name="iFunctions"></param>
        /// <param name="innerMethod">Конкретные действия для конкретной формы. Передайте сюда делегат.Параметр index - дает
        /// номер строки, которая обрабатывается на конкретной итерации
        /// </param>
        /// <param name="object">Список объектов</param>
        /// <param name="form">экземпляр формы, необходим для подключения к ядру.</param>
        public static void MakeLoadAllData<TData>(this ICommonFormsFunctions iFunctions, Action<TData, int> innerMethod, ref IList<TData> @object, DIForm form) where TData : class, new()
        {
            //Ищем контекст операций для типа данных
            var oC = form.DIKernel.Get<OperationsRepository>().Operations.FirstOrDefault(
                o => o.GetType() == typeof (OperationsContext<TData>)) as OperationsContext<TData>;

            if (oC == null)
            {
                throw new Exception("Отсутсвует контекст операций");
            }

            //Ищем делегат подгрузки данных
            var @delegate = oC.Load;
            if (@delegate == null)
                throw new Exception("Отсутствует операция загрузки");

            //Наполняем список данными.
            @object = @delegate();
            if (@object.Count <= 0) return;
            var index = -1;

            //Делаем перебор и вызываем локальный код формы
            foreach (var item in @object)
            {
                index++;
                innerMethod(item, index);
            }
        }

        public static void MakeTableWork<TData, TData2>
            (this ICommonFormsFunctions functions, ref ContextMenuStrip contextMenuStrip, TData2 form, Func<bool> editMethod, Func<TData> deleteMethod, Action updateTable, int rowcount)
            where TData : class, new()
            where TData2 : DIForm, ICommonFormsFunctions
        {
            if (contextMenuStrip == null) throw new ArgumentNullException("contextMenuStrip");

            contextMenuStrip.Items.Add("Редактировать");
            contextMenuStrip.Items.Add("Удалить");

            contextMenuStrip.Items[0].Click += delegate
            {
                //var value = dataGridView1.Rows[e.RowIndex].Cells["ValueTypeId"].Value;
                //if (value == (object)string.Empty) return;
                //var editForm = new EditForm(form.DIKernel,
                //                            Convert.ToInt32(
                //                                value));
                //editForm.ShowDialog();
                var flagResult = editMethod();

                if (flagResult)
                {
                    form.GetPositiveNotice("Успешно изменено");
                }
                else
                {
                    form.GetNegativeNotice("Редактирование отменено пользователем");
                }
                updateTable();
            };

            contextMenuStrip.Items[1].Click += delegate
            {
                try
                {
                    var oC =
                        form.DIKernel.Get<OperationsRepository>().Operations.FirstOrDefault(
                            o => o.GetType() == typeof(OperationsContext<TData>)) as
                        OperationsContext<TData>;

                    if (oC == null)
                        throw new Exception("Отсутсвует контекст операции");

                    var @delegate = oC.Delete;
                    if (@delegate == null)
                        throw new Exception("Отсутсвует операция удаления");

                    //var dO =
                    //    _listObject.FirstOrDefault(
                    //        o =>
                    //        o.ValueTypeId ==
                    //        Convert.ToInt32(
                    //            dataGridView1.Rows[e.RowIndex].Cells["ValueTypeId"].Value));
                    var dO = deleteMethod();

                    var qS = @delegate(dO);
                    if (qS.Status == 1)
                    {
                        //YMessageBox.Information("Успешно удалено");
                        updateTable();

                        form.GetPositiveNotice(
                                               rowcount > 1
                                                   ? "Успешно удалено"
                                                   : "Успешно удалено. В базе нет типов.");
                    }
                    else
                    {
                        YMessageBox.Error(qS.StatusMessage);
                    }
                }
                catch (Exception exp)
                {
                    YMessageBox.Error(exp.Message);
                }
            };


        }

        public static string GetFirstLetterInString(this ICommonFormsFunctions iFormsFunctions, string @string)
        {
            if (@string == null) throw new ArgumentNullException("string");
            return @string.Substring(0, 1);
        }
    }
}
