using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Reflection;
using Microsoft.Office.Interop.Word;
using System.Diagnostics;
using System.Collections;
using System.Drawing.Text;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using Excel = Microsoft.Office.Interop.Excel;

#region Errors Detection Tool
namespace Errors_detection_tool
{
    public partial class Form1 : Form
    {
        #region Global Variables
        StreamWriter Log_File;
        public object file;
        public object filePath;
        string LogFile_Path = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\Log_File.log";
        public string file_path = "" ,version_change = "";
        string str;
        string str_TempstrFile;
        public Thread REqDocCheck = null,a=null;
        string p_tagContent;
        string shallTag_Content = "";
        public delegate void ErrorToolDelegate();
        string[] p_tagContent_split;
        public bool healcheck = false;
        public bool conditionalCheck = false;
        public bool IOCheck = false;
        public bool BoldItalicCheck = false;
        public string PTag = null;
        StreamWriter sw_ptag = null;
        StreamWriter sw_Ascb = null;
        StreamWriter sw_ErrorReport = null;
        List<string> BoldItalicIOList = new List<string>();
        List<string> BoldItalicContentList = new List<string>();
        List<string> list_OfAscbPrams = new List<string>();
        List<string> shall_tags_Of_selected_Ptag = new List<string>();
        bool bracketBool = true;
        int shall_tag_count = -1;
        int indexOf_shallTag_count = 0;
        bool mismatchFound = false;
        bool valid = true; // For ISValidFollower Or Not
        StreamWriter sw = null;
        bool foundFirstShallTag = false;

        List<string> HealParam = new List<string>();
        List<string> HealParamWithOutUnderscore = new List<string>();


        static HashSet<char> leftBrackets = new HashSet<char>() { '(', '[', '{' };
        static HashSet<char> rightBrackets = new HashSet<char>() { ')', ']', '}' };

        private delegate void UpdateDelegate(); 

        int CounteIF = 0;
        int CountIf = 0;
        int CountCase = 0;
        int CounteCase = 0;
        int Countels = 0;
        int CountThen = 0;
        int CountElseIf = 0;
        int CountEIFWithOutSpace = 0;
        int countNoOfChars = 0;


        List<Vals> ListWords = new List<Vals>();

        string p_tag;

        string lastLine = "";

        bool isCorrect;
        bool flag2;
        Stack braces = new Stack();

        char[] allBrackets = new char[1000];
        #endregion 


        #region Form Initialization
        public Form1()
        {
            InitializeComponent();

            groupBox1.Enabled = false;

            if (File.Exists(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\ptag_content.txt"))
                File.Delete(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\ptag_content.txt");

            if (File.Exists(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\comments_from_SSRD.txt"))
                File.Delete(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\comments_from_SSRD.txt");

            if (File.Exists(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\BoldItalicMismatch.txt"))
                File.Delete(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\BoldItalicMismatch.txt");

            if (File.Exists(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\Report1.txt"))
                File.Delete(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\Report1.txt");

            if (File.Exists(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\Report.txt"))
                File.Delete(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\Report.txt");


            if (File.Exists(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\BracesCheck.txt"))
                File.Delete(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\BracesCheck.txt");


            if (File.Exists(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\ASCBParameters.txt"))
                File.Delete(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\ASCBParameters.txt");


            if (File.Exists(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\Mismatch_Heal_ASCB.txt"))
                File.Delete(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\Mismatch_Heal_ASCB.txt");


            if (File.Exists(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\healDBParams.txt"))
                File.Delete(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\healDBParams.txt");

            if (File.Exists(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\FinalReport.txt"))
                File.Delete(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\FinalReport.txt");

            if (File.Exists(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\Temp_str.txt"))
                File.Delete(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\Temp_str.txt");

            if (File.Exists(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\Log_File.txt"))
                File.Delete(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\Log_File.txt");


            if (File.Exists(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\IOParametersMismatch.txt"))
                File.Delete(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\IOParametersMismatch.txt");



            

            groupBox3.Enabled = false;

           // HealCheckBox.Checked = true;
            toolStripStatus.Visible = false;
            toolStripProgressBar1.Visible = false;
          
        }
        #endregion


        #region HEAL CheckBox Event
        private void HealCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (HealCheckBox.Checked)
            {
                groupBox3.Enabled = true;

            }
            else
            {

                groupBox3.Enabled = false;
            }
        }
        #endregion

    

        #region Enum values for IF ENDIF Conditions check
        public enum Vals  // enum list for checking If ENDIF conditions
        {


            IF,

            THEN,
            ELSE,
            ELSEIF,
            ELSE_IF,
            ENDIF,
            END_IF,
            CASE,
            ENDCASE,
            AND_ELSE,

        }
        #endregion


        #region Dictionary for Braces Check
        static Dictionary<char, char> bracketPairs = new Dictionary<char, char>()  // Dictionary which has list of braces to check braces mismatch
            {
            { ')', '(' },
            { ']', '[' },
           
            { '}', '{' },
            };
        #endregion


       
        

    
                

        #region Method for IF ELSE    
        public static bool IsValidFollower(Vals val1, Vals val2)   // This method is for checking the follower of conditional Statements
        {
            if (val1 == Vals.IF)
                return val2 == Vals.THEN || val2 == Vals.AND_ELSE || val2 == Vals.CASE;
            if (val1 == Vals.THEN)
                return val2 == Vals.ELSE || val2 == Vals.ELSEIF || val2 == Vals.ELSE_IF || val2 == Vals.END_IF || val2 == Vals.ENDIF || val2 == Vals.IF || val2 == Vals.CASE;
            if (val1 == Vals.ENDIF)
                return val2 == Vals.IF || val2 == Vals.CASE || val2 == Vals.END_IF || val2 == Vals.ENDIF || val2 == Vals.ELSE || val2 == Vals.ELSE_IF || val2 == Vals.ELSEIF;
            if (val1 == Vals.END_IF)
                return val2 == Vals.IF || val2 == Vals.CASE || val2 == Vals.ENDIF || val2 == Vals.END_IF || val2 == Vals.ENDCASE || val2 == Vals.ELSE;
            if (val1 == Vals.ELSE)
                return val2 == Vals.ENDIF || val2 == Vals.END_IF || val2 == Vals.CASE || val2 == Vals.ENDCASE || val2 == Vals.IF;
            if (val1 == Vals.ELSEIF)
                return val2 == Vals.THEN || val2 == Vals.CASE || val2 == Vals.AND_ELSE;
            if (val1 == Vals.ELSE_IF)
                return  val2 == Vals.THEN || val2 == Vals.CASE || val2 == Vals.AND_ELSE;
            if (val1 == Vals.CASE)
                return val2 == Vals.ENDCASE || val2 == Vals.IF;
            if (val1 == Vals.ENDCASE)
                return val2 == Vals.ENDIF || val2 == Vals.CASE || val2 == Vals.IF || val2 == Vals.END_IF;
            return false;
        }
        #endregion 

       
             
        #region Browse HealDataBase Button
        public void ascbBrowse_Click(object sender, EventArgs e)
        {
            #region Check Heal Database
            if (HealCheckBox.Checked)
            {
                toolStripProgressBar1.Enabled = true;
                toolStripProgressBar1.Style = ProgressBarStyle.Marquee;

                try
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Title = "Choose the HEAL DataBase";
                    openFileDialog.InitialDirectory = @"%Libraries\Documents%";
                    openFileDialog.Filter = "text files (*.txt)|*.txt; *.txt|All files (*.*)|*.*";
                    openFileDialog.FilterIndex = 1;
                    openFileDialog.RestoreDirectory = true;
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        healDbtb.Text = openFileDialog.FileName;

                    }

                    string[] readallParameters = File.ReadAllLines(healDbtb.Text);

                    if (!(readallParameters.Length == 0))
                    {
                        
                        for (int indexOfAscb = 0; indexOfAscb < readallParameters.Length; indexOfAscb++)
                        {
                            if (!(readallParameters[indexOfAscb].StartsWith("'")) && !(readallParameters[indexOfAscb] == ""))
                            {

                                string[] allparameters_split = readallParameters[indexOfAscb].Split('=');


                                HealParam.Add(allparameters_split[0]);

                                
                            }

                        }
                     
                        for (int indexOfHeal = 0; indexOfHeal < HealParam.Count; indexOfHeal++)
                        {

                            if (HealParam[indexOfHeal].EndsWith("1 ") || HealParam[indexOfHeal].EndsWith("2 ") || HealParam[indexOfHeal].EndsWith("3 ") || HealParam[indexOfHeal].EndsWith("4 ") || HealParam[indexOfHeal].EndsWith("5 ") || HealParam[indexOfHeal].EndsWith("6 ") || HealParam[indexOfHeal].EndsWith("7 ") || HealParam[indexOfHeal].EndsWith("8 "))
                            {
                                HealParam[indexOfHeal] = HealParam[indexOfHeal].Remove(HealParam[indexOfHeal].Length - 3);
                                // MessageBox.Show(HealParam[indexOfHeal]);

                            }
                            if (HealParam[indexOfHeal].EndsWith("1") || HealParam[indexOfHeal].EndsWith("2") || HealParam[indexOfHeal].EndsWith("3") || HealParam[indexOfHeal].EndsWith("4") || HealParam[indexOfHeal].EndsWith("5") || HealParam[indexOfHeal].EndsWith("6") || HealParam[indexOfHeal].EndsWith("7") || HealParam[indexOfHeal].EndsWith("8"))
                            {
                                HealParam[indexOfHeal] = HealParam[indexOfHeal].Remove(HealParam[indexOfHeal].Length - 2);
                                // MessageBox.Show(HealParam[indexOfHeal]);

                            }
                            if (HealParam[indexOfHeal].EndsWith("1") || HealParam[indexOfHeal].EndsWith("2") || HealParam[indexOfHeal].EndsWith("3") || HealParam[indexOfHeal].EndsWith("4") || HealParam[indexOfHeal].EndsWith("5") || HealParam[indexOfHeal].EndsWith("6") || HealParam[indexOfHeal].EndsWith("7") || HealParam[indexOfHeal].EndsWith("8"))
                            {
                                HealParam[indexOfHeal] = HealParam[indexOfHeal].Remove(HealParam[indexOfHeal].Length - 2);
                                // MessageBox.Show(HealParam[indexOfHeal]);

                            }

                            if (HealParam[indexOfHeal].Contains("_Good"))
                            {
                                HealParam[indexOfHeal] = HealParam[indexOfHeal].Replace("_Good", ".Good");

                            }
                            if (HealParam[indexOfHeal].Contains("_Valid"))
                            {
                                HealParam[indexOfHeal] = HealParam[indexOfHeal].Replace("_Valid", ".Valid");

                            }
                            if (HealParam[indexOfHeal].Contains("_SSM"))
                            {

                                HealParam[indexOfHeal] = HealParam[indexOfHeal].Replace("_SSM", ".SSM");
                            }

                            HealParamWithOutUnderscore.Add(HealParam[indexOfHeal]);
                            // MessageBox.Show(HealParam[indexOfHeal]);
                        }

                        StreamWriter sw_HealParams = null;
                        sw_HealParams = File.AppendText(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\healDBParams.txt");

                        for (int indexOfHealWO_ = 0; indexOfHealWO_ < HealParamWithOutUnderscore.Count; indexOfHealWO_++)
                        {
                            sw_HealParams.WriteLine(HealParamWithOutUnderscore[indexOfHealWO_]);

                        }
                        write_to_log_file("Generated List Of Heal Parameters.....");
                        //  MessageBox.Show(HealParam[3]);
                        sw_HealParams.Close();
                        // MessageBox.Show("Am here done");
                    }
                    else {
                      
                        MessageBox.Show("Please input the correct Heal File.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    
                    }
                    toolStripProgressBar1.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());

                }
            }
#endregion

            else {



                MessageBox.Show("Please check the Heal DataBase check Box");
            }
        }
#endregion

        #region Import Requirements Doc Button
        public void brwsbtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(HealCheckBox.Checked == true && healDbtb.Text == ""))
                {

                    //MessageBox.Show("Before Pressing OK Button Please save and close your Word Documents  ");

                    //Process[] process = null;
                    //process = Process.GetProcessesByName("WINWORD");
                    //foreach (Process process1 in process)
                    //{
                    //    process1.Kill();

                    //}
                    OpenFileDialog DialogA = new OpenFileDialog();
                    DialogA.CheckFileExists = true;
                    
                    DialogA.Title = "Select a File";
                
                    if (DialogA.ShowDialog() == DialogResult.OK)
                    {
                        filepathtb.Text = DialogA.FileName;
                        file_path = filepathtb.Text;

                        file = filepathtb.Text;



                        toolStripStatus.Text = "Extracting P-Tags. Please Wait.......";
                        write_to_log_file("Document Selected is :" +filepathtb.Text);

                       a = new Thread(new ThreadStart(collectListOfPTags));
                       a.IsBackground = true;
                        a.Start(); 

                        p_tag = (string)ptag_cb.SelectedValue;



                    }

                    else if (filepathtb.Text == "")
                    {
                        MessageBox.Show("Please Select a File");
                    }
                }
                else 
                {
                    MessageBox.Show("Please import an Heal DataBase file", "Browse an Heal Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 
                
                }  
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
     

#region P-Tags Collection Method

        private void collectListOfPTags()
        {


            toolStripStatus.Text = "Collecting the P-Tags from Document....";
            object Target = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\Temp_str.txt";
            Microsoft.Office.Interop.Word.Application newApp = new Microsoft.Office.Interop.Word.Application();
            object Unknown = Type.Missing;
            newApp.Documents.Open(ref file, ref Unknown, ref Unknown, ref Unknown, ref Unknown, ref Unknown, ref Unknown, ref Unknown, ref Unknown, ref Unknown, ref Unknown, ref Unknown, ref Unknown, ref Unknown, ref Unknown, ref Unknown);
            object format = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatText;
            newApp.ActiveDocument.SaveAs(ref Target, ref format, ref Unknown, ref Unknown, ref Unknown,
            ref Unknown, ref Unknown, ref Unknown,
            ref Unknown, ref Unknown, ref Unknown,
            ref Unknown, ref Unknown, ref Unknown,
            ref Unknown, ref Unknown);
         
            newApp.Quit(ref Unknown, ref Unknown, ref Unknown);
            
            Thread.Sleep(1000);
            str_TempstrFile = File.ReadAllText(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\Temp_str.txt");

            string[] forLastPtag = File.ReadAllLines(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\Temp_str.txt");

            lastLine = forLastPtag[forLastPtag.Length - 2];

            char[] chars = str_TempstrFile.ToCharArray();
            countNoOfChars = chars.Length - 1;
            

            toolStripStatus.Text = "Extracting P-Tags.....";
            string tempstr = "hai";
            bool foundMismatchinPtag = false;
                    string[] strsplit = str_TempstrFile.Split('\n');
           
                    foreach (string ss in strsplit)
                    {
                        if (ss.Contains("[DDD") || ss.Contains("[SSRD") || ss.Contains("[SRD"))
                         {
                            if (ss.Contains(tempstr) == false)
                            {
                                tempstr = ss.Trim('[', ']', ' ', '\r', '\n', '\t');

                      
                                if (!(tempstr.StartsWith("SSRD")) || (tempstr.StartsWith("DDD")) || (tempstr.StartsWith("SRD")))
                                {
                                 
                                    write_to_log_file("Invalid P-Tag Found, Please check the spaces between the word in P-Tag");
                                    write_to_log_file();
                                    write_to_log_file("Error Found in the Following Line : ");
                                    toolStripStatus.Text = "Error found in P-Tag....";
                                    toolStripStatus.Text = "Application is closing......";
                                    write_to_log_file();
                                    write_to_log_file(tempstr);
                                    write_to_log_file();
                                    write_to_log_file();
                                    foundMismatchinPtag = true;

                                    Log_File.Close();
                                    System.Windows.Forms.Application.Exit();
                                    break;
                                    
                                }
                                StreamWriter sw = null;
                                sw = File.AppendText(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\Report.txt");


                                if (InvokeRequired) 
                                { 
                                Invoke(new UpdateDelegate( 
                                delegate 
                                { 


                                    ptag_cb.Items.Add(tempstr);
                                })
                                );
                                }


                                if (InvokeRequired)
                                {
                                    Invoke(new UpdateDelegate(
                                    delegate
                                    {


                                        groupBox1.Enabled = true;
                                    })
                                    );
                                }


                            
                                                 
                                sw.Write(tempstr + System.Environment.NewLine);
                                sw.Close();
                            }
                            else
                            {
                                string strshal = ss.Substring(ss.IndexOf("[") + 1, ss.LastIndexOf("]") - ss.IndexOf("[") - 1);
                                StreamWriter sw = null;
                                sw = File.AppendText(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\Report1.txt");
                                sw.Write(strshal + System.Environment.NewLine);
                                sw.Close();
                            }
                        }
                        

                    }
                       if (InvokeRequired) 
                            { 
                            Invoke(new UpdateDelegate( 
                            delegate 
                            {        
                                        ptag_cb.Text = "Select";

                                    if (foundMismatchinPtag == false)
                                    {
                                        write_to_log_file("Generated the file Temp_str.txt");
                                        toolStripStatus.Text = "Generated the file Temp_str.txt ...";
                                        write_to_log_file("Seperated the P-Tags From Document");
                                        toolStripStatus.Text = "Seperated the P-Tags From Document ....";

                                    }


                            })
                            );
                            }
        }
        

#endregion


        public void CheckForErrors()
        {
            toolStripStatus.Text = "Started Checking Errors for Selected P-Tag....";
            write_to_log_file("Checking for Errors in The selected P-Tag......");
            p_tag = PTag;
            
            sw_ptag = File.AppendText(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\ptag_content.txt");

            sw_ErrorReport = File.AppendText(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\ErrorReport.txt");


            string pathOfAscb = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\ASCBParameters.txt";
            sw_Ascb = File.AppendText(pathOfAscb);

            Microsoft.Office.Interop.Word.Document document = new Document();

            Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();
            object miss = System.Reflection.Missing.Value;
            object template = Missing.Value;
            object newTemplate = Missing.Value;
            object documentType = Missing.Value;
            object visible = true;

            toolStripStatus.Text = "Opening The document....";
            document = app.Documents.Open(ref filePath,
                                       ref miss, ref miss, ref miss, ref miss, ref miss,
                                       ref miss, ref miss, ref miss, ref miss, ref miss,
                                       ref visible, ref miss, ref miss, ref miss, ref miss);
            document.Activate();
            bool flag = true;
            bool flagOfBoldItalic = true;
            int TRUE_COde = -1;
            int Index_Of_PTag_inList = 0;

            string p_tags = File.ReadAllText(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\Report.txt");
            string[] pTag_List_split = File.ReadAllLines(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\Report.txt");


            List<string> p_tags_list = new List<string>();

           foreach (string Indiv_p_tag in pTag_List_split)
            {

                p_tags_list.Add(Indiv_p_tag);

            }

           for (int indexOf_pTag_list = 0; indexOf_pTag_list < p_tags_list.Count; indexOf_pTag_list++)
           {

               if (p_tags_list[indexOf_pTag_list] == p_tag)
               {

                   Index_Of_PTag_inList = indexOf_pTag_list;  //finding the index of Selected P-Tag in the list Report.txt
                   break;
               }


           }
           

            bool foundBOLdItalic = false;
            string boldItalicVariable = " ";
      
            bool Ascb_Data = false;
            bool foundGeneral = false;
            bool foundAscborInternal = false;

            toolStripStatus.Text = "Reading The document....";

           // StringBuilder sb = new StringBuilder();
          //  sb.Append(' ');
           // string sLine="";
            object missing = System.Reflection.Missing.Value;
            Range myRange = document.Range(ref missing, ref missing);
          
            object iTagStartIdx = 0;
            object iTagEndIdx = 0;
            int itagStart = 0;
            int itagEnd = 0;
            object Ptag = PTag;
            object what = Microsoft.Office.Interop.Word.WdGoToItem.wdGoToLine;
            object which = Microsoft.Office.Interop.Word.WdGoToDirection.wdGoToFirst;
           // object count = Microsoft.Office.Interop.Word.
             object NextPtag = "";
            // Selection selec = null; ;
            // object lblNumberOfLines;
            // object lblNumberOfWords;
            // object lblNumberOfCharactersExcludingSpaces;
             bool lastPtag = false;
             object oEoF = WdUnits.wdStory;

           //  lblNumberOfLines = document.ComputeStatistics(Microsoft.Office.Interop.Word.WdStatistic.wdStatisticLines, ref missing).ToString();
             //No of words
            // lblNumberOfWords = document.ComputeStatistics(Microsoft.Office.Interop.Word.WdStatistic.wdStatisticWords, ref missing);

          //   lblNumberOfCharactersExcludingSpaces = document.ComputeStatistics(Microsoft.Office.Interop.Word.WdStatistic.wdStatisticCharacters, ref missing).ToString();

            // FileInfo fI = new FileInfo();


             if (PTag == p_tags_list[p_tags_list.Count - 1])
             {

            //  iTagEndIdx = countNoOfChars;
               //  NextPtag = lastLine.ToString();

                 
                

                // iTagEndIdx = lblNumberOfCharactersExcludingSpaces;
               lastPtag = true;
               //  NextPtag = lblNumberOfCharactersExcludingSpaces.ToString();

             }
             else
             {
                 NextPtag = p_tags_list[Index_Of_PTag_inList + 1];
             }

            if (myRange.Find.Execute(ref Ptag, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing))

                iTagStartIdx = myRange.Start;
            itagStart = Convert.ToInt32(iTagStartIdx);

            if (!(lastPtag))
            {
                myRange = document.Range(ref missing, ref missing);

                if (myRange.Find.Execute(ref NextPtag, ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing, ref missing))


                   // myRange = document.ActiveWindow.Selection.EndKey(ref oEoF, ref missing);

                    iTagEndIdx = myRange.Start;
                itagEnd = Convert.ToInt32(iTagEndIdx);
           }

            string sLine = "";
            if (!(lastPtag) && (itagStart<itagEnd) )
            {
                //  myRange = document.ActiveWindow.Selection.EndKey(ref oEoF, ref missing);

                //  iTagEndIdx = myRange.Start;

                // iTagEndIdx =    document.ActiveWindow.Selection.EndKey(ref oEoF, ref missing);



                 StringBuilder sb = new StringBuilder();


                foreach (Microsoft.Office.Interop.Word.Paragraph objparagraph in document.Range(ref iTagStartIdx, ref iTagEndIdx).Paragraphs)
                {

                    string headNum = objparagraph.Range.ListFormat.ListString;

                     foreach (char c in objparagraph.Range.Text)
                     {
                         if (char.IsLetterOrDigit(c) || c == '.' || c == '_' || c == ' ' || c == '%' || c == '{' || c == '}' || c == '(' || c == ')' || c == '[' || c == ']' || c == '#' || c == '@' || c == '!' || c == ':' || c == '*')

                         {
                    
                             sb.Append(c);
                      
               
                    
                         }
                         sLine = sb.ToString();
                     }  


                    sLine = objparagraph.Range.Text;


                    string hNum = objparagraph.Range.ListFormat.ListString;
                    if (flag == false || sLine.Contains(p_tag))
                    {
                        flag = false;

                        // code of indeoxofPtag here

                        //  if (sLine.Contains(p_tags_list[p_tags_list.Count-1]))
                        //   {

                        //  sw_ptag.WriteLine(sLine);
                        //  }

                        //  else  if (!(sLine.Contains(p_tags_list[Index_Of_PTag_inList + 1])))
                        //  {

                        sw_ptag.WriteLine(sLine);

                        //  }


                        if (sLine.Contains("ASCB Data") || sLine.Contains("Internal Data") || Ascb_Data == true & !(sLine.Contains("General")))
                        {

                            sLine.Replace("", "");
                            sLine.Replace("\r", "");
                            sLine.Replace("\a", "");
                            sLine.Replace("●", "");
                            sLine.Replace("#", "");
                            if (sLine.EndsWith("\r") || sLine.EndsWith("\a"))
                            {
                                sLine = sLine.Substring(0, sLine.Length - 1);
                            }
                            if (!(sLine.StartsWith("●") || sLine.StartsWith("\r")))
                            {
                                if (((hNum == "") || !(hNum.StartsWith("."))) && !(sLine.Contains(p_tags_list[Index_Of_PTag_inList + 1])))
                                {


                                    sw_Ascb.WriteLine(sLine);
                                }
                            }
                            Ascb_Data = true;

                        }
                        if (sLine.Contains("General"))
                        {

                            Ascb_Data = false;
                        }

                        if (sLine.Contains(p_tags_list[p_tags_list.Count - 1]))
                        {

                            flag = true;
                            break;
                        }

                        else if (sLine.Contains(p_tags_list[Index_Of_PTag_inList + 1]))
                        {
                            flag = true;
                            break;
                        }
                    }

                    Range rWords = objparagraph.Range;

                    //  if (!(sLine.Contains(p_tags_list[Index_Of_PTag_inList + 1])))
                    // {
                    if (flagOfBoldItalic == false || sLine.Contains(p_tag))
                    {
                        flagOfBoldItalic = false;


                        if (sLine.Contains("ASCB Data") || sLine.Contains("Internal Data"))
                        {

                            foundAscborInternal = true;
                            foundGeneral = false;

                        }

                        if (sLine.Contains("General"))
                        {
                            foundAscborInternal = false;
                            foundGeneral = true;


                        }


                        foreach (Range word in rWords.Words)
                        {



                            if (word.Bold == TRUE_COde && word.Italic == TRUE_COde)
                            {
                                foundBOLdItalic = true;
                                if ((boldItalicVariable != ""))
                                {
                                    boldItalicVariable = boldItalicVariable + word.Text;
                                }

                            }

                            else
                            {

                                foundBOLdItalic = false;
                            }



                            if (foundBOLdItalic == false)
                            {

                                if (foundAscborInternal)
                                {
                                    if (boldItalicVariable != " " || boldItalicVariable != null)
                                    {

                                        boldItalicVariable = boldItalicVariable.Replace('\a', ' ');
                                        boldItalicVariable = boldItalicVariable.Replace('\r', ' ');
                                        boldItalicVariable = boldItalicVariable.TrimEnd();

                                        string[] boldItalicIndividual = boldItalicVariable.Split(' ');

                                        foreach (string toAddinList in boldItalicIndividual)
                                        {
                                            BoldItalicIOList.Add(toAddinList);
                                        }
                                        boldItalicIndividual = null;
                                    }


                                }

                                if (foundGeneral)
                                {
                                    if (boldItalicVariable != " ")
                                    {
                                        boldItalicVariable = boldItalicVariable.TrimEnd();
                                        boldItalicVariable = boldItalicVariable.TrimStart();

                                        if (boldItalicVariable.Contains('\r'))
                                        {
                                            string[] newBoldItalic = boldItalicVariable.Split('\r');
                                            for (int indexOfNewBI = 0; indexOfNewBI < newBoldItalic.Length; indexOfNewBI++)
                                            {
                                                BoldItalicContentList.Add(newBoldItalic[indexOfNewBI]);
                                            }
                                        }
                                        else
                                        {
                                            BoldItalicContentList.Add(boldItalicVariable);
                                        }
                                    }

                                }

                                boldItalicVariable = " ";

                            }


                        }
                        foundBOLdItalic = true;

                    }
                    // }


                }
            }
            else
            {

                #region If Last P-Tag is Selected
                foreach (Microsoft.Office.Interop.Word.Paragraph objparagraph in document.Paragraphs)
                {

                    string headNum = objparagraph.Range.ListFormat.ListString;
                    sLine = objparagraph.Range.Text;
                    string hNum = objparagraph.Range.ListFormat.ListString;
                    if (flag == false || sLine.Contains(p_tag))
                    {
                        flag = false;

                        for (int indexOf_pTag_list = 0; indexOf_pTag_list < p_tags_list.Count; indexOf_pTag_list++)
                        {

                            if (p_tags_list[indexOf_pTag_list] == p_tag)
                            {

                                Index_Of_PTag_inList = indexOf_pTag_list;  //finding the index of Selected P-Tag in the list Report.txt
                                break;
                                // Index_Of_PTag_inList = p_tags_list.

                            }


                        }


                        /*    foreach (Microsoft.Office.Interop.Word.Table tableContent in document.Tables)
                             {
                                // while(document.ActiveWindow.
                                 bool flagOfPtagFound = false;
                                 if (sLine.Contains(p_tag) || flagOfPtagFound = true)
                                 {
                                     flagOfPtagFound = true;
                                     if (!(sLine.Contains(p_tags_list[Index_Of_PTag_inList + 1])) && document.Tables.Count > 0)
                                     {

                                         string tableData = " ";
                                         for (int indexOfTable = 0; indexOfTable < tableContent.Rows.Count; indexOfTable++)
                                         {
                                             if (tableContent.Columns.Count > 1)
                                             {
                                                 tableData = tableContent.Cell(indexOfTable, 2).Range.Text;
                                                 if (tableData.Contains("IF") || tableData.Contains("CASE"))
                                                 {

                                                     LogicforConditionalCheck_WithString(tableData);

                                                 }
                                             }
                                         }


                                     }
                                 }

                             } */

                     //   if (!(sLine.Contains(p_tags_list[Index_Of_PTag_inList + 1])))
                      //  {

                            sw_ptag.WriteLine(sLine);
                      //  }

                        if (sLine.Contains("ASCB Data") || sLine.Contains("Internal Data") || Ascb_Data == true & !(sLine.Contains("General")))
                        {
                            // Ascb_Param.Add(sLine);
                            // sLine.Trim('.', '\r', '\a', '', '●');
                            sLine.Replace("", "");
                            sLine.Replace("\r", "");
                            sLine.Replace("\a", "");
                            sLine.Replace("●", "");
                            sLine.Replace("#", "");
                            if (sLine.EndsWith("\r") || sLine.EndsWith("\a"))
                            {
                                sLine = sLine.Substring(0, sLine.Length - 1);
                            }
                            if (!(sLine.StartsWith("●") || sLine.StartsWith("\r")))
                            {
                                if (((hNum == "") || !(hNum.StartsWith("."))) && !(sLine == document.Sentences.Last.ToString()))
                                {

                                    // if (sLine.Contains("#"))
                                    //    {
                                    //       sLine.Remove(sLine.Length - 1);
                                    //    }


                                    sw_Ascb.WriteLine(sLine);
                                }
                            }
                            Ascb_Data = true;

                        }
                        if (sLine.Contains("General"))
                        {

                            Ascb_Data = false;
                        }

                        if (sLine == document.Sentences.Last.ToString())
                        {
                            flag = true;
                            break;
                        }
                    }

                    Range rWords = objparagraph.Range;



                   // if (!(sLine.Contains(p_tags_list[Index_Of_PTag_inList + 1])))
                    //{
                        if (flagOfBoldItalic == false || sLine.Contains(p_tag))
                        {
                            flagOfBoldItalic = false;


                            if (sLine.Contains("ASCB Data") || sLine.Contains("Internal Data"))
                            {

                                foundAscborInternal = true;
                                foundGeneral = false;

                            }

                            if (sLine.Contains("General"))
                            {
                                foundAscborInternal = false;
                                foundGeneral = true;


                            }


                            foreach (Range word in rWords.Words)
                            {



                                if (word.Bold == TRUE_COde && word.Italic == TRUE_COde)
                                {
                                    foundBOLdItalic = true;
                                    if ((boldItalicVariable != ""))
                                    {
                                        boldItalicVariable = boldItalicVariable + word.Text;
                                    }

                                }

                                else
                                {

                                    foundBOLdItalic = false;
                                }



                                if (foundBOLdItalic == false)
                                {

                                    if (foundAscborInternal)
                                    {
                                        if (boldItalicVariable != " " || boldItalicVariable != null)
                                        {

                                            boldItalicVariable = boldItalicVariable.Replace('\a', ' ');
                                            boldItalicVariable = boldItalicVariable.Replace('\r', ' ');
                                            boldItalicVariable = boldItalicVariable.TrimEnd();

                                            string[] boldItalicIndividual = boldItalicVariable.Split(' ');

                                            foreach (string toAddinList in boldItalicIndividual)
                                            {
                                                // boldItalicindividual[] = boldItalicVariable.Split(' '); 
                                                // boldItalicVariable = boldItalicVariable.Split();

                                                BoldItalicIOList.Add(toAddinList);
                                            }
                                            boldItalicIndividual = null;
                                        }


                                    }

                                    if (foundGeneral)
                                    {
                                        if (boldItalicVariable != " ")
                                        {
                                            boldItalicVariable = boldItalicVariable.TrimEnd();
                                            boldItalicVariable = boldItalicVariable.TrimStart();

                                            if (boldItalicVariable.Contains('\r'))
                                            {
                                                string[] newBoldItalic = boldItalicVariable.Split('\r');
                                                for (int indexOfNewBI = 0; indexOfNewBI < newBoldItalic.Length; indexOfNewBI++)
                                                {
                                                    BoldItalicContentList.Add(newBoldItalic[indexOfNewBI]);
                                                }
                                            }
                                            else
                                            {
                                                BoldItalicContentList.Add(boldItalicVariable);
                                            }
                                        }

                                    }


                                    //  allVariablesList.Add(boldItalicVariable);


                                    boldItalicVariable = " ";

                                }


                            }
                            foundBOLdItalic = true;

                        }
                    //}


                    // here code for ASCB parameters in to a textFile



                }
                #endregion 


            }
            toolStripStatus.Text = "Document Reading Finished....";
            if (IOCheck)
            {
                toolStripStatus.Text = "Checking Input Output Parametrs Mismatch.....";
                sw_ErrorReport.WriteLine("Input Output Missing Parameters Error Report: ");

                str = File.ReadAllText(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\Temp_str.txt");

                int indexTagStart, indexTagEnd;

                string tag = PTag;

                indexTagStart = str.IndexOf(tag);
                indexTagEnd = str.LastIndexOf(tag);

                string strParsedTagContent = str.Substring(indexTagStart, indexTagEnd - indexTagStart);

                int indexEnd = 0;
                int indexStart = 0;
                if (strParsedTagContent.Contains("ASCB Data"))
                {
                    indexStart = strParsedTagContent.IndexOf("ASCB Data");

                }
                else if (strParsedTagContent.Contains("Internal Data"))
                {
                    indexStart = strParsedTagContent.IndexOf("Internal Data");

                }

                if (!(strParsedTagContent.Contains("General")))
                {
                    if (strParsedTagContent.Contains("Output"))
                    {

                        indexEnd = strParsedTagContent.IndexOf("Output");
                    }
                    else
                    {
                        indexEnd = strParsedTagContent.IndexOf("shall ");

                    }
                }
                else {

                    indexEnd = strParsedTagContent.IndexOf("General");
                
                }
                //else if (strParsedTagContent.Contains("Output"))
                //{

                //    indexEnd = strParsedTagContent.IndexOf("Output");
                //}
               
                    string strASCBData = strParsedTagContent.Substring(indexStart + 9, indexEnd - indexStart - 9);
                    strASCBData = strASCBData.Replace("\r", "").Replace("*", "").Replace("Internal Data", "");
                    string[] arrSplit = { "\n" };
                    string[] arrASCBData = strASCBData.Split(arrSplit, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < arrASCBData.Length; i++)
                    {
                        arrASCBData[i] = arrASCBData[i].Trim();
                    }
                    indexStart = indexEnd;
                    indexEnd = strParsedTagContent.Length;
                    string strTagContent = strParsedTagContent.Substring(indexStart, indexEnd - indexStart);
                    string[] arrTagContent = strTagContent.Split(' ');
                    string[] arrTagContentVariables = ExtractVariables(strTagContent);


                    //GenerateReport(arrASCBData, arrTagContentVariables);

                    //for output variables
                    string strAllTags = File.ReadAllText(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\Report.txt");
                    int outStartIndex, outEndIndex;
                    strAllTags = strAllTags.Replace("\r", "").Replace("*", "");
                    string nextTag = string.Empty;
                    string[] arrTagSplit = { "\n" };
                    string[] arrAllTags = strAllTags.Split(arrTagSplit, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < arrAllTags.Length - 1; i++)
                    {
                        arrAllTags[i] = arrAllTags[i].Trim();
                        if (arrAllTags[i].Equals(tag))
                        {
                            nextTag = arrAllTags[i + 1].ToString();
                            break;
                        }
                    }


                    if (!string.IsNullOrEmpty(nextTag))
                    {
                        outEndIndex = str.IndexOf(nextTag);
                        outStartIndex = str.LastIndexOf(tag);
                        string strSubOutput = str.Substring(outStartIndex, outEndIndex - outStartIndex);
                        int indexOutEnd = 0;
                        int indexOutStart = 0;

                        if (strSubOutput.Contains("ASCB Data"))
                        {
                            indexOutStart = strSubOutput.IndexOf("ASCB Data");

                        }
                        else if (strSubOutput.Contains("Internal Data"))
                        {
                            indexOutStart = strSubOutput.IndexOf("Internal Data");

                        }


                        // indexOutStart = strSubOutput.IndexOf("ASCB Data");

                        if (strSubOutput.Contains("Output"))
                        {

                            if (!(strSubOutput.Contains("#")))
                            {
                                indexOutEnd = strSubOutput.LastIndexOf("[");

                            }
                            else
                            {
                                indexOutEnd = strSubOutput.LastIndexOf("#");
                            }
                            string strASCBOutData = strSubOutput.Substring(indexOutStart + 9, indexOutEnd - indexOutStart - 9);
                            strASCBOutData = strASCBOutData.Replace("\r", "").Replace("*", "");
                            string[] arrOutSplit = { "\n" };
                            string[] arrASCBOutData = strSubOutput.Split(arrOutSplit, StringSplitOptions.RemoveEmptyEntries);
                            string[] arrOutVariables = new string[100];
                            for (int i = 0, j = arrASCBData.Length; i < arrASCBOutData.Length; i++)
                            {
                                arrASCBOutData[i] = arrASCBOutData[i].Trim();
                                if (arrASCBOutData[i].EndsWith("#"))
                                {
                                    Array.Resize(ref arrASCBData, arrASCBData.Length + 1);
                                    arrASCBOutData[i] = arrASCBOutData[i].Replace("\r", "").Replace("*", "");
                                    arrASCBData[j] = arrASCBOutData[i].Trim();
                                    j++;
                                }
                            }
                        }
                    }
                    GenerateReport(arrASCBData, arrTagContentVariables);
                
                }

          
            #region Bold and Italics Check

            if (BoldItalicCheck)
            {

                toolStripStatus.Text = "Checking BOld and Italics Mismatch.......";
                   boldItalicsCheck();


            }
            #endregion




            sw_Ascb.Close();
            string[] ASCBPram_list = File.ReadAllLines(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\ASCBParameters.txt");


            for (int indexOf_ascbParamList = 0; indexOf_ascbParamList < ASCBPram_list.Length; indexOf_ascbParamList++)
            {

                if (!(ASCBPram_list[indexOf_ascbParamList].Contains("ASCB Data") || ASCBPram_list[indexOf_ascbParamList].Contains("Internal Data")))
                {

                    if (ASCBPram_list[indexOf_ascbParamList].EndsWith("#"))
                    {
                        ASCBPram_list[indexOf_ascbParamList] = ASCBPram_list[indexOf_ascbParamList].Remove(ASCBPram_list[indexOf_ascbParamList].Length - 1);

                    }
                    if (!(ASCBPram_list[indexOf_ascbParamList] == ""))
                    {
                        list_OfAscbPrams.Add(ASCBPram_list[indexOf_ascbParamList]);
                    }

                }

            }


            if (list_OfAscbPrams.Count >= 1)
            {
                list_OfAscbPrams.RemoveAt(list_OfAscbPrams.Count - 1); // this is the final list of Ascb Parameters from selected P-tag
            }

            #region Check ASCB and HEAL parameters Mismatch
            if (healcheck)
            {
                if (InvokeRequired)
                {
                    Invoke(new UpdateDelegate(
                    delegate
                    {

                        toolStripStatus.Text = "Checking ASCB Parameters with HEAL DataBase Mismatch.....";
                        
                    })
                    );
                }



               
                 HealAscbMismatchCheck();

            }
            #endregion



          


            write_to_log_file("The selected P-Tag content File is saved ");
            sw_ptag.Close(); // here Add delete code for p_tag file

            sw_Ascb.Close(); // here add delete file code for ASCB parametrs
            

            p_tagContent = File.ReadAllText(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\ptag_content.txt");
            p_tagContent_split = File.ReadAllLines(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\ptag_content.txt");
            

            ValidatingPTags(p_tagContent);


            string shall_tagList = File.ReadAllText(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\Report1.txt");
            string[] shall_tagList_Split = File.ReadAllLines(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\Report1.txt");
            
            foreach (string shall_tag in shall_tagList_Split)
            {
                if (shall_tag.Contains(p_tag))
                {

                    shall_tags_Of_selected_Ptag.Add(shall_tag);

                }
            }

            #region Check Conditional Errors
            if (conditionalCheck)
            {

                if (InvokeRequired)
                {
                    Invoke(new UpdateDelegate(
                    delegate
                    {

                        toolStripStatus.Text = "Checking For Conditional Errors.....";

                    })
                    );
                }


                
                conditionalsCheck(shallTag_Content);

            }
            toolStripStatus.Text = "Completed checking all Errors, Please check the Error Report.....";
            object saveChanges = false;
            object originalFormat = Missing.Value;
            object routeDocument = Missing.Value;
            #endregion
        }
        

     #region Generate Error Report Button

        public void generatebtn_Click(object sender, EventArgs e)
           
            {
                healcheck = HealCheckBox.Checked;
                IOCheck = IOCheckBox.Checked;
                conditionalCheck = conditionalCB.Checked;
                BoldItalicCheck = BoldItalicCheckBox.Checked;
               //ascbBrowse.Enabled = false;
               // healDbtb.Enabled = false;



                if (healcheck && healDbtb.Text == "")
                {
                    groupBox3.Enabled = true;
                
                }


                filePath = filepathtb.Text.ToString();

                if (filepathtb.Text != "")
                {
                    if (!(HealCheckBox.Checked == true && healDbtb.Text == ""))
                    {

                        groupBox3.Enabled = true;
                        if ((healcheck || conditionalCheck) || IOCheck || BoldItalicCheck)
                        {

                            try
                            {

                                if (!(ptag_cb.Text == "Select" || ptag_cb.Text == ""))
                                {

                                    PTag = ptag_cb.SelectedItem.ToString();

                                    write_to_log_file("Entering into thread....");

                                    groupBox1.Enabled = false;
                                    groupBox2.Enabled = false;
                                   groupBox3.Enabled = false;
                                    groupBox4.Enabled = false;
                                    ascbBrowse.Enabled = false;
                                    healDbtb.Enabled = false;
                                    statusStrip1.Enabled = true;
                                    a = new Thread(new ThreadStart(CheckForErrors));
                                    a.IsBackground = true;
                                    a.Start();

                                }
                                else
                                {
                                    MessageBox.Show("Please select a P-Tag to check Non-Technical Errors");
                                }
                            }
                            catch
                            {
                                MessageBox.Show("Please Select the correct P-Tag");
                            }

                        }

                        else
                        {

                            MessageBox.Show("Please check any of the check boxes to Find Errors", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else 
                    
                    {
                        MessageBox.Show("Please import an Heal DataBase file", "Browse an Heal Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        
                    }
        }
        else
                
                {
                    MessageBox.Show("Please Select a Requirements Document First","Error ",  MessageBoxButtons.OK,MessageBoxIcon.Error);
    
            }
                toolStripStatus.Text = "Everything is done...!!";

            }
        #endregion

        private void conditionalsCheck(string shallTag_Content)
        {

            toolStripStatus.Text = "Started checking Conditional Errors.....";
            sw = File.AppendText(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\comments_from_SSRD.txt");
            sw.WriteLine("In the P-Tag You Selected " + p_tag);
            sw.WriteLine("-------------------------------------");



            LogicforConditionalCheck(p_tagContent_split);
            

            bool ifElseMistake = false;

            int i = ListWords.Count;
            for (int j = 0; j < i - 1; j++)
            {
                if (!IsValidFollower(ListWords[j], ListWords[j + 1]))
                {
                    valid = false;
                    ifElseMistake = true;
                    sw.WriteLine("there is some mistake in " + ListWords[j] + " and " + ListWords[j + 1]);
                    //  sw.WriteLine();
                    // MessageBox.Show("Some Mistake found");
                    break;

                }


                // sw.WriteLine("everything is working fine");
                // MessageBox.Show("am here" );
            }
            if (ifElseMistake == true)
            {

                sw.WriteLine("there is mistake found in " + shall_tags_Of_selected_Ptag[indexOf_shallTag_count]);
            }



            if (CountIf != CounteIF + CountEIFWithOutSpace)
            {
                sw.WriteLine("There is a mistake in number of IF END IF Statements under " + shall_tags_Of_selected_Ptag[indexOf_shallTag_count]);
             
            }
            if (CounteCase != CountCase)
            {
                sw.WriteLine("There is mistake found in Number of Case - End Case under " + shall_tags_Of_selected_Ptag[indexOf_shallTag_count]);
              
            }


            if (valid == true)
            {
                sw.WriteLine("No Mistakes Found in Sequence of Conditions");
            }
            if (mismatchFound == false)
            {
                sw.WriteLine("No mismatch Found in Braces");

            }
            if (mismatchFound == true)
            {
                sw.WriteLine("----------------------");
            }

            toolStripStatus.Text = "Error Report Generated for Conditioanl Errors.";
            write_to_log_file();
            write_to_log_file("Generated the Braces mismatch and Conditional Statements Error report ");
           // MessageBox.Show("You can Find the error report in \\comments_from_SSRD File", MessageBoxIcon.Information.ToString());
            sw_ptag.Close();
            sw.Close();

        }

        private void LogicforConditionalCheck(string[] p_tagContent_split)
        {
            toolStripStatus.Text = "Entered into the Logic of ConditonalCheck.......";
            foreach (string s in p_tagContent_split)
            {
                if (s.Contains(p_tag))
                {
                    isCorrect = false;

                    if (!(foundFirstShallTag))
                    {
                        foundFirstShallTag = true;
                    }


                }
                if (isCorrect == false && foundFirstShallTag == true)
                {

                    shallTag_Content += Environment.NewLine + s;


                }
                if (isCorrect == false & s.Contains(p_tag) || s.Contains("Output"))
                {
                    flag2 = true;

                }

                if (flag2 == true)
                {
                    // MessageBox.Show(shallTag_Content);
                    if (shallTag_Content.Contains("[") || shallTag_Content.Contains("{") || shallTag_Content.Contains("(") || shallTag_Content.Contains("}") || shallTag_Content.Contains("]") || shallTag_Content.Contains(")"))
                    {
                        shall_tag_count++;
                        char[] brackets = shallTag_Content.ToCharArray();
                        Stack bracketStack = new Stack();
                        foreach (char ch in brackets)
                        {
                            if (ch == '}' || ch == ']' || ch == ')' || ch == '{' || ch == '[' || ch == '(')
                            {
                                if (bracketStack.Count == 0)
                                {
                                    if (leftBrackets.Contains(ch))
                                    {
                                        bracketStack.Push(ch);
                                    }
                                    else
                                    {
                                        bracketBool = false;
                                        write_to_log_file("Mismatch found in character : " + ch);
                                        sw.WriteLine("Mismatch found in character : " + ch);

                                    }


                                }
                                else
                                {
                                    if (leftBrackets.Contains(ch))
                                    {
                                        bracketStack.Push(ch);

                                    }
                                    else if (rightBrackets.Contains(ch))
                                    {
                                        char charect;
                                        object topStack = bracketStack.Pop();
                                        charect = (char)topStack;
                                        if (charect != bracketPairs[ch])
                                        {
                                            bracketBool = false;
                                            write_to_log_file("Mismatch in : " + ch);
                                            sw.WriteLine("Mismatch in : " + ch);
                                        }

                                    }
                                    else
                                    {
                                        bracketBool = false;  // had invalid charecter
                                        write_to_log_file("Invalid character found");
                                        sw.WriteLine("Invalid character found");

                                    }

                                }

                            }

                        }

                        if (bracketStack.Count != 0)
                        {
                            // MessageBox.Show("error found");
                            bracketBool = false;
                            write_to_log_file("Number of characters which doesn't have its pair is :" +bracketStack.Count);
                            sw.WriteLine("Number of characters which doesn't have its pair is :" + bracketStack.Count);
                        }

                        if (bracketBool == false)
                        {
                            // MessageBox.Show("mismatch in braces");
                            if (shall_tag_count == 0 || shall_tag_count == 1)
                            {
                                continue;
                            }
                            else if (shall_tag_count >= 2)
                            {

                                indexOf_shallTag_count = shall_tag_count - 2;
                                if (indexOf_shallTag_count == shall_tags_Of_selected_Ptag.Count)
                                {

                                    indexOf_shallTag_count = indexOf_shallTag_count - 1;
                                }
                                sw.WriteLine("The Above Mismatches in braces found in " + shall_tags_Of_selected_Ptag[indexOf_shallTag_count]);
                                sw.WriteLine("_______________________________________________");
                                sw.WriteLine(Environment.NewLine);
                               // sw.WriteLine("Please check the Log_file for more details about Mismatch Of Braces");
                                mismatchFound = true;

                            }
                        }
                        
                    }


                    // you can insert If and END IF code here

                    string[] split_shall_tagContent = shallTag_Content.Split('\n');

                    foreach (string sLine1 in split_shall_tagContent)
                    {


                        if (sLine1.Contains("IF") || sLine1.Contains("THEN") || sLine1.Contains("ELSE") || sLine1.Contains("END IF") || sLine1.Contains("ENDIF") || sLine1.Contains("CASE") || sLine1.Contains("END CASE") || sLine1.Contains("ELSE IF") || sLine1.Contains("ELSEIF"))
                        {


                            if (sLine1.Contains("END IF"))
                            {
                                CounteIF++;
                                ListWords.Add(Vals.ENDIF);
                            }
                            else if (sLine1.Contains("ENDIF"))
                            {
                                CountEIFWithOutSpace++;
                                ListWords.Add(Vals.END_IF);

                            }

                            else if (sLine1.Contains("ELSE IF"))
                            {
                                CountElseIf++;
                                ListWords.Add(Vals.ELSE_IF);

                            }
                            else if (sLine1.Contains("ELSEIF"))
                            {
                                CountElseIf++;
                                ListWords.Add(Vals.ELSEIF);

                            }
                            else if (sLine1.Contains("IF"))
                            {
                                CountIf++;

                                ListWords.Add(Vals.IF);
                            }

                            else if (sLine1.Contains("ELSE"))
                            {
                                Countels++;
                                ListWords.Add(Vals.ELSE);
                            }


                            else if (sLine1.Contains("THEN"))
                            {
                                CountThen++;
                                ListWords.Add(Vals.THEN);
                            }
                            else if (sLine1.Contains("END CASE"))
                            {
                                CounteCase++;
                                ListWords.Add(Vals.ENDCASE);
                            }

                            else if (sLine1.Contains("CASE"))
                            {
                                CountCase++;
                                ListWords.Add(Vals.CASE);
                            }




                            else if (sLine1.Contains("AND ELSE"))
                            {

                                ListWords.Add(Vals.AND_ELSE);
                            }



                        }

                    }



                    shallTag_Content = "";
                }

                flag2 = false;
                bracketBool = true;

            }
        
        }

        private void boldItalicsCheck()
        {
            write_to_log_file();
            bool NfoundboldItalicMismatch = true;

            StreamWriter sw_BoldItalic = null;
            sw_BoldItalic = File.AppendText(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\BoldItalicMismatch.txt");
            sw_BoldItalic.WriteLine("In the P-Tag you selected " + p_tag);
            
            List<string> MismatchBoldItalic = BoldItalicContentList.Except(BoldItalicIOList).ToList();
            //  List<string> latestMismatchBoldItalic = MismatchBoldItalic.Except(BoldItalicIOList).ToList();

            if (!(MismatchBoldItalic.Count == 0))
            {
                sw_BoldItalic.WriteLine("The following are the list of Mismatched variables which are (Bold and italic)in IO content but not in the requirements : ");
                sw_BoldItalic.WriteLine(Environment.NewLine);
            
            }

            for (int IndexOfVariableList = 0; IndexOfVariableList < MismatchBoldItalic.Count; IndexOfVariableList++)
            {
                if (!(MismatchBoldItalic[IndexOfVariableList] == " "))
                {
                    NfoundboldItalicMismatch = false;
                    sw_BoldItalic.WriteLine(MismatchBoldItalic[IndexOfVariableList]);
                }
            }
            write_to_log_file("Generated Bold and Italic Mismatch Report....");
            if (NfoundboldItalicMismatch == true)
                sw_BoldItalic.WriteLine("No mismatch Found in Bold and Italics across the selected P-Tag");
            sw_BoldItalic.Close();

            toolStripStatus.Text = "Error Report Of Bold and Italics Mismatch Generated....";
          //  MessageBox.Show("You can find Error report of Bold and Italics Mismatch in \\BoldItalicMismatch.txt File");
        
        }


        private void HealAscbMismatchCheck()
        {

            StreamWriter compareHeal = null;
            compareHeal = File.AppendText(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\Mismatch_Heal_ASCB.txt");

            compareHeal.WriteLine("In the P-Tag you have selected : " + p_tag);

            List<string> resultList = list_OfAscbPrams.Except(HealParamWithOutUnderscore).ToList();
            for (int indexOfResultList = 0; indexOfResultList < resultList.Count; indexOfResultList++)
            {

                compareHeal.WriteLine(resultList[indexOfResultList]);

            }
        
            write_to_log_file();
            write_to_log_file("Generated the Report of checking with HealDataBase...");
            write_to_log_file();
            compareHeal.Close();
            toolStripStatus.Text = "Error Report Of Checking With HealDataBase Generated........";
           // MessageBox.Show("You can find the Mismatch List in //Mismatch_Heal_ASCB File");
           // MessageBox.Show("You can find Final Errors Report in //FinalReport.txt File");
        
        }


        private void ValidatingPTags(string ptagInfo)
        {
            toolStripStatus.Text = "Validating P-Tags.....";
            string tempstr = "hai";
            string[] strsplit = ptagInfo.Split('\n');
            List<string> ptagslist = new List<string>();

            write_to_log_file();
            write_to_log_file("Validating Shall Tags in the selected P-Tag");
     
            foreach (string ss in strsplit)
            {

                if (ss.Contains("[DDD") || ss.Contains("[SSRD") || ss.Contains("[SRD"))
                {
                       tempstr = ss.Trim(' ', '\r', '\n', '\t');
                                                                   
                       tempstr = ss.Substring(ss.IndexOf("["), ss.LastIndexOf("]") - ss.IndexOf("[") +1);
                        ptagslist.Add(tempstr);
                    
                }
            
            }

            foreach (string toValidate in ptagslist)
            {
                string withOutSquareBraces = null;
                if (toValidate.StartsWith("[ ") || toValidate.EndsWith(" ]"))    
                {
                    write_to_log_file("The Below Tag contains space after or before the square Bracket Which is not a valid format, Please update it");
                    write_to_log_file(toValidate);
                    write_to_log_file();

                }
                else
                {
                    withOutSquareBraces = toValidate.Trim('[', ']');
                }

                if (!(withOutSquareBraces == null))
                {
                    Match match = Regex.Match(withOutSquareBraces, @"^ *(\w+ ?)|(\w+// )+ *$");
                    //"([a-zA-Z]+([0-9]+)*)(\s([0-9|a-zA-Z]+([0-9]+)*))*$"
                    if (!(match.Success))
                    {
                        write_to_log_file();
                        
                        write_to_log_file("This Tag found invalid, Contains more than one space or any special charater in between the words : ");
                        write_to_log_file(withOutSquareBraces);
                    }
                }
            }
                   
        } 


        #region Events
        private void Form1_Load_1(object sender, EventArgs e)
        {
            ToolTip TP = new ToolTip();
            TP.ShowAlways = true;
            TP.SetToolTip(healDbtb, "Please input your HEAL Data (A Text File)");
            TP.SetToolTip(filepathtb, "Please browse your Requirements Document");
            TP.SetToolTip(ptag_cb, "Here is The List Of P-Tags ");
            Log_File = new StreamWriter(LogFile_Path);
            Log_File.WriteLine("Executed on System : " + System.Environment.MachineName.ToString());
            Log_File.WriteLine("Executed at : " + DateTime.Now);
            Log_File.WriteLine("");
            Log_File.WriteLine("");
            Log_File.Close();

        }

        private void ascbBrowse_MouseEnter(object sender, EventArgs e)
        {
           

            statusStrip1.Visible = true;
           // statusStrip1.Enabled = true;
            if (ascbBrowse.Enabled == true)
            {
                toolStripStatus.Visible = true;
                toolStripStatus.Text = "Select Heal DataBase (Text File)";

            }
        }

        private void ascbBrowse_MouseLeave_1(object sender, EventArgs e)
        { //  statusStrip1.Enabled = false;

            if (ascbBrowse.Enabled == true)
            {
                toolStripStatus.Visible = false;
                toolStripStatus.Text = "";
            
            }
        }

        private void brwsbtn_MouseEnter(object sender, EventArgs e)
        {
           
            statusStrip1.Visible = true;
            statusStrip1.Enabled = true;
            if (brwsbtn.Enabled == true)
            {
                toolStripStatus.Visible = true;
                toolStripStatus.Text = "Select a Requirements Document";
            
            }
        }

        private void brwsbtn_MouseLeave_1(object sender, EventArgs e)
        {
            if (brwsbtn.Enabled == true)
            {
                toolStripStatus.Visible = false;
                toolStripStatus.Text = "";

            }
        }

        private void generatebtn_MouseEnter_1(object sender, EventArgs e)
        {
          
            statusStrip1.Visible = true;
            statusStrip1.Enabled = true;
            if (generatebtn.Enabled == true)
            {
                toolStripStatus.Visible = true;
                toolStripStatus.Text = "Click to Generate Error Report";

            }
        }

        private void generatebtn_MouseLeave_1(object sender, EventArgs e)
        {
            if (generatebtn.Enabled == true)
            {
                toolStripStatus.Visible = false;
                toolStripStatus.Text = "";

            }
        }

        private void ptag_cb_MouseEnter_1(object sender, EventArgs e)
        {

          //  if (ptag_cb.Text != "" || ptag_cb.Text == "Select")
           // {

                statusStrip1.Visible = true;
                toolStripStatus.Visible = true;
                toolStripStatus.Text = "Select a P-Tag";
           // }
        }

        private void ptag_cb_MouseLeave(object sender, EventArgs e)
        {
            toolStripStatus.Visible = false;
            toolStripStatus.Text = "";
        }

        #endregion 

        #region Form Closing Event
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
           /* if (File.Exists(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\ptag_content.txt"))
            {
                File.Delete(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\ptag_content.txt");
                write_to_log_file();
                write_to_log_file("Deleted the file ptag_content");
            } */

            if (File.Exists(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\Report.txt"))
            {
                File.Delete(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\Report.txt");
                write_to_log_file("Deleted the file Report");
            }

            if (File.Exists(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\Report1.txt"))
            {
                File.Delete(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\Report1.txt");
                write_to_log_file("Deleted the file Report1");
            }

            if (File.Exists(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\ASCBParameters.txt"))
            {
                File.Delete(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\ASCBParameters.txt");
                write_to_log_file("Deleted the file ASCBParameters");
            }


            if (File.Exists(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\healDBParams.txt"))
            {
                File.Delete(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\healDBParams.txt");
                write_to_log_file("Deleted the file healDBParams");
            }

            if (File.Exists(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\Temp_str.txt"))
            {
                File.Delete(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\Temp_str.txt");
                write_to_log_file("Deleted the file Temp_str");
            }

            write_to_log_file();
            write_to_log_file("Application Closed.....");

        }
        #endregion

        #region Extract Variable Method 
        private string[] ExtractVariables(string str)
        {

            write_to_log_file("Extracting Variable From Selected P-Tag");
            List<string> lst = new List<string>();
            List<string> lstResult = new List<string>();
            str.Replace("[", " ").Replace("]", " ").Replace("\r", " ").Replace("\n", " ").Replace("\t", " ").Replace("(", " ").Replace(")", " ").Replace("*", " ").Replace(".", "");
            string[] arr = Regex.Split(str, " "); //@"\W+"
            foreach (var item in arr)
            {
                foreach (var word in Regex.Split(item, "\n"))
                {
                    lst.Add(word);
                }
            }

            Regex pattern = new Regex(@"[a-zA-Z0-9.<>#-]+_[a-zA-Z0-9#-]+"); //[a-zA-Z0-9]+_[a-zA-Z0-9_]+\b  "\w+_\w"
            //Regex pattern = new Regex(@"([A-Za-z]*<.>[A-Za-z]*\.[A-Za-z]*)|([A-Za-z]*<.>[A-Za-z]*#)|([A-Za-z]*_[A-Za-z]*#)|([A-Za-z0-9]*#)");
            int index;
            string trimmedItem;
            string dotTrimmedItem;
            foreach (var item in lst)
            {
                trimmedItem = item.Replace("[", "").Replace("]", "").Replace(" ", "").Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace("(", "").Replace(")", "").Replace("*", "");
                if (pattern.IsMatch(trimmedItem))
                {
                    index = trimmedItem.Length;
                    if (trimmedItem[index - 1].Equals('.'))
                    {
                        dotTrimmedItem = trimmedItem.Replace(".", "");
                        lstResult.Add(dotTrimmedItem);
                    }
                    else if (trimmedItem[index - 1].Equals(','))
                    {
                        dotTrimmedItem = trimmedItem.Replace(",", "");
                        lstResult.Add(dotTrimmedItem);
                    }
                    else
                    {
                        lstResult.Add(trimmedItem);
                    }
                }
            }
            lstResult = lstResult.Select(x => x).Distinct().ToList();

            return lstResult.ToArray();
        }
        #endregion

        #region GenerateReport Of IOVarable Mismatch Method
        private void GenerateReport(string[] arrIndex, string[] arrVars)
        {
            write_to_log_file("Generating Report for IO Variable Mismatch....");
            List<string> lstNewVariables = new List<string>();
            List<string> NewVariables = new List<string>();
            foreach (var variable in arrVars)
            {
                if (arrIndex.FirstOrDefault(i => i.Equals(variable)) == null)
                {
                    lstNewVariables.Add(variable);
                }
            }
            foreach (var variable in arrIndex)
            {
                if (arrVars.FirstOrDefault(i => i.Equals(variable)) == null)
                {
                    NewVariables.Add(variable);
                }
            }

            string path = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\IOParametersMismatch.txt";
            StreamWriter sw = null;
            sw = File.AppendText(path);
            sw.Write("Generated on " + DateTime.Now + System.Environment.NewLine + "ASCB Input Data:-" + System.Environment.NewLine);
            foreach (var item in arrIndex)
            {
                sw.Write(item + System.Environment.NewLine);
            }

            sw.Write(System.Environment.NewLine + "Used Variables in Shall TAGS:-" + System.Environment.NewLine);
            foreach (var item in arrVars)
            {
                sw.Write(item + System.Environment.NewLine);
            }
            sw.Write(System.Environment.NewLine + "ASCB to Used Variables comparison:-" + System.Environment.NewLine); // the word which is excess in 1st array ll b printed
            foreach (var item in NewVariables)
            {
                sw.Write(item + System.Environment.NewLine);
            }

            sw.Write(System.Environment.NewLine + "Used Variables to ASCB comparison:-" + System.Environment.NewLine); // the word which is excess in 2nd array ll b printed
            foreach (var item in lstNewVariables)
            {
                sw.Write(item + System.Environment.NewLine);
            }

            sw.Close();
        }
        #endregion

        private void saveLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Log files (*.log)|*.log";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.FileName = "Error_Detection_Tool_Log" + DateTime.Now.ToString("dd-MMM-yyyy") + ".log";

            if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                if (File.Exists(saveFileDialog1.FileName))
                {
                    File.Delete(saveFileDialog1.FileName);
                }
                File.Copy(LogFile_Path, saveFileDialog1.FileName);
            }
            else

            {
                return;
            }
        }

        public void write_to_log_file(string message)
        {
            Log_File = File.AppendText(LogFile_Path);
            Log_File.WriteLine(DateTime.Now + ": " + message);
            Log_File.Close();
        }

        public void write_to_log_file()
        {
            Log_File = File.AppendText(LogFile_Path);
            Log_File.WriteLine("");
            Log_File.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About_Error_Detection_Tool abt_Err = new About_Error_Detection_Tool();
            abt_Err.ShowDialog();


        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void CRRAAnalysisReportGeneratorHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\Autodetection_of_Error_in_SSRD_help.pdf"); 


        }

        private void groupBox3_EnabledChanged(object sender, EventArgs e)
        {
            if (HealCheckBox.Checked == true)
            {
                groupBox3.Enabled = true;
            }
            else
                groupBox3.Enabled = false;

        }

    }
}
#endregion
