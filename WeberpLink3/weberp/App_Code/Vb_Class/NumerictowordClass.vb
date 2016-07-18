Imports Microsoft.VisualBasic


Public Class NumerictowordClass

    'Option Explicit
    Private Shared withFrameFlag As Boolean
    Private Shared mFormRegion As Long
    Private Shared Sp1 As Double
    Private Shared sp2 As Double
    Private Shared Fval As String
    'Dim orval As String
    Private Shared orv1 As String
    Private Shared orv2 As String
    Private Shared orval As String

    Public Shared Function FNumber(ByVal Number As String) As String
        Dim fnum As String
        Dim tnum As String
        Dim ddsp As Boolean
        Dim i As Integer
        tnum = 0
        ddsp = False
        For i = 1 To Len(Number)
            tnum = Mid(Number, i, 1)
            Select Case Asc(tnum)
                Case 48 To 57
                    fnum = fnum + tnum
                    ddsp = False
                Case 46 To 47
                    If ddsp = False Then
                        Sp1 = Val(fnum)
                        orv1 = CStr(Sp1)
                        Fval = ToWords(Sp1)
                        fnum = ""
                        ddsp = True
                    Else
                        ddsp = False
                        GoTo 300
                    End If
            End Select
300:    Next
        sp2 = Val(fnum)
        If InStr(1, Number, ".") > 0 Then
            If sp2 = 0 Then
                Fval = "= " & Fval + " ONLY="
            Else
                orv2 = CStr(sp2)
                Fval = "= " & Fval + " AND PAISA " & ToWords(sp2) + " ONLY="
            End If
        ElseIf InStr(1, Number, "/") > 0 Then
            If sp2 = 0 Then
                Fval = "= " & Fval + " ONLY="
            Else
                orv2 = CStr(sp2)
                Fval = "= " & Fval + " AND PAISA " & ToWords(sp2) + " ONLY="
            End If
        Else
            orv1 = CStr(sp2)
            Fval = "= " & ToWords(sp2) + " ONLY="
        End If
        FNumber = Fval
    End Function

    Private Shared Function ToWords(ByVal Number As Double) As String

        Dim K As String
        Dim X As String
        Dim Y As String
        Dim z As String
        Dim m As String
        Dim h As String
        Dim T As String
        Dim L As String
        Dim C As String
        Dim clthm As String
        ' Dim len1 As Integer
        Dim i As Integer

        C = ""
        L = ""
        T = ""
        h = ""
        m = ""

        K = Number

        If Len(K) > 9 Then
            Return ""
            Exit Function
        End If


        If Len(K) < 9 Then
            For i = 1 To 9 - Len(K)
                K = "0" + K
            Next
        End If
        X = Right(K, 5)
        Y = Left(K, 4)

        If Val(Right(X, 2)) <> 0 Then
            If Mid(X, 4, 1) = "0" Then
                m = OnesVal(Val(Mid(X, 5)))
            Else
                If Val(Mid(X, 4)) <= 20 Then
                    m = TwosVal(Val(Mid(X, 4)))
                Else
                    m = TwosVal(Val(Mid(X, 4, 1) + "0")) + OnesVal(Val(Mid(X, 5, 1)))
                End If

            End If
        End If
        If Val(Mid(X, 3, 1)) <> 0 Then
            h = OnesVal(Val(Mid(X, 3, 1))) + "hundred "
        End If

        If Val(Mid(X, 1, 2)) <> 0 Then
            If Mid(X, 1, 1) = "0" Then
                T = OnesVal(Val(Mid(X, 2, 1))) + "thousand "
            Else
                If Val(Mid(X, 1, 2)) <= 20 Then
                    T = TwosVal(Val(Mid(X, 1, 2))) + "thousand "
                Else
                    T = TwosVal(Val(Mid(X, 1, 1) + "0")) + OnesVal(Val(Mid(X, 2, 1))) + " thousand "
                End If
            End If
        End If

        'y = Left(k, 4)
        If Val(Right(Y, 2)) <> 0 Then
            If Mid(Y, 3, 1) = "0" Then
                L = OnesVal(Val(Mid(Y, 3))) + "lac "
            Else
                If Val(Mid(Y, 3)) <= 20 Then
                    L = TwosVal(Val(Mid(Y, 3))) + "lac "
                Else
                    L = TwosVal(Val(Mid(Y, 3, 1) + "0")) + OnesVal(Val(Mid(Y, 4, 1))) + " lac "
                End If
            End If
        End If
        If Val(Left(Y, 2)) <> 0 Then
            If Mid(Y, 1, 1) = "0" Then
                C = OnesVal(Val(Mid(Y, 2, 1))) + "crore "
            Else
                If Val(Mid(Y, 1, 2)) <= 20 Then
                    C = TwosVal(Val(Mid(Y, 1, 2))) + "crore "
                Else
                    C = TwosVal(Val(Mid(Y, 1, 1) + "0")) + OnesVal(Val(Mid(Y, 2, 1))) + " crore "
                End If
            End If
        End If

        clthm = C + L + T + h + m
        ToWords = UCase(Left(clthm, 1)) + Mid(clthm, 2)
        ToWords = UCase(ToWords)
    End Function

    Private Shared Function OnesVal(ByVal NUM As Integer) As String
        Select Case NUM
            Case 1
                OnesVal = "one "
            Case 2
                OnesVal = "two "
            Case 3
                OnesVal = "three "
            Case 4
                OnesVal = "four "
            Case 5
                OnesVal = "five "
            Case 6
                OnesVal = "six "
            Case 7
                OnesVal = "seven "
            Case 8
                OnesVal = "eight "

            Case 9
                OnesVal = "nine "
            Case Else
                OnesVal = ""
        End Select

    End Function

    Private Shared Function TwosVal(ByVal NUM As Integer) As String
        Select Case NUM
            Case 10
                TwosVal = "ten "
            Case 11
                TwosVal = "eleven "
            Case 12
                TwosVal = "twelve "
            Case 13
                TwosVal = "thirteen "
            Case 14
                TwosVal = "fourteen "
            Case 15
                TwosVal = "fifteen "
            Case 16
                TwosVal = "sixteen "
            Case 17
                TwosVal = "seventeen "
            Case 18
                TwosVal = "eighteen "
            Case 19
                TwosVal = "nineteen "
            Case 20
                TwosVal = "twenty "
            Case 30
                TwosVal = "thirty "
            Case 40
                TwosVal = "forty "
            Case 50
                TwosVal = "fifty "
            Case 60
                TwosVal = "sixty "
            Case 70
                TwosVal = "seventy "
            Case 80
                TwosVal = "eighty "
            Case 90
                TwosVal = "ninety "
            Case Else
                TwosVal = ""
        End Select

    End Function

End Class
