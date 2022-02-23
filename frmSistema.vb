Public Class frmSistema
    Private Sub frmSistema_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Conexion.ConnectionString = CadenaDeConexion
        Conexion.Open()

        Comando.Connection = Conexion
        Comando.CommandType = CommandType.TableDirect
        Comando.CommandText = "Servicios"


        Adaptador = New OleDb.OleDbDataAdapter(Comando)
        DS = New DataSet
        Adaptador.Fill(DS)

        cmbServicios.DataSource = DS.Tables(0)
        cmbServicios.ValueMember = "IdServicio"
        cmbServicios.DisplayMember = "Nombre"

        Conexion.Close()
    End Sub

    Private Sub btnListar_Click(sender As Object, e As EventArgs) Handles btnListar.Click
        Dim total As Decimal = 0
        Dim cantidad As Integer = 0
        Dim promedio As Decimal = 0

        Conexion.ConnectionString = CadenaDeConexion
        Conexion.Open()


        Comando.Connection = Conexion
        Comando.CommandType = CommandType.TableDirect
        Comando.CommandText = "Facturas"
        DR = Comando.ExecuteReader

        ComandoSocio.Connection = Conexion
        ComandoSocio.CommandType = CommandType.TableDirect
        ComandoSocio.CommandText = "Socios"


        DRSoc = ComandoSocio.ExecuteReader
        dgvDatos.Rows.Clear()

        DRSoc.Read()
        If DR.HasRows Then
            Do While DR.Read
                Do While DRSoc("Nombre") = DR("IdSocio")
                    If DR("IdServicio") = cmbServicios.SelectedValue Then
                        dgvDatos.Rows.Add(DR("NumFac"), DR("Fecha"), DRSoc("Nombre"), DR("Importe"))

                        cantidad = cantidad + 1
                        total = total + DR("Importe")

                    End If
                Loop





            Loop


        End If

        lblTotal.Text = total
        lblPromedio.Text = total / cantidad
        Conexion.Close()


    End Sub

    Private Sub btnExportar_Click(sender As Object, e As EventArgs) Handles btnExportar.Click
        Conexion.ConnectionString = CadenaDeConexion
        Conexion.Open()

        Comando.Connection = Conexion
        Comando.CommandType = CommandType.TableDirect
        Comando.CommandText = "Facturas"

        DR = Comando.ExecuteReader

        ComandoSocio.Connection = Conexion
        ComandoSocio.CommandType = CommandType.TableDirect
        ComandoSocio.CommandText = "Socios"
        DRSoc = ComandoSocio.ExecuteReader

        DRSoc.Read()
        If DR.HasRows Then
            FileOpen(1, "DatosExportados.txt", OpenMode.Output)
            Do While DR.Read

                If DR("IdServicio") = cmbServicios.SelectedValue Then

                    Write(1, DR("NumFac"))
                    Write(1, DR("Fecha"))
                    Write(1, DR("IdSocio"))
                    WriteLine(1, DR("Importe"))

                End If
            Loop

            FileClose(1)
        End If

        Conexion.Close()

    End Sub

    Private Sub Imprimir_Click(sender As Object, e As EventArgs) Handles Imprimir.Click
        DialogoImpresora.ShowDialog()
        DocumentoImprimir.PrinterSettings = DialogoImpresora.PrinterSettings
        DocumentoImprimir.Print()
    End Sub

    Private Sub DocumentoImprimir_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles DocumentoImprimir.PrintPage
        Dim LetraTitulo As New Font("Arial", 14)
        Dim LetraTituloColumna As New Font("Arial", 12)
        Dim TipoLetra As New Font("Arial", 10)
        Dim fila As Integer = 100
        Dim titulo As String = "Servicios: " & cmbServicios.Text



        e.Graphics.DrawString(titulo, LetraTitulo, Brushes.Blue, 80, 40)
        e.Graphics.DrawString("Factura", LetraTituloColumna, Brushes.Black, 80, 80)
        e.Graphics.DrawString("Fecha", LetraTituloColumna, Brushes.Black, 380, 80)
        e.Graphics.DrawString("Socio", LetraTituloColumna, Brushes.Black, 480, 80)
        e.Graphics.DrawString("Importe", LetraTituloColumna, Brushes.Black, 580, 80)


        Conexion.ConnectionString = CadenaDeConexion
        Conexion.Open()

        Comando.Connection = Conexion
        Comando.CommandType = CommandType.TableDirect
        Comando.CommandText = "Facturas"

        DR = Comando.ExecuteReader
        dgvDatos.Rows.Clear()

        ComandoSocio.Connection = Conexion
        ComandoSocio.CommandType = CommandType.TableDirect
        ComandoSocio.CommandText = "Socios"
        DRSoc = ComandoSocio.ExecuteReader


        DRSoc.Read()
        If DR.HasRows Then
            Do While DR.Read

                If DR("IdServicio") = cmbServicios.SelectedValue Then

                    e.Graphics.DrawString(DR("NumFac"), TipoLetra, Brushes.Black, 80, fila)
                    e.Graphics.DrawString(DR("Fecha"), TipoLetra, Brushes.Black, 380, fila)
                    e.Graphics.DrawString(DR("IdSocio"), TipoLetra, Brushes.Black, 480, fila)
                    e.Graphics.DrawString(DR("Importe"), TipoLetra, Brushes.Black, 580, fila)

                    fila = fila + 15

                End If

            Loop

            e.Graphics.DrawString("Total de Facturas: " & lblTotal.Text, TipoLetra, Brushes.Black, 80, 200)
        End If


        Conexion.Close()

    End Sub
End Class
