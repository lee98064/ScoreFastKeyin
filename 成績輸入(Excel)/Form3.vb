﻿Public Class Form3
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Text = "更新內容:" & vbNewLine & "1.首發版上線"
    End Sub
End Class