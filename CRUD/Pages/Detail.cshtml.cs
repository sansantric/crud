using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CRUD.Pages
{
    public class DetailModel : PageModel
    {
        public ListKendaraan listKendaraan = new ListKendaraan();
        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=CRUD;Persist Security Info=True;User ID=Admin;Password=admin";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = String.Format("SELECT Nomor_Registrasi_Kendaraan, Nama_Pemilik, Alamat, Merk, Tahun_Pembuatan, Kapasitas , Warna_Kendaraan, Bahan_Bakar FROM tb_kendaraan WHERE Nomor_Registrasi_Kendaraan='{0}'",id);
                    
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        System.Diagnostics.Debug.WriteLine(sql);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                listKendaraan.nomorRegistrasi = reader.GetString(0);
                                listKendaraan.namaPemilik = reader.GetString(1);
                                listKendaraan.alamat = reader.GetString(2);
                                listKendaraan.merkKendaraan = reader.GetString(3);
                                listKendaraan.tahun = reader.GetInt32(4);
                                listKendaraan.kapasitas = reader.GetInt32(5).ToString() + " CC";
                                listKendaraan.warna = reader.GetString(6);
                                listKendaraan.bahanBakar = reader.GetString(7);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message.ToString());
            }

        }
    }
}
