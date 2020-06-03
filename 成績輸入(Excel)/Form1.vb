Imports Excel = Microsoft.Office.Interop.Excel
Public Class Form1
    Public allpeople, numno(100, 26), scoregroup, scoregroupselect As Integer
    Public score(100, 26) As Single
    Dim app As New Excel.Application 'app 是操作 Excel 的變數
    Dim worksheet As Excel.Worksheet 'Worksheet 代表的是 Excel 工作表
    Dim workbook As Excel.Workbook 'Workbook 代表的是一個 Excel 本體
    Dim a As String
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        allpeople = NumericUpDown1.Value
        Button2.Text = "正在導出....."
        'Button2.Enabled = False
        workbook = app.Workbooks.Add()
        worksheet = workbook.Worksheets("工作表1")
        worksheet.Cells(1, 1) = "座號"
        For j = 1 To scoregroup
            worksheet.Cells(1, j + 1) = "第" & j & "個成績"
            For i = 1 To allpeople
                worksheet.Cells(i + 1, 1) = i
                worksheet.Cells(i + 1, j + 1) = score(i, j)
            Next
        Next
        worksheet.Range("A1:A" & allpeople + 1).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
        worksheet.Range("B1").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
        worksheet.Range("a1: a10").Borders.LineStyle = Excel.XlLineStyle.xlContinuous
        worksheet.Range("A1:Z" & allpeople + 1).Borders.LineStyle = Excel.XlLineStyle.xlContinuous
        worksheet.Range("A1:Z" & allpeople + 1).EntireColumn.AutoFit()
        app.DisplayAlerts = False
        app.AlertBeforeOverwriting = False
        SaveFileDialog1.ShowDialog()
        a = SaveFileDialog1.FileName
        If a = "" Then
            workbook.Close(False, SaveFileDialog1.FileName)
            MsgBox("導出失敗! 請檢查是否輸入檔名或按到取消鍵!", MsgBoxStyle.Exclamation, "導出至Excel")
        Else
            workbook.Close(True, SaveFileDialog1.FileName)
            MsgBox("儲存成功!", MsgBoxStyle.Information, "導出至Excel")
        End If
        app.Quit()
        Button2.Text = "導出成Excel檔"
        'Button2.Enabled = True
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim no As Integer
        Dim tempscore As Single
        If IsNumeric(TextBox2.Text) Or IsNumeric(TextBox1.Text) Then
            If ComboBox1.SelectedItem = "無按照號碼" Then
                If IsNumeric(TextBox1.Text) Then
                    no = TextBox1.Text
                Else
                    no = -1
                End If
            Else
                no = NumericUpDown2.Value
            End If
            tempscore = TextBox2.Text
            If tempscore < 0 Or no < 0 Or no > allpeople Or no = 0 Then
                MsgBox("成績不能為負數!!" & vbCrLf & "或是座號不能大於班級人數或為負數!!", MsgBoxStyle.Exclamation, "成績輸入")
            Else
                score(no, scoregroupselect) = tempscore
                numno(no, scoregroupselect) = 1
                If ComboBox1.SelectedItem = "無按照號碼" Then
                    TextBox1.Clear()
                    TextBox2.Clear()
                    TextBox1.Focus()
                Else
                    NumericUpDown2.Value += 1
                    TextBox2.Clear()
                    TextBox2.Focus()
                End If
            End If
        Else
            MsgBox("請檢查座號或成績是否輸入錯誤!!", MsgBoxStyle.Exclamation, "成績輸入")
        End If
    End Sub

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged
        allpeople = NumericUpDown1.Value
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim i As Integer = 0
        Dim temp As String
        ComboBox1.SelectedIndex = 0
        Do Until i = 1
            temp = InputBox("請輸入有幾組成績要輸入(最多26組)", "成績輸入")
            If temp <> "" And IsNumeric(temp) And temp <> 0 And temp <= 26 Then
                scoregroup = temp
                i = 1
            End If
        Loop
        For i = 1 To scoregroup
            ComboBox2.Items.Add("第 " & i & " 個")
        Next
        ComboBox2.SelectedIndex = 0
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedItem = "無按照號碼" Then
            TextBox1.Visible = True
            TextBox1.Focus()
            NumericUpDown2.Visible = False
        Else
            TextBox1.Visible = False
            TextBox2.Focus()
            NumericUpDown2.Visible = True
            NumericUpDown2.Value = 1
        End If
    End Sub

    Private Sub 關於ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles 關於ToolStripMenuItem1.Click
        Form3.Show()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim i As Integer = 0
        Dim temp As String
        ComboBox1.SelectedIndex = 0
        Do Until i = 1
            temp = InputBox("請輸入有幾組成績要輸入(最多26組)", "成績輸入")
            If temp <> "" And IsNumeric(temp) And temp <> 0 And temp <= 26 Then
                scoregroup = temp
                i = 1
            End If
        Loop
        ComboBox2.Items.Clear()
        For i = 1 To scoregroup
            ComboBox2.Items.Add("第 " & i & " 個")
        Next
        ComboBox2.SelectedIndex = 0
        scoregroupselect = ComboBox2.SelectedIndex + 1
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        scoregroupselect = ComboBox2.SelectedIndex + 1
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim a As Integer
        a = MsgBox("確定要清除全部成績嗎?" & vbCrLf & "此動作無法回復!!", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "清除所有成績")
        If a = 6 Then
            Array.Clear(score, 0, score.Length)
            Array.Clear(numno, 0, numno.Length)
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form2.Show()
    End Sub
    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Return Then
            TextBox2.Focus()
        End If
    End Sub

    Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Return Then
            Button3.Focus()
        End If
    End Sub
End Class
