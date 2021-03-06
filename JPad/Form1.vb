﻿Option Explicit On

Imports System
Imports System.IO
Imports System.Security
Imports System.Security.Cryptography
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Printing
Imports JPad.CM
Public Class Form1
    Dim SaveAs As New SaveFileDialog
    Dim OpenFile As New OpenFileDialog
    Dim NewFile As Integer

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            SaveAs.Title = "Save File As..."
            '            SaveAs.Filter = "*.jpad | jpad"
            ' Still need to fix this issue
            SaveAs.AddExtension = True
        Catch ex As Exception
        End Try

        Try
            OpenFile.Title = "Open JPAD File"
            '            OpenFile.Filter = "*.jpad | JPad"
            ' Still need to fix that issue
        Catch ex As Exception

        End Try

    End Sub

    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click
        Try
            RichTextBox1.Clear()
            NewFile = 1
            SaveCurrentToolStripMenuItem.Visible = 0
            Me.Text = "JPad - The Secret Notepad for Writers"
        Catch ex As Exception
        End Try
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveAsToolStripMenuItem.Click
        Try
            SaveAs.ShowDialog()
            If Windows.Forms.DialogResult.Abort Then

            Else
                RichTextBox1.SaveFile(SaveAs.FileName)
                Me.Text = "JPad - The Secret Notepad for Writers - " + SaveAs.FileName
                SaveCurrentToolStripMenuItem.Enabled = 1
                NewFile = 0
                SaveCurrentToolStripMenuItem.Visible = 1
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub SaveCurrentToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveCurrentToolStripMenuItem.Click
        Try
            RichTextBox1.SaveFile(SaveAs.FileName)
            Me.Text = "JPad - The Secret Notepad for Writers - " + SaveAs.FileName
        Catch ex As Exception

        End Try
    End Sub

    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        Try
            OpenFile.ShowDialog()
            RichTextBox1.LoadFile(OpenFile.FileName)
            NewFile = 0
            SaveCurrentToolStripMenuItem.Visible = 0
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox1.TextChanged
        Try
            If NewFile = 1 Then
                NewFile = 0
                SaveCurrentToolStripMenuItem.Enabled = True

            Else
                Me.Text = "JPad - The Secret Notepad for Writers - " + SaveAs.FileName + "*"
            End If

        Catch ex As Exception

        End Try

        'Try
        '    Dim strInput As String = RichTextBox1.Text
        '    Dim strSplit() As String
        '    strSplit = strInput.Split(CChar(" "))
        '    ToolStripStatusLabel2.Text = (strSplit.Length)
        'Catch ex As Exception

        'End Try
    End Sub

    Private Sub ChangeSelectedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChangeSelectedToolStripMenuItem.Click
        Try
            Dim DFont As New FontDialog
            DFont.ShowDialog()
            RichTextBox1.SelectionFont = DFont.Font
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ChangeEntireToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChangeEntireToolStripMenuItem.Click
        Try
            Dim DFont As New FontDialog
            DFont.ShowDialog()
            RichTextBox1.Font = DFont.Font
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ChangeSelectedTextToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChangeSelectedTextToolStripMenuItem.Click
        Try
            Dim DColor As New ColorDialog
            DColor.ShowDialog()
            RichTextBox1.SelectionColor = DColor.Color
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ChangeEntireDocumentToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChangeEntireDocumentToolStripMenuItem.Click
        Try
            Dim DColor As New ColorDialog
            DColor.ShowDialog()
            RichTextBox1.ForeColor = DColor.Color
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LockFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LockFileToolStripMenuItem.Click
        Try
            ' This is just a trial thing~
            LockScreen.TextBox1.Text = ""
            LockScreen.ShowDialog()
            If LockScreen.DialogResult = Windows.Forms.DialogResult.OK Then
                SaveAs.ShowDialog()
                TextBox1.Text = RichTextBox1.ToString
                Dim a As String = SaveAs.FileName
                Dim b As String = TextBox1.Text
                Dim c As Byte() = encrypt(b, a)
                Dim d As String = Convert.ToBase64String(c)
                File.WriteAllText(SaveAs.FileName, d)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub UnlockAndViewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UnlockAndViewToolStripMenuItem.Click

        Try
            LockScreen.TextBox1.Text = ""
            LockScreen.ShowDialog()
            If LockScreen.DialogResult = Windows.Forms.DialogResult.OK Then
                OpenFile.ShowDialog()
                Dim a As String = LockScreen.TextBox1.Text
                Dim b As String = File.ReadAllText(OpenFile.FileName)
                Dim c As String = decrypt(b, a)
                TextBox1.Text = c
                RichTextBox1.Text = TextBox1.Text.ToString
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
