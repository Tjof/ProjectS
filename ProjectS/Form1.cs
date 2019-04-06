using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DSM;


namespace ProjectS
{
    public partial class Form1 : Form
    {
        string FileName = string.Empty;
        string FileText = string.Empty;
        bool Changed = false;
        char c;

        public Form1()
        {
            InitializeComponent();
        }

        void Create()
        {
            if (Changed)
            {
                DialogResult Result = MessageBox.Show("Save?", "SAVE?", MessageBoxButtons.YesNoCancel);
                if (Result == DialogResult.Yes)
                {
                    SaveAs();
                }
                else if (Result == DialogResult.No)
                {
                    TBI.Text = string.Empty;
                }
            }
            else
            {
                TBI.Text = string.Empty;
            }
        }

        void SaveAs()
        {
            using (SaveFileDialog saveFileDialog1 = new SaveFileDialog())
            {
                saveFileDialog1.Filter = "txt files (*.txt)|*.txt";
                saveFileDialog1.FilterIndex = 1;
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    FileName = saveFileDialog1.FileName;
                    File.WriteAllLines(FileName, TBI.Lines);
                    Changed = false;
                }
            }
        }

        void Save()
        {
            if (FileName == string.Empty)
            {
                SaveAs();
            }
            else
            {
                File.WriteAllText(FileName, TBI.Text);
            }
        }

        void Open()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files(*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileName = openFileDialog1.FileName;
                TBI.Lines = File.ReadAllLines(FileName);
                Changed = false;
                TBI.ClearUndo();
            }
        }

        void CloseFile()
        {
            TBI.Text = string.Empty;
            FileName = string.Empty;
            Changed = false;
        }



        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Create();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Changed)
            {
                DialogResult Result = MessageBox.Show("Save?", "SAVE?", MessageBoxButtons.YesNoCancel);
                if (Result == DialogResult.Yes)
                {
                    SaveAs();
                    Open();
                }
                else if (Result == DialogResult.No)
                {
                    Open();
                }
            }
            else
            {
                Open();
            }
        }

        private void TBI_TextChanged(object sender, EventArgs e)
        {
            Changed = true;
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void закрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Changed)
            {
                DialogResult Result = MessageBox.Show("Save?", "SAVE?", MessageBoxButtons.YesNoCancel);
                if (Result == DialogResult.Yes)
                {
                    SaveAs();
                    CloseFile();
                }
                else if (Result == DialogResult.No)
                {
                    CloseFile();
                }
            }
            else
            {
                CloseFile();
            }
        }

        private void отменаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TBI.Undo();
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string SelectedText = string.Empty;
            SelectedText = TBI.SelectedText;
            Clipboard.SetText(SelectedText);
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TBI.SelectedText = Clipboard.GetText();
        }

        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void пускToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string str1 = TBI.Text;
            //StateMachine<State, char> stateMachine = new StateMachine<State, char>(
            //    State.Q0 /*начальное состояние Q0*/,
            //    new[] { State.Q3, State.Q4 }/*финальные состояния Q1,Q3*/,
            //    Transit /*функция переходов*/);

            //OutputRTB.Text = (String.Format("{0}", stateMachine.Check(str1)));

            State s = State.Q0;
            string str1 = TBI.Text;
            for (int i = 0; i < str1.Length; i++)
            {
                c = str1[i];
                s = Transit(s, c);
                
            }

            if (s == State.Q3 || s == State.Q4)
            {
                OutputRTB.Text = "Молодец!";
            }
            else {
                OutputRTB.Text = "Не верно. Хнык.";
            }
            
        }

        enum State { Q0, Q1, Q2, Q3, Q4, Q5, Error }
        static State Transit(State s, char c)
        {
            switch (s)
            {
                case State.Q0:
                    if (c == '+' || c == '-')
                        return State.Q1;
                    else if (c == '0' || c == '1')
                        return State.Q2;
                    return State.Error;
                case State.Q1:
                    if (c == '0' || c == '1')
                        return State.Q2;
                    return State.Error;
                case State.Q2:
                    if (c == '0' || c == '1')
                        return State.Q3;
                    else if (c == '.')
                        return State.Q4;
                    return State.Error;
                case State.Q3:
                    if (c == '0' || c == '1')
                        return State.Q3;
                    else if (c == '.')
                        return State.Q4;
                    return State.Error;
                case State.Q4:
                    if (c == '0' || c == '1')
                        return State.Q4;
                    return State.Error;
                default:
                    return State.Error;
            }
        }

        enum State2 { Q0, Q1, Q2, Q3, Q4, Q5, Error }
        static State2 Transit2(State2 s, char c)
        {
            switch (s)
            {
                case State2.Q0:
                    if (c == 'a')
                        return State2.Q1;
                    else if (c == 'c')
                        return State2.Q4;
                    return State2.Error;
                case State2.Q1:
                    if (c == 'b')
                        return State2.Q2;
                    return State2.Error;
                case State2.Q2:
                    if (c == 'c')
                        return State2.Q3;
                    return State2.Error;
                case State2.Q3:
                    return State2.Error;
                case State2.Q4:
                    if (c == 'c')
                        return State2.Q5;
                    return State2.Error;
                case State2.Q5:
                    return State2.Error;
                default:
                    return State2.Error;
            }
        }


        enum State3 { Q0, Error }
        static State3 Transit3(State3 s, char c)
        {
            switch (s)
            {
                case State3.Q0:
                    if (c == '0' || c == '1')
                        return State3.Q0;
                    return State3.Error;

                default:
                    return State3.Error;
            }

        }
    }
}

