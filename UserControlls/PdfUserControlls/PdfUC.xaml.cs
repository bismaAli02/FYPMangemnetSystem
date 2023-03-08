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

namespace FYPManagementSystem.UserControlls.PdfUserControlls
{
    /// <summary>
    /// Interaction logic for PdfUC.xaml
    /// </summary>
    public partial class PdfUC : UserControl
    {
        public PdfUC()
        {
            InitializeComponent();
        }

        private void TitlePage(ref Document document)
        {
            // Set page size, margins, author, date, title, header
            document.SetPageSize(PageSize.A4);
            document.SetMargins(30, 30, 30, 30);
            document.AddAuthor("Bisma ALi");
            document.AddCreationDate();
            document.AddTitle("PDF Report");
            document.AddHeader("Title", "FYP Management System");

            Font boldFont = new Font(Font.FontFamily.TIMES_ROMAN, 16, Font.BOLD);
            Font textFont = new Font(Font.FontFamily.TIMES_ROMAN, 12);

            // Adding title page
            document.NewPage();
            Paragraph title = new Paragraph("FYP Management System", boldFont);
            title.SpacingBefore = 50f;
            title.SpacingAfter = 50f;
            title.Font.Size = 28;
            title.Alignment = Element.ALIGN_CENTER;
            document.Add(title);

            //document.Add(new Paragraph(Chunk.NEWLINE));

            // Adding UET LOGO Image
            string imageURL = "Assets\\Images\\uet_logo.png";
            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
            //Resize image depend upon your need
            jpg.ScaleToFit(140f, 120f);
            //Give space before image
            jpg.SpacingBefore = 10f;
            //Give some space after the image
            jpg.SpacingAfter = 1f;
            jpg.Alignment = Element.ALIGN_CENTER;

            document.Add(jpg);



            Paragraph session = new Paragraph("Session: 2021 - 2025", textFont);
            session.SpacingBefore = 10f;
            session.SpacingAfter = 50f;
            session.Alignment = Element.ALIGN_CENTER;
            document.Add(session);


            Paragraph submittedParagraph = new Paragraph("Submitted By:", boldFont);
            submittedParagraph.SpacingBefore = 10f;
            submittedParagraph.Font.Size = 16;
            submittedParagraph.Alignment = Element.ALIGN_CENTER;
            document.Add(submittedParagraph);


            Paragraph nameParagraph = new Paragraph("Bisma Muhammad Ali      2021-CS-170", textFont);
            nameParagraph.SpacingBefore = 10f;
            nameParagraph.SpacingAfter = 80f;
            nameParagraph.Font.Size = 14;
            nameParagraph.Alignment = Element.ALIGN_CENTER;
            document.Add(nameParagraph);


            Paragraph submittedToParagraph = new Paragraph("Submitted To:", boldFont);
            submittedToParagraph.SpacingBefore = 10f;
            submittedToParagraph.Font.Size = 16;
            submittedToParagraph.Alignment = Element.ALIGN_CENTER;
            document.Add(submittedToParagraph);


            Paragraph teacherParagraph = new Paragraph("Sir Samyan", textFont);
            teacherParagraph.SpacingBefore = 10f;
            teacherParagraph.SpacingAfter = 50f;
            teacherParagraph.Font.Size = 14;
            teacherParagraph.Alignment = Element.ALIGN_CENTER;
            document.Add(teacherParagraph);

            Paragraph departmentParagraph = new Paragraph("Department of Computer Science", textFont);
            departmentParagraph.SpacingBefore = 80f;
            departmentParagraph.SpacingAfter = 10f;
            departmentParagraph.Font.Size = 18;
            departmentParagraph.Alignment = Element.ALIGN_CENTER;
            document.Add(departmentParagraph);

            Paragraph uetParagraph = new Paragraph("University of Engineering And Technology, Lahore", boldFont);
            uetParagraph.SpacingBefore = 10f;
            uetParagraph.SpacingAfter = 10f;
            uetParagraph.Font.Size = 24;
            uetParagraph.Alignment = Element.ALIGN_CENTER;
            document.Add(uetParagraph);
        }

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
                    cell.BackgroundColor = new BaseColor(128, 128, 128);
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

        private void StudentreportBtn_Click(object sender, RoutedEventArgs e)
        {
            string fileName = "Students.pdf";
            string studentQuery = "Select S.RegistrationNo AS [Registration No], (FirstName + ' ' + LastName) AS Name,L.Value AS Gender,(SELECT FORMAT(DateOfBirth, 'dd-MM-yyyy')) AS [DoB],Contact,Email from Person P JOIN Student S ON S.Id=P.Id JOIN Lookup L ON L.Id=P.Gender";
            string title = "Students";

            CreateReport(fileName, title, studentQuery);
        }

        private void AdvisorReportBtn_Click(object sender, RoutedEventArgs e)
        {
            string fileName = "Advisors.pdf";
            string advisorQuery = "Select P.Id, (FirstName + ' ' + LastName) AS Name,LU1.Value AS Designation,A.Salary,LU.Value AS Gender,(SELECT FORMAT(DateOfBirth, 'dd/MM/yyyy')) AS [DateOfBirth],Contact,Email FROM Person P JOIN Advisor A ON A.Id=P.Id JOIN Lookup LU ON LU.Id=P.Gender JOIN Lookup LU1 ON LU1.Id=A.Designation";
            string title = "Advisors";

            CreateReport(fileName, title, advisorQuery);
        }

        private void AdvBoardReportBtn_Click(object sender, RoutedEventArgs e)
        {
            string fileName = "AdvisoryBoard.pdf";
            string advBoardQuery = "SELECT PA.ProjectId AS [Project Id], MAX(P.Title) AS  Title, MAX(CASE WHEN PA.AdvisorRole = 11 THEN CONCAT(Person.FirstName,' ',Person.LastName) END) AS [Main Advisor], MAX(CASE WHEN PA.AdvisorRole = 12 THEN CONCAT(Person.FirstName,' ',Person.LastName) END) AS [Co-Advisor], MAX(CASE WHEN PA.AdvisorRole = 14 THEN CONCAT(Person.FirstName,' ',Person.LastName) END) AS [Industry Advisor] FROM  ProjectAdvisor PA INNER JOIN Advisor A ON PA.AdvisorId = A.Id JOIN Project P ON P.Id=PA.ProjectId JOIN Person ON Person.Id=A.Id GROUP BY PA.ProjectId";
            string title = "Advisory Board";

            CreateReport(fileName, title, advBoardQuery);
        }

        private void EvaluationReportBtn_Click(object sender, RoutedEventArgs e)
        {
            string fileName = "Evaluation.pdf";
            string advBoardQuery = "SELECT Id AS [Evaluation Id], Name As Title, TotalMarks AS [Total Marks], TotalWeightage AS [Total Weightage] FROM Evaluation";
            string title = "All Evaluations";

            CreateReport(fileName, title, advBoardQuery);
        }
    }
}
