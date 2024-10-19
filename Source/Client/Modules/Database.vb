Imports System.IO
Imports System.Linq
Imports System.Windows.Forms
Imports Core

Module Database
    Friend Function GetFileContents(fullPath As String, Optional ByRef errInfo As String = "") As String
        Dim strContents As String
        Dim objReader As StreamReader

        strContents = ""

        Try
            objReader = New StreamReader(fullPath)
            strContents = objReader.ReadToEnd()
            objReader.Close()
        Catch ex As Exception
            errInfo = ex.Message
        End Try
        Return strContents
    End Function

#Region "Blood"

    Sub ClearBlood()
       For i = 0 To Byte.MaxValue
            Blood(I).Timer = 0
        Next
    End Sub

#End Region

#Region "NPC"

    Sub ClearNPCs()
        Dim i As Integer

        ReDim Type.NPC(MAX_NPCS)

       For i = 1 To MAX_NPCS
            ClearNPC(i)
        Next

    End Sub

    Sub ClearNPC(index As Integer)
        Type.NPC(index) = Nothing
        ReDim Type.NPC(index).Stat(StatType.Count - 1)
        ReDim Type.NPC(index).DropChance(5)
        ReDim Type.NPC(index).DropItem(5)    
        ReDim Type.NPC(index).DropItemValue(5)
        ReDim Type.NPC(index).Skill(6)
        NPC_Loaded(index) = False
    End Sub

    Sub StreamNpc(npcNum As Integer)
        If npcNum > 0 and Type.NPC(npcNum).Name = "" Or NPC_Loaded(npcNum) = False Then
            NPC_Loaded(npcNum) = True
            SendRequestNPC(NPCNum)
        End If
    End Sub

#End Region

#Region "Jobs"
    Sub ClearJobs()
        For i = 1 To MAX_JOBS
            ClearJob(i)
        Next
    End Sub

    Sub ClearJob(index As Integer)
        Type.Job(index) = Nothing
        ReDim Type.Job(index).Stat(StatType.Count - 1)
        Type.Job(index).Name = ""
        Type.Job(index).Desc = ""
        ReDim Type.Job(index).StartItem(5)
        ReDim Type.Job(index).StartValue(5)
        Type.Job(index).MaleSprite = 1
        Type.Job(index).FemaleSprite = 1
    End Sub
#End Region

#Region "Skills"

    Sub ClearSkills()
        Dim i As Integer

       For i = 1 To MAX_SKILLS
            ClearSkill(i)
        Next

    End Sub

    Sub ClearSkill(index As Integer)
        Type.Skill(index) = Nothing
        Type.Skill(index).Name = ""
        Skill_Loaded(index) = False
    End Sub

    Sub StreamSkill(skillNum As Integer)
        If skillNum > 0 And Type.Skill(skillNum).Name = "" Or Skill_Loaded(skillNum) = False Then
            Skill_Loaded(skillNum) = True
            SendRequestSkill(skillNum)
        End If
    End Sub
#End Region
End Module