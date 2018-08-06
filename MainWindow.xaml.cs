using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;
using System.Windows.Controls;



namespace FinalProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        String filename = " ";
        String copy = "";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void command_KeyUp(object sender, KeyEventArgs e)
        {
            string input = command.Text;
            string[] words = input.Split(' ');
            string[] words2 = input.Split('/');
            int num,num2;
            Int32.TryParse(command.Text, out num);
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                if (input == "Save as")
                {
                    save_as();
                }
                else if (input == "Save")
                {
                    save();
                }
                else if (input == "Open")
                {
                    open();
                }
                else if (input == "Search")
                {
                    search();
                }
                else if (input == "Help")
                {
                    helpShow();
                }
                else if (words2[0] == "c")
                {
                    Int32.TryParse(words2[3], out num2);
                    change(words2[1], words2[2], num2);
                }
                else if (input == num.ToString())
                {
                    jumpToLine(num);
                }
                else if (words[0] == "Up")
                {
                    Int32.TryParse(words[1], out num);
                    scrollUp(num);
                }
                else if (words[0] == "Down")
                {
                    Int32.TryParse(words[1], out num);
                    scrollDown(num);
                }
                else if (words[0] == "Left")
                {
                    Int32.TryParse(words[1], out num);
                    scrollLeft(num);
                }
                else if (words[0] == "Right")
                {
                    Int32.TryParse(words[1], out num);
                    scrollRight(num);
                }
                else if (words[0] == "Setcl")
                {
                    Int32.TryParse(words[1], out num);
                    setcl(num);
                }
                else if (input == "Forward")
                {
                    forward();
                }
                else if (input == "Back")
                {
                    back();
                }
                else if (input == "Split")
                {
                    split();
                }
                else if (input == "Merge")
                {
                    merge();
                }

            }
        }

        private void setcl(int num)
        {
            clBox.Margin = new Thickness(32, 60+((num-1)*16.5), 0, 0);
        }

        private void search_KeyUp(object sender, KeyEventArgs e)
        {
            string input = searchBox.Text;
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                search();
            }
        }

        private void open_Click(object sender, RoutedEventArgs e)
        {
            open();
        }

        bool saveVar = true;
        private void save_Click(object sender, RoutedEventArgs e)
        {
            save();

        }

        private void condition(object sender, KeyEventArgs e)
        {
            string text = actualText.Text;
            int _cursorPosition = actualText.SelectionStart;
            string substring = text.Substring(0, _cursorPosition);
            string[] words = substring.Split('\n');
            string[] words2 = text.Split('\n');

            int rowLine = words2.Length;
            string temp = "";
            for (int i = 0; i < words2.Length; i++)
            {
                temp += "====\n";
            }
            suffix.Text = temp;
            int row = words.Length;
            string row2 = row.ToString();
            string column = (words[words.Length - 1].Length).ToString();
            long size = 1;
            if (filename != " ")
                size = new FileInfo(filename).Length;
            infoBox.Text = "Col: " + column + " Line: " + row2 + " " + filename + " Size: " + size;

            String template = "";
            for (int i = 1; i <= rowLine; i++)
            {
                template = template + i.ToString() + "\n";
            }
            lineNumber.Text = template;




        }

        private void save()
        {
            if (filename == " ")
                save_as();
            else
            {
                string createText = actualText.Text + Environment.NewLine;
                File.WriteAllText(filename, createText);
            }
        }

        private void save_as()
        {

            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Buny"; // Default file name
            dlg.DefaultExt = ".txt"; // Default file extension
            dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;

                string createText = actualText.Text + Environment.NewLine;
                File.WriteAllText(filename, createText);
            }
            filename = dlg.FileName;
        }

        private void search()
        {
            
            

        }
        private void change(string text1, string text2, int num)
        {
            
                int _cursorPosition = actualText.SelectionStart;
                string text = actualText.Text;
                
                string[] words = text.Split('\n');
                string temp_text = "";

                

                for (int i = 0; i < words.Length; i++)
                {
                    if (i > _cursorPosition && i <= _cursorPosition + num - 1) words[i] = words[i].Replace(text1, text2);
                    
                    temp_text = temp_text +  words[i] + "\n";
                }
                text = temp_text;

                actualText.Text = text;
            
           

        }

        private void scrollUp(int number)
        {
            for(int i = 0; i < number; i++)
            {
                actualText.LineUp();
            }
        }
        private void scrollDown(int number)
        {
            for (int i = 0; i < number; i++)
            {
                actualText.LineDown();
            }
        }


        private void scrollLeft(int number)
        {
            try
            {
                int _cursorPosition = actualText.SelectionStart;
                actualText.SelectionStart = _cursorPosition - number;
                actualText.Focus();

            }
            catch
            {
                MessageBox.Show(" ");
            }
        }
        private void scrollRight(int number)
        {
            try
            {
                int _cursorPosition = actualText.SelectionStart;
                actualText.SelectionStart = _cursorPosition + number;
                actualText.Focus();
            }
            catch
            {
                MessageBox.Show(" ");
            }
        }

private void suffix_KeyUp(object sender, KeyEventArgs e)
    {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;

                int number = 0;
                int _cursorPosition = suffix.SelectionStart;
                string text = suffix.Text;
                string sub_text = text.Substring(0, _cursorPosition);
                string[] words = sub_text.Split('\n');
                string last_line = words[words.Length - 1];
                last_line = last_line.Replace('=', '\0');

                int row = words.Length;
                int index = row;
                
                


                char[] suffCommand = last_line.ToCharArray();
                if (suffCommand.Length == 2)
                Int32.TryParse(suffCommand[1].ToString(), out number);
                string operation = suffCommand[0].ToString();

                if (operation == "i")
                {
                    insertLine(number, index);
                }
                else if (operation == "m")
                {
                    moveLine(number,index);
                }
                else if (operation == "/")
                {
                    duplicate(index);
                }
                else if (operation == "a")
                {
                    insertAfter(index);
                }
                else if (operation == "b")
                {
                    insertBefore(index);
                }
                else if (operation == "c")
                {
                    copyLine(number, index);
                }

            }
}
private void insertLine(int number, int index)
        {
            string text = actualText.Text;

            string[] words = text.Split('\n');

            string temp = "";

            for (int i = 0; i < index; i++)
            {
                temp += words[i];
            }

            for (int n = 0; n < number; n++)
            {
                temp += "\n";
            }

            for (int i = index; i < words.Length; i++)
            {
                temp += words[i];
            }
            actualText.Text = temp;
}
private void copyLine(int number, int index)
        {
            string buny = "";
            string text = actualText.Text;
            
            string[] words = text.Split('\n');



            for (int i = index - 1; i <= number; i++){
                buny += words[i];
                buny += "\n";
            }
            copy = buny;
            
        }
private void insertAfter(int index)
        {
            string text = actualText.Text;

            string[] words = text.Split('\n');

            string temp = "";

            for (int i = 0; i < index; i++)
            {
                temp += words[i];
            }
            temp += copy;
            for (int i = index; i < words.Length; i++)
            {
                temp += words[i];
            }
            actualText.Text = "\n" + temp;
        }
private void insertBefore(int index)
        {
            string text = actualText.Text;
            string[] words = text.Split('\n');
            string temp = "";
            for (int i = 0; i < index; i++)
            {
                temp += words[i];
            }
            temp += copy;
            for (int i = index; i < words.Length; i++)
            {
                temp += words[i];
            }
            actualText.Text = temp;

}
private void moveLine(int number, int index)
        {
            string text = actualText.Text;

            string[] words = text.Split('\n');

            string temp = "";

            for (int i = 0; i < index - 1; i++)
            {
                temp += words[i];
            }

            for (int n = 0; n < number; n++)
            {
                temp += "\n";
            }

            for (int i = index - 1; i < words.Length; i++)
            {
                temp += words[i];
            }
            actualText.Text = temp;
}
private void duplicate(int index)
        {
            string text = actualText.Text;

            string[] words = text.Split('\n');

            string temp = "";
            string copyLine = words[index - 1];

            for (int i = 0; i < index; i++)
            {
                temp += words[i];
            }
            temp += copyLine;
            for (int i = index; i < words.Length; i++)
            {
                temp += words[i];
            }
            actualText.Text = temp;
        }

    


private void jumpToLine(int num)
        {
            int totalLength = 0;
            string text = actualText.Text;
            string[] words = text.Split('\n');

            text = text.Replace("\n", String.Empty);

            int counter = 0;
            for (int i = 0; i < num - 1; i++)
                if (counter % 2 == 0)
                    totalLength += words[i].Length + 1;
                else
                    totalLength += words[i].Length;

            string substring = text.Substring(0, totalLength);
            actualText.SelectionStart = substring.Length;
            actualText.Focus();
        }

private void forward()
    {

        actualText.PageDown();
        command.Focus();

    }

private void back()
    {

        actualText.PageUp();
        command.Focus();

    }

private void open()
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        if (openFileDialog.ShowDialog() == true)
            actualText.Text = File.ReadAllText(openFileDialog.FileName);
        filename = openFileDialog.FileName;
    }

private void saveAs_Click(object sender, RoutedEventArgs e)
    {
        save_as();
    }
private void helpShow()
        {
            MessageBox.Show("----Help----\n**Commands**\nSave: Saves txt file with given name. (Default:Buny.txt)\nSave as: Gets path from user to save as a different file.\nOpen: Gets path from user to open a new file.\nSearch: Not working properly.\n#: Skips to line number with given number.\nUp #: Slides upwards as given number. -Up 3-> Slides 3 lines upwards.\nDown # Slides downwards as given number.\nLeft #:Goes left columns as given number.\nRight #: Goes right columns as given number.\nForward: Scrolls down 1 screen size (15 lines)\nBack: Back: Scrolls up 1 screen size (15 lines)\nSetcl #: Marks given line in red color.\nChange: -> c/word_to_change/word_to_replace/amount of lines to look at.\nHelp: Opens help message box.\n\n**Suffix Commands**\nThese commands are written near textbox lines. Gets written line as current and operates from that line\ni #: Inserts empty lines as given number after current line.\nx #: Not working\ns #: Not working\n(a,b,c# works together)\na: Inserts copied line after current line\nb: Inserts copied line before current line.\nc#: Copies given number of lines to insert.\nm#: Moves current line and lines under it downwards as given amount.\nQuoation mark: Duplicates current line and prints it to next line.");
        }

        private void help_Click(object sender, RoutedEventArgs e)
        {
            helpShow();
        }

private void actualText_KeyUp(object sender, KeyEventArgs e)
    {

    }

private void split()
    {
        try
        {
            actualText2.Height += 100;
            actualText.Height -= 100;
            lineNumber.Height -= 100;
            suffix.Height -= 100;
        }
        catch
        {
            MessageBox.Show("Something went wrong :(");
        }
    }

private void merge()
    {
        try
        {
            actualText2.Height -= 100;
            actualText.Height += 100;
            lineNumber.Height += 100;
            suffix.Height += 100;
        }
        catch
        {
            MessageBox.Show("Something went wrong :(");
        }
    }

private void Scroll(object sender, ScrollChangedEventArgs e)
    {
        if (sender.GetType().ToString() == "System.Windows.Controls.TextBox")
        {
            string objName = ((TextBox)sender).Name;
            if (objName.ToString() == "actualText")
            {
                lineNumber.ScrollToVerticalOffset(e.VerticalOffset);
                    //clBox.ScrollToVerticalOffset(e.VerticalOffset);
                    suffix.ScrollToVerticalOffset(e.VerticalOffset);
            }
            else
            {
                lineNumber.ScrollToVerticalOffset(e.VerticalOffset);
                    //clBox.ScrollToVerticalOffset(e.VerticalOffset);
                    actualText.ScrollToVerticalOffset(e.VerticalOffset);
            }
        }
        else
        {
            actualText.ScrollToVerticalOffset(e.VerticalOffset);
            lineNumber.ScrollToVerticalOffset(e.VerticalOffset);
        }
    }

private void button_Click(object sender, RoutedEventArgs e)
    {
        split();
    }

private void button1_Click(object sender, RoutedEventArgs e)
    {
        merge();
    }
    }
}
