using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace UltraCalc
{

    public partial class MainPage : ContentPage
    {



        
        Mathos.Parser.MathParser parser = new Mathos.Parser.MathParser();
        public string expr_show = ""; /*переменная для отображения введенных данных*/
        public string expr_show_nakoplenye = ""; /*переменная для отображения введенных данных*/
        public string expr_calc = "";  /*переменная для накапливания формулы*/
        public string last_btn_name;
        public string last_btn_classid;
        public int cnt_btn_equal=0;
        public int font_size_rows = 10;
        public int font_size_exp = 10;
        public string last_number ="";
        public string result_value = "";





        public MainPage()
        {


            InitializeComponent();
            rows.FontSize = font_size_rows;
            exp.FontSize = font_size_exp;

        }


        public void erase_expr_calc(string s) {
            expr_calc = s.Replace("%", "");
            //expr_calc = s.Replace(",", ".");
        }

        public void erase_last_number(string s)
        {
            last_number = s.Replace("%", "");
            //last_number = s.Replace(",", ".");
        }


        private async void Btn_Clicked(object sender, EventArgs e)
        {
          //776 DisplayAlert("tyu", Panel.Height.ToString(), "Ok");
            Button button = (Button)sender;


            if (button.ClassId == "number" || button.ClassId == "dot" || button.ClassId == "percent")
            {
                last_number = last_number + button.Text;
                erase_last_number(last_number);
            }
            else {
                last_number = "";
            }


            if (last_btn_name == "=" & button.ClassId== "number" )
            {
                expr_calc = "";
            }
            


            if (button.Text == "%" & last_btn_classid== "number")
            {
                
                //await DisplayAlert("2", last_number, "Ok");

                // expr_calc = expr_calc + "/100*("+ expr_calc.Remove(expr_calc.Length- last_number.Length, last_number.Length ) + ")";
                expr_calc = expr_calc.Remove(expr_calc.Length - last_number.Length, last_number.Length)
                                  + parser.Parse(last_number + "/100*(" + expr_calc.Remove(expr_calc.Length - last_number.Length-1, last_number.Length+1) + ")").ToString();
            }


            expr_calc = expr_calc + button.Text;
            erase_expr_calc(expr_calc);
            //await DisplayAlert("1", expr_calc, "Ok");
            expr_show = rows.Text + button.Text;



           // rows.Text = expr_show;
            exp.Text = expr_calc;

            font_size_exp = (int)Panel.Height / Math.Max(12, expr_calc.Length);
            exp.FontSize = font_size_exp;
            var heightContentScroll = SV.ContentSize.Height;
            await SV.ScrollToAsync(0, heightContentScroll, false);

            //result.Text = "= "+parser.Parse(expr_calc).ToString();

            last_btn_classid = button.ClassId;
            last_btn_name = button.Text;


            try {
                result_value = parser.Parse(expr_calc).ToString().Replace(",", ".");
               
            }
            catch
            {
                result.Text = result.Text;
            }
            finally {
                result.Text = "=" + result_value;

            }




        }






        private void Btn_equal(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            last_btn_name = button.Text;
            // expr_calc = "(" + expr_calc + ")";
            expr_calc = expr_calc.Replace(",", ".");



            try
            {
                result_value = parser.Parse(expr_calc).ToString().Replace(",", ".");

            }
            catch
            {
                result.Text = result.Text;
            }
            finally
            {
                result.Text = "=" + result_value;

                expr_show_nakoplenye = expr_show_nakoplenye + expr_calc + "=" + result_value + (char)10;
                rows.Text = expr_show_nakoplenye;


                expr_show = expr_show_nakoplenye;

                expr_calc = result_value;
                exp.Text = "";
                cnt_btn_equal++;

                font_size_exp = (int)Panel.Height / Math.Max(12, expr_calc.Length + 1 + result_value.Length);

                if (cnt_btn_equal == 1)
                {
                    font_size_rows = font_size_exp;

                }
                else { font_size_rows = Math.Min(font_size_exp, font_size_rows); }

                rows.FontSize = font_size_rows;
               // exp.IsVisible = false;
            }



            
            
            

        }

        private void Btn_C(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            last_btn_name = button.Text;

            expr_show_nakoplenye = "";
            expr_show = "";
            expr_calc = "";
            rows.Text = "";
            exp.Text = "";

            result.Text = "";
            cnt_btn_equal = 0;
        }

        private void Btn_X(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (last_btn_name != "=" & last_btn_name != "C")
            {
                result_value = "";
                last_btn_name = button.Text;
               //rows.Text = rows.Text.Remove((rows.Text).Length - 1, 1);
                exp.Text = exp.Text.Remove((exp.Text).Length - 1, 1);
                expr_calc = expr_calc.Remove((expr_calc).Length - 1, 1);
                try
                {
                    result_value = parser.Parse(expr_calc).ToString().Replace(",", ".");

                }
                catch
                {
                    result.Text = result_value;
                }
                finally
                {
                    result.Text = "=" + result_value;

                   

                }

            }
                

            
           
        }
        /*
        private void Btn_Brackets(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            expr_calc = "(" + expr_calc + ")";
            expr_show_nakoplenye = expr_show_nakoplenye + expr_calc ;
            result.Text = parser.Parse(expr_calc).ToString();
            rows.Text = expr_show_nakoplenye;
            expr_show = expr_show_nakoplenye;
            exp.Text = expr_calc;
        }*/
    }
}
