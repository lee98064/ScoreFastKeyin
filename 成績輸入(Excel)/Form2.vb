Public Class Form2
    Function printscore()
        TextBox1.Text = "座號   成績" & vbCrLf & "============================" & vbCrLf
        TextBox2.Clear()
        For i = 1 To Form1.allpeople
            TextBox1.Text &= i & "           "
            If i < 10 Then
                TextBox1.Text &= "  " & Form1.score(i, ComboBox1.SelectedIndex + 1) & vbCrLf
            Else
                TextBox1.Text &= Form1.score(i, ComboBox1.SelectedIndex + 1) & vbCrLf
            End If
            If Form1.numno(i, ComboBox1.SelectedIndex + 1) = 0 Then
                TextBox2.Text &= i & "  "
            End If
        Next
    End Function
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox1.Items.Clear()
        For i = 1 To Form1.scoregroup
            ComboBox1.Items.Add("第 " & i & " 個成績")
        Next
        ComboBox1.SelectedIndex = 0
        printscore()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        printscore()
    End Sub
End Class