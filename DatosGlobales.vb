Imports System.Data.OleDb
Module DatosGlobales
    Public Comando As New OleDbCommand
    Public Conexion As New OleDbConnection
    Public Adaptador As New OleDbDataAdapter

    Public DR As OleDbDataReader
    Public DS As DataSet

    Public ComandoSocio As New OleDbCommand
    Public ConexionSocio As New OleDbConnection

    Public DRSoc As OleDbDataReader

    Public CadenaDeConexion As String = "Provider = Microsoft.Jet.OLEDB.4.0;Data Source=Datos.mdb"
End Module
