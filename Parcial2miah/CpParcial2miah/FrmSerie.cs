using CadParcial2miah;
using ClnParcial2miah;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CpParcial2miah
{
    public partial class FrmSerie: Form
    {
        private bool esNuevo; // variable para saber si es un nuevo registro o no
        public FrmSerie()
        {
            InitializeComponent();
        }

        // metodo auxuliar para listar o leeren dgv
        private void listar()
        {
            var lista = SerieCln.leerPa(txtParametro.Text.Trim()); // llama al metodo leerPa de la clase SerieCln
            dgvLista.DataSource = lista; // asigna la lista al datasource del dgv

            //vamos a cambiar los nombres de las colummnas
            dgvLista.Columns["Id"].Visible = false; // oculta la columna Id
            dgvLista.Columns["Titulo"].HeaderText = "Título"; // cambia el nombre de la columna Titulo
            dgvLista.Columns["Sinopsis"].HeaderText = "Sinópsis"; // cambia el nombre de la columna Sinopsis
            dgvLista.Columns["Director"].HeaderText = "Director"; // cambia el nombre de la columna Director
            dgvLista.Columns["Episodio"].HeaderText = "Episódio"; // cambia el nombre de la columna Episodio
            dgvLista.Columns["FechaEstreno"].HeaderText = "Fecha de Estreno"; // cambia el nombre de la columna FechaEstreno
            dgvLista.Columns["Estado"].Visible = false; // oculta la columna Estado
            dgvLista.Columns["usuarioRegistro"].Visible = false; // oculta la columna usuarioRegistro
            dgvLista.Columns["FechaRegistro"].Visible = false; // oculta la columna FechaRegistro

            dgvLista.Columns["Sinopsis"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvLista.Columns["Sinopsis"].Width = 400; // puedes poner el ancho fijo que desees

            dgvLista.Columns["FechaEstreno"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvLista.Columns["FechaEstreno"].Width = 120; // O más, para que entre en una fila

            dgvLista.Columns["FechaEstreno"].HeaderCell.Style.WrapMode = DataGridViewTriState.False;

            //quieo que me seleccione la primera fila
            if (dgvLista.Rows.Count > 0)
            {
                dgvLista.Rows[0].Selected = true; // Selecciona la primera fila
                dgvLista.CurrentCell = dgvLista.Rows[0].Cells[1]; // Establece el foco en la primera celda
            }

            //Esto me sirve para deshabilitar botones si en caso no hay lista de clientes
            btnActualizar.Enabled = lista.Count > 0; // Desactiva el botón de acttualizar si es 0 o menor  o  0
            btnEliminar.Enabled = lista.Count > 0; // Desactiva el botón de eliminar si es 0 o menor o 0
            //--------------------------------------


        }

        private void lblTitulo_Click(object sender, EventArgs e)
        {

        }

        private void FrmSerie_Load(object sender, EventArgs e)
        {
            Size = new Size(964, 457);
            listar();
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            esNuevo = true;
            Size = new Size(964, 734);
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            esNuevo = false; // indica que no es un nuevo registro
            int index = dgvLista.CurrentRow.Index; // obtiene el indice de la fila seleccionada
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["Id"].Value); // obtiene el id de la serie seleccionada
            var serie = SerieCln.obtenerUno(id); // llama al metodo obtenerUno de la clase SerieCln para obtener la serie por su id

            txtTitulo.Text = serie.Titulo; // asigna el titulo de la serie al textbox
            txtSinopsis.Text = serie.Sinopsis; // asigna la sinopsis de la serie al textbox
            txtDirector.Text = serie.Director; // asigna el director de la serie al textbox
            nudEpisodio.Text = serie.Episodio.ToString(); // asigna el episodio de la serie al nud
            cbxIdiomaOriginal.Text = serie.IdiomaOriginal;
            txtUrlPortada.Text = serie.UrlPortada;
            txtFechaEstreno.Text = serie.FechaEstreno.HasValue ? serie.FechaEstreno.Value.ToString("yyyy-MM-dd") : "";

            txtTitulo.Focus(); // enfoca el textbox de titulo
            Size = new Size(964, 734);

        }


        // metodo para guardar
        //antes creareos un metodo extra
        private bool validar()
        {
            bool esValido = true; //si se queda en V entonces quiere decir que no entro a ningun if para qeu se cambie a false entonces quiere decir que esta llenado correctamente
            //limpiar errores anteriores en cada control
            erpTitulo.SetError(txtTitulo, ""); // limpia el error del textbox de titulo
            erpSinopsis.SetError(txtSinopsis, ""); // limpia el error del textbox de sinopsis
            erpDirector.SetError(txtDirector, ""); // limpia el error del textbox de director
            erpEpisodio.SetError(nudEpisodio, ""); // limpia el error del nud de episodio
            erpIdioma.SetError(cbxIdiomaOriginal, "");
            erpFechaEstreno.SetError(txtFechaEstreno, ""); // limpia el error del textbox de fecha estreno

            // en los if vota v o f porque si o se llena el campo de texto entra al if y cambia el esValido a false
            if (string.IsNullOrEmpty(txtTitulo.Text.Trim()))
            {
                erpTitulo.SetError(txtTitulo, "El titulo es obligatorio"); // muestra el error en el textbox de titulo
                esValido = false; // cambia el esValido a false
            }

            if (string.IsNullOrEmpty(txtSinopsis.Text.Trim()))
            {
                erpSinopsis.SetError(txtSinopsis, "La sinopsis es obligatoria"); // muestra el error en el textbox de sinopsis
                esValido = false; // cambia el esValido a false
            }

            if (string.IsNullOrEmpty(txtDirector.Text.Trim()))
            {
                erpDirector.SetError(txtDirector, "El director es obligatorio"); // muestra el error en el textbox de director
                esValido = false; // cambia el esValido a false
            }

            if (nudEpisodio.Value <= 0)
            {
                erpEpisodio.SetError(nudEpisodio, "El episodio debe ser mayor a 0"); // muestra el error en el nud de episodio
                esValido = false; // cambia el esValido a false
            }

            if (string.IsNullOrEmpty(cbxIdiomaOriginal.Text.Trim()))
            {
                erpIdioma.SetError(cbxIdiomaOriginal, "El idioma es obligatoria"); // muestra el error en el textbox de sinopsis
                esValido = false; // cambia el esValido a false
            }

            if (string.IsNullOrEmpty(txtFechaEstreno.Text.Trim()))
            {
                erpFechaEstreno.SetError(txtFechaEstreno, "La fecha de estreno es obligatoria"); // muestra el error en el textbox de fecha estreno
                esValido = false; // cambia el esValido a false
            }
            else
            {
                DateTime fecha;
                if (!DateTime.TryParse(txtFechaEstreno.Text.Trim(), out fecha))
                {
                    erpFechaEstreno.SetError(txtFechaEstreno, "La fecha de estreno no es valida"); // muestra el error en el textbox de fecha estreno
                    esValido = false; // cambia el esValido a false
                }
            }

            return esValido; // devuelve el valor de esValido
        }
        //este metodo es para guardar el cliente
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validar()) // aqui se llama al metodo valdar para saber si esta llenomlos datos o no
            {
                var serie = new Serie(); // crea una nueva instancia de la clase Serie
                //se empieza a llenar los datos proiedades
                serie.Titulo = txtTitulo.Text.Trim(); // asigna el titulo del textbox al objeto serie
                serie.Sinopsis = txtSinopsis.Text.Trim(); // asigna la sinopsis del textbox al objeto serie
                serie.Director = txtDirector.Text.Trim(); // asigna el director del textbox al objeto serie
                serie.Episodio = Convert.ToInt32(nudEpisodio.Value); // asigna el episodio del nud al objeto serie
                serie.IdiomaOriginal = cbxIdiomaOriginal.Text.Trim();
                serie.FechaEstreno = string.IsNullOrEmpty(txtFechaEstreno.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtFechaEstreno.Text.Trim()); // asigna la fecha de estreno del textbox al objeto serie, si esta vacio asigna null
                serie.Estado = 1; // asigna el estado de la serie a 1 (activo)
                serie.usuarioRegistro = "Cristian"; // asigna el usuario de creacion a admin, esto se puede cambiar por el usuario que este logueado

                //aqui vamos a validar si es nuevo o no
                if (esNuevo) // si es nuevo
                {
                    SerieCln.insertar(serie); // llama al metodo insertar de la clase SerieCln para crear la serie
                    MessageBox.Show("Serie creada correctamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information); // muestra un mensaje de exito
                }
                else // si no es nuevo
                {
                    int index = dgvLista.CurrentRow.Index; // obtiene el indice de la fila seleccionada
                    int id = Convert.ToInt32(dgvLista.Rows[index].Cells["Id"].Value); // obtiene el id de la serie seleccionada

                    // obrener la serie iriguna desde la bd
                    var serieExistente = SerieCln.obtenerUno(id); // llama al metodo obtenerUno de la clase SerieCln para obtener la serie por su id

                    if (serieExistente != null)
                    {
                        // Actualizar los campos de la serie existente con los nuevos valores
                        serieExistente.Titulo = serie.Titulo;
                        serieExistente.Sinopsis = serie.Sinopsis;
                        serieExistente.Director = serie.Director;
                        serieExistente.Episodio = serie.Episodio;
                        serieExistente.IdiomaOriginal = serie.IdiomaOriginal;
                        serieExistente.UrlPortada = serie.UrlPortada;
                        serieExistente.FechaEstreno = serie.FechaEstreno;
                        serieExistente.Estado = 1; // Mantener el estado activo
                        serieExistente.usuarioRegistro = "Mery"; // asigna el usuario de modificacion a admin, esto se puede cambiar por el usuario que este logueado

                        SerieCln.actualizar(serieExistente); // llama al metodo actualizar de la clase SerieCln para actualizar la serie
                    }


                }

                listar(); // llama al metodo listar para actualizar la lista de series

                MessageBox.Show("Serie actualizada correctamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information); // muestra un mensaje de exito
            }
        }

        // este evento es para eliminar una serie
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int index = dgvLista.CurrentRow.Index; // obtiene el indice de la fila seleccionada
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["Id"].Value); // obtiene el id de la serie seleccionada
            string Titulo = dgvLista.Rows[index].Cells["Titulo"].Value.ToString(); // obtiene el titulo de la serie seleccionada

            DialogResult resultado = MessageBox.Show(
                $"¿Esta seguro de eliminar la serie {Titulo}?",
                "Confirmar Eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question); // muestra un mensaje de confirmacion

            if (resultado == DialogResult.Yes) // si el usuario confirma la eliminacion
            {
                string usuarioRegistro = "Cristian"; // asigna el usuario de eliminacion a admin, esto se puede cambiar por el usuario que este logueado
                SerieCln.eliminar(id, usuarioRegistro); // llama al metodo eliminar de la clase SerieCln para eliminar la serie
                listar(); // llama al metodo listar para actualizar la lista de series
                MessageBox.Show("Serie eliminada correctamente", "Exito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information); // muestra un mensaje de exito
            }
        }

        private void limpiar()
        {
            txtTitulo.Clear(); // limpia el textbox de titulo
            txtSinopsis.Clear(); // limpia el textbox de sinopsis
            txtDirector.Clear(); // limpia el textbox de director
            nudEpisodio.Value = 1; // pone el nud de episodio en 1
            txtFechaEstreno.Clear(); // limpia el textbox de fecha estreno
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            listar();
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            limpiar(); // limpia los campos del formulario
            Size = new Size(964, 457);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
