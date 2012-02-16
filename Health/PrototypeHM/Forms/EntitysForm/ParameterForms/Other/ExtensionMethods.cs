using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using PrototypeHM.Properties;

namespace PrototypeHM.Forms.EntitysForm.ParameterForms.Other
{
    /// <summary>
    /// Методы расширения для форм работающих с БД.
    /// </summary>
    public static class ExtensionMethods
    {
        public static void SwitchOnNoticePanel(this ICommonFormsFunctions iFunctions, System.Windows.Forms.Form form)
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
                                Text = "Успешно подгружено",
                                Font = new Font("TimesNewRoman", 10f, FontStyle.Bold),
                                Name = "Label",
                                Left = 10,
                                AutoSize = true
                            };
            panel.Controls.Add(label);
            form.Controls.Add(panel);
        }

        public static void GetPositiveNotice(this ICommonFormsFunctions iFunctions, Form form)
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
                                      if(t.Interval >= 600)
                                      {
                                          if (label != null) label.Visible = false;
                                          t.Stop();
                                      }
                                  }
                              };
        }

        public static void GetPositiveNotice(this ICommonFormsFunctions iFunctions, Form form, string message)
        {
            var label = form.Controls["NoticeInfoPanel"].Controls["Label"] as Label;
            if (label != null)
            {
                label.ForeColor = Color.Teal;

                if (message != null) label.Text = message;
            }
            TimeGo(label);
        }

        public static void GetNegativeNotice(this ICommonFormsFunctions iFunctions, Form form)
        {
            var label = form.Controls["NoticeInfoPanel"].Controls["Label"] as Label;
            if (label != null)
            {
                label.ForeColor = Color.Red;
                label.Text = Resources.ExtensionMethods_GetNegativeNotice_Не_удалось_выполнить_;
                TimeGo(label);
            }
        }

        public static void GetNegativeNotice(this ICommonFormsFunctions iFunctions, Form form, string message)
        {
            var label = form.Controls["NoticeInfoPanel"].Controls["Label"] as Label;
            if (label != null)
            {
                label.ForeColor = Color.Red;

                if (message != null) label.Text = message;
                TimeGo(label);
            }
        }
    }
}
