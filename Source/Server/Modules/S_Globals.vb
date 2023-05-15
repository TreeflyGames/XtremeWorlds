Imports Mirage.Sharp.Asfw.IO.Encryption

Module S_Globals
    Friend DebugTxt As Boolean
    Friend ErrorCount As Integer

    ' Used for closing key doors again
    Friend KeyTimer As Integer

    ' Used for gradually giving back npcs hp
    Friend GiveNPCHPTimer As Integer

    Friend GiveNPCMPTimer As Integer

    Friend EKeyPair As New KeyPair()
End Module