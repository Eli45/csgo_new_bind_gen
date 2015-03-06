using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSGO_Buy_Bind_Generator
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        //Too lazy to remove global variables at this point.
        //Dont get angry at me.
        string key = "";
        int old_selected_count;
        bool isChecked;

        private void Menu_Load(object sender, EventArgs e)
        {
            update_combo_boxes(possible_weapons_primary, drpPrimary);
            update_combo_boxes(possible_weapons_secondary, drpSecondary);
            drpPrimary.DropDownStyle = ComboBoxStyle.DropDownList;
            drpSecondary.DropDownStyle = ComboBoxStyle.DropDownList;
            old_selected_count = lsbGrenade.SelectedIndices.Count;
        }

        public static void update_combo_boxes(Array a, ComboBox c)
        {
            foreach (var item in a)
            {
                c.Items.Add(item);
            }
        }

        public static string[] possible_weapons_primary = new string[17]   
        {
            "None",
            "AK47/M4A4/M4A1-S",
            "AUG/SG 553",
            "AWP",
            "FAMAS/Galil",
            "SCAR 20/GS3SG1",
            "SSG 08",
            "MP9/Mac-10",
            "MP7",
            "PP-Bizon",
            "P90",
            "UMP-45",
            "Mag-7/Sawed-off",
            "Nova",
            "XM1014",
            "Negev",
            "M249"
        };
        private static string[] possible_weapons_primary_binds = new string[17] 
        {
            "",
            "buy ak47",
            "buy aug",
            "buy awp",
            "buy famas",
            "buy g3sg1",
            "buy ssg08",
            "buy mac10",
            "buy mp7",
            "buy bizon",
            "buy p90",
            "buy ump45",
            "buy mag7",
            "buy nova",
            "buy xm1014",
            "buy negev",
            "buy m249"
        };

        public static string[] possible_weapons_secondary = new string[6]
        { 
            "None",
            "Glock-18/USP-S/P2000",
            "Dual Berettas",
            "P250",
            "Tec-9/Five-Seven/CZ-75a",
            "Desert Eagle"
        };
        private static string[] possible_weapons_secondary_binds = new string[6]
        { 
            "",
            "buy hkp2000",
            "buy elite",
            "buy p250",
            "buy fn57",
            "buy deagle"
        };

        private static string[] possible_grenade_binds = new string[5]
        {
            "buy decoy",
            "buy smokegrenade",
            "buy flashbang",
            "buy hegrenade",
            "buy incgrenade"
        };
        private static string[] possible_equipment_binds = new string[4]
        {
            "buy vest",
            "buy vesthelm",
            "buy defuser",
            "buy Taser"
        };

        //Numpad buttons
        //0, .
        private void btnDot_Click(object sender, EventArgs e)
        {
            key = "KP_DEL";
            updateBindButtons(btnDot);
        }
        private void btn0_Click(object sender, EventArgs e)
        {
            key = "KP_INS";
            updateBindButtons(btn0);
        }
        //0, .

        //1, 2, 3
        private void btn1_Click(object sender, EventArgs e)
        {
            key = "KP_END";
            updateBindButtons(btn1);
        }
        private void btn2_Click(object sender, EventArgs e)
        {
            key = "KP_DOWNARROW";
            updateBindButtons(btn2);
        }
        private void btn3_Click(object sender, EventArgs e)
        {
            key = "KP_PGDN";
            updateBindButtons(btn3);
        }
        //1, 2, 3

        //4, 5, 6
        private void btn4_Click(object sender, EventArgs e)
        {
            key = "KP_LEFTARROW";
            updateBindButtons(btn4);
        }
        private void btn5_Click(object sender, EventArgs e)
        {
            key = "KP_5";
            updateBindButtons(btn5);
        }
        private void btn6_Click(object sender, EventArgs e)
        {
            key = "KP_RIGHTARROW";
            updateBindButtons(btn6);
        }
        //4, 5, 6

        //7, 8 ,9
        private void btn7_Click(object sender, EventArgs e)
        {
            key = "KP_HOME";
            updateBindButtons(btn7);
        }
        private void btn8_Click(object sender, EventArgs e)
        {
            key = "KP_UPARROW";
            updateBindButtons(btn8);
        }
        private void btn9_Click(object sender, EventArgs e)
        {
            key = "KP_PGUP";
            updateBindButtons(btn9);
        }
        //7, 8 ,9

        // "/", *, -
        private void btnSlash_Click(object sender, EventArgs e)
        {
            key = "KP_SLASH";
            updateBindButtons(btnSlash);
        }
        private void btnAsterik_Click(object sender, EventArgs e)
        {
            key = "KP_MULTIPLY";
            updateBindButtons(btnAsterik);
        }
        private void btnMinus_Click(object sender, EventArgs e)
        {
            key = "KP_MINUS";
            updateBindButtons(btnMinus);
        }
        // "/", *, -

        // +, Enter
        private void btnPlus_Click(object sender, EventArgs e)
        {
            key = "KP_PLUS";
            updateBindButtons(btnPlus);
        }
        private void btnEnter_Click(object sender, EventArgs e)
        {
            key = "KP_ENTER";
            updateBindButtons(btnEnter);
        }
        // +, Enter

        //Numpad buttons

        private bool displayOutput(string s)
        {
            string weapon = s;
            string output;
            if (key != "" && weapon != "")
            {
                output = "bind \"" + key + "\"" + " \"" + weapon + "\";";

                int to_erase = output.Length - 3;
                output = output.Remove(to_erase, 1);

                //Sets up an array to use incase we need to replace an already written value.
                string[] oldLines = txtOutput.Lines;
                for (int replaceStrings = 0; replaceStrings < oldLines.Length - 1; replaceStrings++)
                {
                    oldLines[replaceStrings] = oldLines[replaceStrings] + "\r\n";
                }

                bool replaced = false;
                for (int i = 0; i < txtOutput.Lines.Length; i++)
                {
                    //Checks if we are replacing an already written value and if we are then we set it correctly in our new array.
                    if (txtOutput.Lines[i].StartsWith("bind \"" + key + "\""))
                    {
                        oldLines[i] = "bind \"" + key + "\"" + " \"" + weapon + "\";\r\n";
                        replaced = true;
                    }
                }

                if (replaced)
                {
                    //If we are replacing then we rewrite the text to include all the lines of our new array. We also display an error message.
                    txtOutput.Text = oldLines[0];
                    for (int j = 1; j < oldLines.Length; j++)
                    {
                        txtOutput.Text = txtOutput.Text + oldLines[j];
                    }
                    throwError("Error: " + key + " is already bound!\nRebound " + key);// + " to buy \'" + weapon + "\'");
                }
                else
                {
                    txtOutput.Text = txtOutput.Text + "" + output + "\r\n";
                }
                //operation successful.
                return true;
            }
            else
            {
				//Basic error checking.
                if (key == "" && weapon != "") { throwError("Error: key not selected."); }
                else if (weapon == "" && key != "") { throwError("Error: weapon not selected."); }
                else { throwError("Error: weapon and key not selected."); }
                //operation failure.
                return false;
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            string output = null;

            output = get_outputs();
            bool succeeded = displayOutput(output);

            //Only reset our input options if they are successfully entered.
            if (succeeded)
            {
                resetBindButtons();
                reset_drops();
                reset_lists();
                key = "";
            }

        }

        private void reset_drops()
        {
            drpPrimary.SelectedIndex = -1;
            drpSecondary.SelectedIndex = -1;
        }
        private void reset_lists()
        {
            lsbEquipment.SelectedItem = null;
            lsbGrenade.SelectedItem = null;
        }

        private string get_outputs()
        {
            string local_output;

            local_output = get_selected_values_dropdown(drpPrimary, possible_weapons_primary_binds);
            local_output = local_output + get_selected_values_dropdown(drpSecondary, possible_weapons_secondary_binds);
            local_output = local_output + iterateListBoxEquipment();
            local_output = local_output + iterateListBoxGrenades();

            return local_output;
        }

        private string get_selected_values_dropdown(ComboBox drop, string[] corresponding)
        {
            try
            {
                if ((string)corresponding[drop.SelectedIndex] == "")
                    return "";
                else
                    return (string)corresponding[drop.SelectedIndex] + "; ";
            }
            catch (IndexOutOfRangeException)
            {
                return "";
            }
        }

        private string iterateListBoxGrenades()
        {
            try
            {

                List<int> items = new List<int>();

                foreach (Object selecteditem in lsbGrenade.SelectedItems)
                {
                    int index = lsbGrenade.Items.IndexOf(selecteditem);
                    items.Add(index);
                }

                bool double_flash = false;

                if (radFlashbangTwo.Checked)
                {
                    double_flash = true;
                }
                string names = "";
                if (possible_grenade_binds[items[0]] != "buy flashbang") 
                { 
                    names = possible_grenade_binds[items[0]] + "; ";    //0 should ALWAYS exist in this scenario.
                }
                else
                {
                    if (double_flash)
                    {
                        names = "buy flashbang; buy flashbang; ";
                    }
                    else
                    {
                        names = "buy flashbang; ";
                    }
                }

                for (int i = 1; i < items.Count; i++)
                {
                    if (possible_grenade_binds[items[i]] != "buy flashbang")
                    {
                        names = names + possible_grenade_binds[items[i]] + "; ";
                    }
                    else
                    {
                        if (double_flash)
                        {
                            names = names + "buy flashbang; buy flashbang; ";
                        }
                        else
                        {
                            names = names + "buy flashbang; ";
                        }
                    }
                }

                return names;
            }
            catch (ArgumentOutOfRangeException)
            {
                return "";
            }
        }

        private string iterateListBoxEquipment()
        {
            try
            {
                List<int> items = new List<int>();

                foreach (Object selecteditem in lsbEquipment.SelectedItems)
                {
                    int index = lsbEquipment.Items.IndexOf(selecteditem);
                    items.Add(index);
                }

                string names = possible_equipment_binds[items[0]] + "; ";    //0 should ALWAYS exist in this scenario.

                for (int i = 1; i < items.Count; i++)
                {
                    names = names + possible_equipment_binds[items[i]] + "; ";
                }

                return names;
            }
            catch (ArgumentOutOfRangeException)
            {
                return "";
            }
        }

        private void resetBindButtons()
        {
            btnDot.BackColor        = Color.White;
            btn0.BackColor          = Color.White;
            btn1.BackColor          = Color.White;
            btn2.BackColor          = Color.White;
            btn3.BackColor          = Color.White;
            btn4.BackColor          = Color.White;
            btn5.BackColor          = Color.White;
            btn6.BackColor          = Color.White;
            btn7.BackColor          = Color.White;
            btn8.BackColor          = Color.White;
            btn9.BackColor          = Color.White;
            btnSlash.BackColor      = Color.White;
            btnAsterik.BackColor    = Color.White;
            btnMinus.BackColor      = Color.White;
            btnPlus.BackColor       = Color.White;
            btnEnter.BackColor      = Color.White;
        }

        private void updateBindButtons(object sender)
        {
            resetBindButtons();
            Button clicked = (Button)sender;
            clicked.BackColor = Color.Blue;
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                var newLines = new List<string>(txtOutput.Lines);
                newLines.RemoveAt(0);
                newLines.RemoveAt(0);
                newLines.RemoveAt(newLines.Count - 1);
                string newText = newLines[0];

                for (int i = 1; i < newLines.Count; i++)
                {
                    newText = newText + newLines[i];
                }

                Clipboard.SetText(newText);
                throwError("Output copied to clipboard!");
            }
            catch
            {
                throwError("Error: nothing to copy!");
            }
        }

        private void Menu_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.NumPad0)          { key = "KP_INS"; updateBindButtons(btn0); }
            else if (e.KeyCode == Keys.NumPad1)     { key = "KP_END"; updateBindButtons(btn1); }
            else if (e.KeyCode == Keys.NumPad2)     { key = "KP_DOWNARROW"; updateBindButtons(btn2); }
            else if (e.KeyCode == Keys.NumPad3)     { key = "KP_PGDN"; updateBindButtons(btn3); }
            else if (e.KeyCode == Keys.NumPad4)     { key = "KP_LEFTARROW"; updateBindButtons(btn4); }
            else if (e.KeyCode == Keys.NumPad5)     { key = "KP_5"; updateBindButtons(btn5); }
            else if (e.KeyCode == Keys.NumPad6)     { key = "KP_RIGHTARROW"; updateBindButtons(btn6); }
            else if (e.KeyCode == Keys.NumPad7)     { key = "KP_HOME"; updateBindButtons(btn7); }
            else if (e.KeyCode == Keys.NumPad8)     { key = "KP_UPARROW"; updateBindButtons(btn8); }
            else if (e.KeyCode == Keys.NumPad9)     { key = "KP_PGUP"; updateBindButtons(btn9); }

            else if (e.KeyCode == Keys.Multiply)    { key = "KP_MULTIPLY"; updateBindButtons(btnAsterik); } // *
            else if (e.KeyCode == Keys.Add)         { key = "KP_PLUS"; updateBindButtons(btnPlus); }
            else if (e.KeyCode == Keys.Subtract)    { key = "KP_MINUS"; updateBindButtons(btnMinus); }
            else if (e.KeyCode == Keys.Divide)      { key = "KP_SLASH"; updateBindButtons(btnSlash); } // '/'
            else if (e.KeyCode == Keys.Enter)       { key = "KP_ENTER"; updateBindButtons(btnEnter); } // ENTER
            //For KP_DEL we use KeyPress below.
        }

        private void Menu_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.')                   { key = "KP_DEL"; updateBindButtons(btnDot); }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtOutput.Text = "Output\n-----------\n";
        }

        private void throwError(string message)
        {
            lblError.Text = message;
        }

        private void btnCFG_Click(object sender, EventArgs e)
        {
            TextWriter tw = new StreamWriter("binds.cfg");
            List<string> newLines = new List<string>(txtOutput.Lines);

            newLines.RemoveAt(0); //Removes
            newLines.RemoveAt(0); //Output and --------- lines.
            for (int i = 0; i < newLines.Count; i++)
            {
                tw.WriteLine(newLines[i]);
            }
            tw.WriteLine("//Generated with CS:GO Weapon Bind Generator by Eli");
            tw.WriteLine(@"//To use ingame put the file in your csgo\cfg\ folder and type in console: 'exec binds.cfg'");
            tw.Close();
            throwError("Files written to 'binds.cfg'");
        }

        
        private void lsbGrenade_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string name_of_item;
                int tracker = 1;
                bool has_flash = false;

                if (lsbGrenade.SelectedItems.Count <= 0)
                {
                    radFlashbangTwo.Checked = false;
                    radFlashbangTwo.Enabled = false;
                }

                foreach(Object selecteditem in lsbGrenade.SelectedItems)
                {
                    name_of_item = selecteditem as String;
                    if (name_of_item == "Flashbang")    
                    {
                        has_flash = true;
                    }
                    if (tracker == lsbGrenade.SelectedItems.Count)
                    {
                        if (!has_flash)
                        {
                            radFlashbangTwo.Enabled = false;
                            radFlashbangTwo.Checked = false;
                        }
                        else
                        {
                            radFlashbangTwo.Enabled = true;
                        }
                    }
                    else
                    {
                        tracker++;
                    }
                        
                }
            }
            catch
            {
                //This is just for safety.
                //I don't think there is any problems with the above code but you know how it is.
                Debug.WriteLine("Something unexpected has happened. See line: <487{Hardcoded could be different now}>.");
            }
        } 

        private void radFlashbangTwo_CheckedChanged(object sender, EventArgs e)
        {
            isChecked = radFlashbangTwo.Checked;
        }

        private void radFlashbangTwo_Click(object sender, EventArgs e)
        {
            if (radFlashbangTwo.Checked && !isChecked)
            {
                radFlashbangTwo.Checked = false;
            }
            else
            {
                radFlashbangTwo.Checked = true;
                isChecked = false;
            }
        }        
    }
}
