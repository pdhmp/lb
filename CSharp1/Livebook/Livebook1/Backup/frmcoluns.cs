using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using SGN.CargaDados;

namespace SGN
{
    public partial class frmcoluns : System.Windows.Forms.Form
    {
        CarregaDados CargaDados = new CarregaDados();
        public int Id_usuario;
        public int Id_Grid;
        public frmcoluns()
        {
            InitializeComponent();
        }

        private void frmcoluns_Load(object sender, EventArgs e)
        {
            CargaDados.carregacombo(this.cmdgrid, "Select Id_Grid, Nome from Tb110_Nome_Grids ", "Id_Grid", "Nome", 99);
        }

        private void lstadd_Click(object sender, EventArgs e)
        {
            if (lst1.SelectedItem != null)
            {
                lst2.Items.Add(lst1.SelectedItem);
                lst1.Items.Remove(lst1.SelectedItem);
            }
        }

        private void lstRemove_Click(object sender, EventArgs e)
        {
            if (lst2.SelectedItem.ToString() != "[Position]" && lst2.SelectedItem.ToString() != " [Quantity]")
            {
                if (lst2.SelectedItem != null)
                {
                    lst1.Items.Add(lst2.SelectedItem);
                    lst2.Items.Remove(lst2.SelectedItem);
                }
            }
            else
            {
                MessageBox.Show("This Column cannot be removed!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (this.lst2.SelectedIndex == -1 || this.lst2.SelectedIndex == 0)
                return;

            Object select, previous, temp;
            select = lst2.Items[lst2.SelectedIndex];
            previous = lst2.Items[lst2.SelectedIndex - 1];
            temp = select;
            select = previous;
            previous = temp;
            lst2.Items[lst2.SelectedIndex] = select;
            lst2.Items[lst2.SelectedIndex - 1] = previous;
            lst2.SelectedIndex--;
            
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (this.lst2.SelectedIndex == -1 || this.lst2.SelectedIndex == lst2.Items.Count - 1)
                return;

            Object select, next, temp;
            select = lst2.Items[lst2.SelectedIndex];
            next = lst2.Items[lst2.SelectedIndex + 1];
            temp = select;
            select = next;
            next = temp;
            lst2.Items[lst2.SelectedIndex] = select;
            lst2.Items[lst2.SelectedIndex + 1] = next;
            lst2.SelectedIndex++;
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            lst1.Items.Clear();
            lst2.Items.Clear();
            Id_Grid = Convert.ToInt32(cmdgrid.SelectedValue.ToString());

            //string StringSQl = "Select Nome_Coluna from where Id_Usuario =" + Id_usuario + " and Grid_Cod = " + this.cmdgrid.SelectedValue;

            string StringSQl = " Select Nome_Coluna from Tb109_Caractisticas_Colunas" +
                               " where Visible=0 and Campo_Id= 0" +
                               " and Id_User= " + Id_usuario + " and Id_Grid= " + this.cmdgrid.SelectedValue + " order by Indice";
            
            SqlDataReader dr1 = CargaDados.DB.Execute_Query_DataRead(StringSQl);

            while(dr1.Read())
            {
                lst1.Items.Add(dr1["Nome_Coluna"]); 
            }
            dr1.Close();
            dr1.Dispose();
           
            StringSQl = "Select Nome_Coluna from Tb109_Ordens_Colunas where Id_Usuario =" + Id_usuario + " and Grid_Cod = " + this.cmdgrid.SelectedValue;

             StringSQl = " Select Nome_Coluna from Tb109_Caractisticas_Colunas" +
                   " where Visible=1 and Campo_Id= 0" +
                   " and Id_User= " + Id_usuario + " and Id_Grid= " + this.cmdgrid.SelectedValue + " order by Indice";

            SqlDataReader dr2 = CargaDados.DB.Execute_Query_DataRead(StringSQl);

            while (dr2.Read())
            {
                lst2.Items.Add(dr2["Nome_Coluna"]);
            }
            dr2.Close();
            dr2.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            String StringSQl;
            int contador;
            int F = 0;
            contador = lst2.Items.Count;
            int zzz;
            while (F < contador)
            {

                StringSQl = "update Tb109_Caractisticas_Colunas set Visible = 1 where  Nome_Coluna='" +  lst2.Items[F].ToString() + "' and Id_User= " + Id_usuario + " and Id_Grid= " + Id_Grid;
                zzz = CargaDados.DB.Execute_Insert_Delete_Update(StringSQl);
                if (zzz == 0)
                {
                    MessageBox.Show("Error in Insert!");
                }
                F++;
            }

            /////////////////////////////
            F = 0;
            contador = lst1.Items.Count;

            while (F < contador)
            {

                StringSQl = "update Tb109_Caractisticas_Colunas set Visible = 0 where  Nome_Coluna='" +  lst1.Items[F].ToString() + "' and Id_User= " + Id_usuario + " and Id_Grid= " + Id_Grid;
                zzz = CargaDados.DB.Execute_Insert_Delete_Update(StringSQl);
                if (zzz == 0)
                {
                    MessageBox.Show("Error in Insert!");
                }
                F++;
            }
            MessageBox.Show("Saved Columns!");
        }

	}
}
