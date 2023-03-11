using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using CRUD_Operations;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data.SqlClient;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;
using DataGrid = System.Windows.Controls.DataGrid;
using Paragraph = iTextSharp.text.Paragraph;
using Microsoft.Win32;
using SaveFileDialog = System.Windows.Forms.SaveFileDialog;
using UserControl = System.Windows.Controls.UserControl;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;

namespace FYPManagementSystem.UserControlls.PdfUserControlls
{

    public partial class PdfUC : UserControl
    {
        public PdfUC()
        {
            InitializeComponent();
        }



        //This function is used to generate a title page for a PDF document using iTextSharp library. it adds various elements to it such as the document title, an image, author and submission details, and department and university information. The function sets different fonts and alignments for each element using iTextSharp's.

        private void TitlePage(ref Document document)
        {
            document.Add(new Paragraph("\n\n\n"));

            document.AddTitle("Final Year Project Management System");

            iTextSharp.text.Font font = FontFactory.GetFont(iTextSharp.text.Font.FontFamily.TIMES_ROMAN.ToString(), 20, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);
            iTextSharp.text.Font font1 = FontFactory.GetFont(iTextSharp.text.Font.FontFamily.TIMES_ROMAN.ToString(), 18, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);
            iTextSharp.text.Font font112 = FontFactory.GetFont(iTextSharp.text.Font.FontFamily.TIMES_ROMAN.ToString(), 16, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);

            iTextSharp.text.Paragraph elements = new iTextSharp.text.Paragraph("FYP MANAGEMENT SYSTEM\n", font);
            elements.Alignment = Element.ALIGN_CENTER;
            document.Add(elements);


            document.Add(new Paragraph("\n\n\n"));

            System.Drawing.Image image = Properties.Resources.uet_logo;
            iTextSharp.text.Image image1 = iTextSharp.text.Image.GetInstance(image, System.Drawing.Imaging.ImageFormat.Png);
            image1.Alignment = Element.ALIGN_CENTER;
            image1.ScaleAbsolute(120f, 120f);
            document.Add(image1);


            Paragraph sessionPara = new Paragraph("Session 2021-2025");
            sessionPara.Alignment = Element.ALIGN_CENTER;
            document.Add(sessionPara);

            document.Add(new Paragraph("\n\n"));

            Paragraph elements11 = new Paragraph("Submitted by:\n\n", font1);
            elements11.Alignment = Element.ALIGN_CENTER;
            document.Add(elements11);



            Paragraph namePara = new Paragraph("Bisma Muhammad Ali        2021-CS-170\n", font112);
            namePara.Alignment = Element.ALIGN_CENTER;
            document.Add(namePara);

            document.Add(new Paragraph("\n\n"));

            Paragraph submittedPara = new Paragraph("Submitted to:\n\n", font1);
            submittedPara.Alignment = Element.ALIGN_CENTER;
            document.Add(submittedPara);

            Paragraph sirPara = new Paragraph("Sir Samyan Qayyum Wahla\n", font112);
            sirPara.Alignment = Element.ALIGN_CENTER;
            document.Add(sirPara);

            document.Add(new Paragraph("\n\n"));
            document.Add(new Paragraph("\n\n"));

            Paragraph csPara = new Paragraph("Department of Computer Science\n", font112);
            csPara.Alignment = Element.ALIGN_CENTER;
            document.Add(csPara);

            iTextSharp.text.Paragraph uetPara = new iTextSharp.text.Paragraph("University of Engineering and Technology\r\nLahore Pakistan\n", font);
            uetPara.Alignment = Element.ALIGN_CENTER;
            document.Add(uetPara);


        }

        //it creates a report section in a PDF document using the iTextSharp library. This function take parameter document, page title and query. It then sets up fonts and formatting for the document, executes the SQL query, creates a table with the query results, and adds it to the document.
        private void CreateReportSection(ref Document document, string PageTitle, string query)
        {
            Font boldFont = new Font(Font.FontFamily.TIMES_ROMAN, 16, Font.BOLD);
            Font textFont = new Font(Font.FontFamily.TIMES_ROMAN, 12);
            try
            {
                document.NewPage();
                Paragraph title = new Paragraph(PageTitle, boldFont);
                title.SpacingBefore = 20f;
                title.SpacingAfter = 20f;
                title.Font.Size = 20;
                title.Alignment = Element.ALIGN_CENTER;
                document.Add(title);

                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();

                PdfPTable table = new PdfPTable(reader.FieldCount);
                table.WidthPercentage = 100;
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(reader.GetName(i)));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.BackgroundColor = new BaseColor(51, 153, 255);
                    table.AddCell(cell);
                }

                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(reader[i].ToString()));
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(cell);
                    }
                }
                reader.Close();
                document.Add(table);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //This function creates a PDF.This function take parameter document, page title and query.It opens a dialog box to save the report file, and if the file name is not empty, it creates a new PDF document using iTextSharp library.It then calls two other functions to generate a title page and a report section, respectively.
        private void CreateReport(string fileName, string pageTitle, string query)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PDF (.pdf)|.pdf";
            sfd.FileName = fileName;
            bool errorMessage = false;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(sfd.FileName))
                {
                    try
                    {
                        File.Delete(sfd.FileName);
                    }
                    catch (Exception ex)
                    {
                        errorMessage = true;
                        MessageBox.Show("Unable to save file  on disk" + ex.Message);
                    }
                }
                if (!errorMessage)
                {
                    // Create new PDF document
                    Document document = new Document();
                    PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(sfd.FileName, FileMode.Create));
                    document.Open();


                    TitlePage(ref document);

                    CreateReportSection(ref document, pageTitle, query);

                    // Close PDF document and writer
                    document.Close();
                    writer.Close();
                }
            }
        }

        //creates student report 
        private void StudentreportBtn_Click(object sender, RoutedEventArgs e)
        {
            string fileName = "Students.pdf";
            string studentQuery = "Select S.RegistrationNo AS [Registration No], (FirstName + ' ' + LastName) AS Name,L.Value AS Gender,(SELECT FORMAT(DateOfBirth, 'dd-MM-yyyy')) AS [Date OF Birth],Contact,Email from Person P JOIN Student S ON S.Id=P.Id JOIN Lookup L ON L.Id=P.Gender";
            string title = "Students Report";

            CreateReport(fileName, title, studentQuery);
        }

        //create advisor report
        private void AdvisorReportBtn_Click(object sender, RoutedEventArgs e)
        {
            string fileName = "Advisors.pdf";
            string advisorQuery = "Select (FirstName + ' ' + LastName) AS Name,LU1.Value AS Designation,A.Salary,LU.Value AS Gender,(SELECT FORMAT(DateOfBirth, 'dd/MM/yyyy')) AS [Date Of Birth],Contact,Email FROM Person P JOIN Advisor A ON A.Id=P.Id JOIN Lookup LU ON LU.Id=P.Gender JOIN Lookup LU1 ON LU1.Id=A.Designation";
            string title = "Advisors Report";

            CreateReport(fileName, title, advisorQuery);
        }


        //create advisor board report
        private void AdvBoardReportBtn_Click(object sender, RoutedEventArgs e)
        {
            string fileName = "AdvisoryBoard.pdf";
            string advBoardQuery = "SELECT MAX(P.Title) AS  Title, MAX(CASE WHEN PA.AdvisorRole = 11 THEN CONCAT(Person.FirstName,' ',Person.LastName) END) AS [Main Advisor], MAX(CASE WHEN PA.AdvisorRole = 12 THEN CONCAT(Person.FirstName,' ',Person.LastName) END) AS [Co-Advisor], MAX(CASE WHEN PA.AdvisorRole = 14 THEN CONCAT(Person.FirstName,' ',Person.LastName) END) AS [Industry Advisor] FROM  ProjectAdvisor PA INNER JOIN Advisor A ON PA.AdvisorId = A.Id JOIN Project P ON P.Id=PA.ProjectId JOIN Person ON Person.Id=A.Id GROUP BY PA.ProjectId";
            string title = "Advisory Board Report";

            CreateReport(fileName, title, advBoardQuery);
        }

        //create Evaluation report
        private void EvaluationReportBtn_Click(object sender, RoutedEventArgs e)
        {
            string fileName = "Evaluation.pdf";
            string evaQuery = "SELECT Name As Title, TotalMarks AS [Total Marks], TotalWeightage AS [Total Weightage] FROM Evaluation";
            string title = "Evaluations Report";

            CreateReport(fileName, title, evaQuery);
        }


        //This function is used to creates a report section for a specific group in a PDF document. It takes parameters such as the document , group ID, project title, Main advisor, coAdvisor, industry advisor, and query.
        private void CreateGroupReportSection(ref Document document, int groupId, string ProjectTitle, string mainAdv, string coAdv, string inAdv, string query)
        {
            Font boldFont = new Font(Font.FontFamily.TIMES_ROMAN, 16, Font.BOLD);
            Font textFont = new Font(Font.FontFamily.TIMES_ROMAN, 14);
            try
            {

                Paragraph title = new Paragraph("Group" + groupId, boldFont);
                title.SpacingBefore = 10f;
                title.SpacingAfter = 10f;
                title.Font.Size = 16;
                title.Alignment = Element.ALIGN_CENTER;
                document.Add(title);

                Paragraph PTitle = new Paragraph("Project: " + ProjectTitle, boldFont);
                PTitle.SpacingBefore = 10f;
                PTitle.SpacingAfter = 10f;
                PTitle.Font.Size = 14;
                PTitle.Alignment = Element.ALIGN_LEFT;
                document.Add(PTitle);

                Paragraph AdvisorPara = new Paragraph("Main Advisor: " + mainAdv + "       Co-Advisor: " + coAdv + "      Industry Advisor: " + inAdv, textFont);
                AdvisorPara.SpacingBefore = 10f;
                AdvisorPara.SpacingAfter = 10f;
                AdvisorPara.Font.Size = 12;
                AdvisorPara.Alignment = Element.ALIGN_LEFT;
                document.Add(AdvisorPara);

                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();

                PdfPTable table = new PdfPTable(reader.FieldCount);
                table.WidthPercentage = 100;
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(reader.GetName(i)));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.BackgroundColor = new BaseColor(51, 153, 255);
                    table.AddCell(cell);
                }

                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(reader[i].ToString()));
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(cell);
                    }
                }
                reader.Close();
                document.Add(table);
                document.NewPage();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //This method takes a file name as a parameter and saves the PDF document to that file.
        private void CreateGroupReport(string fileName)
        {
            Font boldFont = new Font(Font.FontFamily.TIMES_ROMAN, 16, Font.BOLD);
            Font textFont = new Font(Font.FontFamily.TIMES_ROMAN, 12);
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PDF (.pdf)|.pdf";
            sfd.FileName = fileName;
            bool errorMessage = false;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(sfd.FileName))
                {
                    try
                    {
                        File.Delete(sfd.FileName);


                    }
                    catch (Exception ex)
                    {
                        errorMessage = true;
                        MessageBox.Show("Unable to save file  on disk" + ex.Message);
                    }
                }
                if (!errorMessage)
                {
                    // Create new PDF document
                    Document document = new Document();
                    PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(sfd.FileName, FileMode.Create));
                    document.Open();


                    TitlePage(ref document);
                    document.NewPage();
                    Paragraph title = new Paragraph("All Groups", boldFont);
                    title.SpacingBefore = 20f;
                    title.SpacingAfter = 20f;
                    title.Font.Size = 20;
                    title.Alignment = Element.ALIGN_LEFT;
                    document.Add(title);


                    List<int> groups = new List<int>();
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("SELECT DISTINCT G.Id FROM [Group] AS G JOIN GroupStudent AS GS ON G.Id=GS.GroupId", con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        groups.Add(int.Parse(reader["Id"].ToString()));
                    }
                    reader.Close();

                    foreach (int group in groups)
                    {
                        string groupQuery = "SELECT CONCAT(P.FirstName ,' ',P.LastName) AS Name ,S.RegistrationNo AS RegNo ,L.Value AS Status,(CASE WHEN L.Value ='Active' THEN (SELECT FORMAT(AssignmentDate, 'dd/MM/yyyy')) END) AS [Active Date],(CASE WHEN L.Value<>'Active' THEN (SELECT FORMAT(AssignmentDate, 'dd/MM/yyyy')) END) AS [InActive Date] FROM GroupStudent AS GS JOIN Lookup AS L ON GS.Status = L.Id JOIN Student AS S ON S.Id = GS.StudentId JOIN Person AS P ON P.Id = S.Id WHERE GS.GroupId = " + group + " ORDER BY L.Value";

                        string project = "", mainAdv = "", coAdv = "", inAdv = "";

                        var con1 = Configuration.getInstance().getConnection();
                        SqlCommand cmd1 = new SqlCommand("SELECT MAX(P.Title) AS  Title, MAX(CASE WHEN PA.AdvisorRole = 11 THEN CONCAT(Person.FirstName,' ',Person.LastName) END) AS [Main Advisor], MAX(CASE WHEN PA.AdvisorRole = 12 THEN CONCAT(Person.FirstName,' ',Person.LastName) END) AS [Co-Advisor], MAX(CASE WHEN PA.AdvisorRole = 14 THEN CONCAT(Person.FirstName,' ',Person.LastName) END) AS [Industry Advisor] FROM  ProjectAdvisor PA JOIN Advisor A ON PA.AdvisorId = A.Id JOIN Project AS P ON P.Id=PA.ProjectId JOIN Person ON Person.Id=A.Id JOIN GroupProject AS GP ON GP.Projectid=P.Id WHERE GP.GroupId=" + group + " GROUP BY PA.ProjectId", con1);
                        SqlDataReader reader1 = cmd1.ExecuteReader();
                        if (reader1.Read())
                        {
                            mainAdv = reader1["Main Advisor"].ToString();
                            coAdv = reader1["Co-Advisor"].ToString();
                            inAdv = reader1["Industry Advisor"].ToString();
                            project = reader1["Title"].ToString();
                        }
                        reader1.Close();

                        CreateGroupReportSection(ref document, group, project, mainAdv, coAdv, inAdv, groupQuery);
                    }

                    // Close PDF document and writer
                    document.Close();
                    writer.Close();
                }
            }
        }


        private void stuGroupPdf_Click(object sender, RoutedEventArgs e)
        {
            string fileName = "Students Group.pdf";

            CreateGroupReport(fileName);
        }

        //This function creates a table to display the evaluation marks and calculates the total marks obtained by the student. The method then adds all this information to the PDF document and creates a new page for the next student's evaluation marks.
        private void CreateMarkEvaluationSection(ref Document document, int stuId)
        {
            Font boldFont = new Font(Font.FontFamily.TIMES_ROMAN, 16, Font.BOLD);
            Font textFont = new Font(Font.FontFamily.TIMES_ROMAN, 12);
            try
            {

                string stuName = "", stuReg = "", groupId = "", project = "", totalMarks = "";

                var con2 = Configuration.getInstance().getConnection();
                SqlCommand cmd2 = new SqlCommand("SELECT CONCAT(P.FirstName,' ',P.LastName) AS Name,S.RegistrationNo AS RegNo,GS.GroupId, PR.Title FROM Student AS S JOIN GroupStudent AS GS ON S.Id=GS.StudentId JOIN GroupProject AS GP ON GP.GroupId=GS.GroupId JOIN Project AS PR ON PR.Id=GP.ProjectId JOIN Person AS P ON P.Id=S.Id JOIN Lookup AS LU ON LU.Id=GS.Status WHERE LU.Value='Active' AND S.Id=" + stuId, con2);
                SqlDataReader reader2 = cmd2.ExecuteReader();
                if (reader2.Read())
                {
                    stuName = reader2["Name"].ToString();
                    stuReg = reader2["RegNo"].ToString();
                    groupId = reader2["GroupId"].ToString();
                    project = reader2["Title"].ToString();
                }
                reader2.Close();


                Paragraph title = new Paragraph("Student Name: " + stuName, boldFont);
                title.SpacingBefore = 10f;
                title.SpacingAfter = 10f;
                title.Font.Size = 16;
                title.Alignment = Element.ALIGN_CENTER;
                document.Add(title);

                Paragraph regPara = new Paragraph("Registration No.: " + stuReg, boldFont);
                regPara.SpacingBefore = 10f;
                regPara.SpacingAfter = 10f;
                regPara.Font.Size = 14;
                regPara.Alignment = Element.ALIGN_LEFT;
                document.Add(regPara);

                Paragraph groupPara = new Paragraph("Group Id: Group" + groupId, boldFont);
                groupPara.SpacingBefore = 10f;
                groupPara.SpacingAfter = 10f;
                groupPara.Font.Size = 12;
                groupPara.Alignment = Element.ALIGN_LEFT;
                document.Add(groupPara);

                Paragraph projectPara = new Paragraph("Project: " + project, boldFont);
                projectPara.SpacingBefore = 10f;
                projectPara.SpacingAfter = 10f;
                projectPara.Font.Size = 12;
                projectPara.Alignment = Element.ALIGN_LEFT;
                document.Add(projectPara);

                string query = "SELECT E.Name AS [Evaluation Name], E.TotalMarks AS [Total Marks], GE.ObtainedMarks AS  [Obtained Marks], E.TotalWeightage AS [Total Weightage], ((GE.ObtainedMarks * E.TotalWeightage)/E.TotalMarks) AS [Obtained Weightage] FROM GroupEvaluation AS GE JOIN Evaluation AS E ON E.Id = GE.EvaluationId JOIN GroupStudent AS GS ON GS.GroupId = GE.GroupId WHERE GS.StudentId = " + stuId;

                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();

                PdfPTable table = new PdfPTable(reader.FieldCount);
                table.WidthPercentage = 100;
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(reader.GetName(i)));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.BackgroundColor = new BaseColor(51, 153, 255);
                    table.AddCell(cell);
                }

                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(reader[i].ToString()));
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(cell);
                    }
                }
                reader.Close();
                document.Add(table);

                var con1 = Configuration.getInstance().getConnection();
                SqlCommand cmd1 = new SqlCommand("SELECT SUM(((GE.ObtainedMarks * E.TotalWeightage)/E.TotalMarks)) AS [Total Marks] FROM GroupEvaluation AS GE JOIN Evaluation AS E ON E.Id = GE.EvaluationId JOIN GroupStudent AS GS ON GS.GroupId = GE.GroupId WHERE GS.StudentId = " + stuId, con);
                SqlDataReader reader1 = cmd1.ExecuteReader();
                if (reader1.Read())
                {
                    totalMarks = reader1["Total Marks"].ToString();
                }
                reader1.Close();

                Paragraph totalMarksPara = new Paragraph("Total Marks: " + totalMarks, textFont);
                totalMarksPara.SpacingBefore = 10f;
                totalMarksPara.SpacingAfter = 10f;
                totalMarksPara.Font.Size = 12;
                totalMarksPara.Alignment = Element.ALIGN_LEFT;
                document.Add(totalMarksPara);

                document.NewPage();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //this function retrieves a list of distinct student IDs from a database and iterates over each student to create a section for their mark evaluation. The details of this section creation are implemented in the CreateMarkEvaluationSection method.
        private void CreateMarkEvaReport(string fileName)
        {
            Font boldFont = new Font(Font.FontFamily.TIMES_ROMAN, 16, Font.BOLD);
            Font textFont = new Font(Font.FontFamily.TIMES_ROMAN, 12);
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PDF (.pdf)|.pdf";
            sfd.FileName = fileName;
            bool errorMessage = false;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(sfd.FileName))
                {
                    try
                    {
                        File.Delete(sfd.FileName);


                    }
                    catch (Exception ex)
                    {
                        errorMessage = true;
                        MessageBox.Show("Unable to save file  on disk" + ex.Message);
                    }
                }
                if (!errorMessage)
                {
                    // Create new PDF document
                    Document document = new Document();
                    PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(sfd.FileName, FileMode.Create));
                    document.Open();


                    TitlePage(ref document);
                    document.NewPage();
                    Paragraph title = new Paragraph("Mark Sheet", boldFont);
                    title.SpacingBefore = 20f;
                    title.SpacingAfter = 20f;
                    title.Font.Size = 20;
                    title.Alignment = Element.ALIGN_LEFT;
                    document.Add(title);


                    List<int> student = new List<int>();
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("SELECT DISTINCT GS.StudentId AS Id FROM [GroupEvaluation] AS GE JOIN GroupStudent AS GS ON GE.GroupId=GS.GroupId JOIN Lookup AS LU ON GS.Status = LU.Id WHERE LU.Value = 'Active' ORDER BY GS.StudentId", con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        student.Add(int.Parse(reader["Id"].ToString()));
                    }
                    reader.Close();

                    foreach (int stu in student)
                    {
                        CreateMarkEvaluationSection(ref document, stu);
                    }

                    // Close PDF document and writer
                    document.Close();
                    writer.Close();
                }
            }
        }

        //  creates a PDF file containing evaluation data using the "CreateMarkEvaReport" method
        private void markSheetPdf_Click(object sender, RoutedEventArgs e)
        {
            string fileName = "Evaluation.pdf";
            CreateMarkEvaReport(fileName);
        }


        // it generates a PDF report based on SQL queries.The report will contain several sections, such as "Students Report", "Advisors Report", "Advisory Board Report", and "Evaluations Report".
        private void CompletePDFReport(string fileName)
        {
            string studentQuery = "Select S.RegistrationNo AS [Registration No], (FirstName + ' ' + LastName) AS Name,L.Value AS Gender,(SELECT FORMAT(DateOfBirth, 'dd-MM-yyyy')) AS [Date Of Birth],Contact,Email from Person P JOIN Student S ON S.Id=P.Id JOIN Lookup L ON L.Id=P.Gender";
            string StuTitle = "Students Report";

            string advisorQuery = "Select (FirstName + ' ' + LastName) AS Name,LU1.Value AS Designation,A.Salary,LU.Value AS Gender,(SELECT FORMAT(DateOfBirth, 'dd/MM/yyyy')) AS [Date Of Birth],Contact,Email FROM Person P JOIN Advisor A ON A.Id=P.Id JOIN Lookup LU ON LU.Id=P.Gender JOIN Lookup LU1 ON LU1.Id=A.Designation";
            string advisorTitle = "Advisors Report";

            string advBoardQuery = "SELECT MAX(P.Title) AS  Title, MAX(CASE WHEN PA.AdvisorRole = 11 THEN CONCAT(Person.FirstName,' ',Person.LastName) END) AS [Main Advisor], MAX(CASE WHEN PA.AdvisorRole = 12 THEN CONCAT(Person.FirstName,' ',Person.LastName) END) AS [Co-Advisor], MAX(CASE WHEN PA.AdvisorRole = 14 THEN CONCAT(Person.FirstName,' ',Person.LastName) END) AS [Industry Advisor] FROM  ProjectAdvisor PA INNER JOIN Advisor A ON PA.AdvisorId = A.Id JOIN Project P ON P.Id=PA.ProjectId JOIN Person ON Person.Id=A.Id GROUP BY PA.ProjectId";
            string advBoardTitle = "Advisory Board Report";

            string evaQuery = "SELECT Name As Title, TotalMarks AS [Total Marks], TotalWeightage AS [Total Weightage] FROM Evaluation";
            string evaTitle = "Evaluations Report";

            Font boldFont = new Font(Font.FontFamily.TIMES_ROMAN, 16, Font.BOLD);
            Font textFont = new Font(Font.FontFamily.TIMES_ROMAN, 12);
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PDF (.pdf)|.pdf";
            sfd.FileName = fileName;
            bool errorMessage = false;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(sfd.FileName))
                {
                    try
                    {
                        File.Delete(sfd.FileName);


                    }
                    catch (Exception ex)
                    {
                        errorMessage = true;
                        MessageBox.Show("Unable to save file  on disk" + ex.Message);
                    }
                }
                if (!errorMessage)
                {
                    // Create new PDF document
                    Document document = new Document();
                    PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(sfd.FileName, FileMode.Create));
                    document.Open();


                    TitlePage(ref document);

                    CreateReportSection(ref document, StuTitle, studentQuery);
                    CreateReportSection(ref document, advisorTitle, advisorQuery);
                    CreateReportSection(ref document, advBoardTitle, advBoardQuery);
                    CreateReportSection(ref document, evaTitle, evaQuery);




                    document.NewPage();
                    Paragraph groupTitle = new Paragraph("All Groups", boldFont);
                    groupTitle.SpacingBefore = 20f;
                    groupTitle.SpacingAfter = 20f;
                    groupTitle.Font.Size = 20;
                    groupTitle.Alignment = Element.ALIGN_LEFT;
                    document.Add(groupTitle);


                    List<int> groups = new List<int>();
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("SELECT DISTINCT G.Id FROM [Group] AS G JOIN GroupStudent AS GS ON G.Id=GS.GroupId", con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        groups.Add(int.Parse(reader["Id"].ToString()));
                    }
                    reader.Close();

                    foreach (int group in groups)
                    {
                        string groupQuery = "SELECT CONCAT(P.FirstName ,' ',P.LastName) AS Name ,S.RegistrationNo AS RegNo ,L.Value AS Status,(CASE WHEN L.Value ='Active' THEN (SELECT FORMAT(AssignmentDate, 'dd/MM/yyyy')) END) AS [Active Date],(CASE WHEN L.Value<>'Active' THEN (SELECT FORMAT(AssignmentDate, 'dd/MM/yyyy')) END) AS [InActive Date] FROM GroupStudent AS GS JOIN Lookup AS L ON GS.Status = L.Id JOIN Student AS S ON S.Id = GS.StudentId JOIN Person AS P ON P.Id = S.Id WHERE GS.GroupId = " + group + " ORDER BY L.Value";

                        string project = "", mainAdv = "", coAdv = "", inAdv = "";

                        var con2 = Configuration.getInstance().getConnection();
                        SqlCommand cmd2 = new SqlCommand("SELECT MAX(P.Title) AS  Title, MAX(CASE WHEN PA.AdvisorRole = 11 THEN CONCAT(Person.FirstName,' ',Person.LastName) END) AS [Main Advisor], MAX(CASE WHEN PA.AdvisorRole = 12 THEN CONCAT(Person.FirstName,' ',Person.LastName) END) AS [Co-Advisor], MAX(CASE WHEN PA.AdvisorRole = 14 THEN CONCAT(Person.FirstName,' ',Person.LastName) END) AS [Industry Advisor] FROM  ProjectAdvisor PA  JOIN Advisor A ON PA.AdvisorId = A.Id JOIN Project AS P ON P.Id=PA.ProjectId JOIN Person ON Person.Id=A.Id JOIN GroupProject AS GP ON GP.Projectid=P.Id WHERE GP.GroupId=" + group + " GROUP BY PA.ProjectId", con2);
                        SqlDataReader reader2 = cmd2.ExecuteReader();
                        if (reader2.Read())
                        {
                            mainAdv = reader2["Main Advisor"].ToString();
                            coAdv = reader2["Co-Advisor"].ToString();
                            inAdv = reader2["Industry Advisor"].ToString();
                            project = reader2["Title"].ToString();
                        }
                        reader2.Close();

                        CreateGroupReportSection(ref document, group, project, mainAdv, coAdv, inAdv, groupQuery);
                    }

                    document.NewPage();
                    Paragraph MarkSheetTitle = new Paragraph("Mark Sheet", boldFont);
                    MarkSheetTitle.SpacingBefore = 20f;
                    MarkSheetTitle.SpacingAfter = 20f;
                    MarkSheetTitle.Font.Size = 20;
                    MarkSheetTitle.Alignment = Element.ALIGN_LEFT;
                    document.Add(MarkSheetTitle);

                    List<int> student = new List<int>();
                    var con1 = Configuration.getInstance().getConnection();
                    SqlCommand cmd1 = new SqlCommand("SELECT DISTINCT GS.StudentId AS Id FROM [GroupEvaluation] AS GE JOIN GroupStudent AS GS ON GE.GroupId=GS.GroupId JOIN Lookup AS LU ON GS.Status = LU.Id WHERE LU.Value = 'Active' ORDER BY GS.StudentId", con1);
                    SqlDataReader reader1 = cmd1.ExecuteReader();
                    while (reader1.Read())
                    {
                        student.Add(int.Parse(reader1["Id"].ToString()));
                    }
                    reader1.Close();

                    foreach (int stu in student)
                    {
                        CreateMarkEvaluationSection(ref document, stu);
                    }

                    // Close PDF document and writer
                    document.Close();
                    writer.Close();
                }
            }
        }


        //creates a PDF file containing All reports data using the "CompletePdfReport" method
        private void completePdf_Click(object sender, RoutedEventArgs e)
        {
            CompletePDFReport("FYP Report");
        }
    }
}
