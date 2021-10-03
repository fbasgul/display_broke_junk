Imports System.Drawing.Imaging
Imports System.IO

Public Class SpolyMusic
    Dim gnr As System.Random = New System.Random()
    Dim dgr As Integer = 0
    Dim saat As Integer = 0
    Dim gun() As String = {"Cumartesi", "Pazar", "Salı", "Cuma"} 'choose day {"Monday","Saturday"}
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        For t = 0 To gun.Count - 1
            If Date.Today.ToString("dddd") = gun(t) Then Exit Sub
        Next
        If Date.Now.Hour > 18 Or Date.Now.Hour < 9 Then Exit Sub
        dgr = gnr.Next(1, 59)
        If Date.Now.Minute = dgr Then

            If saat = Date.Now.Hour Then
                Exit Sub
            Else
                saat = Date.Now.Hour
            End If

            Try
                Dim Str As String = "-*-*-*- " & Date.Now.ToString("ddMMyyyy") & " - " & Date.Now.Hour & " - " & dgr & " -*-*-*-" & vbCrLf
                File.AppendAllText("c:\temp\spilc", Str)
            Catch ex As Exception

            End Try

            Dim bounds As Rectangle
            Dim screenshot As System.Drawing.Bitmap
            Dim graph As Graphics
            bounds = Screen.PrimaryScreen.Bounds
            screenshot = New System.Drawing.Bitmap(bounds.Width, bounds.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb)
            graph = Graphics.FromImage(screenshot)
            graph.CopyFromScreen(0, 0, 0, 0, bounds.Size, CopyPixelOperation.SourceCopy)

            Dim g As Graphics = Graphics.FromImage(screenshot)
            Dim att As New ImageAttributes
            Dim m As New ColorMatrix
            m.Matrix33 = 1.8
            att.SetColorMatrix(m)
            For x = -1 To 3
                For y = -1 To 2
                    g.DrawImage(screenshot, New Rectangle(x, y, screenshot.Width, screenshot.Height), 0, 0, screenshot.Width, screenshot.Height, GraphicsUnit.Pixel, att)
                Next
            Next
            PictureBox1.Image = screenshot
            Me.WindowState = FormWindowState.Maximized
            dgr = gnr.Next(5, 12)
            Timer2.Interval = dgr * 1000
            Timer2.Enabled = True
        End If
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Me.WindowState = FormWindowState.Minimized
        Timer2.Enabled = False
    End Sub

    Private Sub SpolyMusic_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.ShowInTaskbar = False
    End Sub
End Class
